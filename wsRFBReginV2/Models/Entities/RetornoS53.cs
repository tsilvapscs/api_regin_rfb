using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Retorno
/// </summary>
public class RetornoS53
{
    public RetornoS53()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    protected string _codretorno = "00";
    protected string _status = "NOK";
    protected incompRegistroIntegradorEstadual[] _mensagemRetorno;
    protected string _descricao = "";
    protected string _nire = "";
    protected string _cnpj = "";
    protected string _XmlRFB = "";
    protected string _XmlResponseRFB = "";
    protected string _recibo = "";
    protected string _identificacao = "";
    protected string _requerimento = "";

    public string Requerimento
    {
        get { return _requerimento; }
        set { _requerimento = value; }
    }
    public string Recibo
    {
        get { return _recibo; }
        set { _recibo = value; }
    }

    public string Identificacao
    {
        get { return _identificacao; }
        set { _identificacao = value; }
    }
    public string codretorno
    {
        get { return _codretorno; }
        set { _codretorno = value; }
    }
    public string XmlResponseRFB
    {
        get { return _XmlResponseRFB; }
        set { _XmlResponseRFB = value; }
    }

    public string Cnpj
    {
        get { return _cnpj; }
        set { _cnpj = value; }
    }

    public string Nire
    {
        get { return _nire; }
        set { _nire = value; }
    }
    public string status
    {
        get { return _status; }
        set { _status = value; }
    }

    public incompRegistroIntegradorEstadual[] mensagemRetorno
    {
        get { return _mensagemRetorno; }
        set { _mensagemRetorno = value; }
    }

    public string descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }
    public string XmlRFB
    {
        get { return _XmlRFB; }
        set { _XmlRFB = value; }
    }
}
