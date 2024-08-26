namespace helper_api_dotnet_o6_investimento.Domain.Request
{
    public class CalcularInvestimentoRequest
    {
        public double CdiAnual { get; set; }
        public double CdiMensal { get; set; }
        public double Valor {  get; set; }
        public double PorcentagemCdi { get; set; }
        public DateTime DataFim { get; set; }
    }
}
