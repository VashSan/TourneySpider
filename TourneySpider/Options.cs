using System;
using CommandLine;
using TourneySpider.Backend;

namespace TourneySpider
{
	public class Options : IOptions
	{
		[Option( 'm', "matchid", HelpText = "The match id to analyse.", SetName = "matchid")]
		public string MatchId { get; set; }

		[Option( 'p', "platform", HelpText = "Specify the platform the match took place on.", Default = "Steam", SetName = "matchid" )]
		public string Platform { get; set; }

		[Option( 'l', "list", HelpText = "List the available replays.", Default = false, SetName = "list" )]
		public bool List { get; set; }

		[Obsolete( "Not required anymore, might be removed" )]
		[Option( 'r', "region", HelpText = "Specify the region the match took place in.", Default = "Europe", SetName = "matchid" )]
		public string Region { get; set; }
	}
}
