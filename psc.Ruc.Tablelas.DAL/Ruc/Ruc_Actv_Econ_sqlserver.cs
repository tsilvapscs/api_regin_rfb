using System;
using System.Collections.Generic;
using System.Text;
//using psc.Ruc.Tablelas.Entities;
using psc.Ruc.Tablelas.ConnectionBase;
//using psc.ApplicationBlocks.DAL;
using psc.Framework;
using psc.Framework.Data;
using System.Data;
using System.Data.SqlClient;
//using Oracle.DataAccess.Types;
using System.Xml;
using System.IO;
using System.Data.OleDb;
namespace psc.Ruc.Tablelas.Ruc
{
    public class Ruc_Actv_Econ_sqlserver : DBInteractionBaseSQL
    {
        // Variables ******************* 
        #region  Property Declarations
        protected string _rae_calif_actv;
        protected decimal _rae_porcent;
        protected DateTime _rae_fec_actl;
        protected string _rae_tus_cod_usr;
        protected string _rae_tae_cod_actvd;
        protected string _rae_rge_pra_protocolo;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string rae_calif_actv
        {
            get { return _rae_calif_actv; }
            set { _rae_calif_actv = value; }
        }
        public decimal rae_porcent
        {
            get { return _rae_porcent; }
            set { _rae_porcent = value; }
        }
        public DateTime rae_fec_actl
        {
            get { return _rae_fec_actl; }
            set { _rae_fec_actl = value; }
        }
        public string rae_tus_cod_usr
        {
            get { return _rae_tus_cod_usr; }
            set { _rae_tus_cod_usr = value; }
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

        public void Update(SqlTransaction bd)
        {

            StringBuilder Sql = new StringBuilder();

            using (SqlCommand cmdToExecute = new SqlCommand())
            {
                cmdToExecute.CommandType = CommandType.Text;
                Sql.Append(" Insert into ruc_actv_econ");
                Sql.Append("  (");
                Sql.Append("	rae_calif_actv, ");
                Sql.Append("	rae_porcent, ");
                Sql.Append("	rae_fec_actl, ");
                Sql.Append("	rae_tus_cod_usr, ");
                Sql.Append("	rae_tae_cod_actvd, ");
                Sql.Append("	rae_rge_pra_protocolo");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	@v_rae_calif_actv, ");
                Sql.Append("	@v_rae_porcent, ");
                Sql.Append("	@v_rae_fec_actl, ");
                Sql.Append("	@v_rae_tus_cod_usr, ");
                Sql.Append("	@v_rae_tae_cod_actvd, ");
                Sql.Append("	@v_rae_rge_pra_protocolo");
                Sql.Append("  )");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionSQL;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new SqlParameter("v_rae_calif_actv", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_calif_actv));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rae_porcent", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_porcent));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rae_fec_actl", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_fec_actl));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rae_tus_cod_usr", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_tus_cod_usr));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rae_tae_cod_actvd", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_tae_cod_actvd));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rae_rge_pra_protocolo", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_rge_pra_protocolo));

                cmdToExecute.Transaction = bd;

                cmdToExecute.Connection = bd.Connection;

                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.CommandText = Sql.ToString();

                cmdToExecute.ExecuteNonQuery();

            }


        }
    }
}
