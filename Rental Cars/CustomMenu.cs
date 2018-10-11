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

namespace Rental_Cars
{
    class CustomMenu : BaseAdapter<MenuItems>
    {
        List<MenuItems> myUserListArray;
        Activity context;

        public CustomMenu(Activity context, List<MenuItems> modelList)
            : base()
        {
            this.context = context;
            this.myUserListArray = modelList;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override MenuItems this[int position]
        {
            get { return myUserListArray[position]; }
        }
        public override int Count
        {
            get { return myUserListArray.Count; }
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var myUserModel = myUserListArray[position];

            View view = convertView;
            if (view == null)
            { 
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomMenu, null);
            }

            view.FindViewById<TextView>(Resource.Id.MenuTextId).Text = myUserModel.MenuIxt;
            view.FindViewById<ImageView>(Resource.Id.MenuImageId).SetImageResource(myUserModel.MenuImage);
            return view;
        }
    }
}