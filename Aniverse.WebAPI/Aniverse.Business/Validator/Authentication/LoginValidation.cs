using Aniverse.Business.DTO_s.Authentication;
using FluentValidation;
namespace Aniverse.Business.Validator.Authentication
{
    public class LoginValidation : AbstractValidator<Login>
    {
        public LoginValidation()
        {
            RuleFor(u=>u.Username).NotEmpty().NotNull();
            RuleFor(u=>u.Password).NotEmpty().NotNull();
        }
    }
}
