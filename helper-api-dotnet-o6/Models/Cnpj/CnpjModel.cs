using System;
using System.Collections.Generic;

namespace helper_api_dotnet_o6.Models.Cnpj
{
    public class CnaesSecundario
    {
        public int codigo { get; set; }
        public string descricao { get; set; }
    }

    public class Qsa
    {
        public object pais { get; set; }
        public string nome_socio { get; set; }
        public object codigo_pais { get; set; }
        public string faixa_etaria { get; set; }
        public string cnpj_cpf_do_socio { get; set; }
        public string qualificacao_socio { get; set; }
        public int codigo_faixa_etaria { get; set; }
        public string data_entrada_sociedade { get; set; }
        public int identificador_de_socio { get; set; }
        public string cpf_representante_legal { get; set; }
        public string nome_representante_legal { get; set; }
        public int codigo_qualificacao_socio { get; set; }
        public string qualificacao_representante_legal { get; set; }
        public int codigo_qualificacao_representante_legal { get; set; }
    }

    public class Root
    {
        public string uf { get; set; }
        public string cep { get; set; }
        public List<Qsa> qsa { get; set; }
        public string cnpj { get; set; }
        public object pais { get; set; }
        public string email { get; set; }
        public string porte { get; set; }
        public string bairro { get; set; }
        public string numero { get; set; }
        public string ddd_fax { get; set; }
        public string municipio { get; set; }
        public string logradouro { get; set; }
        public int cnae_fiscal { get; set; }
        public object codigo_pais { get; set; }
        public string complemento { get; set; }
        public int codigo_porte { get; set; }
        public string razao_social { get; set; }
        public string nome_fantasia { get; set; }
        public long capital_social { get; set; }
        public string ddd_telefone_1 { get; set; }
        public string ddd_telefone_2 { get; set; }
        public object opcao_pelo_mei { get; set; }
        public string descricao_porte { get; set; }
        public int codigo_municipio { get; set; }
        public List<CnaesSecundario> cnaes_secundarios { get; set; }
        public string natureza_juridica { get; set; }
        public string situacao_especial { get; set; }
        public object opcao_pelo_simples { get; set; }
        public int situacao_cadastral { get; set; }
        public object data_opcao_pelo_mei { get; set; }
        public object data_exclusao_do_mei { get; set; }
        public string cnae_fiscal_descricao { get; set; }
        public int codigo_municipio_ibge { get; set; }
        public string data_inicio_atividade { get; set; }
        public object data_situacao_especial { get; set; }
        public object data_opcao_pelo_simples { get; set; }
        public string data_situacao_cadastral { get; set; }
        public string nome_cidade_no_exterior { get; set; }
        public int codigo_natureza_juridica { get; set; }
        public object data_exclusao_do_simples { get; set; }
        public int motivo_situacao_cadastral { get; set; }
        public string ente_federativo_responsavel { get; set; }
        public int identificador_matriz_filial { get; set; }
        public int qualificacao_do_responsavel { get; set; }
        public string descricao_situacao_cadastral { get; set; }
        public string descricao_tipo_de_logradouro { get; set; }
        public string descricao_motivo_situacao_cadastral { get; set; }
        public string descricao_identificador_matriz_filial { get; set; }
    }}