using System;

namespace TourneySpider.Backend
{
	public interface IOptions
	{
		string MatchId { get; }
		string Platform { get; }
		[Obsolete("probably not necessary anymore")]
		string Region { get; }
	}
}