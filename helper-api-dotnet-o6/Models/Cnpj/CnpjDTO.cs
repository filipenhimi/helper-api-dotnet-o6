    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text.Json.Serialization;
    namespace helper_api_dotnet_o6.Models.Cnpj
    {
    public class CnpjDTO
    {
    public CnpjDTO(string cnpj, string nome,string dataFundacao, EnderecoDto endereco) 
    {
    this.cnpj = cnpj;
    this.nome = nome;
    this.dataFundacao = dataFundacao;
    this.endereco = endereco;

    }
    public string cnpj { get; set; }
    public string nome { get; set; }
    public string dataFundacao { get; set; } 

    public EnderecoDto endereco { get; set; } 

    }
    }