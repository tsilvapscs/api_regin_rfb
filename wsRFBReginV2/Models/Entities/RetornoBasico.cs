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
public class Retorno
{
	public Retorno()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected string _status = "NOK";
    protected string _codretorno = "00";
    protected string _descricao = "";
    protected string _regisroDiferente = "2";
    protected string _XmlDBE = "";
    protected string _nire = "";
    protected string _cnpj = "";
    protected string _recibo = "";
    protected string _identificacao = "";
    protected string _nomeEmpresarial = "";
    protected string _indicadorCNPJNome = "";
    protected WsRFBReginV2.WsServices09RFB.consultaCPFResponse _oCPFResponse;
    protected WsRFBReginV2.WsServices35RFB.serviceResponse _oWs35Response;
    protected WsRFBReginV2.WsServices11RFB.retornoWS11Redesim _oCNPJResponse;
    //protected WsServices07RFB.serviceResponse _oWsResponse07;
    //protected WsServices15RFB.serviceResponse _oWsResponse15;
    //protected WsServices17RFB.serviceResponse _oWsResponse17;


    //public WsServices07RFB.serviceResponse oWsResponse07
    //{
    //    get
    //    {
    //        return _oWsResponse07;
    //    }
    //    set { _oWsResponse07 = value; }
    //}

    //public WsServices15RFB.serviceResponse oWsResponse15
    //{
    //    get
    //    {
    //        return _oWsResponse15;
    //    }
    //    set { _oWsResponse15 = value; }
    //}

    //public WsServices17RFB.serviceResponse oWsResponse17
    //{
    //    get
    //    {
    //        return _oWsResponse17;
    //    }
    //    set { _oWsResponse17 = value; }
    //}
    public string indicadorCNPJNome
    {
        get { return _indicadorCNPJNome; }
        set { _indicadorCNPJNome = value; }
    }
    public string nomeEmpresarial
    {
        get { return _nomeEmpresarial; }
        set { _nomeEmpresarial = value; }
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

    public string regisroDiferente
    {
        get { return _regisroDiferente; }
        set { _regisroDiferente = value; }
    }

    public string XmlDBE
    {
        get { return _XmlDBE; }
        set { _XmlDBE = value; }
    }

    

    public string status
    {
        get { return _status; }
        set { _status = value; }
    }

    public string codretorno
    {
        get { return _codretorno; }
        set { _codretorno = value; }
    }

    public string descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }

    public WsRFBReginV2.WsServices09RFB.consultaCPFResponse oCPFResponse
    {
        get 
        { 
            return _oCPFResponse; 
        }
        set { _oCPFResponse = value; }
    }

    public WsRFBReginV2.WsServices35RFB.serviceResponse oWs35Response
    {
        get
        {
            return _oWs35Response;
        }
        set { _oWs35Response = value; }
    }

    public WsRFBReginV2.WsServices11RFB.retornoWS11Redesim oCNPJResponse
    {
        get
        {
            return _oCNPJResponse;
        }
        set { _oCNPJResponse = value; }
    }

 
}
