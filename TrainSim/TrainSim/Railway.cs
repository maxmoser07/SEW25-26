using System;
using System.Threading;

namespace TrainSim
{
    public class Railway
    {
        private Semaphore[] semaphores;
        private char Rail;
        public string Train { get; set; }
        private int TrackLength;
        private int SectionLength => TrackLength / semaphores.Length;

        public Railway(string train, int sections = 7, int trackLength = 80)
        {
            Rail = '#';
            Train = train;
            TrackLength = trackLength;

            // Create one semaphore per section
            semaphores = new Semaphore[sections];
            for (int i = 0; i < sections; i++)
                semaphores[i] = new Semaphore(1, 1);
        }

        public void BuildTrack()
        {
            Console.WriteLine("\n");
            for (int i = 1; i < semaphores.Length; i++)
                Console.Write("\t/   ");
            Console.WriteLine();
            for (int i = 1; i < semaphores.Length; i++)
                Console.Write($"\t|({i})");
            Console.WriteLine();
            for (int i = 0; i < TrackLength; i++)
                Console.Write(Rail);
        }

        public void RunTrain()
        {
            int trackEnd = TrackLength - Train.Length;

            for (int pos = 0; pos <= trackEnd; pos++)
            {
                // Determine sections the train will occupy in the next move
                int frontSection = pos / SectionLength;
                int rearSection = (pos + Train.Length - 1) / SectionLength;

                // Acquire all sections the train will occupy
                for (int s = frontSection; s <= rearSection && s < semaphores.Length; s++)
                    semaphores[s].WaitOne();

                // Restore rail behind the train
                if (pos > 0)
                {
                    lock (Console.Out)
                    {
                        Console.SetCursorPosition(pos - 1, 4);
                        Console.Write(Rail);
                    }
                }

                // Draw the train
                lock (Console.Out)
                {
                    Console.SetCursorPosition(pos, 4);
                    Console.Write(Train);
                }

                Thread.Sleep(300);

                // Release sections no longer occupied
                int leaveSection = (pos - 1) / SectionLength;
                if (leaveSection >= 0 && leaveSection < semaphores.Length)
                    semaphores[leaveSection].Release();
            }

            // Release remaining sections at the end
            int finalSection = (trackEnd) / SectionLength;
            if (finalSection >= 0 && finalSection < semaphores.Length)
                semaphores[finalSection].Release();

            // Restore the track where the train ended
            lock (Console.Out)
            {
                Console.SetCursorPosition(trackEnd, 4);
                for (int i = 0; i < Train.Length; i++)
                    Console.Write(Rail);
            }
        }
    }
}
