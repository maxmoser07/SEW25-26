using System;
using System.Threading;

namespace TrainSim
{
    public class Railway
    {
        private readonly Semaphore[] semaphores;
        private readonly char Rail;
        public string Train { get; set; }
        private readonly int TrackLength;
        private readonly int SectionCount;
        private readonly int SectionLength;

        private const int Row = 3;            // train row
        private const int SectionMarkRow = 1; // / or - row

        public Railway(string train, int trackLength = 80)
        {
            Rail = '#';
            Train = train;
            TrackLength = trackLength;

            SectionLength = train.Length * 2; // section approx twice train length
            SectionCount = (TrackLength + SectionLength - 1) / SectionLength;

            semaphores = new Semaphore[SectionCount];
            for (int i = 0; i < SectionCount; i++)
                semaphores[i] = new Semaphore(1, 1);
        }

        public void BuildTrack()
        {
            Console.WriteLine();

            // Draw section markers
            for (int i = 0; i < SectionCount; i++)
            {
                int pos = i * SectionLength;
                Console.SetCursorPosition(pos, SectionMarkRow);
                Console.Write('/');
            }

            // Draw section numbers
            for (int i = 0; i < SectionCount; i++)
            {
                int pos = i * SectionLength;
                Console.SetCursorPosition(pos, SectionMarkRow + 1);
                Console.Write($"|({i + 1})");
            }

            // Draw rail
            Console.SetCursorPosition(0, Row);
            for (int i = 0; i < TrackLength; i++)
                Console.Write(Rail);
        }

        public void RunTrain()
        {
            RunTrain(500); // default clearance delay
        }

        private void RunTrain(int clearanceDelayMs)
        {
            int trackEnd = TrackLength - Train.Length;
            int currentSection = -1;

            for (int pos = 0; pos <= trackEnd;)
            {
                // Sections train will occupy
                int front = pos;
                int rear = pos + Train.Length - 1;

                int nextFrontSection = front / SectionLength;
                int nextRearSection = rear / SectionLength;

                // Wait for all new sections before moving
                for (int s = nextFrontSection; s <= nextRearSection; s++)
                {
                    if (s != currentSection)
                    {
                        semaphores[s].WaitOne();

                        // Visual: mark section as blocked
                        lock (Console.Out)
                        {
                            Console.SetCursorPosition(s * SectionLength, SectionMarkRow);
                            Console.Write('-');
                        }

                        // Release previous section
                        if (currentSection != -1)
                        {
                            semaphores[currentSection].Release();
                            Thread.Sleep(clearanceDelayMs); // section remains blocked briefly
                            lock (Console.Out)
                            {
                                Console.SetCursorPosition(currentSection * SectionLength, SectionMarkRow);
                                Console.Write('/');
                            }
                        }

                        currentSection = s;
                    }
                }

                // Draw train at current position
                lock (Console.Out)
                {
                    if (pos > 0)
                    {
                        Console.SetCursorPosition(pos - 1, Row);
                        Console.Write(Rail);
                    }
                    Console.SetCursorPosition(pos, Row);
                    Console.Write(Train);
                }

                Thread.Sleep(300); // train speed
                pos++;
            }

            // Release last section
            if (currentSection != -1)
            {
                semaphores[currentSection].Release();
                Thread.Sleep(clearanceDelayMs);
                lock (Console.Out)
                {
                    Console.SetCursorPosition(currentSection * SectionLength, SectionMarkRow);
                    Console.Write('/');
                }
            }

            // Restore rail at end
            lock (Console.Out)
            {
                Console.SetCursorPosition(trackEnd, Row);
                for (int i = 0; i < Train.Length; i++)
                    Console.Write(Rail);
            }
        }
    }
}
