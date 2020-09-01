using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace UserAccess.Domain.Users
{
    public class Scores : ValueObject
    {
        public static Scores CreateNew(int banter, int comment)
        {
            return new Scores(banter, comment);
        }

        private Scores(int banter, int comment)
        {
            Banter = banter;
            Comment = comment;
        }

        public int Banter { get; }

        public int Comment { get; }

    }
}
