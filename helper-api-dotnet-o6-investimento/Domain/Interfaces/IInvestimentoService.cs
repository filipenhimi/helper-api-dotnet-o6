using helper_api_dotnet_o6_investimento.Domain.Request;
using helper_api_dotnet_o6_investimento.Domain.Response;

namespace helper_api_dotnet_o6_investimento.Domain.Interfaces
{
    public interface IInvestimentoService
    {
        Task<ObterCdiAtualResponse> ObterCdiAtual();
        CalcularInvestimentoResponse CalcularProvisaoInvestimento(CalcularInvestimentoRequest request);
    }
}
