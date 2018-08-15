namespace TourneySpider.Backend
{
	public class PlayerResult
	{
		public PlayerResult( string name, int rank, int kills )
		{
			Name = name;
			Rank = rank;
			Kills = kills;
		}

		public string Name { get; }
		public int Rank { get; }
		public int Kills { get; }
	}
}
