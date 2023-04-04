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
    public class Ruc_actv_outro_estab : DBInteractionBaseORACLE
    {
        // Variables ******************* 
        #region  Property Declarations
        private int _rae_ide_estab;
        protected string _rae_calif_actv;
        protected string _rae_tae_cod_actvd;
        protected string _rae_rge_pra_protocolo;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public int Rae_ide_estab
        {
            get { return _rae_ide_estab; }
            set { _rae_ide_estab = value; }
        }
        public string rae_calif_actv
        {
            get { return _rae_calif_actv; }
            set { _rae_calif_actv = value; }
        }
        public string rae_tae_cod_actvd
        {
            get { return _rae_tae_cod_actvd; }
            set { _rae_tae_cod_actvd = value; }
        }
        public string rae_rge_pra_protocolo
        {
            get { return _rae_rge_pra_protocolo; }
            set { _rae_rge_pra_protocolo = value; }
        }
        #endregion

        public void Update()
        {
            try
            {
                StringBuilder Sql = new StringBuilder();

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandType = CommandType.Text;
                Sql.Append(" Insert into RUC_ACTV_OUTRO_ESTAB");
                Sql.Append("  (");
                Sql.Append("	rae_ide_estab, ");
                Sql.Append("	rae_calif_actv, ");
                Sql.Append("	rae_tae_cod_actvd, ");
                Sql.Append("	rae_rge_pra_protocolo");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	:v_rae_ide_estab, ");
                Sql.Append("	:v_rae_calif_actv, ");
                Sql.Append("	:v_rae_tae_cod_actvd, ");
                Sql.Append("	:v_rae_rge_pra_protocolo");
                Sql.Append("  )");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionORACLE;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new OracleParameter("v_rae_ide_estab", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_ide_estab));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rae_calif_actv", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_calif_actv));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rae_tae_cod_actvd", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_tae_cod_actvd));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rae_rge_pra_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_rge_pra_protocolo));

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
