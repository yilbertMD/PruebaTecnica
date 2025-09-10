﻿namespace Application.Responses
{
    public class ApiResponse<T>
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public T? Datos { get; set; }

        public static ApiResponse<T> Ok(T datos, string mensaje = "Operación exitosa")
        {
            return new ApiResponse<T>
            {
                Exito = true,
                Mensaje = mensaje,
                Datos = datos
            };
        }

        public static ApiResponse<T> Fallo(string mensaje)
        {
            return new ApiResponse<T>
            {
                Exito = false,
                Mensaje = mensaje,
                Datos = default
            };
        }

        public static ApiResponse<T> Fallo(T datos, string mensaje = "Error de validación")
        {
            return new ApiResponse<T>
            {
                Exito = false,
                Mensaje = mensaje,
                Datos = datos
            };
        }

    }
}
