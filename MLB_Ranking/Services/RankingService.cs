using System.Configuration;

namespace MLB_Ranking
{

    public class RankingService : IRankingService
    {

        private readonly IScoreManagerService _scoreManagerService;

        public RankingService(IScoreManagerService scoreManagerService)
        {
            _scoreManagerService = scoreManagerService;
        }

        public static string ScoreFilePath = "scoreFilePath";

        public List<string> PrintMLBRanking()
        {
            string scoreFilePath = ConfigurationManager.AppSettings[ScoreFilePath];

            List<Team> teams = _scoreManagerService.ReadAndCalculateScores(scoreFilePath);
            List<string> rankingStrings = GetSortedRankingStrings(teams);
            foreach (var ranking in rankingStrings)
            {
                Console.WriteLine(ranking);
            }
            return rankingStrings;
        }

        private List<string> GetSortedRankingStrings(List<Team> teams)
        {
            teams.Sort();
            List<string> rankings = new List<string>();
            int currentRank = 1;
            int displayedRank = 1;
            for (int i = 0; i < teams.Count; i++)
            {
                if (i > 0 && teams[i].Points == teams[i - 1].Points)
                {
                    rankings.Add($"{displayedRank}. {teams[i]}");
                }
                else
                {
                    if (i > 0) currentRank = i + 1;
                    displayedRank = currentRank;
                    rankings.Add($"{displayedRank}. {teams[i]}");
                }
            }
            return rankings;
        }
    }


}