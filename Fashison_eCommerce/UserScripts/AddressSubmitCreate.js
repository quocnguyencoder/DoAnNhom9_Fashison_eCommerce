$(function () {
	$(document).ready(function () {
		
		$("#createForm").submit(function (event) {

			submitcreateForm();
			$("#create-modal").modal('hide');
			return false;
		});
		
		$("button#btndefault").click(function () {
			debugger
			var addid = $(this).data('id');
			var addname = $(this).attr('data-name');
			var addphone = $(this).attr('data-phone');
			var addaddress = $(this).attr('data-address');
			var add = {};

			add.Address_ID = addid;
			add.full_name = addname;
			add.phone = addphone;
			add.address1 = addaddress;
			add.default_address = 1;
		
				$.ajax({
					type: "POST",
					url: "/User/Edit",
					data: '{add: ' + JSON.stringify(add) + '}',
					contentType: "application/json; charset=utf-8",
					success: function (response) {
						
						$("#table-address").html(response);

					},
					error: function () {
						alert("Error");
					}
				});
			
		});
		$("button#btnDelete").click(function () {
			
			var r = confirm("Do you want to delete?");
			var addid = $(this).data('id');
			if (r == true) {
				$.ajax({
					type: "GET",
					url: "/User/Delete",
					data: { id: addid },
					success: function (response) {

						$("#table-address").html(response);

					},
					error: function () {
						alert("Error");
					}
				});
			}

		});
	});
	function submitcreateForm() {
		var add = {};
		add.full_name = $('#name').val();
		add.phone = $("#phone").val();
		add.address1 = $("#Address").val();
	
		$.ajax({
			type: "POST",
			url: "/User/Create",
			data: '{add: ' + JSON.stringify(add) + '}',
			contentType: "application/json; charset=utf-8",
			success: function (response) {
				
				$("#table-address").html(response);
				
			},
			error: function () {
				alert("Error");
			}
		});
	}
	
});