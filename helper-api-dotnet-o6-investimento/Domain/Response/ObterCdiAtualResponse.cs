using System.Globalization;

namespace helper_api_dotnet_o6_investimento.Domain.Response
{
    public class ObterCdiAtualResponse
    {
        /// <summary>
        /// Valor do Cdi no último mês.
        /// </summary>
        public string CdiUltimoMes { get; set; } = "";
        /// <summary>
        /// Média do Cdi com base nos últimos 12 meses.
        /// </summary>
        public string CdiMedioMensal { get; set; } = "";
        /// <summary>
        /// Valor do Cdi no último ano.
        /// </summary>
        public string CdiUltimosAno { get; set; } = "";

        public void PreencherValoresCdi(List<Cdi> cdis)
        {
            if (cdis != null && cdis.Any())
            {
                CdiUltimosAno = cdis.Sum(r => r.Valor).ToString();
                CdiMedioMensal = cdis.Average(r => r.Valor).ToString();
                CdiUltimoMes = cdis.OrderBy(r => DateTime.ParseExact(r.Data, "dd/MM/yyyy", CultureInfo.InvariantCulture)).Last().Valor.ToString();
            }
        }
    }
}
