namespace helper_api_dotnet_o6_investimento.Domain.Response
{
    public class CalcularInvestimentoResponse
    {
        public CalcularInvestimentoResponse(double valorCdi, double valorSelic)
        {
            ValorCdi = valorCdi;
            ValorSelic = valorSelic;
        }
        public double ValorCdi { get; set; }
        public double ValorPoupanca { get; set; }
        public double ValorSelic { get; set; }
    }
}
