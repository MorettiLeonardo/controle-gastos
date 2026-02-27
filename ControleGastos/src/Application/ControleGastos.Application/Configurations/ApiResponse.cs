using System.Text.Json.Serialization;

namespace ControleGastos.Application.Configurations
{
    public record ApiResponse<TData>
    {
        private readonly int _code;
        public string Message { get; init; } = string.Empty;
        public TData? Data { get; init; }

        [JsonConstructor]
        public ApiResponse(int V) => _code = 200;

        public ApiResponse(int code, string message, TData? data)
        {
            _code = code;
            Message = message;
            Data = data;
        }

        [JsonIgnore]
        public bool IsSuccess => _code is >= 200 and <= 299;

        [JsonIgnore]
        public int StatusCode => _code;
    }
}
