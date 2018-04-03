using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BackendWatsonApi.Models
{
    public class User : IdentityUser
    {        
        public virtual ICollection<UserPost> UserPosts { get; set; }                  
    }
}
