using System;

namespace TrainSim
{
    public class TrainMovedEventArgs : EventArgs
    {
        public string TrainSymbol { get; }
        public int Position { get; }

        public TrainMovedEventArgs(string trainSymbol, int position)
        {
            TrainSymbol = trainSymbol;
            Position = position;
        }
    }

    public class SectionEventArgs : EventArgs
    {
        public int SectionIndex { get; }

        public SectionEventArgs(int sectionIndex)
        {
            SectionIndex = sectionIndex;
        }
    }

    public class TrackBuiltEventArgs : EventArgs
    {
        public int SectionCount { get; }
        public int SectionLength { get; }
        public int TrackLength { get; }

        public TrackBuiltEventArgs(int sectionCount, int sectionLength, int trackLength)
        {
            SectionCount = sectionCount;
            SectionLength = sectionLength;
            TrackLength = trackLength;
        }
    }
}