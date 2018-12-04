using AdventOfCode2018.Mvvm;

namespace AdventOfCode2018.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _header;

        public string Header
        {
            get => _header;
            set => SetAndNotify(ref _header, value);
        }
    }
}