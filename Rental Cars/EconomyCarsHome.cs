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
    [Activity(Label = "EconomyCars")]
    public class EconomyCarsHome : Fragment
    {
        ListView myList;
        Activity context;
        Realm realmObj;
        SearchView mySearch;
        List<Cars> list = new List<Cars>();

        public EconomyCarsHome(Activity cxt)
        {
            this.context = cxt;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.EconomyCarHomePage, container, false);
            myList = view.FindViewById<ListView>(Resource.Id.EconomyCarsListId);
            mySearch = view.FindViewById<SearchView>(Resource.Id.EconomyCarsSearchId);

            realmObj = Realm.GetInstance();
            var carInfo = realmObj.All<Cars>();
            foreach(var temp in carInfo)
            {
                if(temp.CarType == "Economy")
                {
                    var car = new Cars();
                    car.CarModelName = temp.CarModelName;
                    car.CarMileage = temp.CarMileage;
                    car.CarImageId = temp.CarImageId;
                    car.CarDoors = temp.CarDoors;
                    car.NumberOfCars = temp.NumberOfCars;
                    list.Add(car);
                }
            }

            var myAdapter = new MyCustomCarAdapter(this.Activity, list);
            myList.SetAdapter(myAdapter);
            mySearch.QueryTextChange += searchBarText;
            return view;
        }

        public void searchBarText(object sender, SearchView.QueryTextChangeEventArgs e)
        {

            var myFilterList = myModel(e.NewText);
            var myAdapter = new MyCustomCarAdapter(this.Activity, list);
            myList.SetAdapter(myAdapter);
        }

        private List<Cars> myModel(string text)
        {
            List<Cars> mylist = new List<Cars>();
            foreach (var modelValue in mylist)
            {
                if (modelValue.CarModelName.ToLower().Contains(text.ToLower()))
                {
                    mylist.Add(modelValue);
                }

            }
            return mylist;
        }


    }
}