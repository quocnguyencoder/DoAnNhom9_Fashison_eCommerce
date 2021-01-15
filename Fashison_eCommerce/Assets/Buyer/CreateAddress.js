$(function () {
	$(document).ready(function () {
		$("#createForm").submit(function (event) {

			submitcreateForm();
			$("#create-modal").modal('hide');
			return false;
		});

		//Thay doi dia chi nhan hang
		$('#ChangeAddress').click(function () {
			$.ajax({
				url: '/Buyer/CartCheckout/GetAddress',
				success: function (result) {
					$(".loadAddress").html(result);
				}
			});
		});

		//click hoan thanh thay doi dia chi
		$('#CompleteAddress').click(function () {
			$.ajax({
				url: '/Buyer/CartCheckout/CompleteChangeAddress',
				success: function (result) {
					$(".loadAddress").html(result);
				}
			});
			//$.ajax({
			//	url: '/Buyer/CartCheckout/Index',
			//	success: function (view) {
			//		$(".loadAll").html(view);
			//	}
			//});
		});

		//click radio button change address 
		$('.change-address-btn').click(function () {
			console.log("click");
		
			var id_add = $(this).data("id_address");
			console.log(id_add);
			$.ajax({
				url: '/Buyer/CartCheckout/RadioButton',
				data: { id_address: id_add },
				success: function (result) {
					$(".loadAddress").html(result);
				}
			});
		});

		$('#Exit').click(function () {
			$.ajax({
				// load cac san pham
				url: "/Buyer/CartCheckout/GetUserAddress",
				success: function (result) {
					$(".loadAddress").html(result);
				}
			});
		});

	});
	function submitcreateForm() {
		
		var add = {};
		add.full_name = $('#name').val();
		add.phone = $("#phone").val();
		add.address1 = $("#Address1").val();

		$.ajax({
			type: "POST",
			url: "/Buyer/CartCheckout/Create",
			data: '{add: ' + JSON.stringify(add) + '}',
			contentType: "application/json; charset=utf-8",
			success: function (response) {

				$(".loadAddress").html(response);

			},
			error: function () {
				alert("Error");
			}
		});
	}

});