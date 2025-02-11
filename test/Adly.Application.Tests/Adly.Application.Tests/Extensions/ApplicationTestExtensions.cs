using Adly.Application.Common;
using Xunit.Abstractions;

namespace Adly.Application.Tests.Extensions
{
    public static class ApplicationTestExtensions
    {

        public static void WriteLineOperationResultErrors<TResult>(this ITestOutputHelper testOutputHelper, OperationResult<TResult> operationResult)
        {
            foreach (var error in operationResult.ErrorMessage)
            {
                testOutputHelper.WriteLine($"Prperty Name:{error.Key} Message: {error.Value}");
            }
        }
    }
}
