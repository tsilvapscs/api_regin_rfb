using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using WsRFBReginV2.Models;

/// <summary>
/// Summary description for TipoLogradouroRFB
/// </summary>
public class TipoLogradouroRFB
{
    #region  Property Declarations

   
    protected List<Tipo> _lista;
    public List<Tipo> Lista
    {
        get { return _lista; }
    }
    #endregion

    // Property ******************* 
    #region Class Member Declarations

    public class Tipo
    {
        protected string _cod_rfb = "";
        protected string _cod_regin = "";
        public string Cod_rfb
        {
            get { return _cod_rfb; }
            set { _cod_rfb = value; }
        }
        public string Cod_regin
        {
            get { return _cod_regin; }
            set { _cod_regin = value; }
        }
        
    }
   
    #endregion
    public TipoLogradouroRFB() 
    {
        this.Populate();
    }

    private void Populate()
    {
        DataTable dt = GlobalV1.BuscarTipoLogradouro();
        _lista = new List<Tipo>();

        foreach (DataRow r in dt.Rows)
        {
            Tipo tipo = new Tipo();
            tipo.Cod_rfb = r["Cod_rfb"].ToString();
            tipo.Cod_regin = r["Cod_regin"].ToString();
            _lista.Add(tipo);
        }
    }
}