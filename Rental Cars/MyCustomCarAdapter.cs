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
    public class MyCustomCarAdapter : BaseAdapter<Cars>
    {
        List<Cars> myUserListArray;
        Activity context;
        Realm realmObj;

        public MyCustomCarAdapter(Activity context, List<Cars> modelList)
            : base()
        {
            this.context = context;
            this.myUserListArray = modelList;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override Cars this[int position]
        {
            get { return myUserListArray[position]; }
        }
        public override int Count
        {
            get { return myUserListArray.Count(); }
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var myUserModel = myUserListArray[position];
            realmObj = Realm.GetInstance();

            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.LuxuryCarsCustomList, null);
            }
            view.FindViewById<TextView>(Resource.Id.CarNameId).Text = myUserModel.CarModelName;
            view.FindViewById<TextView>(Resource.Id.LuxuryCarDoorId).Text = myUserModel.CarDoors+" - Doors";
            view.FindViewById<TextView>(Resource.Id.LuxuryCarMileageId).Text = myUserModel.CarMileage;
            view.FindViewById<TextView>(Resource.Id.LuxuryCarQuantityId).Text = "Quantity : "+myUserModel.NumberOfCars.ToString();
            view.FindViewById<ImageView>(Resource.Id.LuxuryCarImageId).SetImageResource(myUserModel.CarImageId);
            view.FindViewById<Button>(Resource.Id.LuxuryCarAddId).Click += delegate
            {
                var CarInformation = realmObj.All<CarsCart>();
              
                var carInfo = new CarsCart();
                carInfo.CarCartModelName = myUserModel.CarModelName;
                carInfo.CarCartDoors = myUserModel.CarDoors;
                carInfo.CarCartMileage = myUserModel.CarMileage;
                carInfo.NumberOfCarsCart = myUserModel.NumberOfCars;
                carInfo.CarCartImageId = myUserModel.CarImageId;
                carInfo.CarCartType = "AddToCart";
                realmObj.Write(() =>
                {
                    realmObj.Add(carInfo, update:true);
                });
                AlertDialog.Builder Dialog = new AlertDialog.Builder(this.context);
                AlertDialog alert = Dialog.Create();
                alert.SetMessage("Item is added to the cart.");
                alert.SetButton("OK", (c, ev) => { });
                alert.Show();
            };
            return view;
        }
    }
}