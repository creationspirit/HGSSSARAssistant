var singleLocationMap = {
  $latitudeInput: $('.js-map-lat'),
  $longitudeInput: $('.js-map-lng'),
  $addressInput: $('.js-map-address'),
  $mapMountEl: $('.js-map'),
  geocoder: null,
  map: null,
  marker: null,

  init: function() {
    var self = this;
    if (!self.$mapMountEl.length) {
        return;
    }

    self.geocoder = new google.maps.Geocoder;
    self.map = new google.maps.Map(self.$mapMountEl[0], {
      center: {
        lat: 45.815,
        lng: 15.982
      },
      zoom: 12,
      streetViewControl: false
    });

    var initialMarkerLat = self.$mapMountEl.data('lat');
    var initialMarkerLng = self.$mapMountEl.data('lng');

    if (initialMarkerLat && initialMarkerLng) {
        self.marker = new google.maps.Marker({
          position: {
            lat: parseFloat(initialMarkerLat, 10),
            lng: parseFloat(initialMarkerLng, 10)
          },
          map: self.map,
          draggable: true
        });
        self.map.setCenter(self.marker.getPosition());
    }

    self.map.addListener('click', function(e) {
        if (self.marker) {
            self.marker.setMap(null);
        }
        self.marker = new google.maps.Marker({
          position: e.latLng,
          map: self.map,
          draggable: true
        });

        self.marker.addListener('dragend', function(event) {
            self.populateAddressName(event.latLng);
            self.populateLocationInputs({
                lat: event.latLng.lat(),
                lng: event.latLng.lng()
            });
        });
        self.populateAddressName(e.latLng);
        self.populateLocationInputs({
            lat: e.latLng.lat(),
            lng: e.latLng.lng()
        });
        self.marker.setMap(self.map);
    });
  },


  populateLocationInputs: function(latLng) {
    this.$latitudeInput.val(latLng.lat);
    this.$longitudeInput.val(latLng.lng);
  },

  populateAddressName: function(latlng) {
    var self = this;
    this.geocoder.geocode({
      location: latlng
    }, function(results, status) {
      if (status === 'OK') {
        if (results[0]) {
          self.$addressInput.val(results[0].formatted_address);
        }
      }
    });
  },

  placeMarker: function(latLng) {
    var self = this;
    if (!latLng) {
      return
    }
    if (self.marker) {
      self.marker.setMap(null);
    }

    self.marker = new google.maps.Marker({
      position: latLng,
      map: self.map,
      draggable: true
    });

    self.marker.addListener('dragend', function(event) {
        self.populateAddressName(event.latLng);
        self.populateLocationInputs({
            lat: event.latLng.lat(),
            lng: event.latLng.lng()
        });
    });
    self.populateAddressName(latLng);
    self.populateLocationInputs({
        lat: latLng.lat,
        lng: latLng.lng
    });
    self.marker.setMap(self.map);
    self.map.setCenter(self.marker.getPosition());
  }

};


var initAddressMap = singleLocationMap.init.bind(singleLocationMap); // mimic module.exports
