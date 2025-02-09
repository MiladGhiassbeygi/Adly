using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User
{
    public sealed class RoleEntity : IdentityRole<Guid>, IEntity
    {
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }

        public string DisplayName { get; private set; }

        public RoleEntity(string displayName, string name) : base(name)
        {
            DisplayName = displayName;
        }

        public ICollection<UserRoleEntity> UserRoles { get; set; }
        public ICollection<RoleClaimEntity> RoleClaims { get; set; }

    }
}
