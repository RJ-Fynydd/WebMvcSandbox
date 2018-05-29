$(document).ready(function () {
    $('#myTable').DataTable();


    $.ajax({
        url: "/api/Test"
    }).then(function (data) {
        console.info(data);

        var xAxis = [];
        var yAxis = [];

        for (var i = 0; i < data.length; i++) {
            xAxis.push(toFormattedDate(data[i]["EntryDateTime"]));
            yAxis.push(data[i]["TempF"]);
        }
        loadChart(xAxis, yAxis);
    });
});


function toFormattedDate(date) {
    var date_ = new Date(date);

    return date_.getMonth() + "-" + date_.getDate() + "-" + date_.getFullYear()
        + " " + date_.getHours() + ":" + date_.getMinutes() + ":" + date_.getSeconds();
}


var myChart;
function loadChart(xAxis, yAxis) {

    if (myChart != undefined) {
        myChart.destroy();
    }

    // Load Temp Table
    var ctx = $("#myChart");
    myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: xAxis,
            datasets: [{
                label: 'Temperature F',
                data: yAxis,
                borderWidth: 1
            }]
        },
        options: {
            title: {
                display: true,
                text: 'Temperature Over Time',
                fontSize: 15,
                fontColor: '#00317A',
                fontStyle: 'bold'
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            },
            animation: false,
            scaleoverride: true,
            responsive: false,
            maintainAspectRatio: false
        }
    });
}