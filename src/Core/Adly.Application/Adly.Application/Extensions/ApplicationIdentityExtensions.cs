using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace Adly.Application.Extensions
{
    public static class ApplicationIdentityExtensions
    {
        public static List<KeyValuePair<string, string>> ConvertToToKeyValuePair([NotNull] this IEnumerable<IdentityError> errors)
        {
            return errors.Select(c => new KeyValuePair<string, string>("GeneralError", c.Description)).ToList();
        }
    }
}
