using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using AdventOfCode2018.Views;

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
            ContentFrame.Navigate(typeof(Day1View));
        }

        private void NavigationView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var label = args.InvokedItem as string;
            Type pageType = null;

            // TODO: convert magic strings into constants, and bind to them from the view
            switch (label)
            {
                case "Day 1":
                    pageType = typeof(Day1View);
                    break;
                case "Day 2":
                    pageType = typeof(Day2View);
                    break;
            }

            if (pageType != null && pageType != ContentFrame.CurrentSourcePageType)
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
