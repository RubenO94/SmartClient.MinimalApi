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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyOrder(1)]
        public int TotalCount { get; set; }
        public DataSuccess(int statusCode, T data, int totalCount = default) : base(statusCode, true, data, null)
        {
            TotalCount = totalCount;
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


        public static DataSuccess<T> ToDataSuccess<T>(this T result, HttpStatusCode statusCode = HttpStatusCode.OK) where T : class
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            var totalCount = 0;

            // Verifica se o objeto result possui a propriedade Count
            var countProperty = result.GetType().GetProperty("Count");
            if (countProperty != null && countProperty.PropertyType == typeof(int))
            {
                totalCount = (int)countProperty.GetValue(result)!;
            }

            return new DataSuccess<T>((int)statusCode, result, totalCount);
        }
        public static DataSuccess<T> ToDataSuccess<T>(this BasicResult? result, T data, HttpStatusCode statusCode = HttpStatusCode.OK) where T : class
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            var totalCount = 0;

            // Verifica se o objeto result possui a propriedade Count
            var countProperty = result.GetType().GetProperty("Count");
            if (countProperty != null && countProperty.PropertyType == typeof(int))
            {
                totalCount = (int)countProperty.GetValue(result)!;
            }

            return new DataSuccess<T>((int)statusCode, data, totalCount);
        }

        public static DataSuccess<T> ToDataSuccess<T>(this LogInResult? result, T data, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) => new DataSuccess<T>((int)statusCode, data);


        public static DataFailed<object> ToDataFailed(this object? result)
        {
            var error = new Error("Ocorreu um erro inesperado ao tentar comunicar com o servidor");
            return new DataFailed<object>(statusCode: 500, error);
        }

        public static DataFailed<object> ToDataFailed(this BasicResult? result)
        {
            var error = new Error("Ocorreu um erro inesperado ao tentar comunicar com o servidor");
            if (result != null)
            {
                error.Message = result.message;
                error.Details = result.exception;
            }
            return new DataFailed<object>(statusCode: 500, error);
        }

        public static DataFailed<object> ToDataFailed(this LogInResult result)
        {
            var error = new Error(result.message);
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
