using System;
using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;

namespace Misha_Calculator_Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState) //Класс выполняется при открытии новой, пустой формы
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.app_bar_main); //назначаем форму, которая затем отобразится
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar); // связь класса с формой
            SetSupportActionBar(toolbar);
        }

        public override bool OnCreateOptionsMenu(IMenu menu) //как я понимаю он отвечает за троеточие сверху.
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu); // связывет форму меню с главной формой
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item) //выполняется при нажатии любого пункта меню из списка
        {
            int id = item.ItemId; // записываем в id тот пункт, который выбрали.
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        //я не знаю что это, но очень похоже на объявление какого то разрешения. Хотя я не помню чтобы в программе они использовались.
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

