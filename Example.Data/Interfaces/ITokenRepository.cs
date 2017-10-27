using Example.Business.Models.Entities;

namespace Example.Data.Interfaces
{
    public interface ITokenRepository : IDataRepository<Token>
    {
        Token CheckTokenIsValid(string guid, bool isPublic);
    }
}
