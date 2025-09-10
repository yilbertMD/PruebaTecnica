namespace Application.Excepciones
{
    public class ValidationException : Exception
    {
        public IEnumerable<string> Errores { get; }

        public ValidationException(IEnumerable<string> errores)
            : base("Error de validación")
        {
            Errores = errores;
        }
    }
}
