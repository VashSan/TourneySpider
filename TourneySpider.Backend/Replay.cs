using System;

namespace TourneySpider.Backend
{
	public class Replay
	{
		public static Replay Parse( string directory )
		{
			string[] parts = directory.Split( new[] {'.'} );

			string type = parts[ 2 ]; // "official", "custom", ... ?

			string version = parts[ 3 ]; // "2018-05"

			string client = parts[ 4 ];
			string gameMode = parts[ 5 ]; // "solo", "solo-fpp"
			string region = parts[ 6 ]; // "eu", ...

			int year = Convert.ToInt32( parts[ 7 ] ); // TODO what if starting with 4 instead 2?
			int month = Convert.ToInt32( parts[ 8 ] );
			int day = Convert.ToInt32( parts[ 9 ] ); 
			var date = new DateTime(year, month, day);

			string id = parts[ 11 ].Substring( 0, 36 );
			var guid = Guid.Parse( id );

			return new Replay()
			{
				FolderName = directory,
				Client = client,
				Type = type,
				Version = version,
				Region = region,
				Mode = gameMode,
				Date = date,
				Id = guid

			};
		}

		public string Client { get; set; }

		public string FolderName { get; set; }

		public string Region { get; set; }

		public string Mode { get; set; }

		public Guid Id { get; set; }

		public DateTime Date { get; set; }

		public string Version { get; set; }

		public string Type { get; set; }
	}
}