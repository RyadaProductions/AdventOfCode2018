using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AdventOfCode2018.Mvvm;

namespace AdventOfCode2018.ViewModels
{
    public class Day1ViewModel : BaseViewModel
    {
        private const string OutputFormat = "Final frequency: ";
        private const string Output2Format = "First double frequency: ";

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

        public Day1ViewModel()
        {
            RunCommand = new RelayCommand(RunChallenges);
        }

        private void RunChallenges()
        {
            if (string.IsNullOrWhiteSpace(Input)) return;

            var frequencyChanges = Input.Split('\r', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            RunChallenge1(frequencyChanges);
            RunChallenge2(frequencyChanges);
        }

        private void RunChallenge1(List<int> frequencyChanges)
        {
            Output = OutputFormat + frequencyChanges.Sum();
        }

        private void RunChallenge2(IReadOnlyCollection<int> frequencyChanges, int currentFrequency = 0)
        {
            var visitedFrequencies = new List<int>();

            while (true)
            {
                foreach (var frequencyChange in frequencyChanges)
                {
                    if (visitedFrequencies.Contains(currentFrequency))
                    {
                        Output2 = Output2Format + currentFrequency;
                        return;
                    }

                    visitedFrequencies.Add(currentFrequency);
                    currentFrequency += frequencyChange;
                }
            }
        }
    }
}
