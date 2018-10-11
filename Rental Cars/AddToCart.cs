using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Realms;

namespace Rental_Cars
{
    [Activity(Label = "AddToCart")]
    public class AddToCart : Activity
    {
        ListView myList;
        SearchView mySearch;
        Realm realmObj;
        Button btn, back;
        List<CarsCart> list = new List<CarsCart>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddToCart);
            string UserFname = Intent.GetStringExtra("FIRSTNAME");
            string UserLname = Intent.GetStringExtra("LASTNAME");
            string UserEmail = Intent.GetStringExtra("EMAIL");
            realmObj = Realm.GetInstance();
            myList = FindViewById<ListView>(Resource.Id.AddToCartListId);
            mySearch = FindViewById<SearchView>(Resource.Id.AddToCartSearchId);
            btn = FindViewById<Button>(Resource.Id.ProceedId);
            back = FindViewById<Button>(Resource.Id.BackToId);
            var carInfo = realmObj.All<CarsCart>();
            foreach (var temp in carInfo)
            {
                    var car = new CarsCart();
                    car.CarCartModelName = temp.CarCartModelName;
                    car.CarCartMileage = temp.CarCartMileage;
                    car.CarCartImageId = temp.CarCartImageId;
                    car.CarCartDoors = temp.CarCartDoors;
                    car.NumberOfCarsCart = temp.NumberOfCarsCart;
                    list.Add(car);
            }
            var myAdapter = new CustomCarCartAdapter(this, list);
            myList.SetAdapter(myAdapter);
            btn.Click += delegate
            {
                Intent newScreen = new Intent(this, typeof(CustomerDetails));
                StartActivity(newScreen);
            };
            back.Click += delegate
            {
                Intent newScreen = new Intent(this, typeof(Main_Page));
                newScreen.PutExtra("FIRSTNAME", UserFname);
                newScreen.PutExtra("LASTNAME", UserLname);
                newScreen.PutExtra("EMAIL", UserEmail);
                StartActivity(newScreen);
            };
          }
    }
}