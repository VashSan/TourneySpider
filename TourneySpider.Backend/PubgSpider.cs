using System;
using Pubg.Net;

namespace TourneySpider.Backend
{
	public class PubgSpider
	{
		private readonly Options myOptions;

		public PubgSpider( Options options )
		{
			myOptions = options;
		}

		public void FetchAndWriteResult()
		{
			PubgRegion region = GetPubgRegion();

			var matchService = new PubgMatchService();
			PubgMatch match = matchService.GetMatch( region, myOptions.MatchId );

			Console.WriteLine( $"Player\tRank\tKills" );

			foreach ( PubgRoster roster in match.Rosters )
			{
				foreach ( PubgParticipant participant in roster.Participants )
				{
					Console.WriteLine( $"{participant.Stats.Name}\t{roster.Stats.Rank}\t{participant.Stats.Kills}" );
				}
			}
		}

		private PubgRegion GetPubgRegion()
		{
			string s = myOptions.Platform + myOptions.Region;
			if ( Enum.TryParse( s, true, out PubgRegion region ) )
			{
				return region;
			}

			PubgRegion defaultRegion = PubgRegion.PCEurope;
			string warning = $"Defaulting platform and region to {defaultRegion}.";
			ConsoleWriteWarning( warning );

			return defaultRegion;
		}

		private static void ConsoleWriteWarning( string warning )
		{
			var color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Error.WriteLine( warning );
			Console.ForegroundColor = color;
		}
	}
}
