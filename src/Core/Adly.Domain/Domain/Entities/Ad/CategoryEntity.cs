using Domain.Common;

namespace Domain.Entities.Ad
{
    public sealed class CategoryEntity : BaseEntity<Guid>
    {
        public string Name { get; set; }

        private List<AdEntity> _ads = new();
        private IReadOnlyCollection<AdEntity> Ads => _ads.AsReadOnly();
        public CategoryEntity(string name)
        {
            Name = name;
        }
    }
}
