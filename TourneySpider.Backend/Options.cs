using System;

namespace TourneySpider.Backend
{
	// TODO should be renamed maybe to clarify what this is used for
	public interface IOptions
	{
		string MatchId { get; }
		string Platform { get; }
		[Obsolete("probably not necessary anymore")]
		string Region { get; }
	}
}