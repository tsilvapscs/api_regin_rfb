using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using psc.Receita.ConnectionBase;

/// <summary>
/// Summary description for T73307_ERRO_PROCESSO_35
/// </summary>
/// 

namespace psc.Receita.Entities
{
    public class T73307_ERRO_PROCESSO_35 : DBInteractionBase
    {
        #region  Property Declarations
        protected int _t73307_id;
        protected string _t73307_arquivo_RFB = "";
        protected string _t73307_erro = "";
        protected DateTime _t73307_data_inclucao = DateTime.Now;
        protected string _t73307_ide_solicitacao = "";
        protected string _t73307_rec_solicitacao = "";
        protected string _t73307_arquivo_regin = "";
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public int t73307_id
        {
            get { return _t73307_id; }
            set { _t73307_id = value; }
        }
        public string t73307_arquivo_regin
        {
            get { return _t73307_arquivo_regin; }
            set { _t73307_arquivo_regin = value; }
        }
        
        public string t73307_arquivo_RFB
        {
            get { return _t73307_arquivo_RFB; }
            set { _t73307_arquivo_RFB = value; }
        }
        public string t73307_erro
        {
            get { return _t73307_erro; }
            set { _t73307_erro = value; }
        }
        public DateTime t73307_data_inclucao
        {
            get { return _t73307_data_inclucao; }
            set { _t73307_data_inclucao = value; }
        }
        public string t73307_ide_solicitacao
        {
            get { return _t73307_ide_solicitacao; }
            set { _t73307_ide_solicitacao = value; }
        }
        public string t73307_rec_solicitacao
        {
            get { return _t73307_rec_solicitacao; }
            set { _t73307_rec_solicitacao = value; }
        }

        #endregion

        #region Implements
        
        public void Update()
        {
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" Insert into t73307_erro_processo_35");
            Sql.AppendLine("  (");
            Sql.AppendLine("	t73307_id, ");
            Sql.AppendLine("	t73307_arquivo_RFB, ");
            Sql.AppendLine("	t73307_erro, ");
            Sql.AppendLine("	t73307_data_inclucao,");
            Sql.AppendLine("	t73307_ide_solicitacao,");
            Sql.AppendLine("	t73307_rec_solicitacao");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_t73307_id, ");
            Sql.AppendLine("	@v_t73307_arquivo_RFB, ");
            Sql.AppendLine("	@v_t73307_erro, ");
            Sql.AppendLine("	@v_t73307_data_inclucao, ");
            Sql.AppendLine("    @v_t73307_ide_solicitacao,");
            Sql.AppendLine("	@v_t73307_rec_solicitacao");
            Sql.AppendLine("  )");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_id", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, null));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_arquivo_RFB", MySqlDbType.MediumText, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_arquivo_RFB));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_erro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_erro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_data_inclucao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_data_inclucao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_ide_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_ide_solicitacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_rec_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_rec_solicitacao));


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

                Sql = new StringBuilder();
                Sql.AppendLine(" DELETE FROM t73307_erro_processo_35 WHERE t73307_data_inclucao < date_sub(sysdate(), INTERVAL 30 DAY)  LIMIT 10 ");
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
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
        public void UpdateS01()
        {
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" Insert into t73307_erro_processo_s01");
            Sql.AppendLine("  (");
            Sql.AppendLine("	t73307_id, ");
            Sql.AppendLine("	t73307_arquivo_RFB, ");
            Sql.AppendLine("	t73307_erro, ");
            Sql.AppendLine("	t73307_arquivo_regin, ");
            Sql.AppendLine("	t73307_data_inclucao,");
            Sql.AppendLine("	t73307_ide_servico,");
            Sql.AppendLine("	t73307_rec_solicitacao");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_t73307_id, ");
            Sql.AppendLine("	@v_t73307_arquivo_RFB, ");
            Sql.AppendLine("	@v_t73307_erro, ");
            Sql.AppendLine("	@v_t73307_arquivo_regin, ");
            Sql.AppendLine("	@v_t73307_data_inclucao, ");
            Sql.AppendLine("    @v_t73307_ide_solicitacao,");
            Sql.AppendLine("	@v_t73307_rec_solicitacao");
            Sql.AppendLine("  )");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_id", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, null));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_arquivo_RFB", MySqlDbType.MediumText, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_arquivo_RFB));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_erro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_erro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_arquivo_regin", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_arquivo_regin));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_data_inclucao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_data_inclucao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_ide_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_ide_solicitacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_rec_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_rec_solicitacao));


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

                cmdToExecute.Parameters.Clear();
                Sql = new StringBuilder();
                Sql.AppendLine(" DELETE FROM t73307_erro_processo_s01 WHERE t73307_data_inclucao < date_sub(sysdate(), INTERVAL 30 DAY)  LIMIT 10 ");
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
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



        public void UpdateConsultaEmpresa()
        {
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" Insert into t73309_consulta_registro");
            Sql.AppendLine("  (");
            Sql.AppendLine("	t73309_cpfcnpj_consulta,");
            Sql.AppendLine("	t73309_cnpj_orgao, ");
            Sql.AppendLine("	t73309_ip_computador ");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_t73307_rec_solicitacao, ");
            Sql.AppendLine("	@v_t73307_ide_solicitacao, ");
            Sql.AppendLine("	@v_t73307_arquivo_RFB ");
            Sql.AppendLine("  )");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_id", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, null));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_ide_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_ide_solicitacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_rec_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_rec_solicitacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_arquivo_RFB", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73307_arquivo_RFB));


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
        #endregion

        #region Atualizar tabela de solicitacao s24
        public void UpdateSolicitaS24(string cnpjOrgaoregistro, string cnpjEmpresa, string statusMarcado)
        {
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" Insert into t73310_dados_s24");
            Sql.AppendLine("  (");
            Sql.AppendLine("	cnpjOrgaoregistro,");
            Sql.AppendLine("	cnpjEmpresa, ");
            Sql.AppendLine("	statusMarcado ");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@cnpjOrgaoregistro, ");
            Sql.AppendLine("	@cnpjEmpresa, ");
            Sql.AppendLine("	@statusMarcado ");
            Sql.AppendLine("  )");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t73307_id", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, null));
                cmdToExecute.Parameters.Add(new MySqlParameter("cnpjOrgaoregistro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cnpjOrgaoregistro));
                cmdToExecute.Parameters.Add(new MySqlParameter("cnpjEmpresa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cnpjEmpresa));
                cmdToExecute.Parameters.Add(new MySqlParameter("statusMarcado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, statusMarcado));


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
        #endregion
    }
}