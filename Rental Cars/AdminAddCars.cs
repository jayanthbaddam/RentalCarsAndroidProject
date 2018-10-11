using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Realms;

namespace Rental_Cars
{
    [Activity(Label = "AdminAddCars")]
    public class AdminAddCars : Activity
    {
        EditText CarName, CarDoors, CarMileage, CarImage, CarNums;
        Spinner mySpinner;
        Button adminAdd, adminDelete;
        string carValue;
        Realm realmObj;
        ArrayAdapter myAdapter;
        string[] CarType = { "Luxury", "Economy", "SUV" };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminAddCars);
            string user = Intent.GetStringExtra("UserProfile");
            CarName = FindViewById<EditText>(Resource.Id.AdminCarModelNameId);
            CarDoors = FindViewById<EditText>(Resource.Id.AdminCarDoorsId);
            CarMileage = FindViewById<EditText>(Resource.Id.AdminCarMileage);
            CarImage = FindViewById<EditText>(Resource.Id.AdminCarImageId);
            mySpinner = FindViewById<Spinner>(Resource.Id.AdminCarTypeId);
            CarNums = FindViewById<EditText>(Resource.Id.AdminNumberOfCarsId);
            adminAdd = FindViewById<Button>(Resource.Id.AddId);
            adminDelete = FindViewById<Button>(Resource.Id.DeleteId);
            myAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, CarType);
            mySpinner.Adapter = myAdapter;
            mySpinner.ItemSelected += spinnerClicked;
            
            realmObj = Realm.GetInstance();
            adminAdd.Click += delegate
            {
                var car = new Cars();
                car.CarModelName = CarName.Text;
                car.CarDoors = "No of Doors : " + CarDoors.Text;
                car.CarMileage = "Mileage : " + CarMileage.Text + "/100km";
                car.CarImageId = (int)typeof(Resource.Drawable).GetField(CarImage.Text).GetValue(null);
                car.NumberOfCars = Convert.ToInt32(CarNums.Text);
                car.CarType = carValue;
                realmObj.Write(() =>
                {
                    realmObj.Add(car, update : true);
                });
                Intent newScreen = new Intent(this, typeof(Main_Page));
                newScreen.PutExtra("ADMIN", "admin");
                StartActivity(newScreen);
            };

            adminDelete.Click += delegate
            {
                var carInfo = realmObj.All<Cars>();
                foreach(var temp in carInfo)
                {
                    if(temp.CarModelName.Equals(CarName.Text))
                    {
                        var car = new Cars();
                        var ModelCar = realmObj.Find<Cars>(temp.CarModelName);
                        realmObj.Write(() =>
                        {
                            realmObj.Remove(ModelCar);
                        });
                        Intent newScreen = new Intent(this, typeof(Main_Page));
                        newScreen.PutExtra("ADMIN", "admin");
                        StartActivity(newScreen);
                    }
                }
            };
        }

        public void spinnerClicked(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var indexClicked = e.Position;
            var myValue = CarType[indexClicked];
            carValue = myValue;
        }
    }
}