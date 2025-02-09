using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User
{
    public sealed class UserTokenEntity : IdentityUserToken<Guid>, IEntity
    {
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }


        public UserEntity User { get; set; }
    }
}
