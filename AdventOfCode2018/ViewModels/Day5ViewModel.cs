using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using AdventOfCode2018.Models;
using AdventOfCode2018.Mvvm;

namespace AdventOfCode2018.ViewModels
{
    public class Day5ViewModel : BaseViewModel
    {
        private const string OutputFormat = "Polymer length after reactions: ";
        private const string Output2Format = "Shortest possible polymer length after reactions: ";

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

        public Day5ViewModel()
        {
            RunCommand = new RelayCommand(RunChallenges);
        }

        private void RunChallenges()
        {
            if (string.IsNullOrWhiteSpace(Input)) return;

            // input format: askdaAWDAhuiqdw
            var input = Input.Replace("\r", "");

            RunChallenge1(input);
            RunChallenge2(input);
        }

        private void RunChallenge1(string input)
        {
            var length = ReactAndCountPolymerLength(input);

            Output = OutputFormat + length;
        }

        private void RunChallenge2(string input)
        {
            var distinctCharacters = input.ToUpper().Distinct();
            var smallestLength = distinctCharacters.Select(x => ReplaceAndCountLength(x, input)).Min();

            Output2 = Output2Format + smallestLength;
        }

        private int ReplaceAndCountLength(char character, string input)
        {
            var changedPolymer = input.Replace($"{character}", "", true, CultureInfo.InvariantCulture);
            return ReactAndCountPolymerLength(changedPolymer);
        }

        private int ReactAndCountPolymerLength(string input)
        {
            for (var index = 0; index < input.Length - 1; index++)
            {
                var currentChar = input[index];
                var nextChar = input[index + 1];

                if (char.ToUpper(currentChar) != char.ToUpper(nextChar)) continue;

                if ((!char.IsLower(currentChar) || !char.IsUpper(nextChar)) &&
                    (!char.IsUpper(currentChar) || !char.IsLower(nextChar))) continue;

                input = input.Remove(index, 2);
                index = index < 2 ? -1 : index - 2;
            }

            return input.Length;
        }
    }
}