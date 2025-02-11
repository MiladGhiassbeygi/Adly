using Adly.Application.Common;
using Adly.Application.Contracts.User;
using Adly.Application.Extensions;
using Domain.Entities.User;
using Mediator;

namespace Adly.Application.Features.User.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<bool>>
    {
        private readonly IUserManager _userManager;

        public RegisterUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async ValueTask<OperationResult<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterUserCommand_Validator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return OperationResult<bool>.FailureResult(validationResult.Errors.ConvertToKeyValuePair());


            var user = new UserEntity(request.FirstName, request.LastName, request.UserName, request.Email)
            {
                PhoneNumber = request.PhoneNumber
            };


            var userCreateResult = await _userManager
                .PasswordCreateAsync(user, request.Password, cancellationToken);

            if (userCreateResult.Succeeded)
                return OperationResult<bool>.SuccessResult(true); //:todo Send Confirmation Email ;-)


            return OperationResult<bool>.FailureResult(userCreateResult.Errors.ConvertToToKeyValuePair());
        }
    }
}
