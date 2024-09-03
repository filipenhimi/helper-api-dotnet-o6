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
                int meses = request.QuantidadeMeses;

                (string dataInicial, string dataFinal) = ObterDatasConsultaTaxas();

                double taxaSelicTotal = ObterTaxaTotalPorMeses(await ObterTaxaSelicMensal(dataInicial, dataFinal), meses);

                double taxaIpcaTotal = ObterTaxaTotalPorMeses(await ObterTaxaIpcaMensal(dataInicial, dataFinal), meses);

                double taxaCdiMensalTotal = await ObterTaxaCdiMensalCalculada(request, meses, dataInicial, dataFinal);

                double resultadoCdi = CalcularResultado(request.Valor, taxaCdiMensalTotal);

                double resultadoSelic = CalcularResultado(request.Valor, taxaSelicTotal);

                double resultadoIpca = CalcularResultado(request.Valor, taxaIpcaTotal);

                return new CalcularInvestimentoResponse(resultadoCdi, resultadoSelic, resultadoIpca);
            }
            catch (Exception ex)
            {
                //WriteTrace
                throw;
            }
        }

        public async Task<List<CalcularInvestimentoResponse>> CalcularProvisaoInvestimentos(CalcularInvestimentoRequest request)
        {
            var valorInicial = request.Valor;
            var mes = request.QuantidadeMeses;
            var taxaCdi = request.PorcentagemCdi;
            (string dataInicial, string dataFinal) = ObterDatasConsultaTaxas();

            var mediaSelic = await ObterTaxaSelicMensal(dataInicial, dataFinal);
            var mediaCdi = await ObterTaxaCdiMensal(dataInicial, dataFinal) * ConverterPorcentagemParaDecimal(taxaCdi);
            var mediaIpca = await ObterTaxaIpcaMensal(dataInicial, dataFinal);

            var rendimentosSelic = CalcularRendimentos(valorInicial, mediaSelic, mes);
            var rendimentosIpca = CalcularRendimentos(valorInicial, mediaIpca, mes);
            var rendimentosCdi = CalcularRendimentos(valorInicial, mediaCdi, mes);

            var lista = new List<CalcularInvestimentoResponse>();

            for (int i = 0; i < mes; i++)
            {
                lista.Add(new CalcularInvestimentoResponse(rendimentosCdi[i], rendimentosSelic[i], rendimentosIpca[i], $"{i +1}"));
            }
            return lista;
        }

        static List<double> CalcularRendimentos(double valorInicial, double taxaCdiMensal, int meses)
        {
            List<double> rendimentos = new List<double>(meses);
            double valorAtual = valorInicial;

            for (int i = 1; i <= meses; i++)
            {
                valorAtual += valorAtual * (taxaCdiMensal / 100);
                rendimentos.Add(valorAtual);
            }

            return rendimentos;
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

            return selicUltimoAno.Average(r => r.Valor);
        }

        private async Task<double> ObterTaxaIpcaMensal(string dataInicial, string dataFinal)
        {
            List<DataValor> ipcaUltimoAno = await _bacenApi.ConsultarIpcaMensal(dataInicial, dataFinal);

            return ipcaUltimoAno.Average(r => r.Valor);
        }

        private async Task<double> ObterTaxaCdiMensal(string dataInicial, string dataFinal)
        {
            List<DataValor> cdiUltimoAno = await _bacenApi.ConsultarCdiMensal(dataInicial, dataFinal);
            return cdiUltimoAno.Average(r => r.Valor);
        }

        private static double CalcularTaxaCdiMensal(double taxaCdiMensal, double porcentagemCdi) => (taxaCdiMensal * porcentagemCdi);
        private static (string dataInicial, string dataFinal) ObterDatasConsultaTaxas()
        {
            string dataFinal = ObterDataFormatoDiaMesAno(DateTime.Now.AddMonths(-1));
            string dataInicial = ObterDataFormatoDiaMesAno(DateTime.Now.AddMonths(-12));

            return (dataInicial, dataFinal);
        }
        private static double CalcularResultado(double valor, double taxa) => (valor * (taxa / 100)) + valor;
        private static double ConverterPorcentagemParaDecimal(double porcentagem) => porcentagem / 100;
        private static string ObterDataFormatoDiaMesAno(DateTime data) => "01/" + data.ToString("MM/yyyy");
    }
}
