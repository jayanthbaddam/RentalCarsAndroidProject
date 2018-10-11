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
    [Activity(Label = "CustomerDetails")]
    public class CustomerDetails : Activity
    {
        EditText CustomerName, CustomerEmail, CustomerLicense, CustomerDropIn, CustomerDropOff, CustomerDays, CustomerAge;
        Spinner CustomerGender;
        Button submit;
        Realm realmObj;
        ArrayAdapter myAdapter;
        static string Gender;
        string[] GenderGroup = { "Male", "Female", "Androgynes" };
        List<CustomerDetailsModel> list = new List<CustomerDetailsModel>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CustomerDetails);

            CustomerName = FindViewById<EditText>(Resource.Id.CustomerNameId);
            CustomerEmail = FindViewById<EditText>(Resource.Id.CustomerEmailId);
            CustomerLicense = FindViewById<EditText>(Resource.Id.CustomerLicenseId);
            CustomerDropIn = FindViewById<EditText>(Resource.Id.CustomerDropInLocId);
            CustomerDropOff = FindViewById<EditText>(Resource.Id.CustomerDropOffLocId);
            CustomerDays = FindViewById<EditText>(Resource.Id.CustomerNoOfDaysId);
            CustomerAge = FindViewById<EditText>(Resource.Id.CustomerAgeId);
            CustomerGender = FindViewById<Spinner>(Resource.Id.CustomerGenderId);
            submit = FindViewById<Button>(Resource.Id.SubmitId);

            myAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, GenderGroup);
            CustomerGender.Adapter = myAdapter;
            CustomerGender.ItemSelected += spinnerClicked;
           
            realmObj = Realm.GetInstance();

            submit.Click += delegate {

                var CustomerInfo = new CustomerDetailsModel();
                CustomerInfo.CustomerName = CustomerName.Text;
                CustomerInfo.CustomerEmail = CustomerEmail.Text;
                CustomerInfo.CustomerLicense = CustomerLicense.Text;
                CustomerInfo.CustomerDropIn = CustomerDropIn.Text;
                CustomerInfo.CustomerDropOff = CustomerDropOff.Text;
                CustomerInfo.CustomerDays = CustomerDays.Text;
                CustomerInfo.CustomerAge = CustomerAge.Text;
                CustomerInfo.CustomerGender = Gender;

                realmObj.Write(() =>
                {
                    realmObj.Add(CustomerInfo);
                });
                AlertDialog.Builder Dialog = new AlertDialog.Builder(this);
                AlertDialog alert = Dialog.Create();
                alert.SetMessage("Proceed to enter your credit card details");
                alert.SetButton("OK", (c, ev) => { });
                alert.Show();

                Intent newScreen = new Intent(this, typeof(MainActivity));
                StartActivity(newScreen);
            };

        }

        public void spinnerClicked(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var indexClicked = e.Position;
            var myValue = GenderGroup[indexClicked];
            Gender = myValue;
        }
    }
}