using System;
using System.Collections.Generic;
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

		public bool WasRegionReset { get; private set; }

		public IEnumerable<PlayerResult> GetResult()
		{
			PubgRegion region = GetPubgRegion();

			var matchService = new PubgMatchService();
			PubgMatch match = matchService.GetMatch( region, myOptions.MatchId );

			
			var result = new List<PlayerResult>();
			foreach ( PubgRoster roster in match.Rosters )
			{
				foreach ( PubgParticipant participant in roster.Participants )
				{
					var player = new PlayerResult( participant.Stats.Name, roster.Stats.Rank, participant.Stats.Kills);
					result.Add( player );
				}
			}

			return result;
		}

		private PubgRegion GetPubgRegion()
		{
			string s = myOptions.Platform + myOptions.Region;
			if ( Enum.TryParse( s, true, out PubgRegion region ) )
			{
				return region;
			}

			PubgRegion defaultRegion = PubgRegion.PCEurope;
			myOptions.Region = "Europe";
			myOptions.Platform = "PC";

			WasRegionReset = true;
			return defaultRegion;
		}
	}
}
