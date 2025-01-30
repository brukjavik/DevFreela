namespace DevFreela.Application.Models
{
    public class PasswordRecoveryRequestInputModel
    {
        public string Email { get; set; }
    }
    public class ValidateRecoveryInputModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
    public class ChangePasswordInputModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }

}
