using System.Net;

namespace HMS.Persistence.Exceptions
{
    public class DuplicateHotelNameException : Exception
    {
        public string CustomMessage { get; set; }
        public int StatusCode { get; set; }
        public DuplicateHotelNameException(string message) : base(message)
        {
            CustomMessage = message;
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
