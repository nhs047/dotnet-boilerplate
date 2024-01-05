using System.ComponentModel.DataAnnotations.Schema;

namespace Localization.Test.Infrastructure.Models
{
    public class Role : EntityBase
    {
        public int UserId { get; set; }
        public int Feature { get; set; }
        public bool ViewAccess { get; set; }
        public bool CreateAccess { get; set; }
        public bool EditAccess { get; set; }
        public bool DeleteAccess { get; set; }

        public virtual User? User { get; set; }
    }
}
