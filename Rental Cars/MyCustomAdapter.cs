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
    public class MyCustomAdapter : BaseAdapter<HomeListViewModel>
    {
        List<HomeListViewModel> myUserListArray;
        Activity context;

        public MyCustomAdapter(Activity context, List<HomeListViewModel> modelList)
            : base()
        {
            this.context = context;
            this.myUserListArray = modelList;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override HomeListViewModel this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.WelcomeCustomListView, null);
            }
            view.FindViewById<ImageView>(Resource.Id.imageId).SetImageResource(myUserModel.imageId);
            view.FindViewById<TextView>(Resource.Id.textId).Text = myUserModel.text;

            return view;
        }
    }
}