using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.HttpAggregator.Models.UserAccess
{
    public class UploadPictureRequest
    {
        public UploadPictureRequest(Guid userId, IFormFile picture)
        {
            UserId = userId;
            Picture = picture;
        }

        public Guid UserId { get; set; }

        public IFormFile Picture { get; set; }
    }
}
