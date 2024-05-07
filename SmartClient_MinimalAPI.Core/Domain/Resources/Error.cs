using System.Text.Json.Serialization;

namespace SmartClientMinimalApi.Core.Domain.Resources
{
    /// <summary>
    /// Classe que representa um objeto de erro personalizado.
    /// </summary>
    public class Error
    {
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Details { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StackTrace { get; set; }

        public Error(string message, string? details = null, string? stackTrace = null)
        {
            Message = message;
            Details = details;
            StackTrace = stackTrace;
        }
    }
}
