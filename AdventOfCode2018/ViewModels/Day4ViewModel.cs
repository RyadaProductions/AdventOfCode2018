using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using AdventOfCode2018.Models;
using AdventOfCode2018.Mvvm;

namespace AdventOfCode2018.ViewModels
{
    public class Day4ViewModel : BaseViewModel
    {
        private const string OutputFormat = "GuardId that sleeps the most * the most frequent minute: ";
        private const string Output2Format = "GuardId that slept the most on the same minute * the same minute: ";

        private string _input;
        private string _output = OutputFormat;
        private string _output2 = Output2Format;

        // Matches everything between the characters '[' and ']'
        private readonly Regex _bracketContentRegex = new Regex(@"(?<=\[)[^\]]+", RegexOptions.Compiled);

        // Matches everything between the characters '#' and ' '
        private readonly Regex _guardIdRegex = new Regex(@"(?<=#)[^\ ]+", RegexOptions.Compiled);

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

        public Day4ViewModel()
        {
            RunCommand = new RelayCommand(RunChallenges);
        }

        private void RunChallenges()
        {
            if (string.IsNullOrWhiteSpace(Input)) return;

            // Input format: #33 @ 525,695: 22x23
            var lines = Input.Split('\r', StringSplitOptions.RemoveEmptyEntries);
            var eventMessages = new SortedDictionary<DateTime, string>();
            var shiftModels = new SortedSet<ShiftModel>();

            foreach (var line in lines)
            {
                var match = _bracketContentRegex.Match(line);
                if (!match.Success) return;

                eventMessages.Add(DateTime.Parse(match.Value, CultureInfo.InvariantCulture), line.Split(']')[1]);
            }

            var fellAsleepAtMinute = -1;

            foreach (var (dateTime, message) in eventMessages)
            {
                var match = _guardIdRegex.Match(message);
                if (match.Success)
                {
                    if (fellAsleepAtMinute != -1)
                    {
                        FillAsleepMinutes(shiftModels.Last(), fellAsleepAtMinute, 59);
                        fellAsleepAtMinute = -1;
                    }

                    // add new shift object to shifts sorted set
                    var model = new ShiftModel(int.Parse(match.Value), dateTime);
                    shiftModels.Add(model);
                    continue;
                }

                // add message to event list in shift object
                if (message.Contains("falls asleep"))
                {
                    fellAsleepAtMinute = dateTime.Minute;
                    continue;
                }

                if (fellAsleepAtMinute == -1) continue;

                FillAsleepMinutes(shiftModels.Last(), fellAsleepAtMinute, dateTime.Minute);
                fellAsleepAtMinute = -1;
            }


            RunChallenge1(shiftModels);
            RunChallenge2(shiftModels);
        }

        private void FillAsleepMinutes(ShiftModel shiftModel, int startMinute, int endMinute)
        {
            for (var currentMinute = startMinute; currentMinute < endMinute; currentMinute++)
            {
                shiftModel.IsAsleepAtMinute[currentMinute] = true;
            }
        }

        private void RunChallenge1(SortedSet<ShiftModel> shiftModels)
        {
            var guardIdSleepMinutes = new Dictionary<int, int>();

            foreach (var shiftModel in shiftModels)
            {
                if (guardIdSleepMinutes.ContainsKey(shiftModel.GuardId))
                {
                    guardIdSleepMinutes[shiftModel.GuardId] += shiftModel.IsAsleepAtMinute.Count(isAsleep => isAsleep);
                    continue;
                }

                guardIdSleepMinutes.Add(shiftModel.GuardId, shiftModel.IsAsleepAtMinute.Count(isAsleep => isAsleep));
            }

            var highestSleepTime = guardIdSleepMinutes.Max(x => x.Value);
            var guardThatSleepsTheMost = guardIdSleepMinutes.FirstOrDefault(x => x.Value == highestSleepTime).Key;
            var sleepAtMinutes = new int[60];

            foreach (var shiftModel in shiftModels.Where(model => model.GuardId == guardThatSleepsTheMost))
            {
                for (var i = 0; i < 60; i++)
                {
                    sleepAtMinutes[i] += shiftModel.IsAsleepAtMinute[i] ? 1 : 0;
                }
            }

            var asleepTheMostOnMinute = sleepAtMinutes.ToList().IndexOf(sleepAtMinutes.Max());

            Output = OutputFormat + guardThatSleepsTheMost * asleepTheMostOnMinute;
        }

        private void RunChallenge2(IEnumerable<ShiftModel> shiftModels)
        {
            var guardIdToSleepCount = new Dictionary<int, int[]>();

            foreach (var shiftModel in shiftModels)
            {
                if (!guardIdToSleepCount.ContainsKey(shiftModel.GuardId))
                    guardIdToSleepCount.Add(shiftModel.GuardId, new int[60]);

                for (var minute = 0; minute < 60; minute++)
                {
                    guardIdToSleepCount[shiftModel.GuardId][minute] += 
                        shiftModel.IsAsleepAtMinute[minute] ? 1 : 0;
                }
            }

            var mostSleptOnMinute = 0;
            var timesSleptOnMinute = 0;
            var guardIdSleptOnMinute = 0;

            foreach (var (guardId, minuteCount) in guardIdToSleepCount)
            {
                var mostFrequentlySleptOnMinute = minuteCount.Max();
                var minuteSleptTheMostOn = minuteCount.ToList().IndexOf(mostFrequentlySleptOnMinute);

                if (mostFrequentlySleptOnMinute <= timesSleptOnMinute) continue;
                
                timesSleptOnMinute = mostFrequentlySleptOnMinute;
                mostSleptOnMinute = minuteSleptTheMostOn;
                guardIdSleptOnMinute = guardId;
            }

            Output2 = Output2Format + guardIdSleptOnMinute * mostSleptOnMinute;
        }
    }
}