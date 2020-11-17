using System;
using System.Collections.Generic;

namespace retroAPI.Models.DbModels
{
    public partial class Jobs
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public string ByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Type { get; set; }
        public int? IsDeleted { get; set; }

        public virtual Boards Board { get; set; }
        public virtual JobTypes TypeNavigation { get; set; }
    }
}
