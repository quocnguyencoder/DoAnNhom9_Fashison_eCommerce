

$(function () {
    var ctx = document.getElementById("lineChart");
    var lineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ViewBag.Date,
            datasets: [{
                label: "My First dataset",
                backgroundColor: "rgba(38, 185, 154, 0.31)",
                borderColor: "rgba(38, 185, 154, 0.7)",
                pointBorderColor: "rgba(38, 185, 154, 0.7)",
                pointBackgroundColor: "rgba(38, 185, 154, 0.7)",
                pointHoverBackgroundColor: "#fff",
                pointHoverBorderColor: "rgba(220,220,220,1)",
                pointBorderWidth: 1,
                data: ViewBag.DateTotals
            }]
        }
    });

});