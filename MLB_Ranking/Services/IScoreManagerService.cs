
namespace MLB_Ranking
{
    public interface IScoreManagerService
    {
        public List<Team> ReadAndCalculateScores(string filePath);
    }
}
