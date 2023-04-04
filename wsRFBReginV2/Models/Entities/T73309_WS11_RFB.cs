using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using psc.Receita.ConnectionBase;
using System.Configuration;

namespace psc.Receita.Entities
{
    public class T73309_WS11_RFB : DBInteractionBase
    {
        #region  Property Declarations
        protected string _t73309_cnpj;
        protected DateTime _t73309_data_consulta = DateTime.Now;
        protected string _t73309_xml;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string t73309_cnpj
        {
            get { return _t73309_cnpj; }
            set { _t73309_cnpj = value; }
        }
        public string t73309_xml
        {
            get { return _t73309_xml; }
            set { _t73309_xml = value; }
        }
        public DateTime t73309_data_consulta
        {
            get { return _t73309_data_consulta; }
        }

        #endregion

        #region Implementes
        public void DeletePk()
        {

            int diasPrazoBuscaws11 = 0;

            if (ConfigurationManager.AppSettings.Get("diasPrazoBuscaws11") != null && ConfigurationManager.AppSettings.Get("diasPrazoBuscaws11").ToString() != "")
            {
                try
                {
                    diasPrazoBuscaws11 = int.Parse(ConfigurationManager.AppSettings.Get("diasPrazoBuscaws11").ToString());
                }
                catch { }
            }

            if (diasPrazoBuscaws11 == 0)
            {
                return;
            }
                StringBuilder SqlU = new StringBuilder();
            // Codigo Update ******************* 
            SqlU.AppendLine(" Delete    from t73309_ws11_rfb ");
            SqlU.AppendLine(" Where	    t73309_cnpj = @v_t73309_cnpj ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_cnpj", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_cnpj));

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


            Sql.AppendLine(" Insert into t73309_ws11_rfb");
            Sql.AppendLine("  (");
            Sql.AppendLine("	t73309_cnpj, ");
            Sql.AppendLine("	t73309_data_consulta, ");
            Sql.AppendLine("	t73309_xml ");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_t73309_cnpj, ");
            Sql.AppendLine("	@v_t73309_data_consulta, ");
            Sql.AppendLine("	@v_t73309_xml ");
            Sql.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update    t73309_ws11_rfb Set ");
            SqlU.AppendLine("		    t73309_data_consulta = @v_t73309_data_consulta, ");
            SqlU.AppendLine("		    t73309_xml = @v_t73309_xml ");
            SqlU.AppendLine(" Where	    1 = 1 ");
            SqlU.AppendLine(" and       t73309_cnpj = @v_t73309_cnpj ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_cnpj", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_cnpj));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_data_consulta", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_data_consulta));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_xml", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_xml));

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

                cmdToExecute.Parameters.Clear();
                Sql = new StringBuilder();
                Sql.AppendLine(" DELETE FROM t73309_ws11_rfb WHERE t73309_data_consulta < date_sub(sysdate(), INTERVAL 1 DAY)  LIMIT 10 ");
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

        public T73309_WS11_RFB Query()
        {
            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(" Select    * ");
            SqlU.AppendLine(" from      t73309_ws11_rfb ");
            SqlU.AppendLine(" Where	    t73309_cnpj = @v_t73309_cnpj ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_cnpj", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_cnpj));

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

                MySqlDataReader dr = cmdToExecute.ExecuteReader();
                T73309_WS11_RFB pResul = new T73309_WS11_RFB();
                if (dr.Read())
                {
                    pResul._t73309_xml = dr.GetString("t73309_xml").ToString();
                    pResul._t73309_data_consulta = dr.GetDateTime("t73309_data_consulta");
                    pResul._t73309_cnpj = _t73309_cnpj;
                }
                else
                {
                    pResul = new T73309_WS11_RFB();
                }

                return pResul;

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