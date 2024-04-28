using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MLB_Ranking
{

    public class ScoreManagerService : IScoreManagerService
    {
        public List<Team> ReadAndCalculateScores(string filePath)
        {
            List<Team> teams = new List<Team>();

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        UpdateScores(line, teams);
                    }
                }
            }
            else
            {        
                Console.WriteLine("File not found: " + filePath);
                throw new FileNotFoundException("File not found", nameof(filePath));
            }

            return teams;
        }

        private void UpdateScores(string line, List<Team> teams)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 2)
            {
                UpdateTeamScore(parts[0].Trim(), parts[1].Trim(), teams);
            }
            else
            {
                Console.WriteLine("Invalid line format: " + line);
                throw new ArgumentException("Invalid line format", nameof(line));
            }
        }

        private void UpdateTeamScore(string team1Info, string team2Info, List<Team> teams)
        {
            var (name1, score1) = ParseTeamInfo(team1Info);
            var (name2, score2) = ParseTeamInfo(team2Info);

            Team t1 = FindOrCreateTeam(name1, teams);
            Team t2 = FindOrCreateTeam(name2, teams);

            if (score1 > score2)
            {
                t1.AddPoints(MatchPoints.WIN);
                t2.AddPoints(MatchPoints.LOSE);
            }
            else if (score2 > score1)
            {
                t1.AddPoints(MatchPoints.LOSE);
                t2.AddPoints(MatchPoints.WIN);
            }
            else
            {
                t1.AddPoints(MatchPoints.DRAW);
                t2.AddPoints(MatchPoints.DRAW);
            }
        }

        private (string Name, int Score) ParseTeamInfo(string teamInfo)
        {
            string[] parts = teamInfo.Split(' ');
            int score = int.Parse(parts[^1]);
            string name = string.Join(" ", parts, 0, parts.Length - 1);
            return (name, score);
        }

        private Team FindOrCreateTeam(string name, List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == name);
            if (team == null)
            {
                team = new Team(name, 0);
                teams.Add(team);
            }
            return team;
        }
    }

}