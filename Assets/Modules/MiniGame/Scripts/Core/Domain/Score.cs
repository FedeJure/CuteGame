namespace Modules.MiniGame.Scripts.Core.Domain
{
    public struct Score
    {
        public float totalScore;
        public float newScore;
        public Score(float totalScore, float newScore)
        {
            this.totalScore = totalScore;
            this.newScore = newScore;
        }
    }
}