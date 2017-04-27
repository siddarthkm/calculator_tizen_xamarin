using Calculator.ViewModels;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Calculator.Views
{
    /// <summary>
    /// A Calculator landscape layout class. </summary>
    public partial class CalculatorMainPageLandscape : ContentPage
    {
        private Mutex CounterMutex = new Mutex(false, "counter_mutex_landscape");
        private int counter;

        public CalculatorMainPageLandscape()
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
