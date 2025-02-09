namespace Domain.Common
{
    public interface IEntity
    {
        DateTime CreateTime { get; set; }
        DateTime? LastModifiedTime { get; set; }

    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }

        public TKey Id { get; protected set; }


        public override bool Equals(object? entity)
        {
            if (entity is null) return false;

            if (entity is not BaseEntity<TKey> baseEntity) return false;

            if (ReferenceEquals(entity, this)) return true;

            return baseEntity.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }


        public static bool operator ==(BaseEntity<TKey>? a, BaseEntity<TKey>? b)
        {
            if (a is null && b is null) return true;

            if (a is null || b is null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntity<TKey>? a, BaseEntity<TKey>? b)
        {
            return !(a == b);
        }
    }
}




