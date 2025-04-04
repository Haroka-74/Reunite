namespace Reunite.Models.Auth
{
    public class AuthModel
    {
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}