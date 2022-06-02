window.setMap = {
    neighbourhood: (stats) => {
        const ctx = document.getElementById('neighbourhoodsChart').getContext('2d');
        const myChart = new Chart(ctx, {
            type: 'bar', data: {
                labels: stats.neighbourhoods, datasets: [{
                    label: 'Listing',
                    data: stats.prices,
                    backgroundColor: ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)'],
                    borderColor: ['rgba(255, 99, 132, 1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'],
                    borderWidth: 1
                }]
            }, options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    },
    property: (stats) => {
        const ctx = document.getElementById('propertyChart').getContext('2d');
        const myChart = new Chart(ctx, {
            type: 'pie', data: {
                labels: stats.propertyTypes, datasets: [{
                    label: 'Property type',
                    data: stats.counts,
                    backgroundColor: ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)'],
                    borderColor: ['rgba(255, 99, 132, 1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'],
                    borderWidth: 1
                }]
            }, options: {}
        });
    },
    room: (stats) => {
        const ctx = document.getElementById('roomChart').getContext('2d');
        const myChart = new Chart(ctx, {
            type: 'pie', data: {
                labels: stats.roomTypes, datasets: [{
                    label: 'Room type',
                    data: stats.counts,
                    backgroundColor: ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)'],
                    borderColor: ['rgba(255, 99, 132, 1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'],
                    borderWidth: 1
                }]
            }, options: {}
        });
    },
    reviews: (stats) => {
        const ctx = document.getElementById('reviewsChart').getContext('2d');
        const myChart = new Chart(ctx, {
            type: 'line', data: {
                labels: stats.dates, datasets: [{
                    label: 'Reviews',
                    data: stats.counts,
                    backgroundColor: ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)'],
                    borderColor: ['rgba(255, 99, 132, 1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'],
                    borderWidth: 1
                }]
            }, options: {}
        });
    }
}