using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using psc.Ruc.Tablelas.ConnectionBase;
using System.Text;
//using psc.Receita.ConnectionBase;

/// <summary>
/// Summary description for PSC_CONTROL_DETALHE
/// </summary>
public class PSC_CONTROL_DETALHE : DBInteractionBaseORACLE
{
    public PSC_CONTROL_DETALHE()
    {
    }

    // Variables ******************* 
    #region  Property Declarations
    protected decimal _pcd_pcq_id = 0;
    protected decimal _pcd_pcc_id = int.MinValue;
    protected string _pcd_motivo = "";


    protected string _pcq_protocolo = "";
    protected string _pcq_nire = "";
    protected string _pcq_cnpj = "";
    protected string _pcq_cnpj_or = "";

    #endregion

    // Property ******************* 
    #region Class Member Declarations
    public decimal pcd_pcq_id
    {
        get { return _pcd_pcq_id; }
        set { _pcd_pcq_id = value; }
    }
    public decimal pcd_pcc_id
    {
        get { return _pcd_pcc_id; }
        set { _pcd_pcc_id = value; }
    }
    public string pcd_motivo
    {
        get { return _pcd_motivo; }
        set { _pcd_motivo = value; }
    }
    public string pcq_protocolo
    {
        get { return _pcq_protocolo; }
        set { _pcq_protocolo = value; }
    }
    public string pcq_nire
    {
        get { return _pcq_nire; }
        set { _pcq_nire = value; }
    }
    public string pcq_cnpj
    {
        get { return _pcq_cnpj; }
        set { _pcq_cnpj = value; }
    }
    public string pcq_cnpj_or
    {
        get { return _pcq_cnpj_or; }
        set { _pcq_cnpj_or = value; }
    }
    #endregion

    public string Update()
    {
        StringBuilder Sql = new StringBuilder();
        OracleCommand cmdToExecute = new OracleCommand();
        cmdToExecute.CommandType = CommandType.StoredProcedure;

        cmdToExecute.Connection = _mainConnectionORACLE;


        // Codigo dbParameter ******************* 
        cmdToExecute.Parameters.Add(new OracleParameter("v_pcd_pcq_id", OracleType.Number, 0, ParameterDirection.InputOutput, true, 0, 0, "", DataRowVersion.Proposed, _pcd_pcq_id));
        cmdToExecute.Parameters.Add(new OracleParameter("v_pcd_pcc_id", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pcd_pcc_id));
        cmdToExecute.Parameters.Add(new OracleParameter("v_pcd_motivo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pcd_motivo));
        cmdToExecute.Parameters.Add(new OracleParameter("v_pcq_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pcq_protocolo));
        cmdToExecute.Parameters.Add(new OracleParameter("v_pcq_nire", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pcq_nire));
        cmdToExecute.Parameters.Add(new OracleParameter("v_pcq_cnpj", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pcq_cnpj));
        cmdToExecute.Parameters.Add(new OracleParameter("v_pcq_cnpj_or", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pcq_cnpj_or));

        cmdToExecute.CommandText = "pkg_control_qualidade.psc_control_detalhe_update";
        cmdToExecute.Connection = _mainConnectionORACLE;
        try
        {
            if (_mainConnectionIsCreatedLocal)
            {
                // Open connection.
                _mainConnectionORACLE.Open();
            }
            else
            {
                if (_mainConnectionProviderORACLE.IsTransactionPending)
                {
                    cmdToExecute.Transaction = _mainConnectionProviderORACLE.CurrentTransaction;
                }
            }

            // Execute query.
            cmdToExecute.ExecuteNonQuery();

            string nroId = cmdToExecute.Parameters["v_pcd_pcq_id"].Value.ToString();

            return nroId;

        }
        catch (Exception ex)
        {
            // some error occured. Bubble it to caller and encapsulate Exception object
            throw ex;
        }
        finally
        {
            if (_mainConnectionIsCreatedLocal)
            {
                // Close connection.
                _mainConnectionORACLE.Close();
            }
            cmdToExecute.Dispose();
        }
    }

}
