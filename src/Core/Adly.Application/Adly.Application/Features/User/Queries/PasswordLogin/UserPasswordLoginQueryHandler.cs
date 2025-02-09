using Adly.Application.Common;
using Adly.Application.Contracts.User;
using Adly.Application.Contracts.User.Models;
using Adly.Application.Extensions;
using Mediator;

namespace Adly.Application.Features.User.Queries.PasswordLogin
{
    public class UserPasswordLoginQueryHandler(IUserManager userManager, IJwtService jwtService) : IRequestHandler<UserPasswordLoginQuery, OperationResult<JwtAccessTokenModel>>
    {

        public async ValueTask<OperationResult<JwtAccessTokenModel>> Handle(UserPasswordLoginQuery request, CancellationToken cancellationToken)
        {
            var validator = new UserPasswordLoginQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return OperationResult<JwtAccessTokenModel>.FailureResult(validationResult.Errors.ConvertToKeyValuePair());

            var user = request.userNameOrEmail.IsEmail() ? await userManager
                .GetUserByEmailAsync(request.userNameOrEmail, cancellationToken)
                : await userManager
                .GetUserByUserNameAsync(request.userNameOrEmail, cancellationToken);

            if (user is null)
                return OperationResult<JwtAccessTokenModel>.NotFoundResult(nameof(UserPasswordLoginQuery.userNameOrEmail), "user not found !");

            var passwordValidation = await userManager.ValidatePasswordAsync(user, request.password, cancellationToken);

            if (passwordValidation.Succeeded)
            {
                var accessToken = await jwtService.GenerateTokenAsync(user, cancellationToken);
                return OperationResult<JwtAccessTokenModel>.SuccessResult(accessToken);
            }


            return OperationResult<JwtAccessTokenModel>.FailureResult(passwordValidation.Errors.ConvertToToKeyValuePair());

        }
    }
}
