using System;
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
		}

		private static void HandleSuccess( Options o )
		{
			var ps = new PubgSpider( o );

			Console.WriteLine( $"Player\tRank\tKills" );

			foreach ( var playerResult in ps.GetResult() )
			{
				Console.WriteLine( $"{playerResult.Name}\t{playerResult.Rank}\t{playerResult.Kills}" );
			}

			if ( ps.WasRegionReset )
			{
				string warning = $"Defaulting platform and region reset to {o.Platform}{o.Region}.";
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