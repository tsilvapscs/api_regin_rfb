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
public class RetornoBasico
{
	public RetornoBasico()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected string _status = "NOK";
    protected string _codretorno = "00";
    protected string _descricao = "";
    protected string _url = "";
    protected string _XmlDBE = "";
    protected string _nire = "";
    protected string _cnpj = "";
   
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
    public string url
    {
        get { return _url; }
        set { _url = value; }
    }
}
