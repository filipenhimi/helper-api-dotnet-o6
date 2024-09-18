namespace helper_api_dotnet_o6_investimento.Domain.Response
{
    public class CalcularInvestimentoResponse
    {
        public CalcularInvestimentoResponse(List<DadosGrafico> dadosGrafico, Taxas taxas)
        {
            DadosGrafico = dadosGrafico;
            ValorCdi = taxas.Cdi;
            ValorIpca = taxas.Ipca;
            ValorSelic = taxas.Selic;
        }

        public List<DadosGrafico> DadosGrafico { get; set; }
        public double ValorCdi { get; set; }
        public double ValorIpca { get; set; }
        public double ValorSelic { get; set; }
    }
    public class DadosGrafico
    {
        public DadosGrafico(double valorCdi, double valorSelic, double valorIpca, string mes = "")
        {
            Mes = mes;
            ValorCdi = valorCdi;
            ValorSelic = valorSelic;
            ValorIpca = valorIpca;
        }

        public string Mes { get; set; }
        public double ValorCdi { get; set; }
        public double ValorIpca { get; set; }
        public double ValorSelic { get; set; }
    }

}
