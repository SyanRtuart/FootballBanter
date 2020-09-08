using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccess.Application.Contracts;

namespace UserAccess.Application.Users.Commands.AddPicture
{
    public class AddPictureCommand : CommandBase
    {
        public byte[] Picture { get; set; }
    }
}
