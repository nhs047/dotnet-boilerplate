using FluentValidation.Results;

namespace Localization.Test.API.Helpers
{
    public class ResponseObject<T> where T : class
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public ResponseObject()
        {
            IsError = false;
            Message = string.Empty;
            Data = null;
        }

        public ResponseObject<T> setMessage(string message)
        {
            Message = message;
            return this;
        }

        public ResponseObject<T> setErrorMessage(List<ValidationFailure> messages)
        {
            Message = messages[0].ToString();
            return this;
        }
        public ResponseObject<T> setData(T? data)
        {
            Data = data;
            return this;
        }

        public ResponseObject<T> setIsError(bool isError)
        {
            IsError = isError;
            return this;
        }

        public static ResponseObject<T> Build()
        {
            return new ResponseObject<T>();
        }
    }
}
