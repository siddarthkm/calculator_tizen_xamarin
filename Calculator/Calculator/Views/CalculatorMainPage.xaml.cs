using System.Threading;
using Calculator.ViewModels;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Calculator.Views
{
    /// <summary>
    /// A Calculator portrait layout class. </summary>
    public partial class CalculatorMainPage : ContentPage
    {
        private Mutex CounterMutex = new Mutex(false, "counter_mutex");
        private int counter;

        public CalculatorMainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            MessagingCenter.Subscribe<MainPageViewModel, string>(this, "alert", (sender, arg) =>
            {
                CounterMutex.WaitOne();
                counter++;
                CounterMutex.ReleaseMutex();

                AlertToast.IsVisible = true;
                AlertToast.Text = arg.ToString();
                CloseAlertToast();
            });
        }

        /// <summary>
        /// A method close alert toast after 1.5 seconds later. </summary>
        async void CloseAlertToast()
        {
            await Task.Delay(1500);
            CounterMutex.WaitOne();
            if (--counter <= 0)
            {
                counter = 0;
                AlertToast.IsVisible = false;
            }

            CounterMutex.ReleaseMutex();
        }
    }
}
