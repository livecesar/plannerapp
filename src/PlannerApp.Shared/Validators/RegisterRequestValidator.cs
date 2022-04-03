using FluentValidation;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is mandatory.")
                .EmailAddress().WithMessage("Not a valid Email");

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("First name is mandatory")
                .MaximumLength(30).WithMessage("Name should be 30 character tops.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Last name is mandatory")
                .MaximumLength(30).WithMessage("Last name should be 30 character tops.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password is mandatory").
                MinimumLength(5);

            RuleFor(p => p.ConfirmPassword)
                .Equal(p => p.Password).WithMessage("Password does not match.");
        }
    }
}
