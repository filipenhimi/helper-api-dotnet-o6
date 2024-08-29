using helper_api_dotnet_o6_investimento.Domain.Interfaces;
using helper_api_dotnet_o6_investimento.Infra;

namespace helper_api_dotnet_o6_investimento.Repositories
{
    public class BancoCentralApiRepository : ApiBase, IBancoCentralApiRepository
    {
        public BancoCentralApiRepository()
        {
            this.BaseUri = "https://api.bcb.gov.br/dados/serie/bcdata.sgs.";
        }
        public async Task<List<DataValor>> ConsultarCdiMensal(string dataInicial = "01/08/2023", string dataFinal = "01/08/2024")
        {
            string action = $"4391/dados?formato=json&dataInicial={dataInicial}&dataFinal={dataFinal}";
            return await GetAsync<List<DataValor>>(action);
        }

        public async Task<List<DataValor>> ConsultarIpcaMensal(string dataInicial, string dataFinal)
        {
            string action = $"433/dados?formato=json&dataInicial={dataInicial}&dataFinal={dataFinal}";
            return await GetAsync<List<DataValor>>(action);
        }

        public async Task<List<DataValor>> ConsultarSelicMensal(string dataInicial, string dataFinal)
        {
            string action = $"4390/dados?formato=json&dataInicial={dataInicial}&dataFinal={dataFinal}";
            return await GetAsync<List<DataValor>>(action);
        }
    }
}
