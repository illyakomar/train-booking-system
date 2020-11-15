const chart = {
    labels: [],
    datasets: [{
        label: 'на маршруті',
        data: [],
        backgroundColor: [
            'rgba(255, 99, 132, 0.2)',
            'rgba(54, 162, 235, 0.2)',
            'rgba(255, 206, 86, 0.2)',
            'rgba(75, 192, 192, 0.2)',
            'rgba(153, 102, 255, 0.2)',
            'rgba(255, 159, 64, 0.2)'
        ],
        borderColor: [
            'rgba(255,99,132,1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)',
            'rgba(255, 159, 64, 1)'
        ],
        borderWidth: 1,
        fill: false
    }]
};



const options = {
    scales: {
        yAxes: [{
            ticks: {
                beginAtZero: true
            }
        }]
    },
    legend: {
        display: false
    },
    elements: {
        point: {
            radius: 0
        }
    }

};

if ($("#barChart").length) {
    const barChartCanvas = $("#barChart").get(0).getContext("2d");
  
    $.getJSON('/Statistics/GetWagonOnRouteBarData', (response) => {
        console.log(response)
        const _labels = [];
        const _data = [];

        response.forEach(x => {
            _labels.push(`${x.departurePoint} - ${x.destination}`);
            _data.push(x.wagons);
        });

        console.log([_data]);
        chart.labels = _labels;
        chart.datasets[0].data = _data;

        const barChart = new Chart(barChartCanvas, {
            type: 'bar',
            data: chart,
            options: options
        });
    });
}
