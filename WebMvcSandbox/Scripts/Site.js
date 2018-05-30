$(document).ready(function () {
    $('#myTable').DataTable();


    $.ajax({
        url: "/api/Temperature"
    }).then(function (data) {
        console.info(data);

        var xAxis = [];
        var yAxis = [];

        for (var i = data.length - 1; i >= 0; i--) {
            xAxis.push(data[i]["EntryDateTime"]);
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
                label: 'Temperature °F',
                data: yAxis,
                borderWidth: 3,
                fill: false,
                lineTension: 0,
                pointRadius: 4,
                borderColor: 'rgba(255, 232, 66, 0.4)',
                backgroundColor: 'rgba(255, 232, 66, 0.7)'
            }]
        },
        options: {
            title: {
                display: true,
                text: 'Temperature Over Time',
                fontSize: 20,
                fontColor: '#4f92ff',
                fontStyle: 'bold'
            },
            scales: {
                scales:
                    {
                        xAxes: [{
                            display: false
                        }]
                    },
                yAxes: [{
                    ticks: {
                        //beginAtZero: true
                    }
                }]
            },
            tooltips: {
                enabled: true,
                position: 'average',
                intersect: true,
                displayColors: true,
                titleFontColor: '#ffe842',
                callbacks: {

                    title: function (tooltipItem, data) {
                        var date = new Date(data.labels[tooltipItem[0].index]);
                        return toFormattedDate(date);
                    },

                    label: function (tooltipItem, data) {
                        return data.datasets[0].data[tooltipItem.index] + ' °F';
                    }
                }
            },
            animation: false,
            scaleoverride: true,
            responsive: true,
            maintainAspectRatio: false
        }
    });
}