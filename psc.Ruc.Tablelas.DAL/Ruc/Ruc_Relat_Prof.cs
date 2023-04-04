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
    public class Ruc_Relat_Prof : DBInteractionBaseORACLE
    {
        // Variables ******************* 
        #region  Property Declarations
        protected string _rrp_rge_pra_protocolo;
        protected string _rrp_cgc_cpf_secd = "";
        protected decimal _rrp_tge_ttip_relac;
        protected decimal _rrp_tge_vtip_relac;
        protected DateTime _rrp_fec_inic_part;
        protected DateTime _rrp_fec_fin_part;
        protected string _rrp_crc_ctdr = "";
        protected string _rrp_uf_crc_ctdr = "";
        protected decimal _rrp_ubic_libr_fisc;
        protected decimal _rrp_tge_tsit_empl;
        protected decimal _rrp_tge_vsit_empl;
        protected DateTime _rrp_fec_actl;
        protected decimal _rrp_porc_part;
        protected decimal _rrp_porc_cap_vota;
        protected decimal _rrp_tip_doc;
        protected decimal _rrp_tge_tcod_qual;
        protected decimal _rrp_tge_vcod_qual;
        protected string _rrp_cedu_prof = "";
        protected string _rrp_desc_doc = "";
        protected string _rrp_tus_cod_usr = "";
        protected decimal _rrp_cnpj_vacio = 0;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string rrp_rge_pra_protocolo
        {
            get { return _rrp_rge_pra_protocolo; }
            set { _rrp_rge_pra_protocolo = value; }
        }
        public string rrp_cgc_cpf_secd
        {
            get { return _rrp_cgc_cpf_secd; }
            set { _rrp_cgc_cpf_secd = value; }
        }
        public decimal rrp_tge_ttip_relac
        {
            get { return _rrp_tge_ttip_relac; }
            set { _rrp_tge_ttip_relac = value; }
        }
        public decimal rrp_tge_vtip_relac
        {
            get { return _rrp_tge_vtip_relac; }
            set { _rrp_tge_vtip_relac = value; }
        }
        public DateTime rrp_fec_inic_part
        {
            get { return _rrp_fec_inic_part; }
            set { _rrp_fec_inic_part = value; }
        }
        public DateTime rrp_fec_fin_part
        {
            get { return _rrp_fec_fin_part; }
            set { _rrp_fec_fin_part = value; }
        }
        public string rrp_crc_ctdr
        {
            get { return _rrp_crc_ctdr; }
            set { _rrp_crc_ctdr = value; }
        }
        public string rrp_uf_crc_ctdr
        {
            get { return _rrp_uf_crc_ctdr; }
            set { _rrp_uf_crc_ctdr = value; }
        }
        public decimal rrp_ubic_libr_fisc
        {
            get { return _rrp_ubic_libr_fisc; }
            set { _rrp_ubic_libr_fisc = value; }
        }
        public decimal rrp_tge_tsit_empl
        {
            get { return _rrp_tge_tsit_empl; }
            set { _rrp_tge_tsit_empl = value; }
        }
        public decimal rrp_tge_vsit_empl
        {
            get { return _rrp_tge_vsit_empl; }
            set { _rrp_tge_vsit_empl = value; }
        }
        public DateTime rrp_fec_actl
        {
            get { return _rrp_fec_actl; }
            set { _rrp_fec_actl = value; }
        }
        public decimal rrp_porc_part
        {
            get { return _rrp_porc_part; }
            set { _rrp_porc_part = value; }
        }
        public decimal rrp_porc_cap_vota
        {
            get { return _rrp_porc_cap_vota; }
            set { _rrp_porc_cap_vota = value; }
        }
        public decimal rrp_tip_doc
        {
            get { return _rrp_tip_doc; }
            set { _rrp_tip_doc = value; }
        }
        public decimal rrp_tge_tcod_qual
        {
            get { return _rrp_tge_tcod_qual; }
            set { _rrp_tge_tcod_qual = value; }
        }
        public decimal rrp_tge_vcod_qual
        {
            get { return _rrp_tge_vcod_qual; }
            set { _rrp_tge_vcod_qual = value; }
        }
        public string rrp_cedu_prof
        {
            get { return _rrp_cedu_prof; }
            set { _rrp_cedu_prof = value; }
        }
        public string rrp_desc_doc
        {
            get { return _rrp_desc_doc; }
            set { _rrp_desc_doc = value; }
        }
        public string rrp_tus_cod_usr
        {
            get { return _rrp_tus_cod_usr; }
            set { _rrp_tus_cod_usr = value; }
        }
        public decimal rrp_cnpj_vacio
        {
            get { return _rrp_cnpj_vacio; }
            set { _rrp_cnpj_vacio = value; }
        }
        #endregion
        
        public void UpdateV2()
        {
            try
            {

                
                StringBuilder Sql = new StringBuilder();

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandType = CommandType.Text;
                Sql.Append(" Insert into ruc_relat_prof");
                Sql.Append("  (");
                Sql.Append("	rrp_rge_pra_protocolo, ");
                Sql.Append("	rrp_cgc_cpf_secd, ");
                Sql.Append("	rrp_tge_ttip_relac, ");
                Sql.Append("	rrp_tge_vtip_relac, ");
                Sql.Append("	rrp_fec_inic_part, ");
                //Sql.Append("	rrp_fec_fin_part, ");
                //Sql.Append("	rrp_crc_ctdr, ");
                //Sql.Append("	rrp_uf_crc_ctdr, ");
                //Sql.Append("	rrp_ubic_libr_fisc, ");
                //Sql.Append("	rrp_tge_tsit_empl, ");
                //Sql.Append("	rrp_tge_vsit_empl, ");
                //Sql.Append("	rrp_fec_actl, ");
                //Sql.Append("	rrp_porc_part, ");
                //Sql.Append("	rrp_porc_cap_vota, ");
                //Sql.Append("	rrp_tip_doc, ");
                Sql.Append("	rrp_tge_tcod_qual, ");
                Sql.Append("	rrp_tge_vcod_qual, ");
                Sql.Append("	rrp_cedu_prof, ");
                Sql.Append("	rrp_desc_doc, ");
                Sql.Append("	rrp_tus_cod_usr, ");
                Sql.Append("	rrp_cnpj_vacio");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	:v_rrp_rge_pra_protocolo, ");
                Sql.Append("	:v_rrp_cgc_cpf_secd, ");
                Sql.Append("	:v_rrp_tge_ttip_relac, ");
                Sql.Append("	:v_rrp_tge_vtip_relac, ");
                Sql.Append("	Sysdate, ");
                //Sql.Append("	:v_rrp_fec_fin_part, ");
                //Sql.Append("	:v_rrp_crc_ctdr, ");
                //Sql.Append("	:v_rrp_uf_crc_ctdr, ");
                //Sql.Append("	:v_rrp_ubic_libr_fisc, ");
                //Sql.Append("	:v_rrp_tge_tsit_empl, ");
                //Sql.Append("	:v_rrp_tge_vsit_empl, ");
                //Sql.Append("	:v_rrp_fec_actl, ");
                //Sql.Append("	:v_rrp_porc_part, ");
                //Sql.Append("	:v_rrp_porc_cap_vota, ");
                //Sql.Append("	:v_rrp_tip_doc, ");
                Sql.Append("	:v_rrp_tge_tcod_qual, ");
                Sql.Append("	:v_rrp_tge_vcod_qual, ");
                Sql.Append("	:v_rrp_cedu_prof, ");
                Sql.Append("	:v_rrp_desc_doc, ");
                Sql.Append("	:v_rrp_tus_cod_usr, ");
                Sql.Append("	:v_rrp_cnpj_vacio");
                Sql.Append("  )");


                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionORACLE;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_rge_pra_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_rge_pra_protocolo));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_cgc_cpf_secd", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_cgc_cpf_secd));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_tge_ttip_relac", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_tge_ttip_relac));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_tge_vtip_relac", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_tge_vtip_relac));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_fec_inic_part", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_fec_inic_part));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_fec_fin_part", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_fec_fin_part));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_crc_ctdr", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_crc_ctdr));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_uf_crc_ctdr", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_uf_crc_ctdr));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_ubic_libr_fisc", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_ubic_libr_fisc));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_tge_tsit_empl", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_tge_tsit_empl));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_tge_vsit_empl", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_tge_vsit_empl));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_fec_actl", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_fec_actl));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_porc_part", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_porc_part));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_porc_cap_vota", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_porc_cap_vota));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_tip_doc", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_tip_doc));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_tge_tcod_qual", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_tge_tcod_qual));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_tge_vcod_qual", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_tge_vcod_qual));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_cedu_prof", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_cedu_prof));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_desc_doc", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_desc_doc));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_tus_cod_usr", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_tus_cod_usr));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rrp_cnpj_vacio", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rrp_cnpj_vacio));

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
