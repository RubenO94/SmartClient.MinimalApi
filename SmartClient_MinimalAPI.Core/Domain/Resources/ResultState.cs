﻿using Microsoft.AspNetCore.Http;
using SmartClient.MinimalAPI.Core.DTO.Authentications;
using SmartClient.MinimalAPI.Core.DTO.StockMovements;
using SmartClientMinimalApi.Core.Domain.Resources;
using SmartClientWS;
using System.Text.Json.Serialization;

namespace SmartClient.MinimalAPI.Core.Domain.Resources
{
    /// <summary>
    /// Classe abstrata que define o estado do resultado genérico.
    /// </summary>
    public abstract class ResultState<T>
    {
        /// <summary>
        /// Obtém ou define o status HTTP do resultado.
        /// </summary>
        [JsonPropertyOrder(0)]
        public int Status { get; set; }

        /// <summary>
        /// Obtém ou define se o resultado foi bem-sucedido.
        /// </summary>
        [JsonPropertyOrder(1)]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Obtém ou define o número total de itens no resultado.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyOrder(2)]
        public int? TotalCount { get; set; }

        /// <summary>
        /// Obtém ou define os dados do resultado.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyOrder(3)]
        public T? Data { get; set; }

        /// <summary>
        /// Obtém ou define os detalhes do erro, se aplicável.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyOrder(4)]
        public Error? Error { get; set; }

        protected ResultState(int statusCode, bool isSuccess, int? totalCount, T? data, Error? error)
        {
            Status = statusCode;
            IsSuccess = isSuccess;
            TotalCount = totalCount == default || totalCount <= 0 ? null : totalCount;
            Data = data;
            Error = error;
        }
    }

    /// <summary>
    /// Classe que representa um resultado de sucesso.
    /// </summary>
    public class ResultSuccess<T> : ResultState<T>
    {
        /// <summary>
        /// Inicializa uma nova instância de <see cref="ResultSuccess{T}"/> com um código de status, o número total de itens e os dados.
        /// </summary>
        /// <param name="statusCode">O código de status HTTP.</param>
        /// <param name="totalCount">O número total de itens.</param>
        /// <param name="data">Os dados do resultado.</param>
        public ResultSuccess(int statusCode, int totalCount, T? data) : base(statusCode, true, totalCount, data, null)
        {
        }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="ResultSuccess{T}"/> com um código de status e os dados.
        /// </summary>
        /// <param name="statusCode">O código de status HTTP.</param>
        /// <param name="data">Os dados do resultado.</param>
        public ResultSuccess(int statusCode, T? data) : base(statusCode, true, null, data, null)
        {
        }
    }

    /// <summary>
    /// Classe que representa um resultado de falha.
    /// </summary>
    public class ResultFailed<T> : ResultState<T?>
    {
        /// <summary>
        /// Inicializa uma nova instância de <see cref="ResultFailed{T}"/> com um código de status e um erro.
        /// </summary>
        /// <param name="statusCode">O código de status HTTP.</param>
        /// <param name="error">O erro ocorrido.</param>

        public ResultFailed(int statusCode, Error error) : base(statusCode, false, null, default(T), error)
        {
        }
    }

    /// <summary>
    /// Extensões para conversão de resultados.
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Converte um resultado básico em um resultado personalizado.
        /// </summary>
        /// <typeparam name="T">O tipo de dados do resultado.</typeparam>
        /// <param name="result">O resultado básico a ser convertido.</param>
        /// <param name="data">Os dados do resultado.</param>
        /// <param name="statusCode">O código de status HTTP do resultado.</param>
        /// <returns>O resultado personalizado convertido.</returns>
        public static IResult ToResult<T>(this BasicResult? result, T? data, int? statusCode = null) where T : class
        {

            if (result == null)
            {
                return Results.StatusCode(statusCode ?? StatusCodes.Status500InternalServerError);
            }

            if (!result.success)
            {
                return Results.BadRequest(new ResultFailed<object>(statusCode ?? StatusCodes.Status500InternalServerError, new Error(result.message, result.exception)));
            }

            var totalCount = 0;

            // Verifica se o objeto result possui a propriedade Count
            var countProperty = result.GetType().GetProperty("Count");
            if (countProperty != null && countProperty.PropertyType == typeof(int))
            {
                totalCount = (int)countProperty.GetValue(result)!;
            }

            return Results.Ok(new ResultSuccess<T>(statusCode ?? StatusCodes.Status200OK, totalCount, data));

        }

        /// <summary>
        /// Converte uma resposta de autenticação em um resultado personalizado.
        /// </summary>
        /// <param name="auth">A resposta de autenticação.</param>
        /// <param name="statusCode">O código de status HTTP do resultado.</param>
        /// <returns>O resultado personalizado convertido.</returns>
        public static IResult ToResult(this AuthenticationResponseDTO? auth, int? statusCode = null)
        {

            if (auth == null || string.IsNullOrEmpty(auth.Token))
            {
                return Results.StatusCode(statusCode ?? StatusCodes.Status500InternalServerError);
            }

            return Results.Ok(new ResultSuccess<AuthenticationResponseDTO>(statusCode ?? StatusCodes.Status200OK, default, auth));

        }

        /// <summary>
        /// Converte um resultado de movimentações de stock em um resultado personalizado.
        /// </summary>
        /// <param name="result">O resultado de movimentações de stock.</param>
        /// <param name="statusCode">O código de status HTTP do resultado.</param>
        /// <returns>O resultado personalizado convertido.</returns>
        public static IResult ToResult(this StockMovementsResult? result, int? statusCode = null)
        {

            if (result == null)
            {
                return Results.StatusCode(statusCode ?? StatusCodes.Status500InternalServerError);
            }

            if (!result.success)
            {
                return Results.BadRequest(new ResultFailed<object>(statusCode ?? StatusCodes.Status500InternalServerError, new Error(result.message, result.exception)));
            }

            var stockMovementsDTO = result.Value.Select(sm => sm.ToResponseDTO()).ToList();

            return Results.Ok(new ResultSuccess<List<StockMovementResponseDTO>>(statusCode ?? StatusCodes.Status200OK, result.Count, stockMovementsDTO));
        }

        /// <summary>
        /// Converte uma coleção de resultados em um resultado personalizado.
        /// </summary>
        /// <typeparam name="T">O tipo de dados dos resultados.</typeparam>
        /// <param name="result">A coleção de resultados.</param>
        /// <param name="page">O número da página.</param>
        /// <param name="pageSize">O tamanho da página.</param>
        /// <param name="statusCode">O código de status HTTP do resultado.</param>
        /// <returns>O resultado personalizado convertido.</returns>
        public static IResult ToResult<T>(this IEnumerable<T>? result, int? page = null, int? pageSize = null, int? statusCode = null)
        {

            if (result == null)
            {
                return Results.StatusCode(statusCode ?? StatusCodes.Status500InternalServerError);
            }

            if (page.HasValue && pageSize.HasValue && page >= 0 && pageSize > 0)
            {
                var data = result.Skip(page.Value * pageSize.Value).Take(pageSize.Value).ToList();
                Results.Ok(new ResultSuccess<List<T>>(statusCode ?? StatusCodes.Status200OK, result.Count(), data));
            }

            return Results.Ok(new ResultSuccess<List<T>>(statusCode ?? StatusCodes.Status200OK, result.Count(), result.ToList()));

        }

        /// <summary>
        /// Converte um resultado em um resultado personalizado.
        /// </summary>
        /// <typeparam name="T">O tipo de dados do resultado.</typeparam>
        /// <param name="result">O resultado.</param>
        /// <param name="statusCode">O código de status HTTP do resultado.</param>
        /// <returns>O resultado personalizado convertido.</returns>
        public static IResult ToResult<T>(this T? result, int? statusCode = null) where T : class
        {

            if (result == null)
            {
                return Results.StatusCode(statusCode ?? StatusCodes.Status500InternalServerError);
            }


            if (typeof(T).IsSubclassOf(typeof(BasicResult)))
            {
                var success = false;
                string message = string.Empty;
                string? exceptionMessage = null;

                var successProperty = result.GetType().GetProperty("success");
                if (successProperty != null && successProperty.PropertyType == typeof(bool))
                {
                    success = (bool)successProperty.GetValue(result)!;
                }

                var messageProperty = result.GetType().GetProperty("message");
                if (messageProperty != null && messageProperty.PropertyType == typeof(string))
                {
                    message = (string)messageProperty.GetValue(result)!;
                }

                var exceptionProperty = result.GetType().GetProperty("exception");
                if (exceptionProperty != null && exceptionProperty.PropertyType == typeof(string))
                {
                    exceptionMessage = (string)exceptionProperty.GetValue(result)!;
                }

                if (!success)
                {
                    return Results.BadRequest(new ResultFailed<object>(statusCode ?? StatusCodes.Status500InternalServerError, new Error(message, exceptionMessage)));
                }

                int totalCount = default;

                // Verifica se o objeto result possui a propriedade Count
                var countProperty = result.GetType().GetProperty("Count");
                if (countProperty != null && countProperty.PropertyType == typeof(int))
                {
                    totalCount = (int)countProperty.GetValue(result)!;
                }


                switch (result)
                {
                    case StockMovementResult subResult:
                        return Results.Ok(new ResultSuccess<object>(statusCode ?? StatusCodes.Status200OK, totalCount, new { StockMovementID = subResult.StockMovementID}));
                    case SaveFormResult subResult:
                        // Manipular SaveFormResult
                        return Results.Ok(new ResultSuccess<SaveFormResult>(statusCode ?? StatusCodes.Status200OK, subResult));
                    case IntBasicResult subResult:
                        // Manipular IntBasicResult
                        // Exemplo: return Results.Ok(new ResultSuccess<IntBasicResult>(statusCode ?? StatusCodes.Status200OK, intBasicResult));
                        break;

                    case NewContractResult subResult:
                        // Manipular NewContractResult
                        // Exemplo: return Results.Ok(new ResultSuccess<NewContractResult>(statusCode ?? StatusCodes.Status200OK, newContractResult));
                        break;
                    case NewFormResult subResult:
                        // Manipular NewContractResult
                        // Exemplo: return Results.Ok(new ResultSuccess<NewContractResult>(statusCode ?? StatusCodes.Status200OK, newContractResult));
                        break;
                    case CheckInResult subResult:
                        // Manipular NewContractResult
                        // Exemplo: return Results.Ok(new ResultSuccess<NewContractResult>(statusCode ?? StatusCodes.Status200OK, newContractResult));
                        break;
                    case DeleteResult subResult:
                        // Manipular NewContractResult
                        // Exemplo: return Results.Ok(new ResultSuccess<NewContractResult>(statusCode ?? StatusCodes.Status200OK, newContractResult));
                        break;
                    case CustomFileInfo subResult:
                        // Manipular NewContractResult
                        // Exemplo: return Results.Ok(new ResultSuccess<NewContractResult>(statusCode ?? StatusCodes.Status200OK, newContractResult));
                        break;
                    case ServiceFormsResult subResult:
                        // Manipular NewContractResult
                        // Exemplo: return Results.Ok(new ResultSuccess<NewContractResult>(statusCode ?? StatusCodes.Status200OK, newContractResult));
                        break;
                    case TicketsResult subResult:
                        // Manipular NewContractResult
                        // Exemplo: return Results.Ok(new ResultSuccess<NewContractResult>(statusCode ?? StatusCodes.Status200OK, newContractResult));
                        break;
                    case ContractsResult subResult:
                        // Manipular NewContractResult
                        // Exemplo: return Results.Ok(new ResultSuccess<NewContractResult>(statusCode ?? StatusCodes.Status200OK, newContractResult));
                        break;
                    case SerialNumberInfoResult subResult:
                        // Manipular NewContractResult
                        // Exemplo: return Results.Ok(new ResultSuccess<NewContractResult>(statusCode ?? StatusCodes.Status200OK, newContractResult));
                        break;
                    case ItemResult subResult:
                        // Manipular NewContractResult
                        // Exemplo: return Results.Ok(new ResultSuccess<NewContractResult>(statusCode ?? StatusCodes.Status200OK, newContractResult));
                        break;

                    // Adicione mais casos para outras subclasses de BasicResult conforme necessário
                    default:
                        // Manipular outros tipos de resultados
                        // Exemplo: return Results.Ok(new ResultSuccess<T>(statusCode ?? StatusCodes.Status200OK, result));
                        break;
                }
            }

            return Results.Ok(new ResultSuccess<T>(statusCode ?? StatusCodes.Status200OK, result));
        }

        /// <summary>
        /// Cria um resultado de falha.
        /// </summary>
        /// <param name="errorMessage">A mensagem de erro.</param>
        /// <param name="isException">Indica se ocorreu uma exceção.</param>
        /// <param name="statusCode">O código de status HTTP do resultado.</param>
        /// <returns>O resultado de falha criado.</returns>
        public static IResult ResultFailed(string? errorMessage = null, bool isException = false, int? statusCode = null)
        {
            if (isException)
            {
                return !string.IsNullOrEmpty(errorMessage) ? Results.Problem(errorMessage) : Results.StatusCode(statusCode ?? StatusCodes.Status500InternalServerError);
            }

            return string.IsNullOrEmpty(errorMessage) ? Results.StatusCode(statusCode ?? StatusCodes.Status400BadRequest) : Results.BadRequest(new ResultFailed<object>(statusCode ?? StatusCodes.Status400BadRequest, new Error(errorMessage!)));
        }
    }
}