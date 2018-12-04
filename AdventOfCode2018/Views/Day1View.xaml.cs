using AdventOfCode2018.Mvvm;
using AdventOfCode2018.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AdventOfCode2018.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Day1View : BasePage
    {
        public Day1ViewModel ViewModel { get; set; }
        
        public Day1View()
        {
            DataContext = new Day1ViewModel();
            InitializeComponent();
        }
    }
}
