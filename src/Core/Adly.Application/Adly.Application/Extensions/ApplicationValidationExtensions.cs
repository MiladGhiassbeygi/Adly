using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace Adly.Application.Extensions
{
    public static class ApplicationValidationExtensions
    {
        public static List<KeyValuePair<string, string>> ConvertToKeyValuePair([NotNull] this List<ValidationFailure> failures)
        {

            return failures
                .Select(x => new KeyValuePair<string, string>(x.PropertyName, x.ErrorMessage))
                .ToList();

        }
    }
}
