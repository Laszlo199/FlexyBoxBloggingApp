namespace BloggingAppFrontend.Application.Dtos.AuthDtos
{
    public class TokenDto
    {
        public string Jwt { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
    }
}
