using System;
using Matches.Domain.Match;

namespace Matches.IntegrationTests.Matches
{
    public struct MatchSampleData
    {
        public static Guid AwayTeamId => Guid.Parse("e822448e-e7c6-43e4-b36e-e0df6c8403b8");

        public static Guid HomeTeamId => Guid.Parse("b76924d6-bad4-4202-81c3-ac3112ba1911");
        
        public static string ExternalId => "cd7d6fd1-aacf-49c8-8be5-38b2af1b11aa";

        public static string Name => "Name";

        public static Score Score => Score.CreateNew("winner", 1, 2);

        public static string Season => "Season";

        public static string Status => "Status";

        public static DateTime UtcDate => DateTime.Parse("01/01/2020");
    }

    public struct EditMatchSampleData
    {

        public static string NewName => "NewName";

        public static Score NewScore => Score.CreateNew("Newwinner", 3, 3);

        public static string NewSeason => "NewSeason";

        public static string NewStatus => "NewStatus";

        public static DateTime NewUtcDate => DateTime.Parse("01/01/2021");
    }
}
