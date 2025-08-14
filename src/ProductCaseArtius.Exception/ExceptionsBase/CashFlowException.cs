namespace ProductCaseArtius.Exception.ExceptionsBase;
public abstract class ProductCaseArtiusException : SystemException
{
    protected ProductCaseArtiusException(string message) : base(message)
    {
        
    }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
