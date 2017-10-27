using Example.Business.Models.Entities;

namespace Example.Data.Interfaces
{
    public interface ITokenRepository : IDataRepository<Token>
    {
        bool CheckTokenIsValid(string guid, bool isPublic);
    }
}
