using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
namespace Misha_Calculator_Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
       public Button number1, number2, number3, number4, number5, number6, number7, number8, number9, number0;
       public Button AC, OpenBrackets, CloseBrackets, Fact, plus, minus, multi, divide, dot, Result;
       public Button sqr, sqrt, sin, cos, log, Power, tenPower, lg, tg, exp, ln, TriHyp;
        public TextView DisplayBox;
        protected override void OnCreate(Bundle savedInstanceState) //Класс выполняется при открытии новой, пустой формы
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_main); //назначаем форму, которая затем отобразится
            // Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar); // связь класса с формой
            //SetSupportActionBar(toolbar);

            dot = FindViewById<Button>(Resource.Id.dot);
            AC = FindViewById<Button>(Resource.Id.buttonAC);
            OpenBrackets = FindViewById<Button>(Resource.Id.scobka);
            CloseBrackets = FindViewById<Button>(Resource.Id.closeSckobka);
            Fact = FindViewById<Button>(Resource.Id.Fact);
            plus = FindViewById<Button>(Resource.Id.Plus);
            minus = FindViewById<Button>(Resource.Id.Minus);
            multi = FindViewById<Button>(Resource.Id.Multi);
            divide = FindViewById<Button>(Resource.Id.divide);
            DisplayBox = FindViewById<TextView>(Resource.Id.DisplayBox);
            
            number0 = FindViewById<Button>(Resource.Id.button0);
            number1 = FindViewById<Button>(Resource.Id.button1);
            number2 = FindViewById<Button>(Resource.Id.button2);
            number3 = FindViewById<Button>(Resource.Id.button3);
            number4 = FindViewById<Button>(Resource.Id.button4);
            number5 = FindViewById<Button>(Resource.Id.button5);
            number6 = FindViewById<Button>(Resource.Id.button6);
            number7 = FindViewById<Button>(Resource.Id.button7);
            number8 = FindViewById<Button>(Resource.Id.button8);
            number9 = FindViewById<Button>(Resource.Id.button9);

            sin = FindViewById<Button>(Resource.Id.sin);
            cos = FindViewById<Button>(Resource.Id.cos);
            sqr = FindViewById<Button>(Resource.Id.step2);
            sqrt = FindViewById<Button>(Resource.Id.sqrt);
            log = FindViewById<Button>(Resource.Id.log);
            lg = FindViewById<Button>(Resource.Id.lg);
            tenPower = FindViewById<Button>(Resource.Id.tenPower);
            tg = FindViewById<Button>(Resource.Id.tg);
            exp = FindViewById<Button>(Resource.Id.exp);
            TriHyp = FindViewById<Button>(Resource.Id.TriHyp);
            Power = FindViewById<Button>(Resource.Id.Power);
            Result = FindViewById<Button>(Resource.Id.ravn);


            Connector Actions = new Connector(DisplayBox,CloseBrackets);
            number0.Click += Actions.DigitsInput;
            number1.Click += Actions.DigitsInput;
            number2.Click += Actions.DigitsInput;
            number3.Click += Actions.DigitsInput;
            number4.Click += Actions.DigitsInput;
            number5.Click += Actions.DigitsInput;
            number6.Click += Actions.DigitsInput;
            number7.Click += Actions.DigitsInput;
            number8.Click += Actions.DigitsInput;
            number9.Click += Actions.DigitsInput;

            plus.Click += Actions.SimpleFunctionInput;
            minus.Click += Actions.SimpleFunctionInput;
            multi.Click += Actions.SimpleFunctionInput;
            divide.Click += Actions.SimpleFunctionInput;
            Result.Click += Actions.CalculateResult;
            AC.Click += Actions.BackspaceInput;
            AC.LongClick += Actions.DeleteInput;
            dot.Click += Actions.DotInput;
            sqr.Click += Actions.SqrInput;
            OpenBrackets.Click += Actions.LeftBracketInput;
            CloseBrackets.Click += Actions.RightBracketInput;
            Power.Click += Actions.PowInput;
            
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

