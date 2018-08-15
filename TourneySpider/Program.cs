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
			var ts = new PubgSpider( o );
			ts.FetchAndWriteResult();
		}
	}
}