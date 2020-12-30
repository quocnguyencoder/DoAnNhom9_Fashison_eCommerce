using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.helper;
using PayPal.Api;
using System.Configuration;
using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Fashison_eCommerce.Areas.Buyer.Controllers
{
    public class PaypalController : Controller
    {
        private PayPal.Api.Payment payment;
        
        [HttpPost]
        public ActionResult Index( string shop_List, string delivery_List)
        {
            // kiem tra con hang
            if(CheckAvailableItems() == null)
            {
                Session["shop_List"] = shop_List;
                Session["delivery_List"] = delivery_List;
                Session["payment"] = "paypal";
                return Content("OK");
            }
            else
            {
                return Content("Mot san pham da het hang!");
            }
            
        }

        public ActionResult PaymentWithPaypal()
        {
            APIContext apiContext = helper.Configuration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class
                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPayPal?";
                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This section is executed when we have received all the payments parameters
                    // from the previous call to the function Create
                    // Executing a payment
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                helper.Logger.Log("Error" + ex.Message);
                return View("FailureView");
            }
            ViewBag.shop_list = Session["shop_List"].ToString();
            ViewBag.delivery_list = Session["delivery_List"].ToString();
            return View("SuccessView");
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            string shop_List = Session["shop_List"].ToString();
            string delivery_List = Session["delivery_List"].ToString();
            double shipping = 0;
            var shop = shop_List.TrimEnd().Split(' ');
            var delivery = delivery_List.TrimEnd().Split(' ');
            var shopAndDelivery = shop.Zip(delivery, (s, d) => new { Shop = s, Delivery = d });
            foreach(var i in shopAndDelivery)
            {
                shipping += 12000;
            }

            var itemList = new ItemList() { items = new List<Item>() };
            //Các giá trị bao gồm danh sách sản phẩm, thông tin đơn hàng
            //Sẽ được thay đổi bằng hành vi thao tác mua hàng trên website
            UserCartClient UC = new UserCartClient();
            var cart_Items = UC.LoadCart(Convert.ToInt32(Session["userID"]));
            double checkout_total = 0;

            foreach (var item in cart_Items)
            {
                var price = Convert.ToDouble(CurrencyConversion(item.Price));
                checkout_total += price* item.Quantity;
                itemList.items.Add(new Item()
                {
                    //Thông tin đơn hàng
                    name = item.Name,
                    currency = "USD",
                    price = price.ToString(),
                    quantity = item.Quantity.ToString(),
                    sku = "sku"
                });
            }
            shipping = Convert.ToDouble(CurrencyConversion(shipping));
            //Hình thức thanh toán qua paypal
            var payer = new Payer() { payment_method = "paypal" };
            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };
            //các thông tin trong đơn hàng
            var details = new Details()
            {
                tax = "0",
                shipping = shipping.ToString(),
                subtotal = checkout_total.ToString()
            };
            //Đơn vị tiền tệ và tổng đơn hàng cần thanh toán
            var amount = new Amount()
            {
                currency = "USD",
                total = (shipping+checkout_total).ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };
            var transactionList = new List<Transaction>();
            //Tất cả thông tin thanh toán cần đưa vào transaction
            transactionList.Add(new Transaction()
            {
                description = "Thanh toan Paypal",
                invoice_number = "#a5D30xCdv6",
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }
        public string CurrencyConversion(double amount)
        {
            double result = 0;
            
            string Strjson = new WebClient().DownloadString("http://data.fixer.io/api/latest?access_key=444283dc9a548f3c957fcba6bfc310b7&symbols=USD,VND&format=1");
            dynamic stuff = JObject.Parse(Strjson);
            // vnd --> eur
            string temp = stuff.rates.VND;
            result += amount / Convert.ToDouble(temp);
            temp = stuff.rates.USD;
            // eur --> usd
            result = result * Convert.ToDouble(temp);
            result = Math.Round(result, 2);
            return result.ToString();
        }
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        public List<int> CheckAvailableItems()
        {
            List<int> outOfOrder = default;
            UserCartClient UC = new UserCartClient();
            var cart_Items = UC.LoadCart(Convert.ToInt32(Session["userID"]));
            ProductClient PC = new ProductClient();
            foreach (var item in cart_Items)
            {
                var check = PC.find(item.ItemID);
                foreach (var i in check)
                {
                    if (i.Amount < item.Quantity)
                    {
                        outOfOrder.Add(item.ItemID);
                    }
                }

            }
            return outOfOrder;
        }
    }
}