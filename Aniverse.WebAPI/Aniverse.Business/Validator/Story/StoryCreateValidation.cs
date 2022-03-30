using Aniverse.Business.DTO_s.Story;
using FluentValidation;

namespace Aniverse.Business.Validator.Story
{
    public class StoryCreateValidation : AbstractValidator<StoryCreateDto>
    {
        public StoryCreateValidation()
        {
            RuleFor(s=>s.StoryFile).NotEmpty().NotNull();
        }
    }
}
