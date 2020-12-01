//var user = {
//    init: function () {
//        console.log("init");
//        user.registerEvents();
//    },
//    registerEvents: function () {
//        $('.AddToCart').off('click').on('click', function (e) {
//            console.log("click");
//            e.preventDefault();
//            var id = $(this).data('ProductID')
//            $.ajax({
//                url: "/Buyer/Cart_Item/AddToCart",
//                type: 'POST',
//                async: true,
//                success: function (msg) {
//                    console.log(msg);
//                }
//                //success: function (result) {
//                //    $(".cart_info").html(result);
//                //}
//            });
//        });
//    }
//}
//user.init();