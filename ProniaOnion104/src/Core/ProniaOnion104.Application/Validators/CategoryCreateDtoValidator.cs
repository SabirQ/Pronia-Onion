using FluentValidation;
using ProniaOnion104.Application.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Application.Validators
{
    public class CategoryCreateDtoValidator:AbstractValidator<CategoryCreateDto>
    {

        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).MinimumLength(3).Matches(@"^[a-zA-Z\s]*$");
        }
    }
}
