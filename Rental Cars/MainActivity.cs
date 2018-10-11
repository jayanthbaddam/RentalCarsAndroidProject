using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Content;

namespace Rental_Cars
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        ListView myListView;
        List<HomeListViewModel> list = new List<HomeListViewModel>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            myListView = FindViewById<ListView>(Resource.Id.WelcomeListId);

            var list1 = new HomeListViewModel(Resource.Drawable.Calendar, "Reserve a Car");
            list.Add(list1);
            var list2 = new HomeListViewModel(Resource.Drawable.Edit, "Manage Reservation");
            list.Add(list2);
            var list3 = new HomeListViewModel(Resource.Drawable.Profile, "My Profile");
            list.Add(list3);
            var list4 = new HomeListViewModel(Resource.Drawable.Call, "Customer Care");
            list.Add(list4);
            var myAdapter = new MyCustomAdapter(this, list);
            myListView.SetAdapter(myAdapter);
            myListView.ItemClick += itemClickedOnList;
        }

        public void itemClickedOnList(object sender, AdapterView.ItemClickEventArgs e)
        {
            var value = list[e.Position].text;
            System.Console.WriteLine("The value is " + value);

            if (value.Equals("My Profile"))
            {
                Intent newScreen = new Intent(this, typeof(Login));
                StartActivity(newScreen);
            }
        }
    }
}

