// map-picker.component.ts
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import maplibregl from 'maplibre-gl';

@Component({
  selector: 'app-map-picker',
  templateUrl: './map-picker.component.html',
  styleUrls: ['./map-picker.component.css']
})
export class MapPickerComponent /*implements OnInit*/ {
  //map!: maplibregl.Map;
  //marker: maplibregl.Marker | null = null;
  //selectedLat: number = 0;
  //selectedLng: number = 0;

  //constructor(private router: Router) { }

  //ngOnInit(): void {
  //  this.loadMap();
  //}

  //loadMap(): void {
  //  this.map = new maplibregl.Map({
  //    container: 'map',
  //    style: 'https://api.maptiler.com/maps/streets/style.json?key=GHlWauOyhIaNsYvIpwrl',
  //    center: [78.486671, 17.385044],
  //    zoom: 13
  //  });

  //  const geolocate = new maplibregl.GeolocateControl({
  //    positionOptions: {
  //      enableHighAccuracy: true
  //    },
  //    trackUserLocation: true
  //  });
  //  this.map.addControl(geolocate, 'top-right');

  //  this.map.on('load', () => {
  //    geolocate.trigger();

  //    // Handle geolocate update
  //    geolocate.on('geolocate', (position) => {
  //      const userLat = position.coords.latitude;
  //      const userLng = position.coords.longitude;

  //      if (!this.marker) {
  //        this.marker = new maplibregl.Marker()
  //          .setLngLat([userLng, userLat])
  //          .addTo(this.map);
  //      } else {
  //        this.marker.setLngLat([userLng, userLat]);
  //      }

  //      this.map.flyTo({ center: [userLng, userLat], zoom: 15 });
  //      this.selectedLat = userLat;
  //      this.selectedLng = userLng;
  //    });
  //  });

  //  this.map.on('click', (e) => {
  //    this.selectedLat = e.lngLat.lat;
  //    this.selectedLng = e.lngLat.lng;

  //    if (this.marker) {
  //      this.marker.setLngLat(e.lngLat);
  //    } else {
  //      this.marker = new maplibregl.Marker({ draggable: false })
  //        .setLngLat(e.lngLat)
  //        .addTo(this.map);
  //    }
  //  });
  //}
  //locateMe(): void {
  //  if (navigator.geolocation) {
  //    navigator.geolocation.getCurrentPosition(
  //      (position) => {
  //        const lat = position.coords.latitude;
  //        const lng = position.coords.longitude;
  //        this.map.setCenter([lng, lat]);
  //        this.map.setZoom(15);

  //        if (this.marker) {
  //          this.marker.remove();
  //        }

  //        this.marker = new maplibregl.Marker({ color: '#e63946' })
  //          .setLngLat([lng, lat])
  //          .addTo(this.map);

  //        this.selectedLat = lat;
  //        this.selectedLng = lng;
  //      },
  //      () => alert("Failed to get your location.")
  //    );
  //  } else {
  //    alert("Geolocation is not supported by this browser.");
  //  }
  //}


  //confirmLocation(): void {
  //  if (this.selectedLat && this.selectedLng) {
  //    localStorage.setItem('selectedLat', this.selectedLat.toString());
  //    localStorage.setItem('selectedLng', this.selectedLng.toString());
  //    this.router.navigate(['/add-shops']);
  //  } else {
  //    alert('Please click on the map to select a location.');
  //  }
  //}
}
