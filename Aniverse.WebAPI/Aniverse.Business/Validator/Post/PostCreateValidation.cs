using Aniverse.Business.DTO_s.Post;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.Validator.Post
{
    public class PostCreateValidation : AbstractValidator<PostCreateDto>
    {
        public PostCreateValidation()
        {
            RuleFor(p => p.Content)
                .NotEmpty()
                .NotNull();
        }
    }
}
