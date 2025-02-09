using Domain.Common;
using Domain.Entities.Ad;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User
{
    public sealed class UserEntity : IdentityUser<Guid>, IEntity
    {


        public DateTime CreateTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }


        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserCode { get; private set; }

        private readonly List<AdEntity> _ads = new();

        public IReadOnlyList<AdEntity> Ads => _ads.AsReadOnly();

        public UserEntity(string firstName, string lastName, string userName, string email) : base(userName)
        {
            FirstName = firstName;
            LastName = lastName;
            UserCode = Guid.NewGuid().ToString("N")[0..7];
            Email = email;
        }

        public ICollection<UserRoleEntity> UserRoles { get; set; }
        public ICollection<UserClaimEntity> UserClaims { get; set; }
        public ICollection<UserLoginEntity> UserLogins { get; set; }
        public ICollection<UserTokenEntity> UserTokens { get; set; }
    }
}
