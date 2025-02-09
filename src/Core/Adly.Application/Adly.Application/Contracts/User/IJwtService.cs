using Adly.Application.Contracts.User.Models;
using Domain.Entities.User;

namespace Adly.Application.Contracts.User
{
    public interface IJwtService
    {
        Task<JwtAccessTokenModel> GenerateTokenAsync(UserEntity user, CancellationToken cancellationToken);
    }
}
