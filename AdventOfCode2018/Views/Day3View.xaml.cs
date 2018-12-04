using AdventOfCode2018.Mvvm;
using AdventOfCode2018.ViewModels;

namespace AdventOfCode2018.Views
{
    public partial class Day3View : BasePage
    {
        public Day3View()
        {
            DataContext = new Day3ViewModel();
            InitializeComponent();
        }

        public Day3ViewModel ViewModel { get; set; }
    }
}
