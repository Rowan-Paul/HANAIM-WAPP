﻿window.mapbox = {
    init: () => {
        mapboxgl.accessToken = 'pk.eyJ1Ijoicm93YW5wYXVsIiwiYSI6ImNsMzF0M2ZxNTB6dDEzanBzZHF0MmdoZXAifQ.b_qn6AFUlzR5cmPYJmq5Ng';
        map = new mapboxgl.Map({
            container: 'map',
            style: 'mapbox://styles/mapbox/streets-v11',
            center: [4.9041, 52.3676],
            zoom: 10,
        })
        map.on('load', () => {
            map.addSource('neighbourhoods', {
                type: 'geojson',
                data: "https://localhost:7000/api/neighbourhoods?geojson=true",
            });
            map.addLayer({
                id: 'neigbourhoods-layer',
                type: 'fill',
                source: 'neighbourhoods',
                paint: {
                    'fill-color': 'rgba(229, 182, 247, 0.4)',
                    'fill-outline-color': 'rgba(200, 100, 240, 1)'
                }
            });
            map.addSource('listings', {
                type: 'geojson',
                data: "https://localhost:7000/api/listings?geojson=true",
                cluster: true,
                clusterRadius: 6
            });
            map.addLayer({
                id: 'listings-layer',
                type: 'circle',
                source: 'listings',
                paint: {
                    'circle-color': '#14248A',
                    'circle-radius': 3,
                }
            });
            map.addControl(new mapboxgl.NavigationControl());
            map.on('click', 'listings-layer', async (e) => {
                await dotNetHelper.invokeMethodAsync('FetchInfo', e.features[0].properties.Id)
            });

            map.on('mouseenter', 'listings-layer', () => {
                map.getCanvas().style.cursor = 'pointer';
            });

            map.on('mouseleave', 'listings-layer', () => {
                map.getCanvas().style.cursor = '';
            });
        })
    },
    updateMap: (url) => {
        map.getSource('listings').setData(url);
    }
}