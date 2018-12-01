using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using AdventOfCode2018.Views;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdventOfCode2018
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void NavigationView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var label = args.InvokedItem as string;
            var pageType = typeof(Day1View);
            if (pageType != ContentFrame.CurrentSourcePageType)
            {
                ContentFrame.Navigate(pageType);
            }
        }

        private async void OnRyadaProductionsTapped(object sender, TappedRoutedEventArgs e)
        {
            var uri = new Uri(@"https://github.com/RyadaProductions/AdventOfCode2018");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
