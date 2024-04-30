using NUnit.Framework;
using NUnit.Framework.Legacy;
using Moq;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace MLB_Ranking.Tests
{
    [TestFixture]
    public class RankingServiceTests
    {
        private string testScoreFilePath = TestConstants.TestFilePath;

        [SetUp]
        public void Setup()
        {
            ConfigurationManager.AppSettings[RankingService.ScoreFilePath] = testScoreFilePath;
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(testScoreFilePath))
            {
                File.Delete(testScoreFilePath);
            }
        }

        [Test]
        public void PrintMLBRanking_TeamsHaveUniqueScoresAndAreSorted()
        {
            List<string> scores = new List<string>();
            scores.Add("Team1 1, Team2 0");
            scores.Add("Team2 5, Team3 5");
            scores.Add("Team3 1, Team1 0");
            writeFile(scores);

            var scoreManagerService = new ScoreManagerService();
            var rankingService = new RankingService(scoreManagerService);

            List<string> result = rankingService.PrintMLBRanking();

            ClassicAssert.IsTrue(result.Count > 0);
            ClassicAssert.IsTrue(result[0].StartsWith("1"));
            ClassicAssert.IsTrue(result[0].Contains(TestConstants.Team3));
            ClassicAssert.IsTrue(result[1].Contains(TestConstants.Team1));
            ClassicAssert.IsTrue(result[2].Contains(TestConstants.Team2));
        }

        [Test]
        public void PrintMLBRanking_AllTeamsAreDrawAndSortedAlphabeticall()
        {
            List<string> scores = new List<string>();
            scores.Add("Team1 1, Team2 1");
            scores.Add("Team2 5, Team3 5");
            scores.Add("Team3 0, Team1 0");
            writeFile(scores);

            var scoreManagerService = new ScoreManagerService();
            var rankingService = new RankingService(scoreManagerService);

            List<string> result = rankingService.PrintMLBRanking();

            ClassicAssert.IsTrue(result.Count > 0);
            ClassicAssert.IsTrue(result[0].StartsWith("1"));
            ClassicAssert.IsTrue(result[0].Contains(TestConstants.Team1));
            ClassicAssert.IsTrue(result[1].Contains(TestConstants.Team2));
            ClassicAssert.IsTrue(result[2].Contains(TestConstants.Team3));
        }

        [Test]
        public void PrintMLBRanking_UnformattedInputThrowsArgumentException()
        {
            List<string> scores = new List<string>();
            scores.Add("Team2 5, Team3 5");
            scores.Add("Team1 10");
            writeFile(scores);

            var scoreManagerService = new ScoreManagerService();
            var rankingService = new RankingService(scoreManagerService);

            Assert.Throws<System.ArgumentException>(() => rankingService.PrintMLBRanking());
        }

        [Test]
        public void PrintMLBRanking_EmptyScoresResultsZero()
        {
            List<string> scores = new List<string>();
            writeFile(scores);

            var scoreManagerService = new ScoreManagerService();
            var rankingService = new RankingService(scoreManagerService);

            List<string> result = rankingService.PrintMLBRanking();

            ClassicAssert.IsTrue(result.Count == 0);
        }

        private void writeFile(List<string> lines)
        {
            using (StreamWriter writer = new StreamWriter(testScoreFilePath))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }


    }
}
