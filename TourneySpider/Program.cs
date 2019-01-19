using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using TourneySpider.Backend;

namespace TourneySpider
{
	internal static class Program
	{
		static void Main( string[] args )
		{
			var parser = new Parser( p => p.HelpWriter = Console.Out );
			var result = parser.ParseArguments<Options>( args );
			result.WithParsed( HandleSuccess );
			result.WithNotParsed( HandleInvalid );
		}

		private static void HandleInvalid( IEnumerable<Error> obj )
		{
			var finder = new ReplayFinder();
			var replays = finder.GetReplays();

			var lastReplay = replays.LastOrDefault();
			if ( lastReplay == null )
			{
				return;
			}

			ConsoleWriteWarning( "I use the last replay found in local app data" );
			var options = new Options()
			{
				MatchId = lastReplay.Id.ToString(),
				Platform = lastReplay.Platform
			};
			HandleSuccess( options );
		}

		private static void HandleSuccess( Options o )
		{
			var ps = new PubgSpider( o );

			Console.WriteLine( $"Player\tRank\tKills" );

			foreach ( var playerResult in ps.GetResult() )
			{
				Console.WriteLine( $"{playerResult.Name}\t{playerResult.Rank}\t{playerResult.Kills}" );
			}
			
			if ( ps.WasPlatformReset )
			{
				string warning = $"Defaulting platform reset to {o.Platform}.";
				ConsoleWriteWarning( warning );
			}
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