using Localization.Test.Common.Constants;

namespace Localization.Test.Infrastructure.Models
{
    public class Template : EntityBase
    {
        public int Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Culture { get; set; } = Keys.DefaultCulture;
        public string TemplateHeader { get; set; } = string.Empty;
        public string TemplateBody { get; set; } = string.Empty;
    }
}