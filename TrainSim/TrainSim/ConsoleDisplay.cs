using System;

namespace TrainSim
{
    public class ConsoleDisplay : IRailwayObserver
    {
        private const int Row = 3;
        private const int SectionMarkRow = 1;
        private readonly char Rail = '#';
        private readonly object consoleLock = new();

        private int sectionLength;
        private int trackLength;

        public void OnTrackBuilt(int sectionCount, int sectionLength, int trackLength)
        {
            this.sectionLength = sectionLength;
            this.trackLength = trackLength;

            Console.Clear();
            Console.WriteLine();

            // Draw section markers
            for (int i = 0; i < sectionCount; i++)
            {
                int pos = i * sectionLength;
                Console.SetCursorPosition(pos, SectionMarkRow);
                Console.Write('/');
                Console.SetCursorPosition(pos, SectionMarkRow + 1);
                Console.Write($"|({i + 1})");
            }

            // Draw rail
            Console.SetCursorPosition(0, Row);
            for (int i = 0; i < trackLength; i++)
                Console.Write(Rail);
        }

        public void OnTrainMoved(string train, int position)
        {
            lock (consoleLock)
            {
                if (position > 0)
                {
                    Console.SetCursorPosition(position - 1, Row);
                    Console.Write(Rail);
                }

                Console.SetCursorPosition(position, Row);
                Console.Write(train);
            }
        }

        public void OnSectionEntered(int sectionIndex)
        {
            lock (consoleLock)
            {
                Console.SetCursorPosition(sectionIndex * sectionLength, SectionMarkRow);
                Console.Write('-');
            }
        }

        public void OnSectionExited(int sectionIndex)
        {
            lock (consoleLock)
            {
                Console.SetCursorPosition(sectionIndex * sectionLength, SectionMarkRow);
                Console.Write('/');
            }
        }
    }
}
