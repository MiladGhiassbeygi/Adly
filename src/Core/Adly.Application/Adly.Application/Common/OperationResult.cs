

namespace Adly.Application.Common
{
    public interface IOperationResult
    {
        bool IsSuccess { get; set; }
        bool IsNotFound { get; set; }
        List<KeyValuePair<string, string>> ErrorMessage { get; set; }
    }

    public class OperationResult<TResult> : IOperationResult
    {
        public TResult Result { get; set; }

        public bool IsSuccess { get; set; }
        public bool IsNotFound { get; set; }
        public List<KeyValuePair<string, string>> ErrorMessage { get; set; } = new();


        public static OperationResult<TResult> SuccessResult(TResult result)
        {
            return new OperationResult<TResult>
            {
                Result = result,
                IsSuccess = true
            };
        }

        public static OperationResult<TResult> FailureResult(string propertyName, string message)
        {
            var result = new OperationResult<TResult>();

            result.Result = default;
            result.ErrorMessage.Add(new(propertyName, message));
            result.IsSuccess = false;
            result.IsNotFound = false;


            return result;
        }



        public static OperationResult<TResult> FailureResult(List<KeyValuePair<string, string>> errors)
        {
            return new OperationResult<TResult>
            {

                ErrorMessage = errors,
                IsSuccess = false,
                IsNotFound = false,
                Result = default
            };
        }



        public static OperationResult<TResult> NotFoundResult(string propertyName, string message)
        {
            var result = new OperationResult<TResult>();

            result.Result = default;
            result.ErrorMessage.Add(new(propertyName, message));
            result.IsSuccess = false;
            result.IsNotFound = true;


            return result;
        }
    }
}
