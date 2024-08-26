using helper_api_dotnet_o6_investimento.Domain.Interfaces;
using helper_api_dotnet_o6_investimento.Domain.Request;
using helper_api_dotnet_o6_investimento.Domain.Response;

namespace helper_api_dotnet_o6_investimento.Services
{
    public class InvestimentoService : IInvestimentoService
    {
        private readonly IBancoCentralApiRepository _bacenApi;
        public InvestimentoService(IBancoCentralApiRepository bacenApi)
        {
            _bacenApi = bacenApi;
        }
        public async Task<ObterCdiAtualResponse> ObterCdiAtual()
        {
            try
            {
                var result = new ObterCdiAtualResponse();
                var (dataInicial, dataFinal) = ObterDatasConsultaTaxas();
                var cdiUltimosMeses = await _bacenApi.ConsultarCdiMensal(dataInicial, dataFinal);
                result.PreencherValoresCdi(cdiUltimosMeses);
                return result;
            }
            catch (Exception ex)
            {
                //WriteTrace
                throw;
            }
        }
        public CalcularInvestimentoResponse CalcularProvisaoInvestimento(CalcularInvestimentoRequest request)
        {
            double taxaCdiMensal = request.CdiMensal / 100;
            double porcentagem = request.PorcentagemCdi / 100;
            double taxaCdiMensal1052 = (taxaCdiMensal * porcentagem) * ObterDataFim(request.DataFim);
            double resultado = request.Valor * (1 + taxaCdiMensal1052);
            return new CalcularInvestimentoResponse(resultado);
        }

        private int ObterDataFim(DateTime dataFim)
        {
            DateTime dataAtual = DateTime.Now;
            int totalMeses = ((dataFim.Year - dataAtual.Year) * 12) + dataFim.Month - dataAtual.Month;
            return totalMeses;
        }
        private string ObterMediaMensalCdi(List<Cdi> cdis) => cdis.Average(r => r.Valor).ToString();

        private (string dataInicial, string dataFinal) ObterDatasConsultaTaxas()
        {
            var final = "01/" + DateTime.Now.ToString("MM/yyyy");
            var inicial = "01/" + DateTime.Now.AddMonths(-11).ToString("MM/yyyy");
            return (inicial, final);
        }


    }
}
