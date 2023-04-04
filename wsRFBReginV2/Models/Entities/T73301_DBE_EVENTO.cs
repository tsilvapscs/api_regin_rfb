using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using psc.Receita.ConnectionBase;

namespace psc.Receita.Entities
{
    class T73301_DBE_EVENTO : DBInteractionBase
    {
        // Variables ******************* 
        #region  Property Declarations
        protected decimal _t73300_id_control;
        protected string _t73301_cod_evento = "";
        protected DateTime _t73301_dat_evento = DateTime.Now;
        protected string _t73301_tip_evento = "1";
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public decimal t73300_id_control
        {
            get { return _t73300_id_control; }
            set { _t73300_id_control = value; }
        }
        public string t73301_cod_evento
        {
            get { return _t73301_cod_evento; }
            set { _t73301_cod_evento = value; }
        }
        public DateTime t73301_dat_evento
        {
            get { return _t73301_dat_evento; }
            set { _t73301_dat_evento = value; }
        }
        public string t73301_tip_evento
        {
            get { return _t73301_tip_evento; }
            set { _t73301_tip_evento = value; }
        }
        #endregion

        #region Implementes
        public void DeletePk()
        {
            StringBuilder SqlU = new StringBuilder();
            // Codigo Update ******************* 
            SqlU.AppendLine(" Delete    from t73301_dbe_evento ");
            SqlU.AppendLine(" Where	    t73300_id_control = @v_t73300_id_control ");
            SqlU.AppendLine(" And	    t73301_cod_evento = @v_t73301_cod_evento ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_id_control", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_id_control));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73301_cod_evento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73301_cod_evento));

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
        public void Delete()
        {
            StringBuilder SqlU = new StringBuilder();
            // Codigo Update ******************* 
            SqlU.AppendLine(" Delete    from t73301_dbe_evento ");
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


            Sql.AppendLine(" Insert into t73301_dbe_evento");
            Sql.AppendLine("  (");
            Sql.AppendLine("	t73300_id_control, ");
            Sql.AppendLine("	t73301_cod_evento, ");
            Sql.AppendLine("	t73301_dat_evento, ");
            Sql.AppendLine("	t73301_tip_evento ");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_t73300_id_control, ");
            Sql.AppendLine("	@v_t73301_cod_evento, ");
            Sql.AppendLine("	@v_t73301_dat_evento, ");
            Sql.AppendLine("	@v_t73301_tip_evento ");
            Sql.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update    t73301_dbe_evento Set ");
            SqlU.AppendLine("		    t73301_dat_evento = @v_t73301_dat_evento, ");
            SqlU.AppendLine("		    t73301_tip_evento = @v_t73301_tip_evento ");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" and   t73300_id_control = @v_t73300_id_control ");
            SqlU.AppendLine(" And	t73301_cod_evento = @v_t73301_cod_evento ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_id_control", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_id_control));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73301_cod_evento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73301_cod_evento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73301_dat_evento", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, t73301_dat_evento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73301_tip_evento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, t73301_tip_evento));

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
    }
}
