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
    public class Ruc_Prof : DBInteractionBaseORACLE
    {
        // Variables ******************* 
        #region  Property Declarations
        protected string _rpr_rge_pra_protocolo;
        protected DateTime _rpr_fec_const_nasc;
        protected string _rpr_num_reg_merc = "";
        protected DateTime _rpr_fec_reg_merc;
        protected decimal _rpr_tge_ttip_doc;
        protected decimal _rpr_tge_vtip_doc;
        protected string _rpr_num_doc_ident = "";
        protected DateTime _rpr_fec_emi_doc_id;
        protected decimal _rpr_sexo;
        protected string _rpr_nume = "";
        protected string _rpr_caja_po = "";
        protected string _rpr_zona_caja_po = "";
        protected decimal _rpr_tge_tpais;
        protected decimal _rpr_tge_vpais;
        protected string _rpr_emis_doc_ident = "";
        protected string _rpr_ident_comp = "";
        protected string _rpr_refer = "";
        protected string _rpr_ttl_tip_logradoro = "";
        protected string _rpr_direccion = "";
        protected string _rpr_urbanizacion = "";
        protected string _rpr_tes_cod_estado = "";
        protected string _rpr_zona_postal = "";
        protected decimal _rpr_tmu_cod_mun;
        protected string _rpr_cgc_cpf_secd = "";
        protected decimal _rpr_tge_ttip_pers; 
        protected decimal _rpr_tge_vtip_pers;
        protected string _rpr_nomb = "";
        protected decimal _rpr_cnpj_vacio;
        protected string _rpr_uf_emis = "";
        protected string _rpr_nome_pai = "";
        protected string _rpr_nome_mae = "";
        protected string _rpr_email = "";
        protected string _rpr_escolaridade = "0";

       
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string rpr_rge_pra_protocolo
        {
            get { return _rpr_rge_pra_protocolo; }
            set { _rpr_rge_pra_protocolo = value; }
        }
        public DateTime rpr_fec_const_nasc
        {
            get { return _rpr_fec_const_nasc; }
            set { _rpr_fec_const_nasc = value; }
        }
        public string rpr_num_reg_merc
        {
            get { return _rpr_num_reg_merc; }
            set { _rpr_num_reg_merc = value; }
        }
        public DateTime rpr_fec_reg_merc
        {
            get { return _rpr_fec_reg_merc; }
            set { _rpr_fec_reg_merc = value; }
        }
        public decimal rpr_tge_ttip_doc
        {
            get { return _rpr_tge_ttip_doc; }
            set { _rpr_tge_ttip_doc = value; }
        }
        public decimal rpr_tge_vtip_doc
        {
            get { return _rpr_tge_vtip_doc; }
            set { _rpr_tge_vtip_doc = value; }
        }
        public string rpr_num_doc_ident
        {
            get { return _rpr_num_doc_ident; }
            set { _rpr_num_doc_ident = value; }
        }
        public DateTime rpr_fec_emi_doc_id
        {
            get { return _rpr_fec_emi_doc_id; }
            set { _rpr_fec_emi_doc_id = value; }
        }
        public decimal rpr_sexo
        {
            get { return _rpr_sexo; }
            set { _rpr_sexo = value; }
        }
        public string rpr_nume
        {
            get { return _rpr_nume; }
            set { _rpr_nume = value; }
        }
        public string rpr_caja_po
        {
            get { return _rpr_caja_po; }
            set { _rpr_caja_po = value; }
        }
        public string rpr_zona_caja_po
        {
            get { return _rpr_zona_caja_po; }
            set { _rpr_zona_caja_po = value; }
        }
        public decimal rpr_tge_tpais
        {
            get { return _rpr_tge_tpais; }
            set { _rpr_tge_tpais = value; }
        }
        public decimal rpr_tge_vpais
        {
            get { return _rpr_tge_vpais; }
            set { _rpr_tge_vpais = value; }
        }
        public string rpr_emis_doc_ident
        {
            get { return _rpr_emis_doc_ident; }
            set { _rpr_emis_doc_ident = value; }
        }
        public string rpr_ident_comp
        {
            get { return _rpr_ident_comp; }
            set { _rpr_ident_comp = value; }
        }
        public string rpr_refer
        {
            get { return _rpr_refer; }
            set { _rpr_refer = value; }
        }
        public string rpr_ttl_tip_logradoro
        {
            get { return _rpr_ttl_tip_logradoro; }
            set { _rpr_ttl_tip_logradoro = value; }
        }
        public string rpr_direccion
        {
            get { return _rpr_direccion; }
            set { _rpr_direccion = value; }
        }
        public string rpr_urbanizacion
        {
            get { return _rpr_urbanizacion; }
            set { _rpr_urbanizacion = value; }
        }
        public string rpr_tes_cod_estado
        {
            get { return _rpr_tes_cod_estado; }
            set { _rpr_tes_cod_estado = value; }
        }
        public string rpr_zona_postal
        {
            get { return _rpr_zona_postal; }
            set { _rpr_zona_postal = value; }
        }
        public decimal rpr_tmu_cod_mun
        {
            get { return _rpr_tmu_cod_mun; }
            set { _rpr_tmu_cod_mun = value; }
        }
        public string rpr_cgc_cpf_secd
        {
            get { return _rpr_cgc_cpf_secd; }
            set { _rpr_cgc_cpf_secd = value; }
        }
        public decimal rpr_tge_ttip_pers
        {
            get { return _rpr_tge_ttip_pers; }
            set { _rpr_tge_ttip_pers = value; }
        }
        public decimal rpr_tge_vtip_pers
        {
            get { return _rpr_tge_vtip_pers; }
            set { _rpr_tge_vtip_pers = value; }
        }
        public string rpr_nomb
        {
            get { return _rpr_nomb; }
            set { _rpr_nomb = value; }
        }
        public decimal rpr_cnpj_vacio
        {
            get { return _rpr_cnpj_vacio; }
            set { _rpr_cnpj_vacio = value; }
        }
        public string rpr_uf_emis
        {
            get { return _rpr_uf_emis; }
            set { _rpr_uf_emis = value; }
        }
        public string rpr_nome_pai
        {
            get { return _rpr_nome_pai; }
            set { _rpr_nome_pai = value; }
        }
        public string rpr_nome_mae
        {
            get { return _rpr_nome_mae; }
            set { _rpr_nome_mae = value; }
        }
        public string rpr_email
        {
            get { return _rpr_email; }
            set { _rpr_email = value; }
        }
        public string rpr_escolaridade
        {
            get { return _rpr_escolaridade; }
            set { _rpr_escolaridade = value; }
        }
        #endregion

        public void Update()
        {
            try
            {

                StringBuilder Sql = new StringBuilder();

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandType = CommandType.Text;
                Sql.Append(" Insert into ruc_prof");
                Sql.Append("  (");
                Sql.Append("	rpr_rge_pra_protocolo, ");
                Sql.Append("	rpr_fec_const_nasc, ");
                Sql.Append("	rpr_num_reg_merc, ");
                Sql.Append("	rpr_fec_reg_merc, ");
                Sql.Append("	rpr_tge_ttip_doc, ");
                Sql.Append("	rpr_tge_vtip_doc, ");
                Sql.Append("	rpr_num_doc_ident, ");
                Sql.Append("	rpr_fec_emi_doc_id, ");
                Sql.Append("	rpr_sexo, ");
                Sql.Append("	rpr_nume, ");
                Sql.Append("	rpr_caja_po, ");
                Sql.Append("	rpr_zona_caja_po, ");
                Sql.Append("	rpr_tge_tpais, ");
                Sql.Append("	rpr_tge_vpais, ");
                Sql.Append("	rpr_emis_doc_ident, ");
                Sql.Append("	rpr_ident_comp, ");
                Sql.Append("	rpr_refer, ");
                Sql.Append("	rpr_ttl_tip_logradoro, ");
                Sql.Append("	rpr_direccion, ");
                Sql.Append("	rpr_urbanizacion, ");
                Sql.Append("	rpr_tes_cod_estado, ");
                Sql.Append("	rpr_zona_postal, ");
                Sql.Append("	rpr_tmu_cod_mun, ");
                Sql.Append("	rpr_cgc_cpf_secd, ");
                Sql.Append("	rpr_tge_ttip_pers, ");
                Sql.Append("	rpr_tge_vtip_pers, ");
                Sql.Append("	rpr_nomb, ");
                Sql.Append("	rpr_cnpj_vacio, ");
                Sql.Append("	rpr_uf_emis, ");
                Sql.Append("	rpr_nome_pai, ");
                Sql.Append("	rpr_nome_mae, ");
                Sql.Append("	rpr_email");//, rpr_escolaridade");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	:v_rpr_rge_pra_protocolo, ");
                Sql.Append("	:v_rpr_fec_const_nasc, ");
                Sql.Append("	:v_rpr_num_reg_merc, ");
                Sql.Append("	:v_rpr_fec_reg_merc, ");
                Sql.Append("	:v_rpr_tge_ttip_doc, ");
                Sql.Append("	:v_rpr_tge_vtip_doc, ");
                Sql.Append("	:v_rpr_num_doc_ident, ");
                Sql.Append("	:v_rpr_fec_emi_doc_id, ");
                Sql.Append("	:v_rpr_sexo, ");
                Sql.Append("	:v_rpr_nume, ");
                Sql.Append("	:v_rpr_caja_po, ");
                Sql.Append("	:v_rpr_zona_caja_po, ");
                Sql.Append("	:v_rpr_tge_tpais, ");
                Sql.Append("	:v_rpr_tge_vpais, ");
                Sql.Append("	:v_rpr_emis_doc_ident, ");
                Sql.Append("	:v_rpr_ident_comp, ");
                Sql.Append("	:v_rpr_refer, ");
                Sql.Append("	:v_rpr_ttl_tip_logradoro, ");
                Sql.Append("	:v_rpr_direccion, ");
                Sql.Append("	:v_rpr_urbanizacion, ");
                Sql.Append("	:v_rpr_tes_cod_estado, ");
                Sql.Append("	:v_rpr_zona_postal, ");
                Sql.Append("	:v_rpr_tmu_cod_mun, ");
                Sql.Append("	:v_rpr_cgc_cpf_secd, ");
                Sql.Append("	:v_rpr_tge_ttip_pers, ");
                Sql.Append("	:v_rpr_tge_vtip_pers, ");
                Sql.Append("	:v_rpr_nomb, ");
                Sql.Append("	:v_rpr_cnpj_vacio, ");
                Sql.Append("	:v_rpr_uf_emis, ");
                Sql.Append("	:v_rpr_nome_pai, ");
                Sql.Append("	:v_rpr_nome_mae, ");
                Sql.Append("	:v_rpr_email");//, :v_rpr_escolaridade");
                Sql.Append("  )");


                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionORACLE;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_rge_pra_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_rge_pra_protocolo));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_fec_const_nasc", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_fec_const_nasc));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_num_reg_merc", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_num_reg_merc));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_fec_reg_merc", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_fec_reg_merc));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_tge_ttip_doc", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_tge_ttip_doc));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_tge_vtip_doc", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_tge_vtip_doc));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_num_doc_ident", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_num_doc_ident));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_fec_emi_doc_id", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_fec_emi_doc_id));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_sexo", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_sexo));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_nume", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_nume));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_caja_po", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_caja_po));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_zona_caja_po", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_zona_caja_po));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_tge_tpais", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_tge_tpais));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_tge_vpais", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_tge_vpais));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_emis_doc_ident", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_emis_doc_ident));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_ident_comp", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_ident_comp));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_refer", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_refer));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_ttl_tip_logradoro", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_ttl_tip_logradoro));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_direccion", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_direccion));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_urbanizacion", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_urbanizacion));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_tes_cod_estado", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_tes_cod_estado));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_zona_postal", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_zona_postal));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_tmu_cod_mun", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_tmu_cod_mun));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_cgc_cpf_secd", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_cgc_cpf_secd));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_tge_ttip_pers", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_tge_ttip_pers));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_tge_vtip_pers", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_tge_vtip_pers));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_nomb", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_nomb));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_cnpj_vacio", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_cnpj_vacio));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_uf_emis", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_uf_emis));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_nome_pai", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_nome_pai));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_nome_mae", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_nome_mae));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_email", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_email));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_rpr_escolaridade", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rpr_escolaridade));

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
