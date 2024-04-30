using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace MLB_Ranking.Tests
{
    [TestFixture]
    public class TeamTests
    {
        [Test]
        public void Constructor_InitializesNameAndPoints()
        {
            string name = TestConstants.Team1;
            int points = 10;

            var team = new Team(name, points);

            ClassicAssert.AreEqual(name, team.Name);
            ClassicAssert.AreEqual(points, team.Points);
        }

        [Test]
        public void AddPoints_IncreasesPointsByGivenValue()
        {
            var team = new Team(TestConstants.Team1, 10);

            team.AddPoints(MatchPoints.WIN);

            ClassicAssert.AreEqual(13, team.Points);
        }

        [Test]
        public void CompareTo_ReturnsPositiveWhenCurrentTeamHasMorePoints()
        {
            var team1 = new Team(TestConstants.Team1, 10);
            var team2 = new Team(TestConstants.Team2, 5);

            int result = team1.CompareTo(team2);

            ClassicAssert.IsTrue(result < 0);
        }

        [Test]
        public void ToString_ReturnsCorrectFormat()
        {
            var team = new Team(TestConstants.Team1, 10);

            string result = team.ToString();
            
            ClassicAssert.IsTrue("Team1 - 10 pts".Equals(result));
        }
    }
}
