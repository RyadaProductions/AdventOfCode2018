using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;
using Windows.UI.Xaml.Shapes;
using AdventOfCode2018.Models;
using AdventOfCode2018.Mvvm;

namespace AdventOfCode2018.ViewModels
{
    public class Day3ViewModel : BaseViewModel
    {
        private const string OutputFormat = "Overlapped square inches of fabric: ";
        private const string Output2Format = "Non overlapped claimId: ";

        private string _input;
        private string _output = OutputFormat;
        private string _output2 = Output2Format;

        private readonly int[,] _fabricInches = new int[1000, 1000];

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

        public Day3ViewModel()
        {
            RunCommand = new RelayCommand(RunChallenges);
        }

        private void RunChallenges()
        {
            if (string.IsNullOrWhiteSpace(Input)) return;

            // input format: #33 @ 525,695: 22x23
            var lines = Input.Split('\r', StringSplitOptions.RemoveEmptyEntries);
            var fabricSquares = new List<FabricClaimModel>();

            foreach (var line in lines)
            {
                var text = line.Replace("#", string.Empty)
                    .Replace("@", string.Empty)
                    .Replace(":", string.Empty);
                var values = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var claimId = int.Parse(values[0]);
                var xCoordinate = int.Parse(values[1].Split(',')[0]);
                var yCoordinate = int.Parse(values[1].Split(',')[1]);
                var width = int.Parse(values[2].Split('x')[0]);
                var height = int.Parse(values[2].Split('x')[1]);

                fabricSquares.Add(new FabricClaimModel(claimId, xCoordinate, yCoordinate, width, height));
            }

            RunChallenge1(fabricSquares);
            RunChallenge2(fabricSquares);
        }

        private void RunChallenge1(IEnumerable<FabricClaimModel> fabricSquares)
        {
            foreach (var fabricSquare in fabricSquares)
            {
                for (var xOffset = 0; xOffset < fabricSquare.Width; xOffset++)
                {
                    for (var yOffset = 0; yOffset < fabricSquare.Height; yOffset++)
                    {
                        var xCoordinate = fabricSquare.XCoordinate + xOffset;
                        var yCoordinate = fabricSquare.YCoordinate + yOffset;

                        if (_fabricInches[xCoordinate, yCoordinate] == 0)
                        {
                            _fabricInches[xCoordinate, yCoordinate] = fabricSquare.ClaimId;
                            continue;
                        }

                        _fabricInches[xCoordinate, yCoordinate] = -1;
                    }
                }
            }

            var duplicateInches = _fabricInches.Cast<int>().Count(x => x == -1);
            Output = OutputFormat + duplicateInches;
        }

        private void RunChallenge2(IEnumerable<FabricClaimModel> fabricSquares)
        {
            foreach (var fabricSquare in fabricSquares)
            {
                var squareSize = fabricSquare.Width * fabricSquare.Height;
                var nonDuplicateClaimSize = 0;

                for (var xOffset = 0; xOffset < fabricSquare.Width; xOffset++)
                {
                    for (var yOffset = 0; yOffset < fabricSquare.Height; yOffset++)
                    {
                        var xCoordinate = fabricSquare.XCoordinate + xOffset;
                        var yCoordinate = fabricSquare.YCoordinate + yOffset;
                        
                        if (_fabricInches[xCoordinate, yCoordinate] == fabricSquare.ClaimId) nonDuplicateClaimSize++;

                        if (squareSize != nonDuplicateClaimSize) continue;

                        Output2 = Output2Format + fabricSquare.ClaimId;
                        return;
                    }
                }
            }
        }
    }
}