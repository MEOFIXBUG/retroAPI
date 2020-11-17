using System;
using System.Collections.Generic;

namespace retroAPI.Models.DbModels
{
    public partial class Boards
    {
        public Boards()
        {
            Jobs = new HashSet<Jobs>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string ByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IsDeleted { get; set; }
        public string SharedLists { get; set; }
        public int IsPublished { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Jobs> Jobs { get; set; }
    }
}
