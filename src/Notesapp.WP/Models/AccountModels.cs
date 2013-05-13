using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notesapp.WP.Models
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string AuthToken { get; set; }
    }
}
