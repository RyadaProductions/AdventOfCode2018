using AdventOfCode2018.Mvvm;
using AdventOfCode2018.ViewModels;

namespace AdventOfCode2018.Views
{
    public partial class Day5View : BasePage
    {
        public Day5ViewModel ViewModel { get; set; }
        
        public Day5View()
        {
            DataContext = new Day5ViewModel();
            InitializeComponent();
        }
    }
}
