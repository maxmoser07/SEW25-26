using System;
using System.Threading;

namespace TrainSim
{
    public class Railway
    {
        private readonly Semaphore[] semaphores;
        private readonly char Rail;
        public char TrainSymbol { get; set; }
        private readonly int TrackLength;
        private readonly int SectionCount;
        private readonly int SectionLength;

        public event EventHandler<TrainMovedEventArgs>? TrainMoved;
        public event EventHandler<SectionEventArgs>? SectionEntered;
        public event EventHandler<SectionEventArgs>? SectionExited;
        public event EventHandler<TrackBuiltEventArgs>? TrackBuilt;

        public Railway(char trainSymbol = '>', int trackLength = 80)
        {
            Rail = '#';
            TrainSymbol = trainSymbol;
            TrackLength = trackLength;

            SectionLength = 6;
            SectionCount = (TrackLength + SectionLength - 1) / SectionLength;

            semaphores = new Semaphore[SectionCount];
            for (int i = 0; i < SectionCount; i++)
                semaphores[i] = new Semaphore(1, 1);
        }

        public void BuildTrack()
        {
            TrackBuilt?.Invoke(this, new TrackBuiltEventArgs(SectionCount, SectionLength, TrackLength));
        }

        public void RunTrain()
        {
            RunTrain(500);
        }

        private void RunTrain(int clearanceDelayMs)
        {
            int trackEnd = TrackLength - 1;
            int currentSection = -1;

            for (int pos = 0; pos <= trackEnd;)
            {
                int nextSection = pos / SectionLength;

                if (nextSection != currentSection)
                {
                    semaphores[nextSection].WaitOne();
                    SectionEntered?.Invoke(this, new SectionEventArgs(nextSection));

                    if (currentSection != -1)
                    {
                        semaphores[currentSection].Release();
                        Thread.Sleep(clearanceDelayMs);
                        SectionExited?.Invoke(this, new SectionEventArgs(currentSection));
                    }

                    currentSection = nextSection;
                }

                TrainMoved?.Invoke(this, new TrainMovedEventArgs(TrainSymbol.ToString(), pos));
                Thread.Sleep(200);
                pos++;
            }

            if (currentSection != -1)
            {
                semaphores[currentSection].Release();
                Thread.Sleep(clearanceDelayMs);
                SectionExited?.Invoke(this, new SectionEventArgs(currentSection));
            }
        }
    }
}
