using FluentValidation;
using ProniaOnion104.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Application.Validators
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x=>x.Email)
                .NotEmpty()
                .MaximumLength(256)
                .EmailAddress();
            RuleFor(x=>x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(150);
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(4);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);
            RuleFor(x => x.Surname)
               .NotEmpty()
               .MinimumLength(3)
               .MaximumLength(50);
            RuleFor(x => x).Must(x => x.ConfirmPassword == x.Password);

        }
    }
}
