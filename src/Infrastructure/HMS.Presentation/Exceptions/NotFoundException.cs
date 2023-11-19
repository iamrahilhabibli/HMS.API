using System.Net;

namespace HMS.Persistence.Exceptions
{
    public class NotFoundException : Exception
    {
        public string CustomMessage { get; set; }
        public int StatusCode { get; set; }
        public NotFoundException(string message) : base(message) 
        {
            CustomMessage = message;
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
