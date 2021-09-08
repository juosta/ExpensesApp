function loadChartData(filterDateFrom, filterDateTo) {
    $.ajax({
        url: '/api/Category/GetCategoriesData/?filterDateFrom=' + filterDateFrom + '&filterDateTo=' + filterDateTo,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            let chartData = [];
            for (let [key, value] of Object.entries(result)) {
                chartData[key] = {
                    name: value.name,
                    y: value.sum
                };
            }
            console.log(chartData);
            Highcharts.chart('pieChart', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: `Expenses per category, from ${filterDateFrom} to ${filterDateTo}`
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.y} Eur</b>'
                },
                accessibility: {
                    point: {
                        valueSuffix: '%'
                    }
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                        }
                    }
                },
                series: [{
                    name: 'Amount',
                    colorByPoint: true,
                    data: chartData
                }]
            });
        },
        error: function (xhr) {
            //$.notify("Error", "error");
        }
    });
}