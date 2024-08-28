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
        public async Task<CalcularInvestimentoResponse> CalcularProvisaoInvestimento(CalcularInvestimentoRequest request)
        {
            try
            {
                int meses = ObterMesesAteDataFim(request.DataFim);

                (string dataInicial, string dataFinal) = ObterDatasConsultaTaxas();

                double taxaSelicTotal = ObterTaxaTotalPorMeses(await ObterTaxaSelicMensal(dataInicial, dataFinal), meses);

                double taxaCdiMensalTotal = await ObterTaxaCdiMensalCalculada(request, meses, dataInicial, dataFinal);

                double resultadoCdi = CalcularResultado(request.Valor, taxaCdiMensalTotal);

                double resultadoSelic = CalcularResultado(request.Valor, taxaSelicTotal);

                return new CalcularInvestimentoResponse(resultadoCdi, resultadoSelic);
            }
            catch (Exception ex)
            {
                //WriteTrace
                throw;
            }
        }

        private double ObterTaxaTotalPorMeses(double taxa, int meses) => taxa * meses;

        private async Task<double> ObterTaxaCdiMensalCalculada(CalcularInvestimentoRequest request, int meses, string dataInicial, string dataFinal)
        {
            double taxaCdiMensal = await ObterTaxaCdiMensal(dataInicial, dataFinal);

            double porcentagemCdi = ConverterPorcentagemParaDecimal(request.PorcentagemCdi);

            double taxaCdiMensalCalculada = CalcularTaxaCdiMensal(taxaCdiMensal, porcentagemCdi);

            return ObterTaxaTotalPorMeses(taxaCdiMensalCalculada, meses);
        }

        private async Task<double> ObterTaxaSelicMensal(string dataInicial, string dataFinal)
        {
            List<DataValor> selicUltimoAno = await _bacenApi.ConsultarSelicMensal(dataInicial, dataFinal);

            double selicMedia = selicUltimoAno.Average(r => r.Valor);
            return selicMedia;
        }

        private async Task<double> ObterTaxaCdiMensal(string dataInicial, string dataFinal)
        {
            List<DataValor> cdiUltimoAno = await _bacenApi.ConsultarCdiMensal(dataInicial, dataFinal);

            double taxaCdiMensal = cdiUltimoAno.Average(r => r.Valor);
            return taxaCdiMensal;
        }

        private static double CalcularTaxaCdiMensal(double taxaCdiMensal, double porcentagemCdi) => (taxaCdiMensal * porcentagemCdi);
        private static int ObterMesesAteDataFim(DateTime dataFim)
        {
            DateTime dataAtual = DateTime.Now;
            int totalMeses = ((dataFim.Year - dataAtual.Year) * 12) + dataFim.Month - dataAtual.Month;
            return totalMeses;
        }
        private static (string dataInicial, string dataFinal) ObterDatasConsultaTaxas()
        {
            string dataFinal = ObterDataFormatoDiaMesAno(DateTime.Now);
            string dataInicial = ObterDataFormatoDiaMesAno(DateTime.Now.AddMonths(-11));

            return (dataInicial, dataFinal);
        }
        private static string ObterMediaMensalCdi(List<DataValor> cdis) => cdis.Average(r => r.Valor).ToString();
        private static double CalcularResultado(double valor, double taxa) => (valor * (taxa / 100)) + valor;
        private static double ConverterPorcentagemParaDecimal(double porcentagem) => porcentagem / 100;
        private static string ObterDataFormatoDiaMesAno(DateTime data) => "01/" + data.ToString("MM/yyyy");
    }
}
