﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Pubg.Net;
using Pubg.Net.Exceptions;

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
			PubgPlatform platform = GetPubgPlatform();

			PubgMatch match = GetMatch( platform );
			if ( match == null )
			{
				yield break;
			}

			foreach ( PlayerResult playerResult in GetPlayerResultFromMatch( match ) )
			{
				yield return playerResult;
			}
		}

		private static IEnumerable<PlayerResult> GetPlayerResultFromMatch( PubgMatch match )
		{
			foreach ( PubgRoster roster in match.Rosters )
			{
				foreach ( PubgParticipant participant in roster.Participants )
				{
					var player = new PlayerResult( participant.Stats.Name, roster.Stats.Rank, participant.Stats.Kills );
					yield return player;
				}
			}
		}

		private PubgMatch GetMatch( PubgPlatform platform )
		{
			var matchService = new PubgMatchService();
			PubgMatch match;
			try
			{
				match = matchService.GetMatch( platform, myOptions.MatchId );
			}
			catch ( PubgNotFoundException e ) when ( e.HttpStatusCode == HttpStatusCode.NotFound )
			{
				// Errorcode not set, we wont parse strings though!
				Console.WriteLine( $"Can not find match. (Id = {myOptions.MatchId})" );
				return null;
			}
			catch ( Exception e )
			{
				Console.WriteLine( $"Can not process match. (Id = {myOptions.MatchId})" );
				Console.WriteLine( e.Message );
				return null;
			}

			return match;
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

		private PubgPlatform GetPubgPlatform()
		{
			string s = myOptions.Platform;
			if ( Enum.TryParse( s, true, out PubgPlatform platform ) )
			{
				return platform;
			}

			WasPlatformReset = true;
			return PubgPlatform.Steam;
		}

		public bool WasPlatformReset { get; private set; }
	}
}