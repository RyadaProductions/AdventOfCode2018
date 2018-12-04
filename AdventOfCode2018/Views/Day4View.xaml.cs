using AdventOfCode2018.Mvvm;
using AdventOfCode2018.ViewModels;

namespace AdventOfCode2018.Views
{
    public sealed partial class Day4View : BasePage
    {
        public Day4ViewModel ViewModel { get; set; }

        public Day4View()
        {
            DataContext = new Day4ViewModel();
            InitializeComponent();
        }
    }
}