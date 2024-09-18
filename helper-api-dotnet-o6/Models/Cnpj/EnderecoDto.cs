    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace helper_api_dotnet_o6.Models.Cnpj
    {
    public class EnderecoDto
    {

    public EnderecoDto(string rua, string numero, string bairro, string cidade, string estado, string cep) 
    {
    this.rua = rua;
    this.numero = numero;
    this.bairro = bairro;
    this.cidade = cidade;
    this.estado = estado;
    this.cep = cep;

    }
    public string rua { get; set; }  


    public string numero { get; set; }  


    public string bairro { get; set; }  


    public string cidade { get; set; }  


    public string estado { get; set; } 

    public string cep { get; set; }  
    }




    }