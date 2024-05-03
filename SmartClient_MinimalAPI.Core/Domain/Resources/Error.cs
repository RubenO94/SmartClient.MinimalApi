namespace SmartClientMinimalApi.Core.Domain.Resources
{
    /// <summary>
    /// Classe que representa um objeto de erro personalizado.
    /// </summary>
    public class Error
    {
        public string Message { get; set; }
        public string? Details { get; set; }
        public string? StackTrace { get; set; }

        public Error(string message, string? details = null, string? stackTrace = null)
        {
            Message = message;
            Details = details;
            StackTrace = stackTrace;
        }
    }
}
