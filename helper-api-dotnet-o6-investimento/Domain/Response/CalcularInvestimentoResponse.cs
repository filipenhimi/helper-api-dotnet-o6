namespace helper_api_dotnet_o6_investimento.Domain.Response
{
    public class CalcularInvestimentoResponse
    {
        //public CalcularInvestimentoResponse()
        //{
            
        //}
        public CalcularInvestimentoResponse(double valorCdi, double valorSelic, double valorIpca, string mes = "")
        {
            Mes = mes;
            ValorCdi = valorCdi;
            ValorSelic = valorSelic;
            ValorIpca = valorIpca;
        }
        //public double ValorCdi { get; set; }
        //public double ValorIpca { get; set; }
        //public double ValorSelic { get; set; }

        public string Mes { get; set; }
        public double ValorCdi { get; set; }
        public double ValorIpca { get; set; }
        public double ValorSelic { get; set; }
    }
}
