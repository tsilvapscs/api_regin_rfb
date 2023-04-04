using System;
using System.Collections.Generic;
using System.Text;

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
    public class Psc_Prot_Evento_Rfb :DBInteractionBaseORACLE
    {
        // Variables ******************* 
        #region  Property Declarations
        protected string _pev_pro_protocolo;
        protected decimal _pev_cod_evento;
        protected decimal _pev_flag_processado;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string pev_pro_protocolo
        {
            get { return _pev_pro_protocolo; }
            set { _pev_pro_protocolo = value; }
        }
        public decimal pev_cod_evento
        {
            get { return _pev_cod_evento; }
            set { _pev_cod_evento = value; }
        }
        public decimal pev_flag_processado
        {
            get { return _pev_flag_processado; }
            set { _pev_flag_processado = value; }
        }
        #endregion

        public void Update()
        {
            try
            {
                StringBuilder Sql = new StringBuilder();

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandType = CommandType.Text;
                Sql.Append(" Insert into psc_prot_evento_rfb");
                Sql.Append("  (");
                Sql.Append("	pev_pro_protocolo, ");
                Sql.Append("	pev_cod_evento, ");
                Sql.Append("	pev_flag_processado");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	:v_pev_pro_protocolo, ");
                Sql.Append("	:v_pev_cod_evento, ");
                Sql.Append("	:v_pev_flag_processado");
                Sql.Append("  )");


                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionORACLE;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new OracleParameter("v_pev_pro_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pev_pro_protocolo));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pev_cod_evento", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pev_cod_evento));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pev_flag_processado", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pev_flag_processado));
               
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
