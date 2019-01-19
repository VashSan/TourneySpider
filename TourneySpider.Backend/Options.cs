using System;
using CommandLine;

namespace TourneySpider.Backend
{
	public class Options
	{
		[Option( 'm', "matchid", HelpText = "The match id to analyse.", Required = true )]
		public string MatchId { get; set; }

		[Option( 'p', "platform", HelpText = "Specify the platform the match took place on.", Default = "Steam" )]
		public string Platform { get; set; }

		[Obsolete("Not required anymore, might be removed")]
		[Option( 'r', "region", HelpText = "Specify the region the match took place in.", Default = "Europe" )]
		public string Region { get; set; }
	}
}