using System.ComponentModel.DataAnnotations.Schema;

namespace Localization.Test.Infrastructure.Models
{
    public class Account: EntityBase
    {
        public int UserId { get; set; }
        public string? Password { get; set; }

        public virtual User? User { get; set; }
    }
}
