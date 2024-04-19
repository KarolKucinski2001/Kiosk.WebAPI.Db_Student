namespace Kiosk.WebAPI.Db.Exceptions
{
    // Wyjątek: żądanie użytkownika zawiera błędy 
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
