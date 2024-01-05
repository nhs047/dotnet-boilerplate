using Localization.Test.Common.Constants;
using Localization.Test.Common.Resources;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Localization.Test.Service
{
    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;

        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName ?? string.Empty);
            _localizer = factory.Create(Keys.SharedResourcePath, assemblyName.Name ?? string.Empty);
        }

        public LocalizedString GetString(string key)
        {
            return _localizer.GetString(key);
        }
    }
}
