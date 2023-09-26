function initChartBaseConfig() {
    google.charts.load('current', { 'packages': ['corechart'], 'language': 'tr' });
}
document.addEventListener('DOMContentLoaded', function () {
    initChartBaseConfig();
    updateChartsPeriodic();
});

function getChartsFromApi() {

    console.log("getChartsFromApi called.");
    var startDate = document.getElementById("startDate").value;
    var endDate = document.getElementById("endDate").value;
    $.ajax({
        type: "GET",
        data: { startDate: startDate, endDate: endDate },
        url: "/Home/GetCryptoChartDatasByDateRange",
        dataType: "json",
        success: function (jsonData) {
            if (jsonData.success == true) {
                setDailyChart(jsonData.data.dailyHistories)
                setWeeklyChart(jsonData.data.weeklyHistories)
                setMonthlyChart(jsonData.data.monthlyHistories)
            }
        },
        error: function (jsonData) {
            console.log(jsonData);
        }
    });
}

function updateChartsPeriodic() {

    getChartsFromApi();
    setTimeout(updateChartsPeriodic, 3000);
    setTimeout(function () { }, 1500);
}



function setDailyChart(arrayData) {

    const dailyChartOption = {
        hAxis: { format: 'short' },
        title: 'Günlük BTC Fiyatları',
        curveType: 'function',
        legend: 'none'
    };

    var dailyChartData = new google.visualization.DataTable();
    dailyChartData.addColumn('number', 'Gün');
    dailyChartData.addColumn('number', 'Fiyat');
    dailyChartData.addRows(arrayData);

    var chart = new google.visualization.LineChart(document.getElementById('daily_chart'));

    chart.draw(dailyChartData, dailyChartOption);
}
function setWeeklyChart(arrayData) {

    const weeklyChartOption = {
        hAxis: { format: 'short' },
        title: 'Haftalık BTC Fiyatları',
        curveType: 'function',
        legend: 'none'
    };


    var weeklyChartData = new google.visualization.DataTable();
    weeklyChartData.addColumn('number', 'Hafta');
    weeklyChartData.addColumn('number', 'Fiyat');
    weeklyChartData.addRows(arrayData);

    var chart = new google.visualization.LineChart(document.getElementById('weekly_chart'));

    chart.draw(weeklyChartData, weeklyChartOption);
}
function setMonthlyChart(arrayData) {

    const monthlyChartOption = {
        hAxis: { format: 'short' },
        title: 'Haftalık BTC Fiyatları',
        curveType: 'function',
        legend: 'none'
    };


    var monthlyChartData = new google.visualization.DataTable();
    monthlyChartData.addColumn('number', 'Ay');
    monthlyChartData.addColumn('number', 'Fiyat');
    monthlyChartData.addRows(arrayData);

    var chart = new google.visualization.LineChart(document.getElementById('monthly_chart'));

    chart.draw(monthlyChartData, monthlyChartOption);
}