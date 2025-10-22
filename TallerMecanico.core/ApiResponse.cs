namespace TallerMecanico.Core;
public record ApiResponse<T>(bool Success, T? Data, string? Message = null, string? ErrorCode = null);
