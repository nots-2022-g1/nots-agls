using api.Model;

namespace api.Services;

public interface ILemmatizerService
{
    Task<string> Get(string token);
}