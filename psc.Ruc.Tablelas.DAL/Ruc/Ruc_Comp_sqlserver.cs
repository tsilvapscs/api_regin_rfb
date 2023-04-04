using System;
using System.Collections.Generic;
using System.Text;
//using psc.Ruc.Tablelas.Entities;
using psc.Ruc.Tablelas.ConnectionBase;
////using psc.ApplicationBlocks.DAL;
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
    public class Ruc_Comp_sqlserver : DBInteractionBaseSQL
    {
        // Variables ******************* 
        #region  Property Declarations
        protected DateTime _rco_fec_const_nasc;
        protected string _rco_num_reg_merc;
        protected DateTime _rco_fec_reg_merc;
        protected decimal _rco_tge_ttip_doc;
        protected decimal _rco_tge_vtip_doc;
        protected string _rco_num_doc_ident;
        protected DateTime _rco_fec_emi_doc_id;
        protected decimal _rco_tnc_cod_natur;
        protected decimal _rco_domic_pais;
        protected DateTime _rco_fec_incorp;
        protected decimal _rco_val_cap_soc;
        protected DateTime _rco_fec_rg_cap_soc;
        protected decimal _rco_sexo;
        protected string _rco_nume;
        protected string _rco_caja_po;
        protected string _rco_zona_caja_po;
        protected decimal _rco_tge_tpais;
        protected decimal _rco_tge_vpais;
        protected string _rco_ruc_ext_uf;
        protected string _rco_tus_cod_usr;
        protected string _rco_emis_doc_ident;
        protected string _rco_quad_lote;
        protected string _rco_ident_comp;
        protected string _rco_refer;
        protected string _rco_lic_mun;
        protected string _rco_ttl_tip_logradoro;
        protected string _rco_direccion;
        protected string _rco_urbanizacion;
        protected string _rco_tes_cod_estado;
        protected string _rco_zona_postal;
        protected decimal _rco_tge_tcier_bal;
        protected decimal _rco_tge_vcier_bal;
        protected decimal _rco_tge_treg_trib;
        protected decimal _rco_tge_vreg_trib;
        protected decimal _rco_tmu_cod_mun;
        protected string _rco_rge_pra_protocolo;
        protected string _rco_num_reg_merc_sede;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public DateTime rco_fec_const_nasc
        {
            get { return _rco_fec_const_nasc; }
            set { _rco_fec_const_nasc = value; }
        }
        public string rco_num_reg_merc
        {
            get { return _rco_num_reg_merc; }
            set { _rco_num_reg_merc = value; }
        }
        public DateTime rco_fec_reg_merc
        {
            get { return _rco_fec_reg_merc; }
            set { _rco_fec_reg_merc = value; }
        }
        public decimal rco_tge_ttip_doc
        {
            get { return _rco_tge_ttip_doc; }
            set { _rco_tge_ttip_doc = value; }
        }
        public decimal rco_tge_vtip_doc
        {
            get { return _rco_tge_vtip_doc; }
            set { _rco_tge_vtip_doc = value; }
        }
        public string rco_num_doc_ident
        {
            get { return _rco_num_doc_ident; }
            set { _rco_num_doc_ident = value; }
        }
        public DateTime rco_fec_emi_doc_id
        {
            get { return _rco_fec_emi_doc_id; }
            set { _rco_fec_emi_doc_id = value; }
        }
        public decimal rco_tnc_cod_natur
        {
            get { return _rco_tnc_cod_natur; }
            set { _rco_tnc_cod_natur = value; }
        }
        public decimal rco_domic_pais
        {
            get { return _rco_domic_pais; }
            set { _rco_domic_pais = value; }
        }
        public DateTime rco_fec_incorp
        {
            get { return _rco_fec_incorp; }
            set { _rco_fec_incorp = value; }
        }
        public decimal rco_val_cap_soc
        {
            get { return _rco_val_cap_soc; }
            set { _rco_val_cap_soc = value; }
        }
        public DateTime rco_fec_rg_cap_soc
        {
            get { return _rco_fec_rg_cap_soc; }
            set { _rco_fec_rg_cap_soc = value; }
        }
        public decimal rco_sexo
        {
            get { return _rco_sexo; }
            set { _rco_sexo = value; }
        }
        public string rco_nume
        {
            get { return _rco_nume; }
            set { _rco_nume = value; }
        }
        public string rco_caja_po
        {
            get { return _rco_caja_po; }
            set { _rco_caja_po = value; }
        }
        public string rco_zona_caja_po
        {
            get { return _rco_zona_caja_po; }
            set { _rco_zona_caja_po = value; }
        }
        public decimal rco_tge_tpais
        {
            get { return _rco_tge_tpais; }
            set { _rco_tge_tpais = value; }
        }
        public decimal rco_tge_vpais
        {
            get { return _rco_tge_vpais; }
            set { _rco_tge_vpais = value; }
        }
        public string rco_ruc_ext_uf
        {
            get { return _rco_ruc_ext_uf; }
            set { _rco_ruc_ext_uf = value; }
        }
        public string rco_tus_cod_usr
        {
            get { return _rco_tus_cod_usr; }
            set { _rco_tus_cod_usr = value; }
        }
        public string rco_emis_doc_ident
        {
            get { return _rco_emis_doc_ident; }
            set { _rco_emis_doc_ident = value; }
        }
        public string rco_quad_lote
        {
            get { return _rco_quad_lote; }
            set { _rco_quad_lote = value; }
        }
        public string rco_ident_comp
        {
            get { return _rco_ident_comp; }
            set { _rco_ident_comp = value; }
        }
        public string rco_refer
        {
            get { return _rco_refer; }
            set { _rco_refer = value; }
        }
        public string rco_lic_mun
        {
            get { return _rco_lic_mun; }
            set { _rco_lic_mun = value; }
        }
        public string rco_ttl_tip_logradoro
        {
            get { return _rco_ttl_tip_logradoro; }
            set { _rco_ttl_tip_logradoro = value; }
        }
        public string rco_direccion
        {
            get { return _rco_direccion; }
            set { _rco_direccion = value; }
        }
        public string rco_urbanizacion
        {
            get { return _rco_urbanizacion; }
            set { _rco_urbanizacion = value; }
        }
        public string rco_tes_cod_estado
        {
            get { return _rco_tes_cod_estado; }
            set { _rco_tes_cod_estado = value; }
        }
        public string rco_zona_postal
        {
            get { return _rco_zona_postal; }
            set { _rco_zona_postal = value; }
        }
        public decimal rco_tge_tcier_bal
        {
            get { return _rco_tge_tcier_bal; }
            set { _rco_tge_tcier_bal = value; }
        }
        public decimal rco_tge_vcier_bal
        {
            get { return _rco_tge_vcier_bal; }
            set { _rco_tge_vcier_bal = value; }
        }
        public decimal rco_tge_treg_trib
        {
            get { return _rco_tge_treg_trib; }
            set { _rco_tge_treg_trib = value; }
        }
        public decimal rco_tge_vreg_trib
        {
            get { return _rco_tge_vreg_trib; }
            set { _rco_tge_vreg_trib = value; }
        }
        public decimal rco_tmu_cod_mun
        {
            get { return _rco_tmu_cod_mun; }
            set { _rco_tmu_cod_mun = value; }
        }
        public string rco_rge_pra_protocolo
        {
            get { return _rco_rge_pra_protocolo; }
            set { _rco_rge_pra_protocolo = value; }
        }
        public string rco_num_reg_merc_sede
        {
            get { return _rco_num_reg_merc_sede; }
            set { _rco_num_reg_merc_sede = value; }
        }
        #endregion

        public void Update(SqlTransaction bd)
        {

            StringBuilder Sql = new StringBuilder();

            using (SqlCommand cmdToExecute = new SqlCommand())
            {
                cmdToExecute.CommandType = CommandType.Text;
                Sql.Append(" Insert into ruc_comp");
                Sql.Append("  (");
                Sql.Append("	rco_fec_const_nasc, ");
                Sql.Append("	rco_num_reg_merc, ");
                Sql.Append("	rco_fec_reg_merc, ");
                Sql.Append("	rco_tge_ttip_doc, ");
                Sql.Append("	rco_tge_vtip_doc, ");
                //Sql.Append("	rco_num_doc_ident, ");
                //Sql.Append("	rco_fec_emi_doc_id, ");
                Sql.Append("	rco_tnc_cod_natur, ");
                Sql.Append("	rco_domic_pais, ");
                Sql.Append("	rco_fec_incorp, ");
                Sql.Append("	rco_val_cap_soc, ");
                //Sql.Append("	rco_fec_rg_cap_soc, ");
                //Sql.Append("	rco_sexo, ");
                Sql.Append("	rco_nume, ");
                //Sql.Append("	rco_caja_po, ");
                //Sql.Append("	rco_zona_caja_po, ");
                Sql.Append("	rco_tge_tpais, ");
                Sql.Append("	rco_tge_vpais, ");
                //Sql.Append("	rco_ruc_ext_uf, ");
                Sql.Append("	rco_tus_cod_usr, ");
                //Sql.Append("	rco_emis_doc_ident, ");
                //Sql.Append("	rco_quad_lote, ");
                Sql.Append("	rco_ident_comp, ");
                Sql.Append("	rco_refer, ");
                Sql.Append("	rco_lic_mun, ");
                Sql.Append("	rco_ttl_tip_logradoro, ");
                Sql.Append("	rco_direccion, ");
                Sql.Append("	rco_urbanizacion, ");
                Sql.Append("	rco_tes_cod_estado, ");
                Sql.Append("	rco_zona_postal, ");
                //Sql.Append("	rco_tge_tcier_bal, ");
                //Sql.Append("	rco_tge_vcier_bal, ");
                //Sql.Append("	rco_tge_treg_trib, ");
                //Sql.Append("	rco_tge_vreg_trib, ");
                Sql.Append("	rco_tmu_cod_mun, ");
                Sql.Append("	rco_rge_pra_protocolo ");
                //Sql.Append("	rco_num_reg_merc_sede");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	dbo.EvalDate(@v_rco_fec_const_nasc), ");
                Sql.Append("	@v_rco_num_reg_merc, ");
                Sql.Append("	dbo.EvalDate(@v_rco_fec_reg_merc), ");
                Sql.Append("	@v_rco_tge_ttip_doc, ");
                Sql.Append("	@v_rco_tge_vtip_doc, ");
                //Sql.Append("	@v_rco_num_doc_ident, ");
                //Sql.Append("	@v_rco_fec_emi_doc_id, ");
                Sql.Append("	@v_rco_tnc_cod_natur, ");
                Sql.Append("	@v_rco_domic_pais, ");
                Sql.Append("	@v_rco_fec_incorp, ");
                Sql.Append("	@v_rco_val_cap_soc, ");
                //Sql.Append("	@v_rco_fec_rg_cap_soc, ");
                //Sql.Append("	@v_rco_sexo, ");
                Sql.Append("	@v_rco_nume, ");
                //Sql.Append("	@v_rco_caja_po, ");
                //Sql.Append("	@v_rco_zona_caja_po, ");
                Sql.Append("	@v_rco_tge_tpais, ");
                Sql.Append("	@v_rco_tge_vpais, ");
                //Sql.Append("	@v_rco_ruc_ext_uf, ");
                Sql.Append("	@v_rco_tus_cod_usr, ");
                //Sql.Append("	@v_rco_emis_doc_ident, ");
                //Sql.Append("	@v_rco_quad_lote, ");
                Sql.Append("	@v_rco_ident_comp, ");
                Sql.Append("	@v_rco_refer, ");
                Sql.Append("	@v_rco_lic_mun, ");
                Sql.Append("	@v_rco_ttl_tip_logradoro, ");
                Sql.Append("	@v_rco_direccion, ");
                Sql.Append("	@v_rco_urbanizacion, ");
                Sql.Append("	@v_rco_tes_cod_estado, ");
                Sql.Append("	@v_rco_zona_postal, ");
                //Sql.Append("	@v_rco_tge_tcier_bal, ");
                //Sql.Append("	@v_rco_tge_vcier_bal, ");
                //Sql.Append("	@v_rco_tge_treg_trib, ");
                //Sql.Append("	@v_rco_tge_vreg_trib, ");
                Sql.Append("	@v_rco_tmu_cod_mun, ");
                Sql.Append("	@v_rco_rge_pra_protocolo ");
                //Sql.Append("	@v_rco_num_reg_merc_sede");
                Sql.Append("  )");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionSQL;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_fec_const_nasc", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_fec_const_nasc));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_num_reg_merc", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_num_reg_merc));
                if (_rco_fec_reg_merc == DateTime.MinValue)
                    cmdToExecute.Parameters.Add(new SqlParameter("v_rco_fec_reg_merc", SqlDbType.Date, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, "01/01/1900"));
                else
                    cmdToExecute.Parameters.Add(new SqlParameter("v_rco_fec_reg_merc", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_fec_reg_merc));

                
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_tge_ttip_doc", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_tge_ttip_doc));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_tge_vtip_doc", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_tge_vtip_doc));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_num_doc_ident", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_num_doc_ident));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_fec_emi_doc_id", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_fec_emi_doc_id));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_tnc_cod_natur", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_tnc_cod_natur));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_domic_pais", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_domic_pais));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_fec_incorp", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_fec_incorp));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_val_cap_soc", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_val_cap_soc));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_fec_rg_cap_soc", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_fec_rg_cap_soc));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_sexo", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_sexo));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_nume", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_nume));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_caja_po", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_caja_po));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_zona_caja_po", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_zona_caja_po));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_tge_tpais", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_tge_tpais));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_tge_vpais", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_tge_vpais));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_ruc_ext_uf", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_ruc_ext_uf));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_tus_cod_usr", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_tus_cod_usr));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_emis_doc_ident", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_emis_doc_ident));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_quad_lote", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_quad_lote));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_ident_comp", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_ident_comp));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_refer", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_refer));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_lic_mun", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_lic_mun));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_ttl_tip_logradoro", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_ttl_tip_logradoro));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_direccion", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_direccion));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_urbanizacion", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_urbanizacion));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_tes_cod_estado", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_tes_cod_estado));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_zona_postal", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_zona_postal));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_tge_tcier_bal", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_tge_tcier_bal));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_tge_vcier_bal", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_tge_vcier_bal));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_tge_treg_trib", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_tge_treg_trib));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_tge_vreg_trib", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_tge_vreg_trib));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_tmu_cod_mun", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_tmu_cod_mun));
                cmdToExecute.Parameters.Add(new SqlParameter("v_rco_rge_pra_protocolo", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_rge_pra_protocolo));
                //cmdToExecute.Parameters.Add(new SqlParameter("v_rco_num_reg_merc_sede", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rco_num_reg_merc_sede));



                cmdToExecute.Transaction = bd;

                cmdToExecute.Connection = bd.Connection;

                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.CommandText = Sql.ToString();

                cmdToExecute.ExecuteNonQuery();
            }
        }
    }
}
