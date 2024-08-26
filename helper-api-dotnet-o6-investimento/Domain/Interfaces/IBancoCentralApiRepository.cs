namespace helper_api_dotnet_o6_investimento.Domain.Interfaces
{
    public interface IBancoCentralApiRepository
    {
        Task<List<Cdi>> ConsultarCdiMensal(string dataInicial, string dataFinal);
    }
}
