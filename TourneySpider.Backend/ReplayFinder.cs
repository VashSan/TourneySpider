using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourneySpider.Backend
{
	public class ReplayFinder
	{
		private const string DefaultPath = @"%localappdata%\TslGame\Saved\Demos";

		public ReplayFinder()
		{
			SourcePath = Environment.ExpandEnvironmentVariables( DefaultPath );
		}

		public string SourcePath { get; set; }

		public IList<Replay> GetReplays()
		{
			var result = new List<Replay>();

			foreach ( var path in Directory.EnumerateDirectories( SourcePath ) )
			{
				string folderName = Path.GetFileName( path );

				if ( folderName != null && folderName.StartsWith( "match.bro" ) )
				{
					var newReplay = Replay.Parse( folderName );
					result.Add( newReplay );
				}
			}

			return result;
		}

	}
}
