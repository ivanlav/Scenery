using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Content;
using Android.Views;
using Android.Gms.Maps;
using Android.Locations;
using System;



namespace Scenery
{
    [Activity(Label = "Scenery", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback
    {
        private GoogleMap mMap;
        public LocationManager locMgr;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            locMgr = GetSystemService(Context.LocationService) as LocationManager;

            SetUpMap();


        }

        private void SetUpMap()
        {
            if (mMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            mMap = googleMap;
        }

    }
}

