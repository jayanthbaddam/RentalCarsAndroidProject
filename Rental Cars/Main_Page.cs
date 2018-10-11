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
    [Activity(Label = "Main_Page")]
    public class Main_Page : Activity
    {
        TextView MainPageTxt;
        Spinner mySpinner;
        Button AdminAdd, AdminLogOut;
        List<MenuItems> listOfMenu = new List<MenuItems>();
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.ActionBar);
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab("Economy Cars", Resource.Drawable.Economy, new EconomyCarsHome(this));
            AddTab("Luxury Cars", Resource.Drawable.Luxury, new LuxuryCarsHome(this));
            //AddTab("SUV Cars", Resource.Drawable.SUV, new SUVCarsHomePage(this));

            SetContentView(Resource.Layout.Main_Page);
            AdminAdd = FindViewById<Button>(Resource.Id.AdminAddCarsId);
            mySpinner = FindViewById<Spinner>(Resource.Id.MenuViewId);
            MainPageTxt = FindViewById<TextView>(Resource.Id.MainPageTextId);
            AdminLogOut = FindViewById<Button>(Resource.Id.AdminLogOutId);

            string UserFname = Intent.GetStringExtra("FIRSTNAME");
            string UserLname = Intent.GetStringExtra("LASTNAME");
            string userName = Intent.GetStringExtra("USERNAME");
            string AdminUser = Intent.GetStringExtra("ADMIN");
            string UserEmail = Intent.GetStringExtra("EMAIL");
            if (AdminUser == "admin")
            {
                mySpinner.Visibility = ViewStates.Invisible;
                MainPageTxt.Text = "Hello , Admin";
                AdminAdd.Click += delegate
                {
                    Intent newScreen = new Intent(this, typeof(AdminAddCars));
                    newScreen.PutExtra("UserProfile", "admin");
                    StartActivity(newScreen);
                };

                AdminLogOut.Click += delegate
                {
                    Intent newScreen = new Intent(this, typeof(MainActivity));
                    StartActivity(newScreen);
                };
            }
            else
            {
                AdminAdd.Visibility = ViewStates.Invisible;
                AdminLogOut.Visibility = ViewStates.Invisible;
                MainPageTxt.Text = "Hello, " + UserFname + " " + UserLname;
                var Menu4 = new MenuItems((int)typeof(Resource.Drawable).GetField(UserFname).GetValue(null), UserFname + " " + UserLname);
                listOfMenu.Add(Menu4);
                var Menu1 = new MenuItems(Resource.Drawable.Cart, "My Cart");
                listOfMenu.Add(Menu1);
                var Menu2 = new MenuItems(Resource.Drawable.MyProfile, "My Profile");
                listOfMenu.Add(Menu2);
                var Menu3 = new MenuItems(Resource.Drawable.LogOut, "Sign Out");
                listOfMenu.Add(Menu3);
            }

           

            var myAdapter = new CustomMenu(this, listOfMenu);
            mySpinner.Adapter = myAdapter;
            mySpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
                var indexClicked = e.Position;
                var myValue = listOfMenu[indexClicked];
                string MenuValue = myValue.MenuIxt;
                if (MenuValue.Equals("My Profile"))
                {
                    Intent newScreen = new Intent(this, typeof(ViewProfile));
                    newScreen.PutExtra("FIRSTNAME", UserFname);
                    newScreen.PutExtra("LASTNAME", UserLname);
                    newScreen.PutExtra("USERID", UserEmail);
                    StartActivity(newScreen);
                }
                else if(MenuValue.Equals("Sign Out"))
                {
                    Intent newScreen = new Intent(this, typeof(MainActivity));
                    StartActivity(newScreen);
                }
                else if (MenuValue.Equals("My Cart"))
                {
                    Intent newScreen = new Intent(this, typeof(AddToCart));
                    newScreen.PutExtra("FIRSTNAME", UserFname);
                    newScreen.PutExtra("LASTNAME", UserLname);
                    newScreen.PutExtra("EMAIL", UserEmail);
                    StartActivity(newScreen);
                }
            };

            

        }
        public void AddTab(string tabText, int iconResourceId, Fragment fragment)
        {
            var tab = ActionBar.NewTab();

            tab.SetCustomView(Resource.Layout.Tab);
            tab.CustomView.FindViewById<ImageView>(Resource.Id.tabImage).SetImageResource(iconResourceId);
            tab.CustomView.FindViewById<TextView>(Resource.Id.tabText).Text = tabText;

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e) {

                e.FragmentTransaction.Replace(Resource.Id.fragmentContainer, fragment);
            };

            ActionBar.AddTab(tab);
        }
    }
}