using System;
using System.Collections.Generic;
using Windows.ApplicationModel.LockScreen;

namespace AdventOfCode2018.Models
{
    public class ShiftModel : IComparable
    {
        public int GuardId { get; }
        
        public DateTime StartTime { get; }
        
        public bool[] IsAsleepAtMinute { get; }

        public ShiftModel(int guardId, DateTime startTime)
        {
            GuardId = guardId;
            StartTime = startTime;
            IsAsleepAtMinute = new bool[60];
        }

      public int CompareTo(object obj) {
        switch (obj) {
          case null:
            return 1;
          case ShiftModel otherShiftModel:
            return StartTime.CompareTo(otherShiftModel.StartTime);
          default:
            throw new ArgumentException("Object is not a ShiftModel");
        }
      }
    }
}