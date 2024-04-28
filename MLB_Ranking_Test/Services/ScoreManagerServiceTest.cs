using NUnit.Framework;
using NUnit.Framework.Legacy;
using Moq;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace MLB_Ranking.Tests
{
    [TestFixture]
    public class ScoreManagerServiceTests
    {
        private string testScoreFilePath = TestConstants.TestFilePath;

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(testScoreFilePath))
            {
                File.Delete(testScoreFilePath);
            }
        }

        [Test]
        public void ReadAndCalculateScores_ScoresAreCalculatedWell()
        {
            List<string> scores = new List<string>();
            scores.Add("Team1 1, Team2 0");
            scores.Add("Team2 5, Team3 5");
            scores.Add("Team3 1, Team1 0");
            writeFile(scores);

            var scoreManagerService = new ScoreManagerService();

            List<Team> result = scoreManagerService.ReadAndCalculateScores(testScoreFilePath);

            var Team1 = result.FirstOrDefault(t => t.Name == TestConstants.Team1);
            var Team2 = result.FirstOrDefault(t => t.Name == TestConstants.Team2);
            var Team3 = result.FirstOrDefault(t => t.Name == TestConstants.Team3);
            var Team4 = result.FirstOrDefault(t => t.Name == TestConstants.Team4);

            ClassicAssert.NotNull(Team1);
            ClassicAssert.AreEqual(Team1.Points, (int)MatchPoints.WIN);

            ClassicAssert.NotNull(Team2);
            ClassicAssert.AreEqual(Team2.Points, (int)MatchPoints.DRAW);

            ClassicAssert.NotNull(Team3);
            ClassicAssert.AreEqual(Team3.Points, (int)MatchPoints.DRAW + (int)MatchPoints.WIN);

            ClassicAssert.IsNull(Team4);
        }

        [Test]
        public void ReadAndCalculateScores_AllTeamsAreDraw()
        {
            List<string> scores = new List<string>();
            scores.Add("Team1 1, Team2 1");
            scores.Add("Team2 5, Team3 5");
            scores.Add("Team3 0, Team1 0");
            writeFile(scores);

            var scoreManagerService = new ScoreManagerService();

            List<Team> result = scoreManagerService.ReadAndCalculateScores(testScoreFilePath);

            var Team1 = result.FirstOrDefault(t => t.Name == TestConstants.Team1);
            var Team2 = result.FirstOrDefault(t => t.Name == TestConstants.Team2);
            var Team3 = result.FirstOrDefault(t => t.Name == TestConstants.Team3);

            ClassicAssert.NotNull(Team1);
            ClassicAssert.AreEqual(Team1.Points, (int)MatchPoints.DRAW + (int)MatchPoints.DRAW);

            ClassicAssert.NotNull(Team2);
            ClassicAssert.AreEqual(Team2.Points, (int)MatchPoints.DRAW + (int)MatchPoints.DRAW);

            ClassicAssert.NotNull(Team3);
            ClassicAssert.AreEqual(Team3.Points, (int)MatchPoints.DRAW + (int)MatchPoints.DRAW);
        }

        [Test]
        public void ReadAndCalculateScores_UnformattedInputThrowsArgumentException()
        {
            List<string> scores = new List<string>();
            scores.Add("Team2 5, Team3 5");
            scores.Add("1 Team1");
            writeFile(scores);

            var scoreManagerService = new ScoreManagerService();

            Assert.Throws<System.ArgumentException>(() => scoreManagerService.ReadAndCalculateScores(testScoreFilePath));
        }

        [Test]
        public void ReadAndCalculateScores_NotFileFoundException()
        {
            List<string> scores = new List<string>();
            scores.Add("Team2 1, Team3 1");
            scores.Add("Team1 10");
            writeFile(scores);

            var scoreManagerService = new ScoreManagerService();

            Assert.Throws<System.IO.FileNotFoundException>(() => scoreManagerService.ReadAndCalculateScores(testScoreFilePath + "_"));
        }

        [Test]
        public void ReadAndCalculateScores_EmptyScoresResultsZero()
        {
            List<string> scores = new List<string>();
            writeFile(scores);

            var scoreManagerService = new ScoreManagerService();

            var result = scoreManagerService.ReadAndCalculateScores(testScoreFilePath); ;

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
