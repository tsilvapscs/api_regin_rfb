using System;
using System.Collections.Generic;
using System.Text;
//using psc.Ruc.Tablelas.Entities;
using psc.Ruc.Tablelas.ConnectionBase;
//using psc.ApplicationBlocks.DAL;
using psc.Framework;
using psc.Framework.Data;
using System.Data;
using System.Data.OracleClient;
//using Oracle.DataAccess.Types;
using System.Xml;
using System.IO;
using System.Data.OleDb;

namespace psc.Ruc.Tablelas.Ruc
{
    public class MAC_LOG_CARGA_JUNTA_HOMOLOG : DBInteractionBaseORACLE
    {
        // Variables ******************* 
        #region  Property Declarations
        protected string _MLC_PROTOCOLO;
        protected DateTime _MLC_DTA_HOMOLOGACAO;
        protected string _MLC_CPF_HOMOLOGADOR;
        protected DateTime _MLC_DATA_CARREGA_WS11;
        #endregion


       
        // Property ******************* 
        #region Class Member Declarations
        public string MLC_PROTOCOLO
        {
            get { return _MLC_PROTOCOLO; }
            set { _MLC_PROTOCOLO = value; }
        }
        public DateTime MLC_DTA_HOMOLOGACAO
        {
            get { return _MLC_DTA_HOMOLOGACAO; }
            set { _MLC_DTA_HOMOLOGACAO = value; }
        }
        public DateTime MLC_DATA_CARREGA_WS11
        {
            get { return _MLC_DATA_CARREGA_WS11; }
            set { _MLC_DATA_CARREGA_WS11 = value; }
        }
        public string MLC_CPF_HOMOLOGADOR
        {
            get { return _MLC_CPF_HOMOLOGADOR; }
            set { _MLC_CPF_HOMOLOGADOR = value; }
        }
      
        #endregion

        public void Update()
        {
            try
            {
                StringBuilder Sql = new StringBuilder();


                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandType = CommandType.Text;
                Sql.Append(" Insert into MAC_LOG_CARGA_JUNTA_HOMOLOG");
                Sql.Append("  (");
                Sql.Append("	MLC_PROTOCOLO, ");
                Sql.Append("	MLC_DTA_HOMOLOGACAO, ");
                Sql.Append("	MLC_CPF_HOMOLOGADOR ");
                //Sql.Append("	MLC_DATA_CARREGA_WS11 ");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	:v_MLC_PROTOCOLO, ");
                Sql.Append("	:v_MLC_DTA_HOMOLOGACAO, ");
                Sql.Append("	:v_MLC_CPF_HOMOLOGADOR ");
                //Sql.Append("	EvalDate(:v_MLC_DATA_CARREGA_WS11) ");
                Sql.Append("  )");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionORACLE;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new OracleParameter("v_MLC_PROTOCOLO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _MLC_PROTOCOLO));
                cmdToExecute.Parameters.Add(new OracleParameter("v_MLC_DTA_HOMOLOGACAO", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _MLC_DTA_HOMOLOGACAO));
                cmdToExecute.Parameters.Add(new OracleParameter("v_MLC_CPF_HOMOLOGADOR", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _MLC_CPF_HOMOLOGADOR));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_MLC_DATA_CARREGA_WS11", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _MLC_DATA_CARREGA_WS11));

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
                    _mainConnectionProviderORACLE.Dispose();
                }
            }
        }
    }
}
