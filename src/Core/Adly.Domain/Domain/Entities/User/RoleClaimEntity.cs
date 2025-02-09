using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User
{
    public sealed class RoleClaimEntity : IdentityRoleClaim<Guid>, IEntity
    {
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }

        public RoleEntity Role { get; set; }
    }
}
