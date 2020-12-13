$(document).ready(function () {
    $("#list-header").on({
        mouseenter: function () {
            $(this).css("background-color", "lightgray");
        },
        mouseleave: function () {
            $(this).css("background-color", "lightblue");
        },
    });
    $("#orders").DataTable({
        'processing': true,
        'autoWidth': true
    });
    $("#btnReloadData").on("click", function () {
        alert("reload data...");
        table.ajax.reload();
    });

});
