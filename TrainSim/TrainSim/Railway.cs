using System;
using System.Collections.Generic;
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

        private readonly List<IRailwayObserver> observers = new();

        public Railway(string train, int trackLength = 80)
        {
            Rail = '#';
            Train = train;
            TrackLength = trackLength;

            SectionLength = train.Length * 2; // section ≈ twice train length
            SectionCount = (TrackLength + SectionLength - 1) / SectionLength;

            semaphores = new Semaphore[SectionCount];
            for (int i = 0; i < SectionCount; i++)
                semaphores[i] = new Semaphore(1, 1);
        }

        // Observer management
        public void Attach(IRailwayObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IRailwayObserver observer)
        {
            observers.Remove(observer);
        }

        private void NotifyTrainMoved(int position)
        {
            foreach (var obs in observers)
                obs.OnTrainMoved(Train, position);
        }

        private void NotifySectionEntered(int sectionIndex)
        {
            foreach (var obs in observers)
                obs.OnSectionEntered(sectionIndex);
        }

        private void NotifySectionExited(int sectionIndex)
        {
            foreach (var obs in observers)
                obs.OnSectionExited(sectionIndex);
        }

        private void NotifyTrackBuilt()
        {
            foreach (var obs in observers)
                obs.OnTrackBuilt(SectionCount, SectionLength, TrackLength);
        }

        public void BuildTrack()
        {
            NotifyTrackBuilt();
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
                int front = pos;
                int rear = pos + Train.Length - 1;

                int nextFrontSection = front / SectionLength;
                int nextRearSection = rear / SectionLength;

                for (int s = nextFrontSection; s <= nextRearSection; s++)
                {
                    if (s != currentSection)
                    {
                        semaphores[s].WaitOne();
                        NotifySectionEntered(s);

                        if (currentSection != -1)
                        {
                            semaphores[currentSection].Release();
                            Thread.Sleep(clearanceDelayMs);
                            NotifySectionExited(currentSection);
                        }

                        currentSection = s;
                    }
                }

                NotifyTrainMoved(pos);
                Thread.Sleep(300);
                pos++;
            }

            if (currentSection != -1)
            {
                semaphores[currentSection].Release();
                Thread.Sleep(clearanceDelayMs);
                NotifySectionExited(currentSection);
            }
        }
    }
}
