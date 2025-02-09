using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Common.ValueObjects;

namespace Domain.Entities.Ad
{
    public sealed class AdEntity : BaseEntity<Guid>
    {

        #region Fields


        public string Title { get; private set; }
        public string Description { get; private set; }


        private readonly List<LogValueObject> _changeLogs = new();
        public IReadOnlyList<LogValueObject> ChangeLogs => _changeLogs.AsReadOnly();

        private readonly List<ImageValueObject> _images = new();
        public IReadOnlyList<ImageValueObject> Images => _images.AsReadOnly();

        public AdStates CurrentState { get; private set; }

        #endregion

        #region Navigation Properties

        public Guid UserId { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid LocationId { get; private set; }

        #endregion

        #region Ctors
        private AdEntity()
        {

        }
        #endregion

        #region Methods


        public DomainResult ChangeStates(AdStates state, string? additionalMessage = null)
        {
            if (CurrentState == AdStates.Approved
                && state is AdStates.Rejected or AdStates.Pending)
                return new DomainResult(false, "This ad is already approved !");


            CurrentState = state;

            this._changeLogs.Add(new LogValueObject(DateTime.Now, "Ad stated changed", additionalMessage));

            return DomainResult.None;

        }
        public static AdEntity Create(string title, string description, Guid? userId, Guid? categoryId, Guid? locationId)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(title);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(description);

            Guard.Against.NullOrEmpty(userId, "Invalid User ID");
            Guard.Against.NullOrEmpty(locationId, "Invalid Location ID");
            Guard.Against.NullOrEmpty(categoryId, "Invalid Category ID");



            var ad = new AdEntity()
            {
                Title = title,
                Id = Guid.NewGuid(),
                Description = description,
                UserId = userId.Value,
                CategoryId = categoryId.Value,
                LocationId = locationId.Value,
                CurrentState = AdStates.Pending
            };

            ad._changeLogs.Add(new LogValueObject(DateTime.Now, "Ad Created"));
            return ad;
        }

        public static AdEntity Create(Guid? id, string title, string description, Guid? userId, Guid? categoryId, Guid? locationId)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(title);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(description);



            Guard.Against.NullOrEmpty(id, "Invalid ID");
            Guard.Against.NullOrEmpty(userId, "Invalid User ID");
            Guard.Against.NullOrEmpty(locationId, "Invalid Location ID");
            Guard.Against.NullOrEmpty(categoryId, "Invalid Category ID");

            var ad = new AdEntity()
            {
                Title = title,
                Id = id.Value,
                Description = description,
                UserId = userId.Value,
                CategoryId = categoryId.Value,
                LocationId = locationId.Value,
                CurrentState = AdStates.Pending
            };


            ad._changeLogs.Add(new LogValueObject(DateTime.Now, "Ad Created"));

            return ad;

        }

        public void Edit(string title, string description, Guid? categoryId, Guid? locationId)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(title);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(description);
            Guard.Against.NullOrEmpty(categoryId, "Invalid Category ID");
            Guard.Against.NullOrEmpty(locationId, "Invalid Location ID");

            this.Title = title;
            this.Description = description;
            this.CategoryId = categoryId.Value;
            this.LocationId = locationId.Value;

            _changeLogs.Add(new LogValueObject(DateTime.Now, "Ad Edited"));
            this.CurrentState = AdStates.Pending;
        }
        #endregion

        public enum AdStates
        {
            Pending,
            Rejected,
            Approved,
            Deleted,
            Expired
        }
    }
}
