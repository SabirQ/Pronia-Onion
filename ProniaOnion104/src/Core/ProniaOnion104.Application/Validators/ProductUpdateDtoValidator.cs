using FluentValidation;
using ProniaOnion104.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Application.Validators
{
    public class ProductUpdateDtoValidator:AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Name is important")
               .MaximumLength(100).WithMessage("Name may contain maximum 100 characters")
               .MinimumLength(2).WithMessage("Name may contain at least 2 characters");

            RuleFor(x => x.SKU).NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.Price).NotEmpty().LessThanOrEqualTo(999999.99m)
                .GreaterThanOrEqualTo(10);
            //.Must(CheckPrice)
            //.Must(x => x > 10 && x < 999999.99m);

            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.CategoryId).Must(c => c > 0);

            RuleForEach(x => x.ColorIds).Must(c => c > 0);
            RuleFor(x => x.ColorIds).NotNull();
        }
    }
}
