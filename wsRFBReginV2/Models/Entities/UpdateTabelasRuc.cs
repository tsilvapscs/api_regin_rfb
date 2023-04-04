using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using psc.Receita.ConnectionBase;
using System.Data.OracleClient;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for UpdateTabelasRuc
/// </summary>
public class UpdateTabelasRuc : DBInteractionBaseORACLE
{
    public UpdateTabelasRuc()
    {

        //using (ConnectionProviderORACLE cp = new ConnectionProviderORACLE())
        //{
        //    cp.OpenConnection();
        //    cp.BeginTransaction();
        //    using (UpdateTabelasRuc p = new UpdateTabelasRuc())
        //    {
        //        p.MainConnectionProvider = cp;
        //        //p.UpdatePSC_PROTOCOLO();

                
        //        cp.CommitTransaction();
        //    }
        //}
    }
    public void UpdateOutrosCamposPSC_Protocolo(string _PRO_PROTOCOLO, string _PRO_ORIGEM_DADO, string _PRO_VPV_COD_PROTOCOLO, string _PRO_NR_DBE, string _PRO_CNPJ_ORG_REG, string _PRO_NR_UNICO)
    {
        OracleCommand cmdToExecute = new OracleCommand();

        StringBuilder Sql = new StringBuilder();

        Sql.Append("Update	PSC_PROTOCOLO ");
        Sql.Append("Set		PRO_ORIGEM_DADO = :v_PRO_ORIGEM_DADO, ");
        Sql.Append("        PRO_NR_DBE = :v_PRO_NR_DBE, ");
        Sql.Append("        PRO_VPV_COD_PROTOCOLO = :v_PRO_VPV_COD_PROTOCOLO, ");
        Sql.Append("        PRO_CNPJ_ORG_REG = :v_PRO_CNPJ_ORG_REG, ");
        Sql.Append("        PRO_NR_UNICO = :v_PRO_NR_UNICO ");
        
        Sql.Append("Where	pro_protocolo = :v_PRO_PROTOCOLO");

        cmdToExecute.CommandText = Sql.ToString();
        cmdToExecute.CommandType = CommandType.Text;
        // Use base class' connection object
        cmdToExecute.Connection = _mainConnectionORACLE;

        try
        {
            cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_ORIGEM_DADO", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _PRO_ORIGEM_DADO));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_NR_DBE", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _PRO_NR_DBE));
            cmdToExecute.Parameters.Add(new OracleParameter("V_PRO_VPV_COD_PROTOCOLO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _PRO_VPV_COD_PROTOCOLO));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_PROTOCOLO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _PRO_PROTOCOLO));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_CNPJ_ORG_REG", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _PRO_CNPJ_ORG_REG));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_NR_UNICO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _PRO_NR_UNICO));

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
    public void UpdatePSC_PROTOCOLO(string _pRO_PROTOCOLO, 
                        string _pRO_TMU_TUF_UF, 
                        decimal _pRO_TMU_COD_MUN, 
                        decimal _pRO_TIP_OPERACAO, 
                        string _pRo_PROTOCOLO_VIABILIDADE, 
                        string _PRO_CNPJ_ORG_REG)
    {
        OracleCommand cmdToExecute = new OracleCommand();
        cmdToExecute.CommandText = "PKG_JUCESC_DATA.PSC_PROTOCOLO_update";
        cmdToExecute.CommandType = CommandType.StoredProcedure;
        // Use base class' connection object
        cmdToExecute.Connection = _mainConnectionORACLE;

        try
        {
            DateTime _PRO_FEC_INC = DateTime.Now;
            cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _pRO_PROTOCOLO));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_STATUS", OracleType.Number, 22, ParameterDirection.Input, true, 22, 0, "", DataRowVersion.Proposed, 1));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_FEC_INC", OracleType.DateTime, 7, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _PRO_FEC_INC));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_TMU_TUF_UF", OracleType.VarChar, 2, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pRO_TMU_TUF_UF));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_TMU_COD_MUN", OracleType.Number, 22, ParameterDirection.Input, true, 22, 0, "", DataRowVersion.Proposed, _pRO_TMU_COD_MUN));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_TIP_OPERACAO", OracleType.Number, 22, ParameterDirection.Input, true, 22, 0, "", DataRowVersion.Proposed, _pRO_TIP_OPERACAO));
            cmdToExecute.Parameters.Add(new OracleParameter("v_pro_env_sef", OracleType.Number, 22, ParameterDirection.Input, true, 22, 0, "", DataRowVersion.Proposed, 2));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_FLAG_VIGILANCIA", OracleType.Number, 22, ParameterDirection.Input, true, 22, 0, "", DataRowVersion.Proposed, 2));
            //cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_VPV_COD_PROTOCOLO", OracleType.Number, 22, ParameterDirection.Input, true, 22, 0, "", DataRowVersion.Proposed, _pRo_PROTOCOLO_VIABILIDADE));
            //cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_CNPJ_ORG_REG", OracleType.VarChar, 22, ParameterDirection.Input, true, 22, 0, "", DataRowVersion.Proposed, _PRO_CNPJ_ORG_REG));
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

    public void UpdatePSC_IDENT_PROTOOLO(string _pIP_PRO_PROTOCOLO, string _pIP_CNPJ, string _pIP_NIRE, string _pIP_RUC, string _pIP_NOME_RAZAO_SOCIAL)
    {
        OracleCommand cmdToExecute = new OracleCommand();
        cmdToExecute.CommandText = "pkg_jucesc_data2.psc_ident_protocolo_update";
        cmdToExecute.CommandType = CommandType.StoredProcedure;
        // Use base class' connection object
        cmdToExecute.Connection = _mainConnectionORACLE;
        string _pIP_ISS = "";
        string _pIP_IPTU = "";
        string _pIP_ALVARA_BOMBEIRO = "";
        decimal _pIP_AREA = 0;
        string _pIP_ALVARA_VIGILANCIA = "";
        string _pIP_ALVARA_PM = "";
        try
        {

            cmdToExecute.Parameters.Add(new OracleParameter("v_PIP_PRO_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _pIP_PRO_PROTOCOLO));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PIP_CNPJ", OracleType.Char, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pIP_CNPJ));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PIP_NIRE", OracleType.Char, 12, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pIP_NIRE));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PIP_RUC", OracleType.Char, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pIP_RUC));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PIP_ALVARA_PM", OracleType.VarChar, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pIP_ALVARA_PM));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PIP_NOME_RAZAO_SOCIAL", OracleType.VarChar, 115, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pIP_NOME_RAZAO_SOCIAL));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PIP_ISS", OracleType.Char, 18, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pIP_ISS));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PIP_IPTU", OracleType.VarChar, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pIP_IPTU));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PIP_ALVARA_BOMBEIRO", OracleType.VarChar, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pIP_ALVARA_BOMBEIRO));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PIP_AREA", OracleType.Number, 22, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, _pIP_AREA));
            cmdToExecute.Parameters.Add(new OracleParameter("v_PIP_ALVARA_VIGILANCIA", OracleType.VarChar, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pIP_ALVARA_VIGILANCIA));

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

    public void UPDATE_PSC_PROT_EVENTO_RFB(string _PEV_PRO_PROTOCOLO, string _pev_cod_evento)
    {
        OracleCommand cmdToExecute = new OracleCommand();
        try
        {


            #region Insert Command

            cmdToExecute = new OracleCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append(" Insert Into PSC_PROT_EVENTO_RFB ");
            sql.Append(" (pev_pro_protocolo, pev_cod_evento) ");
            sql.Append(" Values ");
            sql.Append(" ('" + _PEV_PRO_PROTOCOLO + "','" + _pev_cod_evento + "') ");
            // sql.Append(" (@v_pev_pro_protocolo, @v_pev_cod_evento) ");
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnectionORACLE;
            #endregion
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
            cmdToExecute.ExecuteNonQuery();

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

    public void Update_mac_log_carga_junta_homolog(string _mlc_protocolo, string _mlc_cpf_homologador, bool MLC_DTA_HOMOLOGACAO, bool MLC_DATA_CARREGA_REGIN, bool MLC_DATA_CARREGA_WS11, bool MLC_DATA_VALIDA_XML, bool MLC_DATA_CARREGA_ENVIO)
    {

        StringBuilder Sql = new StringBuilder();
        StringBuilder SqlU = new StringBuilder();

        Sql.AppendLine(" Insert into mac_log_carga_junta_homolog");
        Sql.AppendLine("  (");
        Sql.AppendLine("  MLC_PROTOCOLO,");
        Sql.AppendLine("  MLC_DTA_HOMOLOGACAO,");
        Sql.AppendLine("  MLC_CPF_HOMOLOGADOR");
        Sql.AppendLine("  )");
        Sql.AppendLine(" Values ");
        Sql.AppendLine("  (");
        Sql.AppendLine("'" + _mlc_protocolo + "',");
        Sql.AppendLine(" sysdate,");
        Sql.AppendLine("'" + _mlc_cpf_homologador + "'");
        Sql.AppendLine("  )");

        // Codigo Update ******************* 
        SqlU.AppendLine(" Update    mac_log_carga_junta_homolog Set ");
        SqlU.AppendLine("  MLC_CPF_HOMOLOGADOR = '" + _mlc_cpf_homologador + "'");

        if (MLC_DTA_HOMOLOGACAO)
        {
            SqlU.AppendLine(",  MLC_DTA_HOMOLOGACAO = sysdate");
        }

        if (MLC_DATA_CARREGA_REGIN)
        {
            SqlU.AppendLine(",  MLC_DATA_CARREGA_REGIN = sysdate");
        }

        if (MLC_DATA_CARREGA_WS11)
        {
            SqlU.AppendLine(",  MLC_DATA_CARREGA_WS11 = sysdate");
        }

        if (MLC_DATA_CARREGA_ENVIO)
        {
            SqlU.AppendLine(",  MLC_DATA_CARREGA_ENVIO = sysdate");
        }

        SqlU.AppendLine(" Where	    MLC_PROTOCOLO = '" + _mlc_protocolo + "'");


        OracleCommand cmdToExecute = new OracleCommand();
        cmdToExecute.CommandText = SqlU.ToString(); ;
        cmdToExecute.CommandType = CommandType.Text;

        // Use base class' connection object
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

            if (cmdToExecute.ExecuteNonQuery() == 0)
            {
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();
            }

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
            //adapter.Dispose();
        }
    }

    public void UpdateOutrosCamposT01_PROCESSO_DEFERIDOS(decimal pId, string pProtocoloRegin)
    {
        OracleCommand cmdToExecute = new OracleCommand();

        StringBuilder Sql = new StringBuilder();

        Sql.Append("Update	T0101_RFB_PROCESSO_DEFERIDOS ");
        Sql.Append("Set		T0101_PROTOCOLO_REGIN = :v_T0101_PROTOCOLO_REGIN ");

        Sql.Append("Where	T0101_ID_RFB = :v_T0101_ID_RFB");

        cmdToExecute.CommandText = Sql.ToString();
        cmdToExecute.CommandType = CommandType.Text;
        // Use base class' connection object
        cmdToExecute.Connection = _mainConnectionORACLE;

        try
        {
            cmdToExecute.Parameters.Add(new OracleParameter("v_T0101_PROTOCOLO_REGIN", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocoloRegin));
            cmdToExecute.Parameters.Add(new OracleParameter("v_T0101_ID_RFB", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pId));

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