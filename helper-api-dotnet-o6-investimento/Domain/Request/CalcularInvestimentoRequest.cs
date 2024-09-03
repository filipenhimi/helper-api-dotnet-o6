namespace helper_api_dotnet_o6_investimento.Domain.Request
{
    public class CalcularInvestimentoRequest
    {
        public double Valor { get; set; }
        public double PorcentagemCdi { get; set; }
        public int QuantidadeMeses { get; set; }
    }
}
