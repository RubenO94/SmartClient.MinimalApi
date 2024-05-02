namespace SmartClientMinimalApi.Core.Domain.Resources
{
    /// <summary>
    /// Classe que representa um objeto de erro personalizado.
    /// </summary>
    public class Error
    {
        public string Message { get; }
        public int? ErrorCode { get; }
        public string? Details { get; }
        public string? StackTrace { get; }

        public Error(string message, int errorCode = default, string? details = null, string? stackTrace = null)
        {
            Message = message;
            ErrorCode = errorCode;
            Details = details;
            StackTrace = stackTrace;
        }
    }
}
