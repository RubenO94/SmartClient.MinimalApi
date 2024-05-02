using SmartClientMinimalApi.Core.Domain.Resources;
using SmartClientMinimalApi.Core.DTO;
using SmartClientWS;
using System.Net;
using System.Text.Json.Serialization;

namespace SmartClientMinimalApi.Core.Domain.Resources
{
    /// <summary>
    /// Classe abstrata que define um estado de dados genérico.
    /// </summary>
    public abstract class DataState<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Error? Error { get; set; }

        protected DataState(int statusCode, bool isSuccess, T? data, Error? error)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
        }
    }

    /// <summary>
    /// Classe que representa um estado de dados bem-sucedido.
    /// </summary>
    public class DataSuccess<T> : DataState<T>
    {
        public DataSuccess(int statusCode, T data) : base(statusCode, true, data, null)
        {
        }
    }

    /// <summary>
    /// Classe que representa um estado de dados falhado.
    /// </summary>
    public class DataFailed<T> : DataState<T>
    {
        public DataFailed(int statusCode, Error error) : base(statusCode, false, default(T), error)
        {
        }
    }


    public static class DataStateExtensions
    {
        public static DataFailed<object> CreateDataFailed(Error error) => new DataFailed<object>(500, error);
        public static DataSuccess<T> ToDataSuccess<T>(this T result, HttpStatusCode statusCode ) => new DataSuccess<T>((int)statusCode, result);
        public static DataFailed<object> ToDataFailed(this LogInResult result)
        {
            var error = new Error(result.message); // Use a mensagem de erro do resultado
            return new DataFailed<object>(statusCode: 500, error);
        }

        public static DataFailed<object> ToDataFailed(this BasicResult result)
        {
            var error = new Error(result.message); // Use a mensagem de erro do resultado
            return new DataFailed<object>(statusCode: 500, error);
        }

        public static DataFailed<object> ToDataFailed(this SaveFormResult result)
        {
            var error = new Error(result.message); // Use a mensagem de erro do resultado
            return new DataFailed<object>(statusCode: 500, error);
        }

        public static DataFailed<object> ToDataFailed(this NewTicketResponseResult result)
        {
            var error = new Error(result.message); // Use a mensagem de erro do resultado
            return new DataFailed<object>(statusCode: 500, error);
        }

        public static DataFailed<object> ToDataFailed(this NewContractResult result)
        {
            var error = new Error(result.message); // Use a mensagem de erro do resultado
            return new DataFailed<object>(statusCode: 500, error);
        }


    }
}
