using System.Net;

namespace HMS.Persistence.Exceptions
{
    public class ManagerHotelRegistered: Exception
    {
        public int StatusCode { get; set; }
        public string CustomMessage { get; set; }
        public ManagerHotelRegistered(string message): base(message)
        {
            CustomMessage = message;
            StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}
