namespace helper_api_dotnet_o6_investimento.Domain.Interfaces
{
    public interface IBancoCentralApiRepository
    {
        Task<List<DataValor>> ConsultarCdiMensal(string dataInicial, string dataFinal);
        Task<List<DataValor>> ConsultarSelicMensal(string dataInicial, string dataFinal);
    }
}
