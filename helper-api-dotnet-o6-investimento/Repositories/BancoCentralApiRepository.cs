using helper_api_dotnet_o6_investimento.Domain.Interfaces;
using helper_api_dotnet_o6_investimento.Infra;

namespace helper_api_dotnet_o6_investimento.Repositories
{
    public class BancoCentralApiRepository : ApiBase, IBancoCentralApiRepository
    {
        public BancoCentralApiRepository()
        {
            this.BaseUri = "https://api.bcb.gov.br/dados/serie/bcdata.sgs.4391/dados?formato=json";
        }
        public async Task<List<Cdi>> ConsultarCdiMensal(string dataInicial = "01/08/2023", string dataFinal = "01/08/2024")
        {
            //https://api.bcb.gov.br/dados/serie/bcdata.sgs.4391/dados?formato=json&dataInicial=01/08/2023&dataFinal=01/08/2024
            string action = $"&dataInicial={dataInicial}&dataFinal={dataFinal}";
            var result = await this.GetAsync<List<Cdi>>(action);

            return result;
        }
    }
}
