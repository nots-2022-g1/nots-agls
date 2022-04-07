using Refit;
using frontend.Models;

namespace frontend.Services;

[Headers("Content-Type: application/json")]
public interface IOpenAIService
{
    [Post("/engines/text-davinci-002/completions")]
    Task<ApiResponse<OpenAI>> extractReasons(OpenAIExtractDTO extractDTO);

    [Post("/engines/text-davinci-002/completions")]
    Task<ApiResponse<OpenAI>> summarizeText(OpenAISummarizeDTO summarizeDTO);
}
