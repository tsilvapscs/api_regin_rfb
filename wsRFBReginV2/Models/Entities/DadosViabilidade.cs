using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for DadosViabilidade
/// </summary>
public class DadosViabilidade
{
    #region Variaveis
    int _areaEstabelecimento = 0;
    int _areaImovel = 0;
    string _bairro = "";
    string _cep = "";
    string _cnaePrincipal = "";
    string[] _cnaeSecundaria;
    string _cnpj = "";
    string _CNPJMatriz = "";
    string _cnpjFilial = "";
    string[] _codEventosViabilidade;
    string _codMunicipio = "";
    string _codStatusViabilidade = "";
    string _codTipoLogradouro = "";
    string _complementoLogradouro = "";
    string _complementoLogradouroRFB = "";
    string[] _cpfSocioPf;
    string[] _nomeSocioPf;
    string _dataFimAnaliseViabilidadeEndereco = "";
    string _dataFimAnaliseViabilidadeNome = "";
    string _dataInicioAnaliseViabilidade = "";
    string _dataValidadeViabilidade = "";
    string _distrito = "";
    string[] _formaAtuacao;
    string _formaAtuacaoStr;
    string _inscricaoImovel = "";
    string _logradouro = "";
    string _naturezaJuridica = "";
    string _nomeEmpresarial = "";
    string _numLogradouro = "";
    string _objetoSocial = "";
    string _protocoloViabilidade = "";
    string _referencia = "";
    string _resultadoViabilidadeEndereco = "";
    string _resultadoViabilidadeNome = "";
    string _tipoInscricao = "";
    string _tipoOrgaoRegistro = "";
    string _tipoUnidade = "";
    string _uf = "";
    string _nire = "";
    string _nireMatriz = "";

    string _horaInicioAnaliseViabilidade = "";
    string _horaFimAnaliseViabilidadeNome = "";
    string _horaFimAnaliseViabilidadeEndereco = "";
    string _cpfSolicitante = "";
    string _indicadorContabilista = "";
    string _indicadorCnpjNomeEmpresarial = "";

    #endregion

    #region Propertis
    public DadosViabilidade()
    {
    }
    public string indicadorCnpjNomeEmpresarial
    {
        get
        {
            return _indicadorCnpjNomeEmpresarial;
        }
        set
        {
            _indicadorCnpjNomeEmpresarial = value;
        }
    }
    public string indicadorContabilista
    {
        get
        {
            return _indicadorContabilista;
        }
        set
        {
            _indicadorContabilista = value;
        }
    }

    public string cpfSolicitante
    {
        get
        {
            return _cpfSolicitante;
        }
        set
        {
            _cpfSolicitante = value;
        }
    }

    public string horaFimAnaliseViabilidadeEndereco
    {
        get
        {
            return _horaFimAnaliseViabilidadeEndereco;
        }
        set
        {
            _horaFimAnaliseViabilidadeEndereco = value;
        }
    }


    public string horaFimAnaliseViabilidadeNome
    {
        get
        {
            return _horaFimAnaliseViabilidadeNome;
        }
        set
        {
            _horaFimAnaliseViabilidadeNome = value;
        }
    }



    //****************************
    public string horaInicioAnaliseViabilidade
    {
        get
        {
            return _horaInicioAnaliseViabilidade;
        }
        set
        {
            _horaInicioAnaliseViabilidade = value;
        }
    }
    public string nireMatriz
    {
        get
        {
            return _nireMatriz;
        }
        set
        {
            _nireMatriz = value;
        }
    }
    public string nire
    {
        get
        {
            return _nire;
        }
        set
        {
            _nire = value;
        }
    }
    public int areaEstabelecimento
    {
        get
        {
            return _areaEstabelecimento;
        }
        set
        {
            _areaEstabelecimento = value;
        }
    }
    public int areaImovel
    {
        get
        {
            return _areaImovel;
        }
        set
        {
            _areaImovel = value;
        }
    }
    public string bairro
    {
        get
        {
            return _bairro;
        }
        set
        {
            _bairro = value;
        }
    }
    public string cep
    {
        get
        {
            return _cep;
        }
        set
        {
            _cep = value;
        }
    }
    public string cnaePrincipal
    {
        get
        {
            return _cnaePrincipal;
        }
        set
        {
            _cnaePrincipal = value;
        }
    }
    public string[] cnaeSecundaria
    {
        get
        {
            return _cnaeSecundaria;
        }
        set
        {
            _cnaeSecundaria = value;
        }
    }
    public string cnpj
    {
        get
        {
            return _cnpj;
        }
        set
        {
            _cnpj = value;
        }
    }
    public string CNPJMatriz
    {
        get
        {
            return _CNPJMatriz;
        }
        set
        {
            _CNPJMatriz = value;
        }
    }

    public string cnpjFilial
    {
        get
        {
            return _cnpjFilial;
        }
        set
        {
            _cnpjFilial = value;
        }
    }
    public string[] codEventosViabilidade
    {
        get
        {
            return _codEventosViabilidade;
        }
        set
        {
            _codEventosViabilidade = value;
        }
    }
    public string codMunicipio
    {
        get
        {
            return _codMunicipio;
        }
        set
        {
            _codMunicipio = value;
        }
    }
    public string codStatusViabilidade
    {
        get
        {
            return _codStatusViabilidade;
        }
        set
        {
            _codStatusViabilidade = value;
        }
    }
    public string codTipoLogradouro
    {
        get
        {
            return _codTipoLogradouro;
        }
        set
        {
            _codTipoLogradouro = value;
        }
    }

    public string complementoLogradouroRFB
    {
        get
        {
            return _complementoLogradouroRFB;
        }
        set
        {
            _complementoLogradouroRFB = value;
        }
    }

    public string complementoLogradouro
    {
        get
        {
            return _complementoLogradouro;
        }
        set
        {
            _complementoLogradouro = value;
        }
    }
    public string[] cpfSocioPf
    {
        get
        {
            return _cpfSocioPf;
        }
        set
        {
            _cpfSocioPf = value;
        }
    }
    public string[] nomeSocioPf
    {
        get
        {
            return _nomeSocioPf;
        }
        set
        {
            _nomeSocioPf = value;
        }
    }
    public string dataFimAnaliseViabilidadeEndereco
    {
        get
        {
            return _dataFimAnaliseViabilidadeEndereco;
        }
        set
        {
            _dataFimAnaliseViabilidadeEndereco = value;
        }
    }
    public string dataFimAnaliseViabilidadeNome
    {
        get
        {
            return _dataFimAnaliseViabilidadeNome;
        }
        set
        {
            _dataFimAnaliseViabilidadeNome = value;
        }
    }
    public string dataInicioAnaliseViabilidade
    {
        get
        {
            return _dataInicioAnaliseViabilidade;
        }
        set
        {
            _dataInicioAnaliseViabilidade = value;
        }
    }
    public string dataValidadeViabilidade
    {
        get
        {
            return _dataValidadeViabilidade;
        }
        set
        {
            _dataValidadeViabilidade = value;
        }
    }
    public string distrito
    {
        get
        {
            return _distrito;
        }
        set
        {
            _distrito = value;
        }
    }
    public string formaAtuacaoStr
    {
        get
        {
            return _formaAtuacaoStr;
        }
        set
        {
            _formaAtuacaoStr = value;
        }
    }
    public string[] formaAtuacao
    {
        get
        {
            return _formaAtuacao;
        }
        set
        {
            _formaAtuacao = value;
        }
    }
    public string inscricaoImovel
    {
        get
        {
            return _inscricaoImovel;
        }
        set
        {
            _inscricaoImovel = value;
        }
    }
    public string logradouro
    {
        get
        {
            return _logradouro;
        }
        set
        {
            _logradouro = value;
        }
    }
    public string naturezaJuridica
    {
        get
        {
            return _naturezaJuridica;
        }
        set
        {
            _naturezaJuridica = value;
        }
    }
    public string nomeEmpresarial
    {
        get
        {
            return _nomeEmpresarial;
        }
        set
        {
            _nomeEmpresarial = value;
        }
    }
    public string numLogradouro
    {
        get
        {
            return _numLogradouro;
        }
        set
        {
            _numLogradouro = value;
        }
    }
    public string objetoSocial
    {
        get
        {
            return _objetoSocial;
        }
        set
        {
            _objetoSocial = value;
        }
    }
    public string protocoloViabilidade
    {
        get
        {
            return _protocoloViabilidade;
        }
        set
        {
            _protocoloViabilidade = value;
        }
    }
    public string referencia
    {
        get
        {
            return _referencia;
        }
        set
        {
            _referencia = value;
        }
    }
    public string resultadoViabilidadeEndereco
    {
        get
        {
            return _resultadoViabilidadeEndereco;
        }
        set
        {
            _resultadoViabilidadeEndereco = value;
        }
    }
    public string resultadoViabilidadeNome
    {
        get
        {
            return _resultadoViabilidadeNome;
        }
        set
        {
            _resultadoViabilidadeNome = value;
        }
    }
    public string tipoInscricao
    {
        get
        {
            return _tipoInscricao;
        }
        set
        {
            _tipoInscricao = value;
        }
    }
    public string tipoOrgaoRegistro
    {
        get
        {
            return _tipoOrgaoRegistro;
        }
        set
        {
            _tipoOrgaoRegistro = value;
        }
    }
    public string tipoUnidade
    {
        get
        {
            return _tipoUnidade;
        }
        set
        {
            _tipoUnidade = value;
        }
    }
    public string uf
    {
        get
        {
            return _uf;
        }
        set
        {
            _uf = value;
        }
    }

    #endregion

    #region Outros 
    public static string TiraAcento(string pValue)
    {
        return TiraAcento(pValue, "");
    }
    public static string TiraAcento(string pValue, string Tipo)
    {

        string pResult = pValue.Trim();

        pResult = pResult.Replace("\n", " ");
        pResult = pResult.Replace("\t", " ");

        pResult = pResult.Replace('À', 'A');
        pResult = pResult.Replace('Á', 'A');
        pResult = pResult.Replace('Â', 'A');
        pResult = pResult.Replace('Ã', 'A');
        pResult = pResult.Replace('Ä', 'A');

        pResult = pResult.Replace('à', 'a');
        pResult = pResult.Replace('á', 'a');
        pResult = pResult.Replace('â', 'a');
        pResult = pResult.Replace('ã', 'a');
        pResult = pResult.Replace('ä', 'a');

        pResult = pResult.Replace('È', 'E');
        pResult = pResult.Replace('É', 'E');
        pResult = pResult.Replace('Ê', 'E');
        pResult = pResult.Replace('Ë', 'E');

        pResult = pResult.Replace('è', 'e');
        pResult = pResult.Replace('é', 'e');
        pResult = pResult.Replace('ê', 'e');
        pResult = pResult.Replace('ë', 'e');

        pResult = pResult.Replace('Ì', 'I');
        pResult = pResult.Replace('Í', 'I');
        pResult = pResult.Replace('Î', 'I');
        pResult = pResult.Replace('Ï', 'I');

        pResult = pResult.Replace('ì', 'i');
        pResult = pResult.Replace('í', 'i');
        pResult = pResult.Replace('î', 'i');
        pResult = pResult.Replace('ï', 'i');

        pResult = pResult.Replace('Ò', 'O');
        pResult = pResult.Replace('Ó', 'O');
        pResult = pResult.Replace('Ô', 'O');
        pResult = pResult.Replace('Õ', 'O');
        pResult = pResult.Replace('Ö', 'O');

        pResult = pResult.Replace('ò', 'o');
        pResult = pResult.Replace('ó', 'o');
        pResult = pResult.Replace('ô', 'o');
        pResult = pResult.Replace('õ', 'o');
        pResult = pResult.Replace('ö', 'o');

        pResult = pResult.Replace('Ù', 'U');
        pResult = pResult.Replace('Ú', 'U');
        pResult = pResult.Replace('Û', 'U');
        pResult = pResult.Replace('Ü', 'U');

        pResult = pResult.Replace('ù', 'u');
        pResult = pResult.Replace('ú', 'u');
        pResult = pResult.Replace('û', 'u');
        pResult = pResult.Replace('ü', 'u');

        pResult = pResult.Replace('Ç', 'C');
        pResult = pResult.Replace('ç', 'c');

        pResult = pResult.Replace('ñ', 'n');
        pResult = pResult.Replace('Ñ', 'N');

        pResult = Regex.Replace(pResult, "[^0-9a-zA-Z.&,'@/ ]+", " ");


        if (Tipo == "Complemento")
        {
            pResult = pResult.Replace(',', ' ');
            pResult = pResult.Replace('&', ' ');
            pResult = pResult.Replace("'", " ");
            pResult = pResult.Replace("*", " ");
            pResult = pResult.Trim().Replace("-", " ");
        }

        pResult = pResult.Replace(';', ' ');
        pResult = pResult.Replace("  ", " ");
        
        //pResult = pResult.Replace("   ", " ");

        //string pattern = "^(\r\n)*";
        //string replacement = "";
        //Regex rgx = new Regex(pattern);
        //pResult = rgx.Replace(pResult, replacement);

        //String pattern2 = @"[\\r|\\n|\\t]";
        //String replaceValue = String.Empty;
        //pResult = Regex.Replace(pResult, pattern2, replaceValue);



        return pResult;


    }

    public static string TiraAcentoNomeEmpresarial(string pValue, string natureza)
    {

        string pResult = pValue;

        pResult = pResult.Replace("\n", " ");
        pResult = pResult.Replace("\t", " ");

        pResult = pResult.Replace('À', 'A');
        pResult = pResult.Replace('Á', 'A');
        pResult = pResult.Replace('Â', 'A');
        pResult = pResult.Replace('Ã', 'A');
        pResult = pResult.Replace('Ä', 'A');

        pResult = pResult.Replace('à', 'a');
        pResult = pResult.Replace('á', 'a');
        pResult = pResult.Replace('â', 'a');
        pResult = pResult.Replace('ã', 'a');
        pResult = pResult.Replace('ä', 'a');

        pResult = pResult.Replace('È', 'E');
        pResult = pResult.Replace('É', 'E');
        pResult = pResult.Replace('Ê', 'E');
        pResult = pResult.Replace('Ë', 'E');

        pResult = pResult.Replace('è', 'e');
        pResult = pResult.Replace('é', 'e');
        pResult = pResult.Replace('ê', 'e');
        pResult = pResult.Replace('ë', 'e');

        pResult = pResult.Replace('Ì', 'I');
        pResult = pResult.Replace('Í', 'I');
        pResult = pResult.Replace('Î', 'I');
        pResult = pResult.Replace('Ï', 'I');

        pResult = pResult.Replace('ì', 'i');
        pResult = pResult.Replace('í', 'i');
        pResult = pResult.Replace('î', 'i');
        pResult = pResult.Replace('ï', 'i');

        pResult = pResult.Replace('Ò', 'O');
        pResult = pResult.Replace('Ó', 'O');
        pResult = pResult.Replace('Ô', 'O');
        pResult = pResult.Replace('Õ', 'O');
        pResult = pResult.Replace('Ö', 'O');

        pResult = pResult.Replace('ò', 'o');
        pResult = pResult.Replace('ó', 'o');
        pResult = pResult.Replace('ô', 'o');
        pResult = pResult.Replace('õ', 'o');
        pResult = pResult.Replace('ö', 'o');

        pResult = pResult.Replace('Ù', 'U');
        pResult = pResult.Replace('Ú', 'U');
        pResult = pResult.Replace('Û', 'U');
        pResult = pResult.Replace('Ü', 'U');

        pResult = pResult.Replace('ù', 'u');
        pResult = pResult.Replace('ú', 'u');
        pResult = pResult.Replace('û', 'u');
        pResult = pResult.Replace('ü', 'u');

        pResult = pResult.Replace('Ç', 'C');
        pResult = pResult.Replace('ç', 'c');

        pResult = pResult.Replace('ñ', 'n');
        pResult = pResult.Replace('Ñ', 'N');

        /*
            isto aqui e somente para o RJ, ja que eles nao quiseram colocar que a pessoa tem que nao possa digitar
         * ME e EPP no nome com isso da problemas ao tentar retirar o ME e EPP quando tem - 
         */
        if ((natureza == "2305" || natureza == "2313") && ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            pResult = Regex.Replace(pResult, "[^0-9a-zA-Z.&,'@/ ]+", " ");
        else
            pResult = Regex.Replace(pResult, "[^0-9a-zA-Z.&,'-@/ ]+", " ");



        pResult = pResult.Replace(';', ' ');
        pResult = pResult.Replace("  ", " ");


        return pResult;


    }
    public static string getLinhaComplemento(string complemento, int linha)
    {
        int TotalLinha = 20 * linha;
        string Result = "";
        if (complemento.Length > TotalLinha)
        {
            if (complemento.Length <= 20 * (linha + 1))
                Result = complemento.Substring(TotalLinha, complemento.Length - TotalLinha);
            else
                Result = complemento.Substring(TotalLinha, 20);
        }
        //if (Result.Length > 0)
        //{
        //    Result = "      " + Result;
        //}
        return Result;
    }

    public bool isEventoStr(string EventoViabilidade, string eventoRFB)
    {
        string[] eventos = eventoRFB.Split(',');
        foreach (string pValueEventoRFB in eventos)
        {
            if (EventoViabilidade.Trim() == pValueEventoRFB.Trim())
            {
                return true;
            }
        }
        return false;
    }

    public bool isEvento(string[] ArrayEventoViabilidade, string eventoRFB)
    {
        string[] eventos = eventoRFB.Split(',');
        if (ArrayEventoViabilidade.Length > 0)
        {
            foreach (string pValue in ArrayEventoViabilidade)
            {
                if (pValue != null)
                {
                    foreach (string pValueEventoRFB in eventos)
                    {
                        if (pValue.Trim() == pValueEventoRFB.Trim())
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    public bool isTipoUnidade(string[] ArrayTipoUnidadeViabilidade, string tipoUnidade)
    {
        string[] eventos = tipoUnidade.Split(',');
        if (ArrayTipoUnidadeViabilidade.Length > 0)
        {
            foreach (string pValue in ArrayTipoUnidadeViabilidade)
            {
                if (pValue != null)
                {
                    foreach (string pValueEventoRFB in eventos)
                    {
                        if (pValue.Trim() == pValueEventoRFB.Trim())
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
    #endregion
}
