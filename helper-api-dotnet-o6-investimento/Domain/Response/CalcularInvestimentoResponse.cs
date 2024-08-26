namespace helper_api_dotnet_o6_investimento.Domain.Response
{
    public class CalcularInvestimentoResponse
    {
        public CalcularInvestimentoResponse(double valorCdi)
        {
            ValorCdi = valorCdi;
        }
        public double ValorCdi { get; set; }
        public double ValorPoupanca { get; set; }
        public double ValorSelic { get; set; }
    }
}
