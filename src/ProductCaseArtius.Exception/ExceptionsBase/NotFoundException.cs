using System.Net;

namespace ProductCaseArtius.Exception.ExceptionsBase;
public class NotFoundException : ProductCaseArtiusException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
