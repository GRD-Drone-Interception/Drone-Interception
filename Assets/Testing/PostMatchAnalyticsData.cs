using System;
using Utility;

namespace Testing
{
    [Serializable]
    public class PostMatchAnalyticsData : SaveData
    {
        public float interceptionSuccessRate;
        public float interceptionAccuracy;
        public float averageInterceptionTime;
        public float droneSurvivalRate;
        public float averageDistanceTravelled;
        public int numOfUniqueStrategiesUsed;
        public int matchLengthSeconds;
        public int numDronesDeployed;
        public int numDronesDestroyed;
        public int numObjectivesCompleted;
        // favourite strategy deployed?
    }
}