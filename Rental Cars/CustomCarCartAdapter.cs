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
    class CustomCarCartAdapter : BaseAdapter<CarsCart>
    {
        List<CarsCart> myUserListArray;
        Activity context;
        Realm realmObj;

        public CustomCarCartAdapter(Activity context, List<CarsCart> modelList)
            : base()
        {
            this.context = context;
            this.myUserListArray = modelList;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override CarsCart this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomCarsCart, null);
            }
            view.FindViewById<TextView>(Resource.Id.CarCartNameId).Text = myUserModel.CarCartModelName;
            view.FindViewById<TextView>(Resource.Id.CarCartDoorId).Text = myUserModel.CarCartDoors;
            view.FindViewById<TextView>(Resource.Id.CarCartMileageId).Text = myUserModel.CarCartMileage;
            view.FindViewById<TextView>(Resource.Id.CarCartQuantityId).Text = "Quantity : " + myUserModel.NumberOfCarsCart.ToString();
            view.FindViewById<ImageView>(Resource.Id.CarCartImageId).SetImageResource(myUserModel.CarCartImageId);

            view.FindViewById<Button>(Resource.Id.CarDeleteId).Click += delegate
            {
                var carInfo = realmObj.All<CarsCart>();
                System.Console.WriteLine("The value added to cart is " + myUserModel.CarCartModelName);
                foreach(var car in carInfo)
                {
                        var carObj = realmObj.Find<CarsCart>(myUserModel.CarCartModelName);
                        System.Console.WriteLine("The if value is " + car.CarCartModelName);
                        using (var db = realmObj.BeginWrite())
                        {
                            realmObj.Remove(carObj);
                            db.Commit();
                        AlertDialog.Builder Dialog = new AlertDialog.Builder(this.context);
                        AlertDialog alert = Dialog.Create();
                        alert.SetMessage("Item is deleted from cart");
                        alert.SetButton("OK", (c, ev) => { });
                        alert.Show();
                    }
                    
                }

            };
            return view;


        }
        
    }
}