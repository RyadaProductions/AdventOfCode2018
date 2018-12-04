using System;
using Windows.UI.Xaml.Input;
using Microsoft.UI.Xaml.Controls;
using AdventOfCode2018.Mvvm;
using AdventOfCode2018.ViewModels;
using AdventOfCode2018.Views;

namespace AdventOfCode2018
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : BasePage
    {
        public MainPageViewModel ViewModel { get; set; }

        public MainPage()
        {
            DataContext = new MainPageViewModel();
            InitializeComponent();
            NavigateTo("Day 1");
        }

        private void NavigationView_OnItemInvoked(NavigationView navigationView,
          NavigationViewItemInvokedEventArgs args)
        {
            var label = args.InvokedItemContainer.Content as string;
            NavigateTo(label);
        }

        private void NavigateTo(string pageName)
        {
            Type pageType = null;

            // TODO: convert magic strings into constants, and bind to them from the view
            switch (pageName)
            {
                case "Day 1":
                    pageType = typeof(Day1View);
                    break;
                case "Day 2":
                    pageType = typeof(Day2View);
                    break;
                case "Day 3":
                    pageType = typeof(Day3View);
                    break;
                case "Day 4":
                    pageType = typeof(Day4View);
                    break;
            }

            if (pageType == null || pageType == ContentFrame.CurrentSourcePageType) return;

            ViewModel.Header = pageName;
            ContentFrame.Navigate(pageType);
        }

        private async void OnRyadaProductionsTapped(object sender, TappedRoutedEventArgs e)
        {
            var uri = new Uri(@"https://github.com/RyadaProductions/AdventOfCode2018");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}