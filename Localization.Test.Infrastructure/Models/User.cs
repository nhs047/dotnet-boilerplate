using Localization.Test.Common.Constants;

namespace Localization.Test.Infrastructure.Models
{
    public class User: EntityBase
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public DateTime? Birthdate { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? MobileNumber { get; set; }
        public string Culture { get; set; } = Keys.DefaultCulture;

        public string FullName
        {
            get 
            {
                var fullname = $"{Firstname} {Lastname}".Trim();
                return !string.IsNullOrEmpty(fullname)? fullname: "-"; 
            }
        }

        public virtual Account? Account { get; set; }
        public virtual ICollection<Account>? AccountCreators { get; set; }
        public virtual ICollection<Account>? AccountUpdators { get; set; }

        public virtual ICollection<Role>? Roles { get; set; }
        public virtual ICollection<Role>? RoleCreators { get; set; }
        public virtual ICollection<Role>? RoleUpdators { get; set; }

        public virtual ICollection<Template>? TemlateCreators { get; set; }
        public virtual ICollection<Template>? TemplateUpdators { get; set; }

        public virtual ICollection<User>? UserCreators { get; set; }
        public virtual ICollection<User>? UserUpdators { get; set; }


    }
}
