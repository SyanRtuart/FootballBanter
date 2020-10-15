using Matches.Domain.Team;

namespace Matches.IntegrationTests.Teams
{
    public struct TeamSampleData
    {
        public static string Country => "Country";

        public static string Description => "Description";

        public static string ExternalId => "ExternalId";

        public static string Facebook => "Facebook";

        public static int FormedYear => 1990;

        public static string Instagram => "Instagram";

        public static string League => "League";

        public static byte[] Logo => new byte[1];

        public static string Manager => "Manager";

        public static string Name => "Name";

        public static Stadium Stadium => Stadium.CreateNew("Name", "Description", "Location");
    }

    public struct EditTeamSampleData
    {
        public static string NewCountry => "NewCountry";

        public static string NewDescription => "NewDescription";

        public static string NewExternalId => "NewExternalId";

        public static string NewFacebook => "NewFacebook";

        public static int NewFormedYear => 2020;

        public static string NewInstagram => "NewInstagram";

        public static string NewLeague => "NewLeague";

        public static byte[] NewLogo => new byte[10];

        public static string NewManager => "NewManager";

        public static string NewName => "NewName";

        public static Stadium NewStadium => Stadium.CreateNew("NewName", "NewDescription", "NewLocation");
    }
}
