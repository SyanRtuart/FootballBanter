using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phrases.IntegrationTests.Phrases
{
    public struct PhraseSampleData
    {
        public static Guid TeamId => Guid.Parse("bcf75b98-cbef-4bd8-a574-d4404dd5c6a7");

        public static Guid MatchId => Guid.Parse("c9eb48dd-07b1-4bd0-8806-23b81fc48755");

        public static Guid CreatedByUserId => Guid.Parse("58cfef9b-f0a8-4404-9738-052dd0e6a74c");

        public static string Description => "description";

        public static bool Positive => true;
    }
}
