using Adly.Application.Common;
using Adly.Application.Contracts.User.Models;
using Mediator;

namespace Adly.Application.Features.User.Queries.PasswordLogin
{
    public record UserPasswordLoginQuery(string userNameOrEmail, string password) : IRequest<OperationResult<JwtAccessTokenModel>>;
}
