using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retroAPI.Modules.Boards.Domain
{
    public class BoardReq
    {
        //public int Id { get; set; }
        //public int UserId { get; set; }
        public string ByName { get; set; }
        //public DateTime CreatedAt { get; set; }
        public int IsDeleted { get; set; }
        public string SharedLists { get; set; }
        public int IsPublished { get; set; }
    }
}
