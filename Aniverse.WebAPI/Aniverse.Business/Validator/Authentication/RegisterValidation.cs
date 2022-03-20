using Aniverse.Business.DTO_s.Authentication;
using FluentValidation;
namespace Aniverse.Business.Validator.Authentication
{
    public class RegisterValidation : AbstractValidator<Register>
    {
        public RegisterValidation()
        {
            RuleFor(u => u.Firstname)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(30);
            RuleFor(u => u.Lastname)
                .NotEmpty()
                .NotNull()  
                .MinimumLength(3)
                .MaximumLength(30);
            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull()
                .Equal(u => u.ConfirmPasword);
            RuleFor(u => u.Username)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(30); 
        }
    }
}
