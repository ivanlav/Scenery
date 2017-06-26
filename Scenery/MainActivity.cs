using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Content;
using Android.Views;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.Gms.Location;
using Android.Gms.Common.Apis;
using Android.Gms.Common;

using System;

namespace Scenery
{
    [Activity(Label = "Scenery", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback, ILocationListener
    {
        private GoogleMap mMap;
        public LatLng currLoc;

        LocationManager locMgr;
        Location locat;

        string Provider = LocationManager.GpsProvider;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            locMgr = GetSystemService (Context.LocationService) as LocationManager;

            locat = locMgr.GetLastKnownLocation(Provider);

            SetUpMap();

        }

        protected override void OnResume()
        {
            base.OnResume();


            if (locMgr.IsProviderEnabled(Provider))
            {
                locMgr.RequestLocationUpdates(Provider, 2000, 1, this);
            }
            else
            {
                //Log.Info(tag, Provider + " is not available. Does the device have location services enabled?");
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            locMgr.RemoveUpdates(this);
        }

        private void SetUpMap()
        {
            if (mMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }
            MoveMap(currLoc);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            mMap = googleMap;

        }

        private void MoveMap(LatLng location)
        {
            location = new LatLng(50.897778, 3.013333);
            // LatLng location = currLoc;

            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);

            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            if (mMap != null)
            {
                mMap.MoveCamera(cameraUpdate);
            }
        }

        public void OnLocationChanged(Location location)
        {
            currLoc = new LatLng(location.Latitude, location.Longitude);
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}

