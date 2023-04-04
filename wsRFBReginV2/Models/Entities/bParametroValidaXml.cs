using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using psc.Receita;

/// <summary>
/// Summary description for bParametro
/// </summary>
public class bParametroValidaXml
{
    #region Propriedades
    private string _cnpj = string.Empty;
    private string _codigo = string.Empty;
    private string _descricao = string.Empty;
    private string _valor = string.Empty;
    private List<bParametroValidaXml> _listaParam = new List<bParametroValidaXml>();

    public List<bParametroValidaXml> ListaParam
    {
        get { return _listaParam; }
        set { _listaParam = value; }
    }

    public string cnpj
    {
        get { return _cnpj; }
        set { _cnpj = value; }
    }
    public string descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }
    public string codigo
    {
        get { return _codigo; }
        set { _codigo = value; }
    }
    public string valor
    {
        get { return _valor; }
        set { _valor = value; }
    }

    #endregion


    #region Constructors
    public bParametroValidaXml()
    {

        //InitClass();
    }
    public bParametroValidaXml(string pCNPJ, Valores codParametro)
        : this()
    {
        _cnpj = pCNPJ;
        _codigo = ((int)codParametro).ToString();
        Populate();
    }
    public bParametroValidaXml(string pCNPJ)
        : this()
    {
        _cnpj = pCNPJ;
        Populate();
    }
    #endregion

    public enum Valores : int
    {
        URL_CHAMADAWS11 = 1,
        VALIDA_QSA_SEM_SOCIO = 2,
        VALIDA_CNAE_PRINCIPAL_DIFERENTE_RFB = 3,
        VALIDA_CNAE_DIFERENTE_RFB_OR = 4,
        VALIDA_QSA_SEM_REPRESENTANTE = 5,
        VALIDA_FAIXA_DE_CEP_ENDERECO = 6,
        VALIDA_CPFCNPJ_QSA = 7,
        VALIDA_NOME_EMPRESARIAL = 8,
        VALIDA_ENDERECO_EMPRESA_MUNICIPIO = 9,
        VALIDA_ENDERECO_EMPRESA_BAIRRO = 10,
        VALIDA_ENDERECO_EMPRESA_LOGRADOURO = 11,
        VALIDA_EMPRESA_PORTE = 20,
        VALIDA_TIPO_MATRIZ_FILIAL_RFB_JUNTA = 21,
        VALIDA_NATUREZA_JURIDICA = 25,


    }

    #region Implementação

    public string getValor(Valores pCodigo)
    {
        string ret = "";
        foreach (bParametroValidaXml obj in _listaParam)
        {
            if (obj.codigo == ((int)pCodigo).ToString())
            {
                ret = obj.valor;
                break;
            }
        }

        return ret;
    }

    //public string getValor(int pCodigo)
    //{
    //    string ret = "";
    //    foreach (bParametro obj in _listaParam)
    //    {
    //        if (obj.codigo == pCodigo.ToString())
    //        {
    //            ret = obj.valor;
    //            break;
    //        }
    //    }

    //    return ret;
    //}

    public void Populate()
    {
        _listaParam = new List<bParametroValidaXml>();

        System.Data.DataTable dtOrgaoReg = dHelperQuery.GetParametros(_cnpj, _codigo);
        foreach (System.Data.DataRow row in dtOrgaoReg.Rows)
        {
            bParametroValidaXml temp = new bParametroValidaXml();

            temp.cnpj = row["cnpj"].ToString();
            temp.descricao = row["descricao"].ToString();
            temp.codigo = row["codigo"].ToString();
            temp.valor = row["mensagem"].ToString();

            _listaParam.Add(temp);
        }
    }
    #endregion
}
