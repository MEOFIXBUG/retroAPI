using System;
using System.Collections.Generic;

namespace retroAPI.Models.DbModels
{
    public partial class JobTypes
    {
        public JobTypes()
        {
            Jobs = new HashSet<Jobs>();
        }

        public int Id { get; set; }
        public string ByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IsDeleted { get; set; }

        public virtual ICollection<Jobs> Jobs { get; set; }
    }
}
