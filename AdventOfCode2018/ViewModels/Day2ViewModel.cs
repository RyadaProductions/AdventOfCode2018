using System;
using System.Linq;
using System.Windows.Input;
using AdventOfCode2018.Mvvm;

namespace AdventOfCode2018.ViewModels
{
    public class Day2ViewModel : BaseViewModel
    {
        private const string OutputFormat = "Box list checksum: ";
        private const string Output2Format = "Common letters on fabric boxIds: ";

        private string _input;
        private string _output = OutputFormat;
        private string _output2 = Output2Format;

        public string Input
        {
            get => _input;
            set => SetAndNotify(ref _input, value);
        }

        public string Output
        {
            get => _output;
            private set => SetAndNotify(ref _output, value);
        }

        public string Output2
        {
            get => _output2;
            private set => SetAndNotify(ref _output2, value);
        }

        public ICommand RunCommand { get; }

        public Day2ViewModel()
        {
            RunCommand = new RelayCommand(RunChallenges);
        }

        private void RunChallenges()
        {
            if (string.IsNullOrWhiteSpace(Input)) return;

            // Input format: asdhasdhjkash
            var boxIds = Input.Split('\r', StringSplitOptions.RemoveEmptyEntries);

            RunChallenge1(boxIds);
            RunChallenge2(boxIds);
        }

        private void RunChallenge1(string[] boxIds)
        {
            var doubles = 0;
            var triples = 0;

            foreach (var boxId in boxIds)
            {
                if (boxId.GroupBy(x => x).Any(c => c.Count() == 2)) doubles++;
                if (boxId.GroupBy(x => x).Any(c => c.Count() == 3)) triples++;
            }

            Output = OutputFormat + doubles * triples;
        }

        private void RunChallenge2(string[] boxIds)
        {
            var duplicateId = "";

            foreach (var boxId in boxIds)
            {
                foreach (var checkId in boxIds)
                {
                    if (boxId == checkId) continue;
                    var result = boxId.Zip(checkId, (char1, char2) => char1 == char2 ? 0 : 1).Sum();
                    if (result != 1) continue;

                    duplicateId = string.Concat(boxId.Zip(checkId, (char1, char2) => char1 == char2 ? char1 : '\0'));
                    break;
                }

                if (!string.IsNullOrWhiteSpace(duplicateId)) break;
            }

            Output2 = Output2Format + duplicateId;
        }
    }
}
