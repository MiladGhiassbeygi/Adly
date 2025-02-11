using FluentValidation;

namespace Adly.Application.Features.User.Commands.Register
{
    public class RegisterUserCommand_Validator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommand_Validator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();


            RuleFor(c => c.Password)
                .NotEmpty();

            RuleFor(c => c.RepeatPassword)
                .NotEmpty()
                .Equal(c => c.Password)
                .WithMessage("Password and Repeat Password must be same !");

            RuleFor(c => c.FirstName)
                .NotEmpty();

            RuleFor(c => c.LastName)
                .NotEmpty();

            RuleFor(c => c.UserName)
                .NotEmpty();

            //RuleFor(c => c.PhoneNumber)
            //    .NotEmpty()
            //    .Matches(@"^\?[1-9]\d{1,14}$");
        }
    }
}
