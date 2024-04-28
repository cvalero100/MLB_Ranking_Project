namespace MLB_Ranking
{
    public class Team : IComparable<Team>
    {
        public string Name { get; set; }
        public int Points { get; set; }

        public Team(string name, int points)
        {
            Name = name;
            Points = points;
        }

        public void AddPoints(MatchPoints pointsToAdd)
        {
            Points += (int)pointsToAdd;
        }

        public int CompareTo(Team other)
        {
            int result = other.Points.CompareTo(this.Points);
            return result == 0 ? this.Name.CompareTo(other.Name) : result;
        }

        public override string ToString()
        {
            return $"{Name} - {Points} pts";
        }
    }
}
