﻿using Windows.UI.Xaml.Controls;

namespace AdventOfCode2018.Mvvm
{
    public class BasePage : Page
    {
        private const string ViewModelPropertyName = "ViewModel";

        public BasePage()
        {
            DataContextChanged += (sender, eventArgs) => { SetVmInDataContext(); };
        }

        public void SetVmInDataContext()
        {
            var viewModelProperty = GetType().GetProperty(ViewModelPropertyName);
            if (viewModelProperty != null)
                viewModelProperty.SetMethod.Invoke(this, new[] {DataContext});
        }
    }
}