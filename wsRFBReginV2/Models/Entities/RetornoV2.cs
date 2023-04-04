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
public class RetornoV2
{
	public RetornoV2()
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

   // private WsServices07RFB.redesim _oWsResponse07;
   // protected WsServices15RFB.serviceResponse _oWsResponse15;
  //  protected WsServices17RFB.serviceResponse _oWsResponse17;


    //public WsServices07RFB.redesim oWsResponse07
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

    
}
