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
using System.Xml;
using System.IO;
using System.Data.OleDb;

namespace psc.Ruc.Tablelas.Ruc
{
    public class Ruc_Outro_Estab : DBInteractionBaseORACLE
    {
        // Variables ******************* 
        #region  Property Declarations
        protected decimal _res_ide_estab;
        private string _res_viabilidade;
        protected decimal _res_tip_estab;
        protected decimal _res_tge_ttip_reg;
        protected decimal _res_tge_vtip_reg;
        protected decimal _res_tge_tcond_uso;
        protected decimal _res_tge_vcond_uso;
        protected string _res_sigl;
        protected decimal _res_area;
        protected decimal _res_tge_tuni_medid;
        protected decimal _res_tge_vuni_medid;
        protected DateTime _res_fec_inic;
        protected DateTime _res_fec_fin;
        protected string _res_nume;
        protected string _res_caja_po;
        protected string _res_zona_caja_po;
        protected string _res_tus_cod_usr;
        protected string _res_nom_estab;
        protected string _res_num_reg_prop;
        protected string _res_quad_lote;
        protected string _res_ident_comp;
        protected string _res_refer;
        protected string _res_ttl_tip_logradoro;
        protected string _res_direccion;
        protected string _res_urbanizacion;
        protected string _res_tes_cod_estado;
        protected string _res_zona_postal;
        protected decimal _res_tmu_cod_mun;
        protected string _res_rge_pra_protocolo;
        protected string _res_nire_sede;
        protected string _res_cnpj_sede;
        private string _res_auto_latitude;
        protected string _res_auto_longitude;
        protected string _res_ajuste_latitude;
        protected string _res_ajuste_longitude;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public decimal res_ide_estab
        {
            get { return _res_ide_estab; }
            set { _res_ide_estab = value; }
        }
        public string Res_viabilidade
        {
            get { return _res_viabilidade; }
            set { _res_viabilidade = value; }
        }
        public decimal res_tip_estab
        {
            get { return _res_tip_estab; }
            set { _res_tip_estab = value; }
        }
        public decimal res_tge_ttip_reg
        {
            get { return _res_tge_ttip_reg; }
            set { _res_tge_ttip_reg = value; }
        }
        public decimal res_tge_vtip_reg
        {
            get { return _res_tge_vtip_reg; }
            set { _res_tge_vtip_reg = value; }
        }
        public decimal res_tge_tcond_uso
        {
            get { return _res_tge_tcond_uso; }
            set { _res_tge_tcond_uso = value; }
        }
        public decimal res_tge_vcond_uso
        {
            get { return _res_tge_vcond_uso; }
            set { _res_tge_vcond_uso = value; }
        }
        public string res_sigl
        {
            get { return _res_sigl; }
            set { _res_sigl = value; }
        }
        public decimal res_area
        {
            get { return _res_area; }
            set { _res_area = value; }
        }
        public decimal res_tge_tuni_medid
        {
            get { return _res_tge_tuni_medid; }
            set { _res_tge_tuni_medid = value; }
        }
        public decimal res_tge_vuni_medid
        {
            get { return _res_tge_vuni_medid; }
            set { _res_tge_vuni_medid = value; }
        }
        public DateTime res_fec_inic
        {
            get { return _res_fec_inic; }
            set { _res_fec_inic = value; }
        }
        public DateTime res_fec_fin
        {
            get { return _res_fec_fin; }
            set { _res_fec_fin = value; }
        }
        public string res_nume
        {
            get { return _res_nume; }
            set { _res_nume = value; }
        }
        public string res_caja_po
        {
            get { return _res_caja_po; }
            set { _res_caja_po = value; }
        }
        public string res_zona_caja_po
        {
            get { return _res_zona_caja_po; }
            set { _res_zona_caja_po = value; }
        }
        public string res_tus_cod_usr
        {
            get { return _res_tus_cod_usr; }
            set { _res_tus_cod_usr = value; }
        }
        public string res_nom_estab
        {
            get { return _res_nom_estab; }
            set { _res_nom_estab = value; }
        }
        public string res_num_reg_prop
        {
            get { return _res_num_reg_prop; }
            set { _res_num_reg_prop = value; }
        }
        public string res_quad_lote
        {
            get { return _res_quad_lote; }
            set { _res_quad_lote = value; }
        }
        public string res_ident_comp
        {
            get { return _res_ident_comp; }
            set { _res_ident_comp = value; }
        }
        public string res_refer
        {
            get { return _res_refer; }
            set { _res_refer = value; }
        }
        public string res_ttl_tip_logradoro
        {
            get { return _res_ttl_tip_logradoro; }
            set { _res_ttl_tip_logradoro = value; }
        }
        public string res_direccion
        {
            get { return _res_direccion; }
            set { _res_direccion = value; }
        }
        public string res_urbanizacion
        {
            get { return _res_urbanizacion; }
            set { _res_urbanizacion = value; }
        }
        public string res_tes_cod_estado
        {
            get { return _res_tes_cod_estado; }
            set { _res_tes_cod_estado = value; }
        }
        public string res_zona_postal
        {
            get { return _res_zona_postal; }
            set { _res_zona_postal = value; }
        }
        public decimal res_tmu_cod_mun
        {
            get { return _res_tmu_cod_mun; }
            set { _res_tmu_cod_mun = value; }
        }
        public string res_rge_pra_protocolo
        {
            get { return _res_rge_pra_protocolo; }
            set { _res_rge_pra_protocolo = value; }
        }
        public string res_nire_sede
        {
            get { return _res_nire_sede; }
            set { _res_nire_sede = value; }
        }
        public string res_cnpj_sede
        {
            get { return _res_cnpj_sede; }
            set { _res_cnpj_sede = value; }
        }
        
        #endregion

        public void Update()
        {
            try
            {
                StringBuilder Sql = new StringBuilder();

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandType = CommandType.Text;
                Sql.Append(" Insert into RUC_OUTRO_ESTAB");
                Sql.Append("  (");
                Sql.Append("	res_ide_estab, ");
                Sql.Append("	res_viabilidade, ");
                Sql.Append("	res_tip_estab, ");
                Sql.Append("	res_tge_ttip_reg, ");
                Sql.Append("	res_tge_vtip_reg, ");
                Sql.Append("	res_tge_tcond_uso, ");
                Sql.Append("	res_tge_vcond_uso, ");
                Sql.Append("	res_sigl, ");
                Sql.Append("	res_area, ");
                Sql.Append("	res_tge_tuni_medid, ");
                Sql.Append("	res_tge_vuni_medid, ");
                //Sql.Append("	res_fec_inic, ");
                //Sql.Append("	res_fec_fin, ");
                Sql.Append("	res_nume, ");
                //Sql.Append("	res_caja_po, ");
                //Sql.Append("	res_zona_caja_po, ");
                Sql.Append("	res_tus_cod_usr, ");
                Sql.Append("	res_nom_estab, ");
                //Sql.Append("	res_num_reg_prop, ");
                //Sql.Append("	res_quad_lote, ");
                Sql.Append("	res_ident_comp, ");
                Sql.Append("	res_refer, ");
                Sql.Append("	res_ttl_tip_logradoro, ");
                Sql.Append("	res_direccion, ");
                Sql.Append("	res_urbanizacion, ");
                Sql.Append("	res_tes_cod_estado, ");
                Sql.Append("	res_zona_postal, ");
                Sql.Append("	res_tmu_cod_mun, ");
                Sql.Append("	res_rge_pra_protocolo, ");
                Sql.Append("	res_nire_sede, ");
                Sql.Append("	res_cnpj_sede");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	:v_res_ide_estab, ");
                Sql.Append("    :v_res_viabilidade, ");
                Sql.Append("	:v_res_tip_estab, ");
                Sql.Append("	:v_res_tge_ttip_reg, ");
                Sql.Append("	:v_res_tge_vtip_reg, ");
                Sql.Append("	:v_res_tge_tcond_uso, ");
                Sql.Append("	:v_res_tge_vcond_uso, ");
                Sql.Append("	:v_res_sigl, ");
                Sql.Append("	:v_res_area, ");
                Sql.Append("	:v_res_tge_tuni_medid, ");
                Sql.Append("	:v_res_tge_vuni_medid, ");
                //Sql.Append("	:v_res_fec_inic, ");
                //Sql.Append("	:v_res_fec_fin, ");
                Sql.Append("	:v_res_nume, ");
                //Sql.Append("	:v_res_caja_po, ");
                //Sql.Append("	:v_res_zona_caja_po, ");
                Sql.Append("	:v_res_tus_cod_usr, ");
                Sql.Append("	:v_res_nom_estab, ");
                //Sql.Append("	:v_res_num_reg_prop, ");
                //Sql.Append("	:v_res_quad_lote, ");
                Sql.Append("	:v_res_ident_comp, ");
                Sql.Append("	:v_res_refer, ");
                Sql.Append("	:v_res_ttl_tip_logradoro, ");
                Sql.Append("	:v_res_direccion, ");
                Sql.Append("	:v_res_urbanizacion, ");
                Sql.Append("	:v_res_tes_cod_estado, ");
                Sql.Append("	:v_res_zona_postal, ");
                Sql.Append("	:v_res_tmu_cod_mun, ");
                Sql.Append("	:v_res_rge_pra_protocolo, ");
                Sql.Append("	:v_res_nire_sede, ");
                Sql.Append("	:v_res_cnpj_sede");
                Sql.Append("  )");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionORACLE;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_ide_estab", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_ide_estab));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_viabilidade", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_viabilidade));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_tip_estab", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_tip_estab));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_tge_ttip_reg", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_tge_ttip_reg));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_tge_vtip_reg", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_tge_vtip_reg));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_tge_tcond_uso", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_tge_tcond_uso));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_tge_vcond_uso", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_tge_vcond_uso));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_sigl", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_sigl));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_area", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_area));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_tge_tuni_medid", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_tge_tuni_medid));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_tge_vuni_medid", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_tge_vuni_medid));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_res_fec_inic", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_fec_inic));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_res_fec_fin", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_fec_fin));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_nume", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_nume));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_res_caja_po", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_caja_po));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_res_zona_caja_po", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_zona_caja_po));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_tus_cod_usr", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_tus_cod_usr));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_nom_estab", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_nom_estab));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_res_num_reg_prop", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_num_reg_prop));
                //cmdToExecute.Parameters.Add(new OracleParameter("v_res_quad_lote", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_quad_lote));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_ident_comp", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_ident_comp));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_refer", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_refer));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_ttl_tip_logradoro", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_ttl_tip_logradoro));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_direccion", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_direccion));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_urbanizacion", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_urbanizacion));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_tes_cod_estado", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_tes_cod_estado));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_zona_postal", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_zona_postal));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_tmu_cod_mun", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_tmu_cod_mun));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_rge_pra_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_rge_pra_protocolo));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_nire_sede", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_nire_sede));
                cmdToExecute.Parameters.Add(new OracleParameter("v_res_cnpj_sede", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _res_cnpj_sede));

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
