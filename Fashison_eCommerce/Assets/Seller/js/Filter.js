$(function () {
    $(document).ready(function () {
        $("#search").click(function (e) {
            e.preventDefault();
            debugger

            var name = $("#Name").val();
            var type = parseInt($("#TypeID").val()) || 0;
            var min = $("#MinAmount").val();
            if (min == "") {
                var min = 0;
            }
            var max = $("#MaxAmount").val();
            if (max == "") {
                var max = 0;
            }
            $("#TypeID").hide();
            $("#MaintypeID").val("Select one");
            $.ajax(
                {
                    type: 'GET',
                    url: "/Products/Search",
                    data: { Name: name, TypeID: type, MinAmount: min, MaxAmount: max },
                    success: function (response) {
                        console.log(response);
                        $("#product_area").html(response);
                    }
                });
        });
        $("#MaintypeID").change(function () {
            $("#TypeID").show();
            $.get("/Products/GetTypeList", { MaintypeID: $("#MaintypeID").val() }, function (data) {
                $("#TypeID").empty();
                $.each(data, function (index, row) {
                    $("#TypeID").append("<option value='" + row.TypeID + "'>" + row.Name + "</option>")
                });
            });

        });

    });
});