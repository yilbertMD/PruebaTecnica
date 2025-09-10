namespace Application.Excepciones
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string mensaje) : base(mensaje) { }
    }
}
