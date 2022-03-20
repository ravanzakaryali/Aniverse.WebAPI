using Aniverse.Business.DTO_s.Page;
using FluentValidation;

namespace Aniverse.Business.Validator.Page
{
    public class PageCreateValidation : AbstractValidator<PageCreateDto>
    {
        public PageCreateValidation()
        {
            RuleFor(p=>p.Pagename).MinimumLength(3).MaximumLength(30).NotNull().NotEmpty();
            RuleFor(p=>p.Name).NotNull().MinimumLength(3).MaximumLength(50);
        }
    }
}
