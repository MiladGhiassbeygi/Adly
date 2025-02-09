﻿using Adly.Application.Common;
using Mediator;

namespace Adly.Application.Features.User.Commands.Register
{
    public record RegisterUserCommand(string FirstName,
        string LastName,
        string UserName,
        string Email,
        string PhoneNumber,
        string Password,
        string RepeatPassword) : IRequest<OperationResult<bool>>;

}
