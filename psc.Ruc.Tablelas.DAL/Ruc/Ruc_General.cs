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
    public class Ruc_General : DBInteractionBaseORACLE
    {
        // Variables ******************* 
        #region  Property Declarations
        protected string _rge_ruc;
        protected decimal _rge_tge_ttip_reg;
        protected decimal _rge_tge_vtip_reg;
        protected decimal _rge_tge_ttip_ctrib;
        protected decimal _rge_tge_vtip_ctrib;
        protected decimal _rge_tge_ttip_pers;
        protected decimal _rge_tge_vtip_pers;
        protected string _rge_cgc_cpf;
        protected decimal _rge_tsc_sit_cad;
        protected decimal _rge_tab_cod_unid;
        protected decimal _rge_ruc_cond;
        protected decimal _rge_tco_cond_pago;
        protected string _rge_opt_simp;
        protected decimal _rge_ind_regm_esp;
        protected decimal _rge_ind_subst_trib;
        protected decimal _rge_tge_ttamanho;
        protected decimal _rge_tge_vtamanho;
        protected decimal _rge_mes_cier_ejer;
        protected DateTime _rge_fec_ini_act_ec;
        protected DateTime _rge_fec_sit_cad;
        protected DateTime _rge_fec_actl;
        protected DateTime _rge_fec_val_ruc;
        protected decimal _rge_tge_torig_actu;
        protected decimal _rge_tge_vorig_actu;
        protected string _rge_tus_cod_usr;
        protected decimal _rge_tge_ttip_insc;
        protected decimal _rge_tge_vtip_insc;
        protected string _rge_nomb;
        protected decimal _rge_codg_mun;
        protected decimal _rge_tae_cod_actvd;
        protected string _rge_tuf_cod_uf;
        protected decimal _rge_tda_cod_daf;
        protected string _rge_pra_protocolo;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string rge_ruc
        {
            get { return _rge_ruc; }
            set { _rge_ruc = value; }
        }
        public decimal rge_tge_ttip_reg
        {
            get { return _rge_tge_ttip_reg; }
            set { _rge_tge_ttip_reg = value; }
        }
        public decimal rge_tge_vtip_reg
        {
            get { return _rge_tge_vtip_reg; }
            set { _rge_tge_vtip_reg = value; }
        }
        public decimal rge_tge_ttip_ctrib
        {
            get { return _rge_tge_ttip_ctrib; }
            set { _rge_tge_ttip_ctrib = value; }
        }
        public decimal rge_tge_vtip_ctrib
        {
            get { return _rge_tge_vtip_ctrib; }
            set { _rge_tge_vtip_ctrib = value; }
        }
        public decimal rge_tge_ttip_pers
        {
            get { return _rge_tge_ttip_pers; }
            set { _rge_tge_ttip_pers = value; }
        }
        public decimal rge_tge_vtip_pers
        {
            get { return _rge_tge_vtip_pers; }
            set { _rge_tge_vtip_pers = value; }
        }
        public string rge_cgc_cpf
        {
            get { return _rge_cgc_cpf; }
            set { _rge_cgc_cpf = value; }
        }
        public decimal rge_tsc_sit_cad
        {
            get { return _rge_tsc_sit_cad; }
            set { _rge_tsc_sit_cad = value; }
        }
        public decimal rge_tab_cod_unid
        {
            get { return _rge_tab_cod_unid; }
            set { _rge_tab_cod_unid = value; }
        }
        public decimal rge_ruc_cond
        {
            get { return _rge_ruc_cond; }
            set { _rge_ruc_cond = value; }
        }
        public decimal rge_tco_cond_pago
        {
            get { return _rge_tco_cond_pago; }
            set { _rge_tco_cond_pago = value; }
        }
        public string rge_opt_simp
        {
            get { return _rge_opt_simp; }
            set { _rge_opt_simp = value; }
        }
        public decimal rge_ind_regm_esp
        {
            get { return _rge_ind_regm_esp; }
            set { _rge_ind_regm_esp = value; }
        }
        public decimal rge_ind_subst_trib
        {
            get { return _rge_ind_subst_trib; }
            set { _rge_ind_subst_trib = value; }
        }
        public decimal rge_tge_ttamanho
        {
            get { return _rge_tge_ttamanho; }
            set { _rge_tge_ttamanho = value; }
        }
        public decimal rge_tge_vtamanho
        {
            get { return _rge_tge_vtamanho; }
            set { _rge_tge_vtamanho = value; }
        }
        public decimal rge_mes_cier_ejer
        {
            get { return _rge_mes_cier_ejer; }
            set { _rge_mes_cier_ejer = value; }
        }
        public DateTime rge_fec_ini_act_ec
        {
            get { return _rge_fec_ini_act_ec; }
            set { _rge_fec_ini_act_ec = value; }
        }
        public DateTime rge_fec_sit_cad
        {
            get { return _rge_fec_sit_cad; }
            set { _rge_fec_sit_cad = value; }
        }
        public DateTime rge_fec_actl
        {
            get { return _rge_fec_actl; }
            set { _rge_fec_actl = value; }
        }
        public DateTime rge_fec_val_ruc
        {
            get { return _rge_fec_val_ruc; }
            set { _rge_fec_val_ruc = value; }
        }
        public decimal rge_tge_torig_actu
        {
            get { return _rge_tge_torig_actu; }
            set { _rge_tge_torig_actu = value; }
        }
        public decimal rge_tge_vorig_actu
        {
            get { return _rge_tge_vorig_actu; }
            set { _rge_tge_vorig_actu = value; }
        }
        public string rge_tus_cod_usr
        {
            get { return _rge_tus_cod_usr; }
            set { _rge_tus_cod_usr = value; }
        }
        public decimal rge_tge_ttip_insc
        {
            get { return _rge_tge_ttip_insc; }
            set { _rge_tge_ttip_insc = value; }
        }
        public decimal rge_tge_vtip_insc
        {
            get { return _rge_tge_vtip_insc; }
            set { _rge_tge_vtip_insc = value; }
        }
        public string rge_nomb
        {
            get { return _rge_nomb; }
            set { _rge_nomb = value; }
        }
        public decimal rge_codg_mun
        {
            get { return _rge_codg_mun; }
            set { _rge_codg_mun = value; }
        }
        public decimal rge_tae_cod_actvd
        {
            get { return _rge_tae_cod_actvd; }
            set { _rge_tae_cod_actvd = value; }
        }
        public string rge_tuf_cod_uf
        {
            get { return _rge_tuf_cod_uf; }
            set { _rge_tuf_cod_uf = value; }
        }
        public decimal rge_tda_cod_daf
        {
            get { return _rge_tda_cod_daf; }
            set { _rge_tda_cod_daf = value; }
        }
        public string rge_pra_protocolo
        {
            get { return _rge_pra_protocolo; }
            set { _rge_pra_protocolo = value; }
        }
        #endregion


        public void Update(OracleTransaction bd)
        {
            StringBuilder Sql = new StringBuilder();

            using (OracleCommand cmdToExecute = new OracleCommand())
            {
                cmdToExecute.CommandType = CommandType.Text;

                Sql.Append(" Insert into ruc_general");
                Sql.Append("  (");
                Sql.Append("	rge_ruc, ");
                Sql.Append("	rge_tge_ttip_reg, ");
                Sql.Append("	rge_tge_vtip_reg, ");
                Sql.Append("	rge_tge_ttip_ctrib, ");
                Sql.Append("	rge_tge_vtip_ctrib, ");
                Sql.Append("	rge_tge_ttip_pers, ");
                Sql.Append("	rge_tge_vtip_pers, ");
                Sql.Append("	rge_cgc_cpf, ");
                //Sql.Append("	rge_tsc_sit_cad, ");
                //Sql.Append("	rge_tab_cod_unid, ");
                //Sql.Append("	rge_ruc_cond, ");
                //Sql.Append("	rge_tco_cond_pago, ");
                //Sql.Append("	rge_opt_simp, ");
                //Sql.Append("	rge_ind_regm_esp, ");
                //Sql.Append("	rge_ind_subst_trib, ");
                Sql.Append("	rge_tge_ttamanho, ");
                Sql.Append("	rge_tge_vtamanho, ");
                //Sql.Append("	rge_mes_cier_ejer, ");
                Sql.Append("	rge_fec_ini_act_ec, ");
                //Sql.Append("	rge_fec_sit_cad, ");
                Sql.Append("	rge_fec_actl, ");
                //Sql.Append("	rge_fec_val_ruc, ");
                Sql.Append("	rge_tge_torig_actu, ");
                Sql.Append("	rge_tge_vorig_actu, ");
                Sql.Append("	rge_tus_cod_usr, ");
                //Sql.Append("	rge_tge_ttip_insc, ");
                //Sql.Append("	rge_tge_vtip_insc, ");
                Sql.Append("	rge_nomb, ");
                Sql.Append("	rge_codg_mun, ");
                Sql.Append("	rge_tae_cod_actvd, ");
                Sql.Append("	rge_tuf_cod_uf, ");
                //Sql.Append("	rge_tda_cod_daf, ");
                Sql.Append("	rge_pra_protocolo");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	:v_rge_ruc, ");
                Sql.Append("	:v_rge_tge_ttip_reg, ");
                Sql.Append("	:v_rge_tge_vtip_reg, ");
                Sql.Append("	:v_rge_tge_ttip_ctrib, ");
                Sql.Append("	:v_rge_tge_vtip_ctrib, ");
                Sql.Append("	:v_rge_tge_ttip_pers, ");
                Sql.Append("	:v_rge_tge_vtip_pers, ");
                Sql.Append("	:v_rge_cgc_cpf, ");
                //Sql.Append("	:v_rge_tsc_sit_cad, ");
                //Sql.Append("	:v_rge_tab_cod_unid, ");
                //Sql.Append("	:v_rge_ruc_cond, ");
                //Sql.Append("	:v_rge_tco_cond_pago, ");
                //Sql.Append("	:v_rge_opt_simp, ");
                //Sql.Append("	:v_rge_ind_regm_esp, ");
                //Sql.Append("	:v_rge_ind_subst_trib, ");
                Sql.Append("	:v_rge_tge_ttamanho, ");
                Sql.Append("	:v_rge_tge_vtamanho, ");
                //Sql.Append("	:v_rge_mes_cier_ejer, ");
                Sql.Append("	:v_rge_fec_ini_act_ec, ");
                //Sql.Append("	:v_rge_fec_sit_cad, ");
                Sql.Append("	Sysdate, ");
                //Sql.Append("	:v_rge_fec_val_ruc, ");
                Sql.Append("	:v_rge_tge_torig_actu, ");
                Sql.Append("	:v_rge_tge_vorig_actu, ");
                Sql.Append("	:v_rge_tus_cod_usr, ");
                //Sql.Append("	:v_rge_tge_ttip_insc, ");
                //Sql.Append("	:v_rge_tge_vtip_insc, ");
                Sql.Append("	:v_rge_nomb, ");
                Sql.Append("	:v_rge_codg_mun, ");
                Sql.Append("	:v_rge_tae_cod_actvd, ");
                Sql.Append("	:v_rge_tuf_cod_uf, ");
                //Sql.Append("	:v_rge_tda_cod_daf, ");
                Sql.Append("	:v_rge_pra_protocolo");
                Sql.Append("  )");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionORACLE;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_ruc", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_ruc));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tge_ttip_reg", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tge_ttip_reg));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tge_vtip_reg", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tge_vtip_reg));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tge_ttip_ctrib", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tge_ttip_ctrib));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tge_vtip_ctrib", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tge_vtip_ctrib));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tge_ttip_pers", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tge_ttip_pers));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tge_vtip_pers", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tge_vtip_pers));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_cgc_cpf", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_cgc_cpf));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tsc_sit_cad", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tsc_sit_cad));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tab_cod_unid", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tab_cod_unid));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_ruc_cond", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_ruc_cond));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tco_cond_pago", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tco_cond_pago));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_opt_simp", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_opt_simp));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_ind_regm_esp", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_ind_regm_esp));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_ind_subst_trib", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_ind_subst_trib));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tge_ttamanho", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tge_ttamanho));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tge_vtamanho", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tge_vtamanho));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_mes_cier_ejer", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_mes_cier_ejer));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_fec_ini_act_ec", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_fec_ini_act_ec));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_fec_sit_cad", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_fec_sit_cad));
                // cmdToExecute.Parameters.Add(new OracleParameter("v_rge_fec_actl", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_fec_actl));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_fec_val_ruc", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_fec_val_ruc));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tge_torig_actu", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, 154));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tge_vorig_actu", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, 91));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tus_cod_usr", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, "REGIN"));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tge_ttip_insc", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tge_ttip_insc));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tge_vtip_insc", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tge_vtip_insc));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_nomb", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_nomb));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_codg_mun", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_codg_mun));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tae_cod_actvd", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tae_cod_actvd));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tuf_cod_uf", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tuf_cod_uf));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rge_tda_cod_daf", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_tda_cod_daf));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rge_pra_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rge_pra_protocolo));


                cmdToExecute.Transaction = bd;

                cmdToExecute.Connection = bd.Connection;

                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.CommandText = Sql.ToString();

                cmdToExecute.ExecuteNonQuery();
            }
        }
    }
}
