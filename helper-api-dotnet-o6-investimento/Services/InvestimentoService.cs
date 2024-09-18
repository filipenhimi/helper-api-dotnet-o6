﻿using helper_api_dotnet_o6_investimento.Domain;
using helper_api_dotnet_o6_investimento.Domain.Interfaces;
using helper_api_dotnet_o6_investimento.Domain.Request;
using helper_api_dotnet_o6_investimento.Domain.Response;

namespace helper_api_dotnet_o6_investimento.Services
{
    public class InvestimentoService : IInvestimentoService
    {
        private readonly IBancoCentralApiRepository _bacenApi;
        private readonly IGerenciamentoCacheRepository _cache;
        public InvestimentoService(IBancoCentralApiRepository bacenApi, IGerenciamentoCacheRepository cache)
        {
            _bacenApi = bacenApi;
            _cache = cache;
        }
        public async Task<ObterCdiAtualResponse> ObterCdiAtual()
        {
            try
            {
                var result = new ObterCdiAtualResponse();

                var valoresCdi = _cache.Obter<ObterCdiAtualResponse>("valoresCdi");
                if (valoresCdi is not null)
                    return valoresCdi;

                var (dataInicial, dataFinal) = ObterDatasConsultaTaxas();
                var cdiUltimosMeses = await _bacenApi.ConsultarCdiMensal(dataInicial, dataFinal);
                result.PreencherValoresCdi(cdiUltimosMeses);

                _cache.Salvar("valoresCdi", result, TimeSpan.FromMinutes(5));
                return result;
            }
            catch (Exception ex)
            {
                //WriteTrace
                throw;
            }
        }

        public async Task<CalcularInvestimentoResponse> CalcularProvisaoInvestimentos(CalcularInvestimentoRequest request)
        {
            var valorInicial = request.Valor;
            var mes = request.QuantidadeMeses;
            var taxaCdi = request.PorcentagemCdi;

            var taxas = await ObterTaxas(taxaCdi);

            var rendimentosSelic = CalcularRendimentos(valorInicial, taxas.Selic, mes);
            var rendimentosIpca = CalcularRendimentos(valorInicial, taxas.Ipca, mes);
            var rendimentosCdi = CalcularRendimentos(valorInicial, taxas.Cdi * ConverterPorcentagemParaDecimal(taxaCdi), mes);

            var lista = new List<DadosGrafico>();

            for (int i = 0; i < mes; i++)
            {
                lista.Add(new DadosGrafico(rendimentosCdi[i], rendimentosSelic[i], rendimentosIpca[i], $"{i + 1}"));
            }

            return new CalcularInvestimentoResponse(lista, taxas);
        }

        private async Task<Taxas> ObterTaxas(double taxaCdi)
        {
            (string dataInicial, string dataFinal) = ObterDatasConsultaTaxas();

            var taxas = _cache.Obter<Taxas>("taxas");

            if (taxas is not null)
                return taxas;

            var mediaSelic = await ObterTaxaSelicMensal(dataInicial, dataFinal);
            var mediaCdi = await ObterTaxaCdiMensal(dataInicial, dataFinal);
            var mediaIpca = await ObterTaxaIpcaMensal(dataInicial, dataFinal);
            taxas = new Taxas(mediaSelic, mediaIpca, mediaCdi);
            _cache.Salvar("taxas", taxas, TimeSpan.FromMinutes(5));

            return taxas;
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

        private static (string dataInicial, string dataFinal) ObterDatasConsultaTaxas()
        {
            string dataFinal = ObterDataFormatoDiaMesAno(DateTime.Now.AddMonths(-1));
            string dataInicial = ObterDataFormatoDiaMesAno(DateTime.Now.AddMonths(-12));

            return (dataInicial, dataFinal);
        }
        private static double ConverterPorcentagemParaDecimal(double porcentagem) => porcentagem / 100;
        private static string ObterDataFormatoDiaMesAno(DateTime data) => "01/" + data.ToString("MM/yyyy");
    }
}
