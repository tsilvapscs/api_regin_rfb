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
/// Summary description for Entrada
/// </summary>
public class Entrada
{
	public Entrada()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected string _CpfCnpj = "";
    protected string _Usuario = "";
    protected string _Senha = "";
    protected string _CnpjOrgaoConsulta = "";

    public string CnpjOrgaoConsulta
    {
        get { return _CnpjOrgaoConsulta; }
        set { _CnpjOrgaoConsulta = value; }
    }

    public string Senha
    {
        get { return _Senha; }
        set { _Senha = value; }
    }

    public string Usuario
    {
        get { return _Usuario; }
        set { _Usuario = value; }
    }

    public string CpfCnpj
    {
        get { return _CpfCnpj; }
        set { _CpfCnpj = value; }
    }

}
