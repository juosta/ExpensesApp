// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Show/hide tabs in home transactions list
const allTabBtns = document.querySelectorAll('[id^="tab-btn"]');
for (const tabBtn of allTabBtns) {
    tabBtn.addEventListener('click', () => {
        const allTabs = document.querySelectorAll('.tab');
        for (const item of allTabBtns) {
            item.classList.remove('active');
        }
        tabBtn.classList.add('active');
        for (const item of allTabs) {
            item.classList.remove('show');
        }
        const whichToShow = tabBtn.id.substr(-1);
        const showTab = document.querySelector(`#tab-${whichToShow}`);
        showTab.classList.add('show');
    });
}
const xButton = document.getElementById('x-button');
xButton.addEventListener('click', () => {
    document.getElementById('categoryDelPart').classList.remove('show');
});
const changeCategoryBtn = document.getElementById('change-category-btn');
changeCategoryBtn.addEventListener('click', () => {
    document.getElementById('select-category-area').style.display = "block";
});

// category on Delete 
function onCategoryDelete(id) {
    $.ajax({
        url: '/api/Category/GetTransactionsCount?id=' + id,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {            
            if (response != 0) {
                document.querySelector('#categoryDelPart').classList.add("show");
                document.getElementById('categoryIdToDelete').value = id;
            }
            else {
                $.ajax({
                    url: 'api/Category/delete?id=' + id,
                    type: 'DELETE',
                    dataType: 'JSON',
                    success: function (response) {
                        window.location.href = 'Category';
                    }
                });
            }
        }
    });
}

function deleteCategory(deleteOnly) {
    debugger;
    const id = document.getElementById('categoryIdToDelete').value;
    $.ajax({
        url: 'api/Category/delete?id=' + id + (deleteOnly ? '' : '&categoryIdToChange=' + document.getElementById('categoryIdToChange').value),
        type: 'DELETE',
        dataType: 'JSON',
        success: function (response) {
            window.location.href = 'Category';
        }
    });
}

function onTransactionDelete(id) {
    $.ajax({
        url: '/api/Category/DeleteTransactionsByCategory?id=' + id,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {

            if (response != 0) {
                document.querySelector('#categoryDelPart').classList.add("show");
            }
            else {
                // window.location.href = 'Category/Delete?id=' + id;
                $.ajax({
                    url: 'api/Category/delete?id=' + id,
                    type: 'DELETE',
                    dataType: 'JSON',
                    success: function (response) {
                        window.location.href = 'Category';
                    }
                });
            }
        },
        error: function (xhr) {
            //$.notify("Error", "error");
        }
    });
}
function onTransactionChange(id, idToChange) {
    $.ajax({
        url: '/api/Category/DeleteTransactionsByCategory?id=' + id + '&idToChange=' + idToChange,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {

            if (response != 0) {
                document.querySelector('#categoryDelPart').classList.add("show");
            }
            else {
                // window.location.href = 'Category/Delete?id=' + id;
                $.ajax({
                    url: 'api/Category/delete?id=' + id,
                    type: 'DELETE',
                    dataType: 'JSON',
                    success: function (response) {
                        window.location.href = 'Category';
                    }
                });
            }
        },
        error: function (xhr) {
            //$.notify("Error", "error");
        }
    });
}


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
                    text: `Expenses per category, ${filterDateFrom} - ${filterDateTo}`
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
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
                    name: 'Brands',
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