namespace Base.Domain.SeedWork
{
    public interface IBusinessRule
    {
        string Message { get; }
        bool IsBroken();
    }
}