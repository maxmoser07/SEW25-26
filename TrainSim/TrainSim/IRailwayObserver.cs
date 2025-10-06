namespace TrainSim
{
    public interface IRailwayObserver
    {
        void OnTrainMoved(string train, int position);
        void OnSectionEntered(int sectionIndex);
        void OnSectionExited(int sectionIndex);
        void OnTrackBuilt(int sectionCount, int sectionLength, int trackLength);
    }
}