using FluentValidation;

namespace Adly.Application.Features.User.Queries.PasswordLogin
{
    public class UserPasswordLoginQueryValidator : AbstractValidator<UserPasswordLoginQuery>
    {

        public UserPasswordLoginQueryValidator()
        {
            RuleFor(c => c.userNameOrEmail)
                .NotEmpty();

            RuleFor(c => c.password)
                .NotEmpty();
        }
    }
}
