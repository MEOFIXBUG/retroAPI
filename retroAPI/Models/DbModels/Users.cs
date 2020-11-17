using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace retroAPI.Models.DbModels
{
    public partial class Users
    {
        public Users()
        {
            Boards = new HashSet<Boards>();
        }

        public int Id { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public uint IsDeleted { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }

        public virtual ICollection<Boards> Boards { get; set; }
    }
}
