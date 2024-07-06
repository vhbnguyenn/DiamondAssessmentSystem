namespace DiamondAssessmentSystem.BusinessObject.DTO
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
    public class LoginResponseDto
    {
        public CustomerDto Customer { get; set; }
        public string Token { get; set; }
    }
}