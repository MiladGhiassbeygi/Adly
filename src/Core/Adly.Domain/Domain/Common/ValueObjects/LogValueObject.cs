
namespace Domain.Common.ValueObjects
{
    public record LogValueObject(DateTime EntryDate, string Message, string? AddtionalDescription = null);

}
