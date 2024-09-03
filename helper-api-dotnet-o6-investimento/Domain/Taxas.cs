using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helper_api_dotnet_o6_investimento.Domain
{
    public class Taxas
    {
        public Taxas()
        {
            
        }

        public Taxas(double selic, double ipca, double cdi)
        {
            Selic = selic;
            Ipca = ipca;
            Cdi = cdi;
        }

        public double Selic { get; set; }
        public double Ipca { get; set; }
        public double Cdi { get; set; }
    }
}
