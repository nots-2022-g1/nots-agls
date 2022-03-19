using api.Model;

namespace api.Services;

public interface ILemmatizerService
{
    Task Hello();
    Task<string> Get(string token);
}