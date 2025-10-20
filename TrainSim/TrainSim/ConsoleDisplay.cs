using System;

namespace TrainSim
{
    public class ConsoleDisplay
    {
        private const int Row = 3;
        private const int SectionMarkRow = 1;
        private readonly char Rail = '#';
        private readonly object consoleLock = new();

        private int sectionLength;
        private int trackLength;

        public void Subscribe(Railway railway)
        {
            railway.TrackBuilt += OnTrackBuilt;
            railway.TrainMoved += OnTrainMoved;
            railway.SectionEntered += OnSectionEntered;
            railway.SectionExited += OnSectionExited;
        }

        private void OnTrackBuilt(object? sender, TrackBuiltEventArgs e)
        {
            sectionLength = e.SectionLength;
            trackLength = e.TrackLength;

            Console.Clear();
            Console.WriteLine();

            // Draw section markers and labels
            for (int i = 0; i < e.SectionCount; i++)
            {
                int pos = i * e.SectionLength;
                Console.SetCursorPosition(pos, SectionMarkRow);
                Console.Write('/');
                Console.SetCursorPosition(pos, SectionMarkRow + 1);
                Console.Write($"|({i + 1})");
            }

            // Draw the rail line
            Console.SetCursorPosition(0, Row);
            for (int i = 0; i < e.TrackLength; i++)
                Console.Write(Rail);
        }

        private void OnTrainMoved(object? sender, TrainMovedEventArgs e)
        {
            lock (consoleLock)
            {
                if (e.Position > 0)
                {
                    Console.SetCursorPosition(e.Position - 1, Row);
                    Console.Write(Rail);
                }
                Console.SetCursorPosition(e.Position, Row);
                Console.Write(e.TrainSymbol);
            }
        }

        private void OnSectionEntered(object? sender, SectionEventArgs e)
        {
            lock (consoleLock)
            {
                Console.SetCursorPosition(e.SectionIndex * sectionLength, SectionMarkRow);
                Console.Write('-');
            }
        }

        private void OnSectionExited(object? sender, SectionEventArgs e)
        {
            lock (consoleLock)
            {
                Console.SetCursorPosition(e.SectionIndex * sectionLength, SectionMarkRow);
                Console.Write('/');
            }
        }
    }
}
