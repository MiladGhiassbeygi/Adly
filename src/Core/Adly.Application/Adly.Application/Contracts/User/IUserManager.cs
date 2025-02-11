using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace Adly.Application.Contracts.User
{
    public interface IUserManager
    {
        Task<IdentityResult> PasswordCreateAsync(UserEntity user, string password, CancellationToken cancellationToken);
        Task<UserEntity?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken);
        Task<UserEntity?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<IdentityResult> ValidatePasswordAsync(UserEntity user, string givenPassword, CancellationToken cancellationToken);
    }
}
