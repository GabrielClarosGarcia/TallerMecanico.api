// Core/ApiResponse.cs

namespace TallerMecanico.Api
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }  // Indica si la respuesta fue exitosa
        public T Data { get; set; }        // Datos devueltos en la respuesta
        public string? Message { get; set; } // Mensaje asociado a la respuesta
        public string? ErrorCode { get; set; } // Código de error, si corresponde
        public Pagination? Pagination { get; set; } // Información de paginación (si aplica)
        public List<Message>? Messages { get; set; } // Mensajes adicionales (ej. errores o éxito)

        public ApiResponse(bool success, T data, string? message = null, string? errorCode = null, Pagination? pagination = null, List<Message>? messages = null)
        {
            Success = success;
            Data = data;
            Message = message;
            ErrorCode = errorCode;
            Pagination = pagination;
            Messages = messages;
        }
    }

    // Clase para la información de la paginación
    public class Pagination
    {
        public int TotalCount { get; set; }  // Total de elementos en la consulta
        public int PageSize { get; set; }    // Cantidad de elementos por página
        public int CurrentPage { get; set; } // Página actual
        public int TotalPages { get; set; }  // Total de páginas
        public bool HasNextPage { get; set; } // Si existe una página siguiente
        public bool HasPreviousPage { get; set; } // Si existe una página anterior
    }

    // Clase para los mensajes de la respuesta
    public class Message
    {
        public string Type { get; set; }      // Tipo de mensaje (Ej. "Error", "Success")
        public string Description { get; set; } // Descripción del mensaje
    }
}
