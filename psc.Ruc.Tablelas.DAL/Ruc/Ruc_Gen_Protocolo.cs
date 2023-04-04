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
    public class Ruc_Gen_Protocolo : DBInteractionBaseORACLE
    {
        // Variables ******************* 
        #region  Property Declarations
        protected string _rgp_rge_pra_protocolo;
        protected decimal _rgp_tge_tip_tab;
        protected decimal _rgp_tge_cod_tip_tab;
        protected DateTime _rgp_fec_actl;
        protected string _rgp_tus_cod_usr;
        protected string _rgp_valor;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string rgp_rge_pra_protocolo
        {
            get { return _rgp_rge_pra_protocolo; }
            set { _rgp_rge_pra_protocolo = value; }
        }
        public decimal rgp_tge_tip_tab
        {
            get { return _rgp_tge_tip_tab; }
            set { _rgp_tge_tip_tab = value; }
        }
        public decimal rgp_tge_cod_tip_tab
        {
            get { return _rgp_tge_cod_tip_tab; }
            set { _rgp_tge_cod_tip_tab = value; }
        }
        public DateTime rgp_fec_actl
        {
            get { return _rgp_fec_actl; }
            set { _rgp_fec_actl = value; }
        }
        public string rgp_tus_cod_usr
        {
            get { return _rgp_tus_cod_usr; }
            set { _rgp_tus_cod_usr = value; }
        }
        public string rgp_valor
        {
            get { return _rgp_valor; }
            set { _rgp_valor = value; }
        }
        #endregion

        public void Update(OracleTransaction bd)
        {

            StringBuilder Sql = new StringBuilder();

           using (OracleCommand cmdToExecute = new OracleCommand())
            {
                cmdToExecute.Transaction = bd;

                cmdToExecute.Connection = bd.Connection;

                Sql = new StringBuilder();
                Sql.AppendLine(" delete ruc_gen_protocolo ");
                Sql.AppendLine(" where	rgp_rge_pra_protocolo = '" + _rgp_rge_pra_protocolo + "'");
                Sql.AppendLine(" And    rgp_tge_tip_tab = " + _rgp_tge_tip_tab);
                Sql.AppendLine(" And    rgp_tge_cod_tip_tab = " + _rgp_tge_cod_tip_tab);

                cmdToExecute.Parameters.Clear();
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.ExecuteNonQuery();


                Sql = new StringBuilder();
                cmdToExecute.CommandType = CommandType.Text;
                Sql.Append(" Insert into ruc_gen_protocolo");
                Sql.Append("  (");
                Sql.Append("	rgp_rge_pra_protocolo, ");
                Sql.Append("	rgp_tge_tip_tab, ");
                Sql.Append("	rgp_tge_cod_tip_tab, ");
                Sql.Append("	rgp_fec_actl, ");
                Sql.Append("	rgp_tus_cod_usr, ");
                Sql.Append("	rgp_valor");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	:v_rgp_rge_pra_protocolo, ");
                Sql.Append("	:v_rgp_tge_tip_tab, ");
                Sql.Append("	:v_rgp_tge_cod_tip_tab, ");
                Sql.Append("	:v_rgp_fec_actl, ");
                Sql.Append("	:v_rgp_tus_cod_usr, ");
                Sql.Append("	:v_rgp_valor");
                Sql.Append("  )");

                cmdToExecute.Parameters.Clear();
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionORACLE;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new OracleParameter("v_rgp_rge_pra_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rgp_rge_pra_protocolo));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rgp_tge_tip_tab", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rgp_tge_tip_tab));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rgp_tge_cod_tip_tab", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rgp_tge_cod_tip_tab));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rgp_fec_actl", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rgp_fec_actl));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rgp_tus_cod_usr", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rgp_tus_cod_usr));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rgp_valor", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rgp_valor));

                cmdToExecute.Transaction = bd;

                cmdToExecute.Connection = bd.Connection;

                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.CommandText = Sql.ToString();

                cmdToExecute.ExecuteNonQuery();
            }

        }

    }
}
