using Aniverse.Business.DTO_s.Authentication;
using FluentValidation;
namespace Aniverse.Business.Validator.Authentication
{
    public class RegisterValidation : AbstractValidator<Register>
    {
        public RegisterValidation()
        {
            RuleFor(u=>u.Firstname).NotEmpty().NotNull();
            RuleFor(u=>u.Lastname).NotEmpty().NotNull();
            RuleFor(u=>u.Password).NotEmpty().NotNull().Equal(u=>u.ConfirmPasword);
        }
    }
}
