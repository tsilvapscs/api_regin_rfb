using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using psc.Receita.ConnectionBase;

/// <summary>
/// Summary description for T73308_DBE_FORMA_ATUACAO
/// </summary>
/// 

namespace psc.Receita.Entities
{
    public class T73308_DBE_FORMA_ATUACAO : DBInteractionBase
    {
        #region  Property Declarations
        protected decimal _t73300_id_control;
        protected string _t73308_cod_forma_atuacao = "";
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public decimal t73300_id_control
        {
            get { return _t73300_id_control; }
            set { _t73300_id_control = value; }
        }
        public string t73308_cod_forma_atuacao
        {
            get { return _t73308_cod_forma_atuacao; }
            set { _t73308_cod_forma_atuacao = value; }
        }
        #endregion

        #region Implementes
        public void Delete()
        {
            StringBuilder SqlU = new StringBuilder();
            // Codigo Update ******************* 
            SqlU.AppendLine(" Delete    from t73308_dbe_forma_atuacao ");
            SqlU.AppendLine(" Where	    t73300_id_control = @v_t73300_id_control ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_id_control", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_id_control));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
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
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }
        public void Update()
        {
            StringBuilder SqlU = new StringBuilder();
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" Insert into t73308_dbe_forma_atuacao");
            Sql.AppendLine("  (");
            Sql.AppendLine("	t73300_id_control, ");
            Sql.AppendLine("	t73308_cod_forma_atuacao ");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_t73300_id_control, ");
            Sql.AppendLine("	@v_t73308_cod_forma_atuacao ");
            Sql.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update    t73308_dbe_forma_atuacao Set ");
            SqlU.AppendLine("		    t73308_cod_forma_atuacao = @v_t73308_cod_forma_atuacao ");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" and   t73300_id_control = @v_t73300_id_control ");
            SqlU.AppendLine(" And	t73308_cod_forma_atuacao = @v_t73308_cod_forma_atuacao ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_id_control", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_id_control));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73308_cod_forma_atuacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73308_cod_forma_atuacao));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
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
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }
        #endregion


        public void UpdateStatusT73309Servico99(decimal pid, decimal pStatus, string pDescricaoErro)
        {
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" UPDATE t73309_dados_servico99 ");
            Sql.AppendLine(" SET ");
            Sql.AppendLine("  StatusDbe = @pStatus, errorProcssamento = @pDescricaoErro ");
            if (pStatus == 1)
            {
                Sql.AppendLine(", DataProcessamento = NOW() ");
            }
            Sql.AppendLine(" WHERE ");
            Sql.AppendLine("  id = @pid");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("pid", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pid));
                cmdToExecute.Parameters.Add(new MySqlParameter("pStatus", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pStatus));
                cmdToExecute.Parameters.Add(new MySqlParameter("pDescricaoErro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pDescricaoErro));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
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
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }

        public void UpdateStatusT73309Servico99V2(decimal pid, decimal pStatus, string pDescricaoErro, string pXmlRFB11, string pXmlRFB09)
        {
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" UPDATE t73309_dados_servico99 ");
            Sql.AppendLine(" SET ");
            Sql.AppendLine("  StatusDbe = @pStatus, errorProcssamento = @pDescricaoErro, XmlRFB11 = @pXmlRFB11, XmlRFB09 = @pXmlRFB09");
            if (pStatus == 1)
            {
                Sql.AppendLine(", DataProcessamento = NOW() ");
            }
            Sql.AppendLine(" WHERE ");
            Sql.AppendLine("  id = @pid");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("pid", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pid));
                cmdToExecute.Parameters.Add(new MySqlParameter("pStatus", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pStatus));
                cmdToExecute.Parameters.Add(new MySqlParameter("pDescricaoErro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pDescricaoErro));
                cmdToExecute.Parameters.Add(new MySqlParameter("pXmlRFB11", MySqlDbType.Text, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pXmlRFB11));
                cmdToExecute.Parameters.Add(new MySqlParameter("pXmlRFB09", MySqlDbType.Text, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pXmlRFB09));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
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
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }


        public void UpdateStatusEnviaMEIJUCERJAServico99(decimal pid, decimal pStatus, string pDescricaoErro)
        {
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" UPDATE t73309_dados_servico99 ");
            Sql.AppendLine(" SET ");
            Sql.AppendLine("  StatusEnvioMEIJunta = @pStatus, errorProcssamento = @pDescricaoErro ");
            if (pStatus == 1)
            {
                Sql.AppendLine(", DataEnvioMEIJUNTA = NOW() ");
            }
            Sql.AppendLine(" WHERE ");
            Sql.AppendLine("  id = @pid");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("pid", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pid));
                cmdToExecute.Parameters.Add(new MySqlParameter("pStatus", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pStatus));
                cmdToExecute.Parameters.Add(new MySqlParameter("pDescricaoErro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pDescricaoErro));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
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
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }


        public void UpdateStatusSERVICOS04RFB(decimal pid, int pStatus, string pDescricaoErro)
        {
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" UPDATE t73309_dados_servico99 ");
            Sql.AppendLine(" SET ");
            Sql.AppendLine("  StatusDbeS04 = @pStatus, ErroEnvioS04 = @pDescricaoErro ");
            if (pStatus == 1)
            {
                Sql.AppendLine(", dataEnvioS04 = NOW() ");
            }
            Sql.AppendLine(" WHERE ");
            Sql.AppendLine("  id = @pid");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("pid", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pid));
                cmdToExecute.Parameters.Add(new MySqlParameter("pStatus", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pStatus));
                cmdToExecute.Parameters.Add(new MySqlParameter("pDescricaoErro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pDescricaoErro));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
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
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }
    }
}
