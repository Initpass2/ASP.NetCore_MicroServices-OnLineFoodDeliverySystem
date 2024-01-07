using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.CustomerManagement.Auth
{
    public class FacebookUserData
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Locale { get; set; }
        public FacebookPictureData Picture { get; set; }
    }

    public class FacebookPictureData
    {
        public FacebookPicture Data { get; set; }
    }

    public class FacebookPicture
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }

}
