using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Matches.Infrastructure.Configuration.Integration.Matches
{
    public class MatchesResponse
    {
        [JsonProperty("events")] 
        public List<MatchResponse> Matches { get; set; }
    }

    public class MatchResponse
    {
        public string idEvent { get; set; }
        public string idSoccerXML { get; set; }
        public string idAPIfootball { get; set; }
        public string strEvent { get; set; }
        public string strEventAlternate { get; set; }
        public string strFilename { get; set; }
        public string strSport { get; set; }
        public string idLeague { get; set; }
        public string strLeague { get; set; }
        public string strSeason { get; set; }
        public string strDescriptionEN { get; set; }
        public string strHomeTeam { get; set; }
        public string strAwayTeam { get; set; }
        public string intHomeScore { get; set; }
        public string intRound { get; set; }
        public string intAwayScore { get; set; }
        public string intSpectators { get; set; }
        public string strHomeGoalDetails { get; set; }
        public string strHomeRedCards { get; set; }
        public string strHomeYellowCards { get; set; }
        public string strHomeLineupGoalkeeper { get; set; }
        public string strHomeLineupDefense { get; set; }
        public string strHomeLineupMidfield { get; set; }
        public string strHomeLineupForward { get; set; }
        public string strHomeLineupSubstitutes { get; set; }
        public string strHomeFormation { get; set; }
        public string strAwayRedCards { get; set; }
        public string strAwayYellowCards { get; set; }
        public string strAwayGoalDetails { get; set; }
        public string strAwayLineupGoalkeeper { get; set; }
        public string strAwayLineupDefense { get; set; }
        public string strAwayLineupMidfield { get; set; }
        public string strAwayLineupForward { get; set; }
        public string strAwayLineupSubstitutes { get; set; }
        public string strAwayFormation { get; set; }
        public string intHomeShots { get; set; }
        public string intAwayShots { get; set; }
        public string dateEvent { get; set; }
        public string dateEventLocal { get; set; }
        public string strDate { get; set; }
        public string strTime { get; set; }
        public string strTimeLocal { get; set; }
        public string strTVStation { get; set; }
        public string idHomeTeam { get; set; }
        public string idAwayTeam { get; set; }
        public string strResult { get; set; }
        public string strCircuit { get; set; }
        public string strCountry { get; set; }
        public string strCity { get; set; }
        public string strPoster { get; set; }
        public string strFanart { get; set; }
        public string strThumb { get; set; }
        public string strBanner { get; set; }
        public string strMap { get; set; }
        public string strTweet1 { get; set; }
        public string strTweet2 { get; set; }
        public string strTweet3 { get; set; }
        public string strVideo { get; set; }
        public string strPostponed { get; set; }
        public string strLocked { get; set; }
    }
}
