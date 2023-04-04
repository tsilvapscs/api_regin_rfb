using System;
using System.Collections.Generic;
using System.Text;
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
using psc.Ruc.Tablelas.Ruc;
using psc.Ruc.Tablelas.DAL.wsRfbRegin;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Serialization;
using psc.Ruc.Tablelas.DAL.Ruc;
using MySql.Data.MySqlClient;

namespace psc.Ruc.Tablelas.Helper
{
    public class dHelper //: DBInteractionBaseORACLE
    {
        public dHelper()
        {

        }

        private string validaNulo(object pValue)
        {
            if (pValue == null)
            {
                return "";
            }
            return pValue.ToString();
        }

        private string CalDvMunicipio(string CodMUnicipio)
        {
            if (CodMUnicipio != "")
            {
                string codMuni = psc.Framework.General.CalculateVerificationDigit(CodMUnicipio, 11).ToString();

                return CodMUnicipio + codMuni;
            }

            return "";

        }

        private DateTime ConvertStringDateTime(string _data)
        {
            if (_data.Trim() != "")
            {
                int dia = int.Parse(_data.Substring(6, 2));
                int mes = int.Parse(_data.Substring(4, 2));
                int ano = int.Parse(_data.Substring(0, 4));

                return new DateTime(ano, mes, dia);
            }
            return new DateTime(1, 1, 1);
        }
        public static Object CreateObject(string XMLString, Object YourClassObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(YourClassObject.GetType());
            //The StringReader will be the stream holder for the existing XML file 
            YourClassObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
            //initially deserialized, the data is represented by an object without a defined type 
            return YourClassObject;
        }

        public void GravaCompletaRucQsaSqlServer(SqlTransaction bd, string Xmlws11, string pProtocolo, DataTable DtTipoLogra)
        {

            psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim)CreateObject(Xmlws11, dados);

            #region RUC_RELAT_PROF e RUC_PROF

            decimal ValorCapitalEmpresa = 0;

            if (dados.dadosCNPJ[0].capitalSocial != null && dados.dadosCNPJ[0].capitalSocial != "" && decimal.Parse(dados.dadosCNPJ[0].capitalSocial) >= 0)
            {
                ValorCapitalEmpresa = decimal.Parse(dados.dadosCNPJ[0].capitalSocial) / 100;
            }

            #region Verifica quando a empresa e Empresario com isso faz diferente

            if (dados.dadosCNPJ[0].naturezaJuridica == "2135" 
                || dados.dadosCNPJ[0].naturezaJuridica == "1279"
                || dados.dadosCNPJ[0].naturezaJuridica == "3077"
                || dados.dadosCNPJ[0].naturezaJuridica == "3131"
                || dados.dadosCNPJ[0].naturezaJuridica == "3220"
                || dados.dadosCNPJ[0].naturezaJuridica == "1023"
                || dados.dadosCNPJ[0].naturezaJuridica == "2178")
            {
                string CPFResponsavel = dados.dadosCNPJ[0].cpfRepresentante;
                dadosCPF dados09 = new dadosCPF();

                ServiceReginRFB ws09 = new ServiceReginRFB();
                Retorno resp09 = new Retorno();
                ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                resp09 = ws09.ServiceWs09(CPFResponsavel);
                if (resp09.status == "OK")
                {
                    dados09 = resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                }
                else
                {
                    throw new Exception("Erro ao tentar buscar o Tipo o cpf " + CPFResponsavel + " no ws09 na dll psc.ruc.tabelas.dall");
                }

                Ruc_Relat_Prof_co_sqlserver rp = new Ruc_Relat_Prof_co_sqlserver();

                rp.rrp_rge_pra_protocolo = pProtocolo;
                rp.rrp_cgc_cpf_secd = CPFResponsavel;
                rp.rrp_tge_ttip_relac = 24;
                rp.rrp_tge_vtip_relac = 2;
                rp.rrp_fec_inic_part = ConvertStringDateTime(dados.dadosCNPJ[0].dataAberturaEstabelecimento);
                rp.rrp_tge_tcod_qual = 23;
                rp.rrp_tge_vcod_qual = 50;
                string qualificacao = dados.dadosCNPJ[0].qualificacaoRepresentante;
                if (qualificacao != "")
                    rp.rrp_tge_vcod_qual = decimal.Parse(dados.dadosCNPJ[0].qualificacaoRepresentante.ToString());
                rp.rrp_desc_doc = "";
                rp.rrp_tus_cod_usr = "JUNTA";
                rp.rrp_cnpj_vacio = 0;

                decimal CapitalSocio = ValorCapitalEmpresa;
                decimal PercentualSocio = 100;

                rp.rrp_porc_part = PercentualSocio;
                rp.RRP_VAL_CAP_SOC = CapitalSocio;
                rp.Update(bd);

                Ruc_Prof_co_sqlserver rf = new Ruc_Prof_co_sqlserver();
                //rp.MainConnectionProvider = cp;
                rf.rpr_rge_pra_protocolo = pProtocolo;
                rf.rpr_tge_tpais = 22;
                rf.rpr_tge_vpais = 105;// Decimal.Parse(s.EndPais);
                rf.rpr_cgc_cpf_secd = CPFResponsavel;
                rf.rpr_tge_ttip_pers = 233;
                rf.rpr_tge_vtip_pers = CPFResponsavel.Length < 12 ? 1 : 2;
                rf.rpr_nomb = dados.dadosCNPJ[0].nomeRepresentante;

                if (dados09 != null && dados09.numCPF != null && dados09.numCPF != "")
                {
                    rf.rpr_nome_mae = dados09.nomeMae;
                    rf.rpr_fec_const_nasc = ConvertStringDateTime(dados09.dataNascimento);
                    rf.rpr_sexo = dados09.sexo == "1" ? 1 : 2;
                    rf.rpr_nume = validaNulo(dados09.endereco.numLogradouro);
                    rf.rpr_nome_mae = validaNulo(dados09.nomeMae);
                }
                rf.Update(bd);
            }
            #endregion

            #region Quando a empresa e normal ou seja tem Qsa no node Socio
            if (dados.dadosCNPJ[0].dadosSocio != null)
            {
                foreach (dadosSocio socio in dados.dadosCNPJ[0].dadosSocio)
                {
                    if (socio.cpfCnpj.Trim() != "99999999999999")
                    {
                        dadosCPF dados09 = new dadosCPF();

                        if (socio.cpfCnpj.Length < 12)
                        {
                            ServiceReginRFB ws09 = new ServiceReginRFB();
                            Retorno resp09 = new Retorno();
                            ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                            resp09 = ws09.ServiceWs09(socio.cpfCnpj);

                            if (resp09.status == "OK")
                            {
                                dados09 = resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                            }
                            else
                            {
                                throw new Exception("Erro ao tentar buscar o Tipo o cpf " + socio.cpfCnpj + " no ws09 na dll psc.ruc.tabelas.dall");
                            }
                        }

                        Ruc_Relat_Prof_co_sqlserver rp = new Ruc_Relat_Prof_co_sqlserver();

                        //rp.MainConnectionProvider = cp;
                        rp.rrp_rge_pra_protocolo = pProtocolo;
                        rp.rrp_cgc_cpf_secd = socio.cpfCnpj;
                        rp.rrp_tge_ttip_relac = 24;
                        rp.rrp_tge_vtip_relac = 2;
                        rp.rrp_fec_inic_part = ConvertStringDateTime(socio.dataInclusao);
                        rp.rrp_tge_tcod_qual = 23;
                        rp.rrp_tge_vcod_qual = decimal.Parse(socio.qualificacao);
                        rp.rrp_desc_doc = "";
                        rp.rrp_tus_cod_usr = "JUNTA";
                        rp.rrp_cnpj_vacio = 0;

                        decimal CapitalSocio = 0;
                        decimal PercentualSocio = 0;

                        if (validaNulo(socio.valorPartCapitalSocialString) != "")
                            CapitalSocio = decimal.Parse(socio.valorPartCapitalSocialString) / 100;

                        if (ValorCapitalEmpresa > 0 && CapitalSocio > 0)
                        {
                            PercentualSocio = (CapitalSocio * 100) / ValorCapitalEmpresa;
                            if (PercentualSocio > 100)
                            {
                                PercentualSocio = 0;
                            }
                        }

                        rp.rrp_porc_part = Math.Round(PercentualSocio, 5);
                        rp.RRP_VAL_CAP_SOC = CapitalSocio;
                        rp.Update(bd);

                        Ruc_Prof_co_sqlserver rf = new Ruc_Prof_co_sqlserver();
                        rf.rpr_rge_pra_protocolo = pProtocolo;
                        rf.rpr_tge_tpais = 22;
                        rf.rpr_tge_vpais = 105;// Decimal.Parse(s.EndPais);
                        rf.rpr_cgc_cpf_secd = socio.cpfCnpj;
                        rf.rpr_tge_ttip_pers = 233;
                        rf.rpr_tge_vtip_pers = socio.cpfCnpj.Length < 12 ? 1 : 2;
                        rf.rpr_nomb = socio.nome;

                        if (dados09 != null && dados09.numCPF != null && dados09.numCPF != "")
                        {
                            rf.rpr_nome_mae = dados09.nomeMae;

                            rf.rpr_fec_const_nasc = ConvertStringDateTime(dados09.dataNascimento);
                            rf.rpr_sexo = dados09.sexo == "1" ? 1 : 2;
                            rf.rpr_nome_mae = validaNulo(dados09.nomeMae);

                        }
                        rf.Update(bd);

                        #region Carrega Representante QSA
                        if (validaNulo(socio.cpfRepresentanteLegal) != "" && validaNulo(socio.cpfRepresentanteLegal) != "00000000000")
                        {
                            RUC_REPRESENTANTES_CO_SQLSERVER repre = new RUC_REPRESENTANTES_CO_SQLSERVER();
                            repre.rsr_pra_protocolo = pProtocolo;
                            repre.rsr_cgc_cpf_princ = socio.cpfCnpj;
                            repre.rsr_cgc_cpf_secd = socio.cpfRepresentanteLegal;
                            repre.rsr_tipo = 1;
                            repre.rsr_nomb = socio.nomeRepresentanteLegal;
                            repre.rsr_tge_tcod_qual = 23;
                            repre.rsr_tge_vcod_qual = decimal.Parse(socio.qualificacaoRepresentanteLegal);
                            repre.rsr_tge_ttip_pers = 233;
                            repre.rsr_tge_vtip_pers = socio.cpfRepresentanteLegal.Length < 12 ? 1 : 2; ;

                            ServiceReginRFB ws09 = new ServiceReginRFB();
                            Retorno ws0911Socio = new Retorno();
                            ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                            ws0911Socio = ws09.ServiceWs09(socio.cpfRepresentanteLegal);

                            if (ws0911Socio.status == "OK")
                            {
                                dados09 = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                            }
                            else
                            {
                                throw new Exception("Erro ao tentar buscar Reprsentante do cpf " + socio.cpfCnpj + " Representante " + socio.cpfRepresentanteLegal + " no ws09 na dll psc.ruc.tabelas.dall");
                            }

                            psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                            cc.Bairro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.bairro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.bairro;
                            cc.Cep = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.cep == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.cep;
                            cc.Codigo_municipio = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codMunicipio == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codMunicipio;
                            cc.Complemento = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.complementoLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.complementoLogradouro;
                            cc.Logradouro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.logradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.logradouro;
                            cc.Numero = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.numLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.numLogradouro;
                            cc.Pais = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codPais == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codPais;
                            cc.TipLogradoro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codTipoLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codTipoLogradouro;
                            cc.Uf = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.uf == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.uf;

                            cc.TrataEndereco(ref cc, DtTipoLogra);


                            repre.rsr_direccion = cc.Logradouro;
                            repre.rsr_nume = cc.Numero;
                            repre.rsr_tge_vpais = cc.Pais;
                            repre.rsr_ttl_tip_logradoro = cc.TipLogradoro;
                            repre.rsr_urbanizacion = cc.Bairro;
                            repre.rsr_tes_cod_estado = cc.Uf;
                            repre.rsr_zona_postal = cc.Cep;
                            repre.rsr_ident_comp = cc.Complemento;
                            repre.rsr_tmu_cod_mun = cc.Codigo_municipio;
                            repre.rsr_tge_vpais = cc.Pais;

                            repre.Update(bd);
                        }
                        #endregion
                    }
                }
            }
            #endregion


            #endregion
        }

        public void GravaCompletaRucQsaOracle(OracleTransaction bd, string Xmlws11, string pProtocolo, DataTable DtTipoLogra)
        {

            psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim)CreateObject(Xmlws11, dados);

            #region RUC_RELAT_PROF e RUC_PROF

            decimal ValorCapitalEmpresa = 0;

            if (dados.dadosCNPJ[0].capitalSocial != null && dados.dadosCNPJ[0].capitalSocial != "" && decimal.Parse(dados.dadosCNPJ[0].capitalSocial) >= 0)
            {
                ValorCapitalEmpresa = decimal.Parse(dados.dadosCNPJ[0].capitalSocial) / 100;
            }

            
            #region Guarda QSA

            if (dados.dadosCNPJ[0].dadosSocio != null)
            {
                #region Quando a empresa e normal ou seja tem Qsa no node Socio
                foreach (dadosSocio socio in dados.dadosCNPJ[0].dadosSocio)
                {
                    if (socio.cpfCnpj.Trim() != "99999999999999")
                    {
                        dadosCPF dados09 = new dadosCPF();

                        if (socio.cpfCnpj.Length < 12)
                        {
                            ServiceReginRFB ws09 = new ServiceReginRFB();
                            Retorno resp09 = new Retorno();
                            ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                            resp09 = ws09.ServiceWs09(socio.cpfCnpj);

                            if (resp09.status == "OK")
                            {
                                dados09 = resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                            }
                            else
                            {
                                throw new Exception("Erro ao tentar buscar o Tipo o cpf " + socio.cpfCnpj + " no ws09 na dll psc.ruc.tabelas.dall");
                            }
                        }

                        Ruc_Relat_Prof_co_oracle rp = new Ruc_Relat_Prof_co_oracle();

                        //rp.MainConnectionProvider = cp;
                        rp.rrp_rge_pra_protocolo = pProtocolo;
                        rp.rrp_cgc_cpf_secd = socio.cpfCnpj;
                        rp.rrp_tge_ttip_relac = 24;
                        rp.rrp_tge_vtip_relac = 2;
                        rp.rrp_fec_inic_part = ConvertStringDateTime(socio.dataInclusao);
                        rp.rrp_tge_tcod_qual = 23;
                        rp.rrp_tge_vcod_qual = decimal.Parse(socio.qualificacao);
                        rp.rrp_desc_doc = "";
                        rp.rrp_tus_cod_usr = "JUNTA";
                        rp.rrp_cnpj_vacio = 0;

                        decimal CapitalSocio = 0;
                        decimal PercentualSocio = 0;

                        if (validaNulo(socio.valorPartCapitalSocialString) != "")
                            CapitalSocio = decimal.Parse(socio.valorPartCapitalSocialString) / 100;

                        if (ValorCapitalEmpresa > 0 && CapitalSocio > 0)
                        {
                            PercentualSocio = (CapitalSocio * 100) / ValorCapitalEmpresa;
                            if (PercentualSocio > 100)
                            {
                                PercentualSocio = 0;
                            }
                        }

                        rp.rrp_porc_part = Math.Round(PercentualSocio, 5);
                        rp.RRP_VAL_CAP_SOC = CapitalSocio;
                        rp.Update(bd);

                        Ruc_Prof_co_oracle rf = new Ruc_Prof_co_oracle();
                        //rp.MainConnectionProvider = cp;
                        rf.rpr_rge_pra_protocolo = pProtocolo;
                        rf.rpr_tge_tpais = 22;
                        rf.rpr_tge_vpais = 105;// Decimal.Parse(s.EndPais);
                        rf.rpr_cgc_cpf_secd = socio.cpfCnpj;
                        rf.rpr_tge_ttip_pers = 233;
                        rf.rpr_tge_vtip_pers = socio.cpfCnpj.Length < 12 ? 1 : 2;
                        rf.rpr_nomb = socio.nome;

                        if (dados09 != null && dados09.numCPF != null && dados09.numCPF != "")
                        {
                            rf.rpr_nome_mae = dados09.nomeMae;

                            rf.rpr_fec_const_nasc = ConvertStringDateTime(dados09.dataNascimento);
                            rf.rpr_sexo = dados09.sexo == "1" ? 1 : 2;
                            rf.rpr_nome_mae = validaNulo(dados09.nomeMae);

                        }
                        rf.Update(bd);

                        #region Carrega Representante QSA
                        //As veces a qualificação do QSA vem nula, entao nao carrego o representante
                        if (socio.qualificacaoRepresentanteLegal != null && validaNulo(socio.cpfRepresentanteLegal) != "" && validaNulo(socio.cpfRepresentanteLegal) != "00000000000")
                        {
                            RUC_REPRESENTANTES_CO_ORACLE repre = new RUC_REPRESENTANTES_CO_ORACLE();
                            repre.rsr_pra_protocolo = pProtocolo;
                            repre.rsr_cgc_cpf_princ = socio.cpfCnpj;
                            repre.rsr_cgc_cpf_secd = socio.cpfRepresentanteLegal;
                            repre.rsr_tipo = 1;
                            repre.rsr_nomb = socio.nomeRepresentanteLegal;
                            repre.rsr_tge_tcod_qual = 23;
                            repre.rsr_tge_vcod_qual = decimal.Parse(socio.qualificacaoRepresentanteLegal);
                            repre.rsr_tge_ttip_pers = 233;
                            repre.rsr_tge_vtip_pers = socio.cpfRepresentanteLegal.Length < 12 ? 1 : 2; ;

                            ServiceReginRFB ws09 = new ServiceReginRFB();
                            Retorno ws0911Socio = new Retorno();
                            ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                            ws0911Socio = ws09.ServiceWs09(socio.cpfRepresentanteLegal);

                            if (ws0911Socio.status == "OK")
                            {
                                dados09 = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                            }
                            else
                            {
                                throw new Exception("Erro ao tentar buscar Reprsentante do cpf " + socio.cpfCnpj + " Representante " + socio.cpfRepresentanteLegal + " no ws09 na dll psc.ruc.tabelas.dall");
                            }

                            if (ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0] != null &&
                                                    ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco != null)
                            {
                                psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                                cc.Bairro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.bairro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.bairro;
                                cc.Cep = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.cep == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.cep;
                                cc.Codigo_municipio = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codMunicipio == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codMunicipio;
                                cc.Complemento = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.complementoLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.complementoLogradouro;
                                cc.Logradouro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.logradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.logradouro;
                                cc.Numero = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.numLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.numLogradouro;
                                cc.Pais = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codPais == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codPais;
                                cc.TipLogradoro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codTipoLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codTipoLogradouro;
                                cc.Uf = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.uf == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.uf;

                                cc.TrataEndereco(ref cc, DtTipoLogra);


                                repre.rsr_direccion = cc.Logradouro;
                                repre.rsr_nume = cc.Numero;
                                repre.rsr_tge_vpais = cc.Pais;
                                repre.rsr_ttl_tip_logradoro = cc.TipLogradoro;
                                repre.rsr_urbanizacion = cc.Bairro;
                                repre.rsr_tes_cod_estado = cc.Uf;
                                repre.rsr_zona_postal = cc.Cep;
                                repre.rsr_ident_comp = cc.Complemento;
                                repre.rsr_tmu_cod_mun = cc.Codigo_municipio;
                                repre.rsr_tge_vpais = cc.Pais;
                            }
                            repre.Update(bd);
                        }
                        #endregion
                    }
                }
                #endregion
            }

            else
            {
                #region Verifica quando a empresa e Empresario com isso faz diferente
                if (dados.dadosCNPJ[0].cpfRepresentante != null && dados.dadosCNPJ[0].cpfRepresentante != "")
                {
                    string CPFResponsavel = dados.dadosCNPJ[0].cpfRepresentante;
                    dadosCPF dados09 = new dadosCPF();

                    ServiceReginRFB ws09 = new ServiceReginRFB();
                    Retorno resp09 = new Retorno();
                    ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                    resp09 = ws09.ServiceWs09(CPFResponsavel);
                    if (resp09.status == "OK")
                    {
                        dados09 = resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                    }
                    else
                    {
                        throw new Exception("Erro ao tentar buscar o Tipo o cpf " + CPFResponsavel + " no ws09 na dll psc.ruc.tabelas.dall");
                    }

                    Ruc_Relat_Prof_co_oracle rp = new Ruc_Relat_Prof_co_oracle();

                    //rp.MainConnectionProvider = cp;
                    rp.rrp_rge_pra_protocolo = pProtocolo;
                    rp.rrp_cgc_cpf_secd = CPFResponsavel;
                    rp.rrp_tge_ttip_relac = 24;
                    rp.rrp_tge_vtip_relac = 2;
                    rp.rrp_fec_inic_part = ConvertStringDateTime(dados.dadosCNPJ[0].dataAberturaEstabelecimento);
                    rp.rrp_tge_tcod_qual = 23;
                    rp.rrp_tge_vcod_qual = 50;
                    string qualificacao = dados.dadosCNPJ[0].qualificacaoRepresentante;
                    if (qualificacao != "")
                        rp.rrp_tge_vcod_qual = decimal.Parse(dados.dadosCNPJ[0].qualificacaoRepresentante.ToString());
                    rp.rrp_desc_doc = "";
                    rp.rrp_tus_cod_usr = "JUNTA";
                    rp.rrp_cnpj_vacio = 0;

                    decimal CapitalSocio = ValorCapitalEmpresa;
                    decimal PercentualSocio = 100;

                    rp.rrp_porc_part = PercentualSocio;
                    rp.RRP_VAL_CAP_SOC = CapitalSocio;
                    rp.Update(bd);

                    Ruc_Prof_co_oracle rf = new Ruc_Prof_co_oracle();
                    //rp.MainConnectionProvider = cp;
                    rf.rpr_rge_pra_protocolo = pProtocolo;
                    rf.rpr_tge_tpais = 22;
                    rf.rpr_tge_vpais = 105;// Decimal.Parse(s.EndPais);
                    rf.rpr_cgc_cpf_secd = CPFResponsavel;
                    rf.rpr_tge_ttip_pers = 233;
                    rf.rpr_tge_vtip_pers = CPFResponsavel.Length < 12 ? 1 : 2;
                    rf.rpr_nomb = dados.dadosCNPJ[0].nomeRepresentante;

                    if (dados09 != null && dados09.numCPF != null && dados09.numCPF != "")
                    {
                        rf.rpr_nome_mae = dados09.nomeMae;
                        rf.rpr_fec_const_nasc = ConvertStringDateTime(dados09.dataNascimento);
                        rf.rpr_sexo = dados09.sexo == "1" ? 1 : 2;
                        rf.rpr_nume = validaNulo(dados09.endereco.numLogradouro);
                        rf.rpr_nome_mae = validaNulo(dados09.nomeMae);

                    }
                    rf.Update(bd);
                }
                #endregion
            
            }
            #endregion

            #endregion
        }

        #region Carrega dados Ruc Oracle

        public static string GetTipoEmpresaPorNaturezaJuridica(String pNatureza)
        {


            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                StringBuilder sql = new StringBuilder();

                sql.Append(@"SELECT TNJ_CO_GRUPO
                         FROM TAB_NATUREZA_JURIDCA
                         WHERE TNJ_CO_NATUREZA_JURIDICA = :pNatureza");


                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable toReturn = new DataTable("Pessoa_Juridica");
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                try
                {
                    cmdToExecute.Parameters.Add(new OracleParameter("pNatureza", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pNatureza));

                    cmdToExecute.Connection.Open();
                    // Execute query.

                    adapter.Fill(toReturn);

                    if (toReturn.Rows.Count > 0)
                    {
                        if (toReturn.Rows[0]["TNJ_CO_GRUPO"].ToString() == "")
                            return "11";

                        return toReturn.Rows[0]["TNJ_CO_GRUPO"].ToString();
                    }
                    else
                    {
                        return "11";
                    }

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                }
            }
        }

        public psc.Ruc.Tablelas.DAL.wsRfbRegin.Retorno RecuperaEmpresaWs11(string pCnpj)
        {

            if (ConfigurationManager.AppSettings["urlServicesRFBRegin"] != null && ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString() != "")
            {
                if (pCnpj.Length == 14)
                {
                    psc.Ruc.Tablelas.DAL.wsRfbRegin.ServiceReginRFB regin = new psc.Ruc.Tablelas.DAL.wsRfbRegin.ServiceReginRFB();
                    psc.Ruc.Tablelas.DAL.wsRfbRegin.Retorno resulRegin = new psc.Ruc.Tablelas.DAL.wsRfbRegin.Retorno();
                    regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                    resulRegin = regin.ServiceWs11(pCnpj);

                    return resulRegin;

                }
                return null;
            }
            return null;
        }

        public void ApagaRegistrosRuc(OracleTransaction bd, string pProtocolo)
        {
            string Sql = "";
            using (OracleCommand cmdToExecute = new OracleCommand())
            {
                cmdToExecute.Transaction = bd;
                cmdToExecute.Connection = bd.Connection;
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.CommandText = Sql.ToString();

                Sql = " Delete RUC_EMPRESAS_VINCULADAS Where  REV_PROTOCOLO = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery(); 

                Sql = " Delete ruc_actv_econ Where  rae_rge_pra_protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_prof  Where rpr_rge_pra_protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_relat_prof Where rrp_rge_pra_protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_gen_protocolo Where rgp_rge_pra_protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_representantes Where rsr_pra_protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete tab_inform_extra_junta Where tie_protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_comp Where RCO_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_estab Where RES_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_estab Where RES_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete RUC_CARD_RFB Where PROTOCOLO = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_general Where Rge_Pra_Protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

            }

        }
        public void GravaWsRFB11RucOracle(OracleTransaction cp, string Xmlws11, string pProtocolo)
        {
            List<string> pEventos = new List<string>();

            GravaWsRFB11RucOracle(cp, Xmlws11, pProtocolo, "", "", "", "", "", "", pEventos);
        }

        public void GravaWsRFB11RucOracle(OracleTransaction cp, string Xmlws11, string pProtocolo, string pCNPJ, string nire, string pNroViabilidade, string NroRequerimento, string pCNPJOrgaoRegistro, string pNroDBE, List<string> pEventos)
        {

            ApagaRegistrosRuc(cp, pProtocolo);

            psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim ws11 = new psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim();
            ws11 = (psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim)CreateObject(Xmlws11, ws11);

            dadosCNPJ dados = new dadosCNPJ();

            dados = ws11.dadosCNPJ[0];

            DateTime DtHoje = DateTime.Now;//dHelper.SysdateOracle();

            if (dados.indMatrizFilial == "2")
            {
                string cnpjMatriz = "";
                if (dados.cnpjMatriz != null && dados.cnpjMatriz != "")
                {
                    cnpjMatriz = dados.cnpjMatriz;
                }

                psc.Ruc.Tablelas.DAL.wsRfbRegin.ServiceReginRFB reginmatriz = new psc.Ruc.Tablelas.DAL.wsRfbRegin.ServiceReginRFB();
                psc.Ruc.Tablelas.DAL.wsRfbRegin.Retorno resulReginMatriz = RecuperaEmpresaWs11(cnpjMatriz);

                if (resulReginMatriz.status != "OK")
                {
                    throw new Exception("Erro ao acessar a RFB Buscando Matriz" + resulReginMatriz.descricao);
                }

                dados.nomeEmpresarial = resulReginMatriz.oCNPJResponse.dadosCNPJ[0].nomeEmpresarial;
                dados.naturezaJuridica = resulReginMatriz.oCNPJResponse.dadosCNPJ[0].naturezaJuridica;

            }


            string _vTipRegistro = GetTipoEmpresaPorNaturezaJuridica(dados.naturezaJuridica); //Verificar

            decimal CodMuniEmpresa = decimal.Parse(CalDvMunicipio(dados.endereco.codMunicipio));

            if (2 == 1)
            {
                #region PSC_PROTOCOLO
                using (PSC_PROTOCOLO p = new PSC_PROTOCOLO())
                {
                    //p.MainConnectionProvider = cp;
                    p.Pro_protocolo = pProtocolo;
                    p.Pro_status = 1;
                    p.Pro_fec_inc = DtHoje;
                    p.Pro_tmu_tuf_uf = dados.endereco.uf;
                    p.Pro_tmu_cod_mun = int.Parse(CodMuniEmpresa.ToString());
                    p.Pro_tip_operacao = 1; //Verificar
                    p.Pro_env_sef = 2;
                    p.Pro_flag_vigilancia = 2;
                    p.Pro_fec_atualizacao = DtHoje;
                    p.Pro_tge_tgacao = 700;
                    p.Pro_tge_vgacao = Int32.Parse("2");
                    p.Pro_cnpj_org_reg = pCNPJOrgaoRegistro;
                    p.PRO_NR_REQUERIMENTO = NroRequerimento;
                    p.PRO_VPV_COD_PROTOCOLO = pNroViabilidade;
                    p.PRO_NR_DBE = pNroDBE;

                    p.Update(cp);

                }
                #endregion

                #region PSC_IDENT_PROTOCOLO
                StringBuilder sqlIdent = new StringBuilder();
                sqlIdent.AppendLine(@" Insert Into PSC_IDENT_PROTOCOLO 
                                    (PIP_PRO_PROTOCOLO, 
                                     PIP_CNPJ, 
                                     PIP_NIRE, 
                                     PIP_NOME_RAZAO_SOCIAL ) 
                                    Values ( ");
                sqlIdent.Append("'" + pProtocolo + "'");
                sqlIdent.Append(", '" + dados.cnpj + "'");
                sqlIdent.Append(", '" + nire + "'");
                sqlIdent.Append(", '" + dados.nomeEmpresarial + "'");
                sqlIdent.Append(" )");

                //using (dHelper c = new dHelper())
                //{
                //    //c.MainConnectionProvider = cp;
                //    c.ExecuteNonQuery(sqlIdent);
                //}

                #endregion

                #region PSC_PROT_EVENTO_RFB
                StringBuilder sqlEvento = new StringBuilder();
                for (int i = 0; i < pEventos.Count; i++)
                {
                    sqlEvento = new StringBuilder();
                    sqlEvento.AppendLine("Insert Into PSC_PROT_EVENTO_RFB ");
                    sqlEvento.AppendLine(" (PEV_PRO_PROTOCOLO, PEV_COD_EVENTO ) ");
                    sqlEvento.AppendLine(" Values ( ");
                    sqlEvento.AppendLine("'" + pProtocolo + "'");
                    sqlEvento.AppendLine(" ," + pEventos[i].ToString());
                    sqlEvento.AppendLine(" ) ");
                    //using (dHelperORACLE cpe = new dHelperORACLE())
                    //{
                    //    cpe.MainConnectionProvider = cp;
                    //    cpe.ExecuteNonQuery(sqlEvento);
                    //}
                }


                #endregion

                #region MAC_LOG_CARGA_JUNTA_HOMOLOG
                using (MAC_LOG_CARGA_JUNTA_HOMOLOG m = new MAC_LOG_CARGA_JUNTA_HOMOLOG())
                {
                    //m.MainConnectionProvider = cp;

                    m.MLC_PROTOCOLO = pProtocolo;
                    m.MLC_CPF_HOMOLOGADOR = "11111111111";
                    m.MLC_DTA_HOMOLOGACAO = DtHoje;
                    m.MLC_DATA_CARREGA_WS11 = DtHoje;
                    m.Update();

                }
                #endregion
            }

            #region RUC_GENERAL
            using (Ruc_General rg = new Ruc_General())
            {
                //rg.MainConnectionProvider = cp;
                rg.rge_pra_protocolo = pProtocolo;
                rg.rge_ruc = "";
                rg.rge_tge_ttip_reg = 257;
                rg.rge_tge_vtip_reg = Int32.Parse(_vTipRegistro);
                if (dados.opcaoSimei == "S")
                    rg.rge_tge_vtip_reg = 13;

                rg.rge_tge_ttip_ctrib = 153;
                rg.rge_tge_vtip_ctrib = 9999;
                rg.rge_tge_ttip_pers = 233;
                rg.rge_tge_vtip_pers = 2;
                rg.rge_cgc_cpf = dados.cnpj;
                rg.rge_tge_ttamanho = 21;
                rg.rge_tge_vtamanho = 3;
                rg.rge_fec_ini_act_ec = DateTime.Now;
                if (validaNulo(dados.dataAberturaEstabelecimento) != "")
                    rg.rge_fec_ini_act_ec = ConvertStringDateTime(dados.dataAberturaEstabelecimento);
                if (dados.porte != null)
                {
                    if (dados.porte == "01") //ME
                    {
                        rg.rge_tge_vtamanho = 1;
                    }
                    if (dados.porte == "03")//EPP
                    {
                        rg.rge_tge_vtamanho = 2;
                    }
                }

                rg.rge_nomb = dados.nomeEmpresarial;
                rg.rge_codg_mun = CodMuniEmpresa;
                //rg.rge_tae_cod_actvd = dados.cnaePrincipal;
                rg.rge_tuf_cod_uf = dados.endereco.uf;

                rg.Update(cp);

            }
            #endregion

            #region RUC_ESTAB
            using (Ruc_Estab rb = new Ruc_Estab())
            {
                rb.res_rge_pra_protocolo = pProtocolo;
                rb.res_ide_estab = 0;
                rb.res_tip_estab = 1;
                rb.res_tge_ttip_reg = 155;
                rb.res_tge_vtip_reg = 9999;
                rb.res_tge_tcond_uso = 152;
                rb.res_tge_vcond_uso = 9999;
                rb.res_sigl = "";
                rb.res_area = 0;
                rb.res_tge_tuni_medid = 156;
                rb.res_tge_vuni_medid = 9999;
                rb.res_nire_sede = "";
                rb.res_cnpj_sede = validaNulo(dados.cnpjMatriz);

                rb.res_nume = validaNulo(dados.endereco.numLogradouro);
                rb.res_tus_cod_usr = "REGIN";
                rb.res_nom_estab = dados.nomeEmpresarial;
                rb.res_ident_comp = validaNulo(dados.endereco.complementoLogradouro);
                rb.res_refer = validaNulo(dados.endereco.referencia);
                rb.res_ttl_tip_logradoro = validaNulo(dados.endereco.codTipoLogradouro);
                rb.res_direccion = validaNulo(dados.endereco.logradouro);
                rb.res_urbanizacion = validaNulo(dados.endereco.bairro);
                rb.res_tes_cod_estado = dados.endereco.uf;
                rb.res_zona_postal = validaNulo(dados.endereco.cep);
                rb.res_tmu_cod_mun = CodMuniEmpresa;

                rb.Update(cp);
            }
            #endregion

            #region RUC_COMP

            using (Ruc_Comp rc = new Ruc_Comp())
            {
                rc.rco_num_reg_merc = nire;
                rc.rco_tge_ttip_doc = 151;
                rc.rco_tge_vtip_doc = 9999;
                rc.rco_tnc_cod_natur = decimal.Parse(dados.naturezaJuridica);
                rc.rco_domic_pais = 1;
                rc.rco_fec_incorp = DtHoje;
                rc.rco_val_cap_soc = decimal.Parse(dados.capitalSocial) / 100;
                rc.rco_nume = validaNulo(dados.endereco.numLogradouro);
                rc.rco_tge_tpais = 22;
                rc.rco_tge_vpais = 105;
                rc.rco_tus_cod_usr = "REGIN";
                rc.rco_ident_comp = validaNulo(dados.endereco.complementoLogradouro);
                rc.rco_refer = validaNulo(dados.endereco.referencia);
                rc.rco_lic_mun = "";
                rc.rco_ttl_tip_logradoro = validaNulo(dados.endereco.codTipoLogradouro);
                rc.rco_direccion = validaNulo(dados.endereco.logradouro);
                rc.rco_urbanizacion = validaNulo(dados.endereco.bairro);
                rc.rco_tes_cod_estado = validaNulo(dados.endereco.uf);
                rc.rco_zona_postal = validaNulo(dados.endereco.cep);
                rc.rco_tmu_cod_mun = CodMuniEmpresa;
                rc.rco_rge_pra_protocolo = pProtocolo;
                if (validaNulo(dados.dataAberturaEstabelecimento) != "")
                    rc.rco_fec_const_nasc = ConvertStringDateTime(dados.dataAberturaEstabelecimento);

                rc.Update(cp);
            }

            #endregion

            #region RUC_ACTV_ECON
            Ruc_Actv_Econ cav = new Ruc_Actv_Econ();
            // Esse 8888888 e porque tinha uma empresa na BA que estava com esse cnae CNPJ 13526249000187
            // <cnaePrincipal>8888888</cnaePrincipal>
            if (dados.cnaePrincipal != null && dados.cnaePrincipal != "0000000" && dados.cnaePrincipal != "8888888")
            {

                cav.rae_rge_pra_protocolo = pProtocolo;
                cav.rae_tae_cod_actvd = dados.cnaePrincipal;
                cav.rae_calif_actv = "1";
                cav.rae_porcent = 100;
                cav.rae_tus_cod_usr = "REGIN";
                cav.rae_fec_actl = DtHoje;
                cav.Update(cp);
            }

            foreach (string pCNAE in dados.cnaeSecundaria)
            {
                if (pCNAE != null && pCNAE != "0000000" && pCNAE != "8888888")
                {
                    using (cav = new Ruc_Actv_Econ())
                    {
                        cav.rae_rge_pra_protocolo = pProtocolo;
                        cav.rae_tae_cod_actvd = pCNAE;
                        cav.rae_calif_actv = "2";
                        cav.rae_porcent = 0;
                        cav.rae_tus_cod_usr = "REGIN";
                        cav.rae_fec_actl = DtHoje;
                        cav.Update(cp);
                    }
                }
            }
            #endregion

            #region RUC_GEN_PROTOCOLO
            using (Ruc_Gen_Protocolo gc = new Ruc_Gen_Protocolo())
            {
                gc.rgp_rge_pra_protocolo = pProtocolo;
                gc.rgp_tge_tip_tab = 902;
                gc.rgp_tge_cod_tip_tab = 1;
                gc.rgp_valor = validaNulo(dados.objetoSocial);
                gc.rgp_tus_cod_usr = "REGIN";
                gc.rgp_fec_actl = DtHoje;
                if (gc.rgp_valor != "")
                {
                    gc.Update(cp);
                }
            }

            #endregion

            //}
            //catch (Exception ex)
            //{
            //    new Exception("Erro procedimento GravaWs11 " + ex.Message + "  " + ex.StackTrace);
            //}
        }

        public void gravarRucActvEconRFB(OracleTransaction cp, string Xmlws11, string pProtocolo)
        {
            psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim ws11 = new psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim();
            ws11 = (psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim)CreateObject(Xmlws11, ws11);

            dadosCNPJ dados = new dadosCNPJ();

            dados = ws11.dadosCNPJ[0];

            DateTime DtHoje = DateTime.Now;//dHelper.SysdateOracle();

            Ruc_Actv_Econ cav = new Ruc_Actv_Econ();
            if (dados.cnaePrincipal != null && dados.cnaePrincipal != "0000000" && dados.cnaePrincipal != "8888888")
            {
                cav.rae_rge_pra_protocolo = pProtocolo;
                cav.rae_tae_cod_actvd = dados.cnaePrincipal;
                cav.rae_calif_actv = "1";
                cav.rae_porcent = 100;
                cav.rae_tus_cod_usr = "REGIN";
                cav.rae_fec_actl = DtHoje;
                cav.Update(cp);
            }

            foreach (string pCNAE in dados.cnaeSecundaria)
            {
                if (pCNAE != null && pCNAE != "0000000" && pCNAE != "8888888")
                {
                    using (cav = new Ruc_Actv_Econ())
                    {
                        cav.rae_rge_pra_protocolo = pProtocolo;
                        cav.rae_tae_cod_actvd = pCNAE;
                        cav.rae_calif_actv = "2";
                        cav.rae_porcent = 0;
                        cav.rae_tus_cod_usr = "REGIN";
                        cav.rae_fec_actl = DtHoje;
                        cav.Update(cp);
                    }
                }
            }
        }
        #endregion

        #region Carrega dados Ruc Oracle SqlServer
        public static string GetTipoEmpresaPorNaturezaJuridicaSqlServer(String pNatureza)
        {
            using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                StringBuilder sql = new StringBuilder();

                sql.Append(@"SELECT TNJ_CO_GRUPO
                         FROM TAB_NATUREZA_JURIDCA
                         WHERE TNJ_CO_NATUREZA_JURIDICA = @pNatureza");


                SqlCommand cmdToExecute = new SqlCommand();
                cmdToExecute.CommandText = sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable toReturn = new DataTable("Pessoa_Juridica");
                cmdToExecute.Connection = _conn;

                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                try
                {
                    cmdToExecute.Parameters.Add(new SqlParameter("pNatureza", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pNatureza));

                    cmdToExecute.Connection.Open();
                    // Execute query.

                    adapter.Fill(toReturn);

                    if (toReturn.Rows.Count > 0)
                    {
                        return toReturn.Rows[0]["TNJ_CO_GRUPO"].ToString();
                    }
                    else
                    {
                        return "11";
                    }

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                }
            }
        }
        public void ApagaRegistrosRucSqlServer(SqlTransaction bd, string pProtocolo)
        {
            string Sql = "";
            using (SqlCommand cmdToExecute = new SqlCommand())
            {
                cmdToExecute.Transaction = bd;
                cmdToExecute.Connection = bd.Connection;
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.CommandText = Sql.ToString();

                Sql = " Delete RUC_EMPRESAS_VINCULADAS Where  REV_PROTOCOLO = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_actv_econ Where  rae_rge_pra_protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_prof  Where rpr_rge_pra_protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_relat_prof Where rrp_rge_pra_protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_gen_protocolo Where rgp_rge_pra_protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_representantes Where rsr_pra_protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete tab_inform_extra_junta Where tie_protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_comp Where RCO_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_estab Where RES_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_estab Where RES_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                Sql = " Delete ruc_general Where Rge_Pra_Protocolo = '" + pProtocolo + "'";
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

            }

        }

        public void GravaWsRFB11RucSqlServer(SqlTransaction cp, string Xmlws11, string pProtocolo)
        {
            List<string> pEventos = new List<string>();

            GravaWsRFB11RucSqlServer(cp, Xmlws11, pProtocolo, "", "", "", "", "", "", pEventos);
        }

        public void GravaWsRFB11RucSqlServer(SqlTransaction cp, string Xmlws11, string pProtocolo, string pCNPJ, string nire, string pNroViabilidade, string NroRequerimento, string pCNPJOrgaoRegistro, string pNroDBE, List<string> pEventos)
        {

            ApagaRegistrosRucSqlServer(cp, pProtocolo);

            psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim ws11 = new psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim();
            ws11 = (psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim)CreateObject(Xmlws11, ws11);

            dadosCNPJ dados = new dadosCNPJ();

            dados = ws11.dadosCNPJ[0];

            DateTime DtHoje = DateTime.Now;//dHelper.SysdateOracle();

            if (dados.indMatrizFilial == "2")
            {
                string cnpjMatriz = "";
                if (dados.cnpjMatriz != null && dados.cnpjMatriz != "")
                {
                    cnpjMatriz = dados.cnpjMatriz;
                }

                psc.Ruc.Tablelas.DAL.wsRfbRegin.ServiceReginRFB reginmatriz = new psc.Ruc.Tablelas.DAL.wsRfbRegin.ServiceReginRFB();
                psc.Ruc.Tablelas.DAL.wsRfbRegin.Retorno resulReginMatriz = RecuperaEmpresaWs11(cnpjMatriz);

                if (resulReginMatriz.status != "OK")
                {
                    throw new Exception("Erro ao acessar a RFB Buscando Matriz" + resulReginMatriz.descricao);
                }

                dados.nomeEmpresarial = resulReginMatriz.oCNPJResponse.dadosCNPJ[0].nomeEmpresarial;
                dados.naturezaJuridica = resulReginMatriz.oCNPJResponse.dadosCNPJ[0].naturezaJuridica;

            }


            string _vTipRegistro = GetTipoEmpresaPorNaturezaJuridicaSqlServer(dados.naturezaJuridica); //Verificar

            decimal CodMuniEmpresa = decimal.Parse(CalDvMunicipio(dados.endereco.codMunicipio));

            #region RUC_GENERAL
            using (Ruc_General_sqlserver rg = new Ruc_General_sqlserver())
            {
                //rg.MainConnectionProvider = cp;
                rg.rge_pra_protocolo = pProtocolo;
                rg.rge_ruc = "";
                rg.rge_tge_ttip_reg = 257;
                rg.rge_tge_vtip_reg = Int32.Parse(_vTipRegistro);
                if (dados.opcaoSimei == "S")
                    rg.rge_tge_vtip_reg = 13;

                rg.rge_tge_ttip_ctrib = 153;
                rg.rge_tge_vtip_ctrib = 9999;
                rg.rge_tge_ttip_pers = 233;
                rg.rge_tge_vtip_pers = 2;
                rg.rge_cgc_cpf = dados.cnpj;
                rg.rge_tge_ttamanho = 21;
                rg.rge_tge_vtamanho = 3;
                rg.rge_fec_ini_act_ec = DateTime.Now;
                if (validaNulo(dados.dataAberturaEstabelecimento) != "")
                    rg.rge_fec_ini_act_ec = ConvertStringDateTime(dados.dataAberturaEstabelecimento);
                if (dados.porte != null)
                {
                    if (dados.porte == "01") //ME
                    {
                        rg.rge_tge_vtamanho = 1;
                    }
                    if (dados.porte == "03")//EPP
                    {
                        rg.rge_tge_vtamanho = 2;
                    }
                }

                rg.rge_nomb = dados.nomeEmpresarial;
                rg.rge_codg_mun = CodMuniEmpresa;
                //rg.rge_tae_cod_actvd = dados.cnaePrincipal;
                rg.rge_tuf_cod_uf = dados.endereco.uf;

                rg.Update(cp);


            }
            #endregion

            #region RUC_ESTAB
            using (Ruc_Estab_Sqlserver rb = new Ruc_Estab_Sqlserver())
            {
                rb.res_rge_pra_protocolo = pProtocolo;
                rb.res_ide_estab = 0;
                rb.res_tip_estab = 1;
                rb.res_tge_ttip_reg = 155;
                rb.res_tge_vtip_reg = 9999;
                rb.res_tge_tcond_uso = 152;
                rb.res_tge_vcond_uso = 9999;
                rb.res_sigl = "";
                rb.res_area = 0;
                rb.res_tge_tuni_medid = 156;
                rb.res_tge_vuni_medid = 9999;
                rb.res_nire_sede = "";
                rb.res_cnpj_sede = validaNulo(dados.cnpjMatriz);

                rb.res_nume = dados.endereco.numLogradouro;
                rb.res_tus_cod_usr = "REGIN";
                rb.res_nom_estab = dados.nomeEmpresarial;
                rb.res_ident_comp = validaNulo(dados.endereco.complementoLogradouro);
                rb.res_refer = validaNulo(dados.endereco.referencia);
                rb.res_ttl_tip_logradoro = validaNulo(dados.endereco.codTipoLogradouro);
                rb.res_direccion = validaNulo(dados.endereco.logradouro);
                rb.res_urbanizacion = validaNulo(dados.endereco.bairro);
                rb.res_tes_cod_estado = dados.endereco.uf;
                rb.res_zona_postal = validaNulo(dados.endereco.cep);
                rb.res_tmu_cod_mun = CodMuniEmpresa;

                rb.Update(cp);
            }
            #endregion

            #region RUC_COMP

            using (Ruc_Comp_sqlserver rc = new Ruc_Comp_sqlserver())
            {
                rc.rco_num_reg_merc = nire;
                rc.rco_tge_ttip_doc = 151;
                rc.rco_tge_vtip_doc = 9999;
                rc.rco_tnc_cod_natur = decimal.Parse(dados.naturezaJuridica);
                rc.rco_domic_pais = 1;
                rc.rco_fec_incorp = DtHoje;
                rc.rco_val_cap_soc = decimal.Parse(dados.capitalSocial) / 100;
                rc.rco_nume = validaNulo(dados.endereco.numLogradouro);
                rc.rco_tge_tpais = 22;
                rc.rco_tge_vpais = 105;
                rc.rco_tus_cod_usr = "REGIN";
                rc.rco_ident_comp = validaNulo(dados.endereco.complementoLogradouro);
                rc.rco_refer = validaNulo(dados.endereco.referencia);
                rc.rco_lic_mun = "";
                rc.rco_ttl_tip_logradoro = validaNulo(dados.endereco.codTipoLogradouro);
                rc.rco_direccion = validaNulo(dados.endereco.logradouro);
                rc.rco_urbanizacion = validaNulo(dados.endereco.bairro);
                rc.rco_tes_cod_estado = validaNulo(dados.endereco.uf);
                rc.rco_zona_postal = validaNulo(dados.endereco.cep);
                rc.rco_tmu_cod_mun = CodMuniEmpresa;
                rc.rco_rge_pra_protocolo = pProtocolo;
                if (validaNulo(dados.dataAberturaEstabelecimento) != "")
                    rc.rco_fec_const_nasc = ConvertStringDateTime(dados.dataAberturaEstabelecimento);

                rc.Update(cp);
            }

            #endregion

            #region RUC_ACTV_ECON

            Ruc_Actv_Econ_sqlserver cav = new Ruc_Actv_Econ_sqlserver();
            cav.rae_rge_pra_protocolo = pProtocolo;
            cav.rae_tae_cod_actvd = dados.cnaePrincipal;
            cav.rae_calif_actv = "1";
            cav.rae_porcent = 100;
            cav.rae_tus_cod_usr = "REGIN";
            cav.rae_fec_actl = DtHoje;
            cav.Update(cp);

            foreach (string pCNAE in dados.cnaeSecundaria)
            {
                if (pCNAE != null && pCNAE != "0000000")
                {
                    using (cav = new Ruc_Actv_Econ_sqlserver())
                    {
                        cav.rae_rge_pra_protocolo = pProtocolo;
                        cav.rae_tae_cod_actvd = pCNAE;
                        cav.rae_calif_actv = "2";
                        cav.rae_porcent = 0;
                        cav.rae_tus_cod_usr = "REGIN";
                        cav.rae_fec_actl = DtHoje;
                        cav.Update(cp);
                    }
                }
            }
            #endregion

            #region RUC_GEN_PROTOCOLO
            using (Ruc_Gen_Protocolo_sqlserver gc = new Ruc_Gen_Protocolo_sqlserver())
            {
                gc.rgp_rge_pra_protocolo = pProtocolo;
                gc.rgp_tge_tip_tab = 902;
                gc.rgp_tge_cod_tip_tab = 1;
                gc.rgp_valor = validaNulo(dados.objetoSocial);
                gc.rgp_tus_cod_usr = "REGIN";
                gc.rgp_fec_actl = DtHoje;
                if (gc.rgp_valor != "")
                {
                    gc.Update(cp);
                }
            }

            #endregion

            //}
            //catch (Exception ex)
            //{
            //    new Exception("Erro procedimento GravaWs11 " + ex.Message + "  " + ex.StackTrace);
            //}
        }

        #endregion

        #region Completa ruc com dados de DBE, Requerimento

        public void gravarContadorRequerimento(OracleTransaction cp, string pProtocolo, string pProtRequerimento)
        {
            DataTable toReturn = new DataTable();
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.CommandText = @"SELECT aa.T093_CO_CRC_EMPRESA CRCEmpresa
                                             , aa.T093_CO_CRC_RESP CRCResponsavel
                                             , aa.T093_CPF_RESP CpfContador
                                             , aa.T093_CPFCNPJ CpfCNPJContador
                                             , aa.T093_TIP_CLASS_EMPRESA ClassEmpresa
                                             , aa.T093_TIP_CLASS_RESP ClassContador
                                             , aa.T093_TIP_CRC_EMPRESA TipoCrcEmpresa
                                             , aa.T093_TIP_CRC_RESP TipoCRCContador
                                             , aa.T093_UF_CRC_EMPRESA UfCRCEmpresa
                                             , aa.T093_UF_CRC_RESP UfCrcContador
                                             , aa.T093_DS_PESSOA NomePessoa
                                             , aa.t093_DDD ddd
                                             , aa.t093_ds_tipo_logradouro TipoLogradouro
                                             , aa.t093_email email
                                             , aa.t093_end_bairro Bairro
                                             , aa.t093_end_cep Cep
                                             , aa.t093_end_cod_munic CodMunic
                                             , aa.t093_end_complemento Complemento
                                             , aa.t093_end_logradouro Logradouro
                                             , aa.t093_end_numero Numero
                                             , aa.t093_end_uf Uf
                                             , aa.t093_telefone Telefone
                                             , aa.T005_NR_PROTOCOLO Protocolo

                                        FROM
                                          requerimento.t093_contador aa
                                        WHERE
                                          1 = 1
                                            and aa.T093_CO_CRC_EMPRESA <> ''
                                            and aa.T093_CPFCNPJ <> ''
                                            and aa.T005_NR_PROTOCOLO = '" + pProtRequerimento + "'";

                    cmd.CommandType = CommandType.Text;
                    //   DataTable toReturn = new DataTable("getCursorDados99");

                    cmd.Connection = _conn;
                    cmd.Connection.Open();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(toReturn);
                    }
                }
            }

            if (toReturn.Rows.Count == 0)
                return;

            //Para validar caso crc nao for numerico nao ponho
            string crc = toReturn.Rows[0]["CRCResponsavel"].ToString().Trim();
            if (crc != "")
            {
                try
                {
                    decimal pValorCRC = decimal.Parse(crc);
                }
                catch
                {
                    return;
                }
            }

            //Para validar caso crc nao for numerico nao ponho
            crc = toReturn.Rows[0]["CRCEmpresa"].ToString();
            if (crc != "")
            {
                try
                {
                    decimal pValorCRC = decimal.Parse(crc);
                }
                catch
                {
                    return;
                }
            }

            using (OracleCommand cmdToExecuteSql = new OracleCommand())
            {
                StringBuilder sSocio = new StringBuilder();
                sSocio.AppendLine(" delete ");
                sSocio.AppendLine(" from	ruc_relat_prof ");
                sSocio.AppendLine(" where	RRP_RGE_PRA_PROTOCOLO = :v_rrp_rge_pra_protocolo");
                sSocio.AppendLine(" and		RRP_TGE_VTIP_RELAC = :v_rrp_tge_vtip_relac ");

                cmdToExecuteSql.CommandText = sSocio.ToString();
                cmdToExecuteSql.CommandType = CommandType.Text;

                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rrp_rge_pra_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rrp_tge_vtip_relac", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, 3));


                cmdToExecuteSql.Transaction = cp;
                cmdToExecuteSql.Connection = cp.Connection;

                object pCount = cmdToExecuteSql.ExecuteNonQuery();

                Ruc_Relat_Prof_co_oracle rp = new Ruc_Relat_Prof_co_oracle();

                string CpfCNPJContador = toReturn.Rows[0]["CpfCNPJContador"].ToString();

                // rp.MainConnectionProvider = cp;
                rp.rrp_rge_pra_protocolo = pProtocolo;
                rp.rrp_cgc_cpf_secd = CpfCNPJContador;
                rp.rrp_tge_ttip_relac = 24;
                rp.rrp_tge_vtip_relac = 3;
                rp.rrp_fec_inic_part = DateTime.Now;
                rp.rrp_tge_tcod_qual = 23;
                rp.rrp_tge_vcod_qual = 9999;
                rp.rrp_crc_ctdr = toReturn.Rows[0]["CRCEmpresa"].ToString();
                rp.rrp_uf_crc_ctdr = toReturn.Rows[0]["UfCRCEmpresa"].ToString();

                rp.RRP_TIPO_CRC_CTDR = toReturn.Rows[0]["TipoCrcEmpresa"].ToString();
                if (toReturn.Rows[0]["ClassEmpresa"] != null)
                {
                    rp.RRP_CLASS_CRC_CTDR = decimal.Parse(toReturn.Rows[0]["ClassEmpresa"].ToString());
                }

                rp.rrp_desc_doc = "";
                rp.rrp_tus_cod_usr = "JUNTA";
                rp.rrp_cnpj_vacio = int.MinValue;

                decimal CapitalSocio = 0;
                decimal PercentualSocio = 0;

                rp.rrp_porc_part = PercentualSocio;
                rp.RRP_VAL_CAP_SOC = CapitalSocio;
                rp.Update(cp);


                Ruc_Prof_co_oracle rf = new Ruc_Prof_co_oracle();
                //   rp.MainConnectionProvider = cp;
                rf.rpr_rge_pra_protocolo = pProtocolo;
                rf.rpr_tge_tpais = 22;
                rf.rpr_tge_vpais = 105;// Decimal.Parse(s.EndPais);
                rf.rpr_cgc_cpf_secd = CpfCNPJContador;
                rf.rpr_tge_ttip_pers = 233;
                rf.rpr_tmu_cod_mun = int.MinValue;
                if (toReturn.Rows[0]["CodMunic"] != null && toReturn.Rows[0]["CodMunic"].ToString() != "")
                {
                    rf.rpr_tmu_cod_mun = decimal.Parse(toReturn.Rows[0]["CodMunic"].ToString());
                }
                rf.rpr_ident_comp = toReturn.Rows[0]["Complemento"].ToString();
                rf.rpr_ttl_tip_logradoro = toReturn.Rows[0]["TipoLogradouro"].ToString();
                rf.rpr_direccion = toReturn.Rows[0]["Logradouro"].ToString();
                rf.rpr_urbanizacion = toReturn.Rows[0]["Bairro"].ToString();
                rf.rpr_tes_cod_estado = toReturn.Rows[0]["Uf"].ToString();
                rf.rpr_zona_postal = toReturn.Rows[0]["cep"].ToString();
                rf.rpr_nume = toReturn.Rows[0]["Numero"].ToString();




                rf.rpr_tge_vtip_pers = CpfCNPJContador.Length < 12 ? 1 : 2;

                rf.rpr_email = toReturn.Rows[0]["email"].ToString();
                rf.rpr_nomb = toReturn.Rows[0]["NomePessoa"].ToString();

                rf.UpdateContador(cp);

                StringBuilder sqlD = new StringBuilder();
                sqlD.AppendLine(" update TAB_INFORM_EXTRA_JUNTA ");
                sqlD.AppendLine(" set	 TIE_CPF_CNPJ = :v_TIE_CPF_CNPJ, ");
                sqlD.AppendLine(" 		 TIE_EMAIL = :v_TIE_EMAIL ");
                sqlD.AppendLine(" where	TIE_PROTOCOLO = :v_TIE_PROTOCOLO ");
                sqlD.AppendLine(" and	TIE_TIPO_RELACAO = 3 ");

                cmdToExecuteSql.Parameters.Clear();
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_CPF_CNPJ", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, CpfCNPJContador));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_EMAIL", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, toReturn.Rows[0]["email"].ToString()));

                cmdToExecuteSql.CommandText = sqlD.ToString();
                cmdToExecuteSql.CommandType = CommandType.Text;
                if (cmdToExecuteSql.ExecuteNonQuery() == 0)
                {
                    sqlD = new StringBuilder();
                    sqlD.AppendLine(" Insert into TAB_INFORM_EXTRA_JUNTA (TIE_PROTOCOLO, TIE_TIPO_RELACAO, TIE_CPF_CNPJ, TIE_EMAIL) ");
                    sqlD.AppendLine(" Values (:v_TIE_PROTOCOLO, 3, :v_TIE_CPF_CNPJ, :v_TIE_EMAIL) ");
                    cmdToExecuteSql.CommandText = sqlD.ToString();
                    cmdToExecuteSql.CommandType = CommandType.Text;
                    cmdToExecuteSql.ExecuteNonQuery();

                }


                if (toReturn.Rows[0]["CRCResponsavel"].ToString() != "")
                {

                    RUC_REPRESENTANTES_CO_ORACLE repre = new RUC_REPRESENTANTES_CO_ORACLE();
                    string cpfRepresentanteLegal = toReturn.Rows[0]["CpfContador"].ToString().Trim();

                    repre.rsr_pra_protocolo = pProtocolo;
                    repre.rsr_cgc_cpf_princ = CpfCNPJContador;
                    repre.rsr_cgc_cpf_secd = cpfRepresentanteLegal;
                    repre.rsr_crc_ctdr = toReturn.Rows[0]["CRCResponsavel"].ToString().Trim();
                    repre.rsr_uf_crc_ctdr = toReturn.Rows[0]["UfCrcContador"].ToString().Trim();
                    repre.rsr_tipo = 2; //Representante contador
                    repre.rsr_nomb = toReturn.Rows[0]["NomePessoa"].ToString().Trim();
                    repre.rsr_tge_tcod_qual = 23;
                    repre.rsr_tge_vcod_qual = decimal.Parse("9999");
                    repre.rsr_tge_ttip_pers = 233;
                    repre.rsr_tge_vtip_pers = cpfRepresentanteLegal.Length < 12 ? 1 : 2;
                    repre.Update(cp);




                }


                //psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                //cc.Bairro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.bairro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.bairro;
                //cc.Cep = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.cep == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.cep;
                //cc.Codigo_municipio = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codMunicipio == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codMunicipio;
                //cc.Complemento = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.complementoLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.complementoLogradouro;
                //cc.Logradouro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.logradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.logradouro;
                //cc.Numero = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.numLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.numLogradouro;
                //cc.Pais = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codPais == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codPais;
                //cc.TipLogradoro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codTipoLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codTipoLogradouro;
                //cc.Uf = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.uf == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.uf;

                //cc.TrataEndereco(ref cc, DtTipoLogra);


                //repre.rsr_direccion = cc.Logradouro;
                //repre.rsr_nume = cc.Numero;
                //repre.rsr_tge_vpais = cc.Pais;
                //repre.rsr_ttl_tip_logradoro = cc.TipLogradoro;
                //repre.rsr_urbanizacion = cc.Bairro;
                //repre.rsr_tes_cod_estado = cc.Uf;
                //repre.rsr_zona_postal = cc.Cep;
                //repre.rsr_ident_comp = cc.Complemento;
                //repre.rsr_tmu_cod_mun = cc.Codigo_municipio;
                //repre.rsr_tge_vpais = cc.Pais;


            }

        }

        public void gravarContadorWs35(OracleTransaction cp, string pXml35, string pProtocolo, DataTable DtTipoLogra, string pCodServico)
        {
            
            psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse)CreateObject(pXml35, dados);

            psc.Ruc.Tablelas.DAL.wsRfbRegin.dadosCNPJ dadosCNPJ = new psc.Ruc.Tablelas.DAL.wsRfbRegin.dadosCNPJ();
            //dadosCNPJ = (psc.Ruc.Tablelas.DAL.wsRfbRegin)CreateObject(pXml35, dadosCNPJ);

            
            if (dados.dadosRedesim.fcpj == null)
            {
                return;
            }
            if (dados.dadosRedesim.fcpj.cnpjEmpresaContabil == null && dados.dadosRedesim.fcpj.cpfContadorPF == null)
            {
                return;
            }

            using (OracleCommand cmdToExecuteSql = new OracleCommand())
            {
              
                cmdToExecuteSql.Transaction = cp;
                cmdToExecuteSql.Connection = cp.Connection;

                string CpfCNPJContador = validaNulo(dados.dadosRedesim.fcpj.cnpjEmpresaContabil);
                string rrp_crc_ctdrPJ = validaNulo(dados.dadosRedesim.fcpj.seqCRCempresaContabil);
                string rrp_uf_crc_ctdrPJ = validaNulo(dados.dadosRedesim.fcpj.ufCRCempresaContabil);
                string RRP_TIPO_CRC_CTDRPJ = validaNulo(dados.dadosRedesim.fcpj.codTipoCRCempresaContabil);
                string RRP_CLASS_CRC_CTDRPJ = validaNulo(dados.dadosRedesim.fcpj.codClassificEmpresaContabil);
                string NomePJ = validaNulo(dados.dadosRedesim.fcpj.nomeEmpresaContabil);

                string rrp_crc_ctdrPF = validaNulo(dados.dadosRedesim.fcpj.numSeqContadorPF);
                string rrp_uf_crc_ctdrPF = validaNulo(dados.dadosRedesim.fcpj.ufContadorPF);
                string RRP_TIPO_CRC_CTDRPF = validaNulo(dados.dadosRedesim.fcpj.codTipoCRCcontadorPF);
                string RRP_CLASS_CRC_CTDRPF = validaNulo(dados.dadosRedesim.fcpj.codClassificCRCcontadorPF);
                string NomePF = validaNulo(dados.dadosRedesim.fcpj.nomeContadorPF);
                string rpr_email = ""; 
                string CPFContador = validaNulo(dados.dadosRedesim.fcpj.cpfContadorPF);

                if (dados.dadosRedesim.fcpj.contatoContadorPf != null)
                {
                    rpr_email = validaNulo(dados.dadosRedesim.fcpj.contatoContadorPf.correioEletronico);
                }

                psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                if (dados.dadosRedesim.fcpj.endContadorPf != null)
                {
                    cc.Bairro = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.bairro);
                    cc.Cep = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.cep);
                    cc.Codigo_municipio = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.codMunicipio);
                    cc.Complemento = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.complementoLogradouro);
                    cc.Logradouro = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.logradouro);
                    cc.Numero = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.numLogradouro);
                    cc.Pais = "";
                    cc.TipLogradoro = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.codTipoLogradouro);
                    cc.Uf = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.uf);


                    cc.TrataEndereco(ref cc, DtTipoLogra);
                }

                if (CpfCNPJContador == "")
                {
                    CpfCNPJContador = validaNulo(dados.dadosRedesim.fcpj.cpfContadorPF);
                    rrp_crc_ctdrPJ = validaNulo(dados.dadosRedesim.fcpj.numSeqContadorPF);
                    rrp_uf_crc_ctdrPJ = validaNulo(dados.dadosRedesim.fcpj.ufContadorPF);
                    RRP_TIPO_CRC_CTDRPJ = validaNulo(dados.dadosRedesim.fcpj.codTipoCRCcontadorPF);
                    RRP_CLASS_CRC_CTDRPJ = validaNulo(dados.dadosRedesim.fcpj.codClassificCRCcontadorPF);
                    NomePJ = validaNulo(dados.dadosRedesim.fcpj.nomeContadorPF);

                    rrp_crc_ctdrPF = "";
                    rrp_uf_crc_ctdrPF = "";
                    RRP_TIPO_CRC_CTDRPF = "";
                    RRP_CLASS_CRC_CTDRPF = "";
                    NomePF = "";
                    CPFContador = "";

                }

                Ruc_Relat_Prof_co_oracle rp = new Ruc_Relat_Prof_co_oracle();
                // rp.MainConnectionProvider = cp;
                rp.rrp_rge_pra_protocolo = pProtocolo;
                rp.rrp_cgc_cpf_secd = CpfCNPJContador;
                rp.rrp_tge_ttip_relac = 24;
                rp.rrp_tge_vtip_relac = 3;
                rp.rrp_fec_inic_part = DateTime.Now;
                rp.rrp_tge_tcod_qual = 23;
                rp.rrp_tge_vcod_qual = 9999;
                rp.rrp_crc_ctdr = rrp_crc_ctdrPJ;
                rp.rrp_uf_crc_ctdr = rrp_uf_crc_ctdrPJ;

                rp.RRP_TIPO_CRC_CTDR = RRP_TIPO_CRC_CTDRPJ;
                if (RRP_CLASS_CRC_CTDRPJ != null)
                {
                    rp.RRP_CLASS_CRC_CTDR = decimal.Parse(RRP_CLASS_CRC_CTDRPJ);
                }

                rp.rrp_desc_doc = "";
                rp.rrp_tus_cod_usr = "JUNTA";
                rp.rrp_cnpj_vacio = int.MinValue;

                decimal CapitalSocio = 0;
                decimal PercentualSocio = 0;

                rp.rrp_porc_part = PercentualSocio;
                rp.RRP_VAL_CAP_SOC = CapitalSocio;
                rp.Update(cp);


                Ruc_Prof_co_oracle rf = new Ruc_Prof_co_oracle();
                //   rp.MainConnectionProvider = cp;
                rf.rpr_rge_pra_protocolo = pProtocolo;
                rf.rpr_tge_tpais = 22;
                rf.rpr_tge_vpais = 105;// Decimal.Parse(s.EndPais);
                rf.rpr_cgc_cpf_secd = CpfCNPJContador;
                rf.rpr_tge_ttip_pers = 233;
                rf.rpr_tge_vtip_pers = CpfCNPJContador.Length < 12 ? 1 : 2;

                //ServiceReginRFB ws09 = new ServiceReginRFB(); 
                //Retorno resp09 = new Retorno();
                //ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                //resp09 = ws09.ServiceWs09(CpfCNPJContador);
                //if (resp09.status == "OK")
                //{
                //    if (resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0].sexo != null)
                //    {
                //        decimal psexo = 2;
                //        if (resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0].sexo == "2")
                //            psexo = 1;

                //        rf.rpr_sexo = psexo;
                //    }
                //}
                //else
                //{
                //    throw new Exception("Erro ao tentar buscar os dados do cpf contador do dbe " + CpfCNPJContador + " no ws09 na dll psc.ruc.tabelas.dall");
                //}



                rf.rpr_email = rpr_email;

                rf.rpr_urbanizacion = cc.Bairro;
                rf.rpr_zona_postal = cc.Cep;
                if (cc.Codigo_municipio == "")
                    cc.Codigo_municipio = "0";
                rf.rpr_tmu_cod_mun = decimal.Parse(cc.Codigo_municipio);
                rf.rpr_ident_comp = cc.Complemento;
                rf.rpr_direccion = cc.Logradouro;
                rf.rpr_nume = cc.Numero;
                rf.rpr_tge_vpais = decimal.Parse(cc.Pais);
                rf.rpr_ttl_tip_logradoro = cc.TipLogradoro;
                rf.rpr_tes_cod_estado = cc.Uf;

                rf.rpr_nomb = NomePJ;

                rf.UpdateContador(cp);

                StringBuilder sqlD = new StringBuilder();
                sqlD.AppendLine(" update TAB_INFORM_EXTRA_JUNTA ");
                sqlD.AppendLine(" set	 TIE_CPF_CNPJ = :v_TIE_CPF_CNPJ, ");
                sqlD.AppendLine(" 		 TIE_EMAIL = :v_TIE_EMAIL ");
                sqlD.AppendLine(" where	TIE_PROTOCOLO = :v_TIE_PROTOCOLO ");
                sqlD.AppendLine(" and	TIE_TIPO_RELACAO = 3 ");

                cmdToExecuteSql.Parameters.Clear();
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_CPF_CNPJ", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, CpfCNPJContador));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_EMAIL", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, rpr_email));

                cmdToExecuteSql.CommandText = sqlD.ToString();
                cmdToExecuteSql.CommandType = CommandType.Text;
                if (cmdToExecuteSql.ExecuteNonQuery() == 0)
                {
                    sqlD = new StringBuilder();
                    sqlD.AppendLine(" Insert into TAB_INFORM_EXTRA_JUNTA (TIE_PROTOCOLO, TIE_TIPO_RELACAO, TIE_CPF_CNPJ, TIE_EMAIL) ");
                    sqlD.AppendLine(" Values (:v_TIE_PROTOCOLO, 3, :v_TIE_CPF_CNPJ, :v_TIE_EMAIL) ");
                    cmdToExecuteSql.CommandText = sqlD.ToString();
                    cmdToExecuteSql.CommandType = CommandType.Text;
                    cmdToExecuteSql.ExecuteNonQuery();

                }


                if (CPFContador != "")
                {
                    RUC_REPRESENTANTES_CO_ORACLE repre = new RUC_REPRESENTANTES_CO_ORACLE();
                    string cpfRepresentanteLegal = CPFContador;

                    repre.rsr_pra_protocolo = pProtocolo;
                    repre.rsr_cgc_cpf_princ = CpfCNPJContador;
                    repre.rsr_cgc_cpf_secd = CPFContador;
                    repre.rsr_crc_ctdr = rrp_crc_ctdrPF;
                    repre.rsr_uf_crc_ctdr = rrp_uf_crc_ctdrPF;
                    repre.rsr_tipo = 2; //Representante contador
                    repre.rsr_nomb = NomePF;
                    repre.rsr_tge_tcod_qual = 23;
                    repre.rsr_tge_vcod_qual = decimal.Parse("9999");
                    repre.rsr_tge_ttip_pers = 233;
                    repre.rsr_tge_vtip_pers = cpfRepresentanteLegal.Length < 12 ? 1 : 2;
                    if (RRP_CLASS_CRC_CTDRPF != null)
                    {
                        repre.RSR_CLASS_CRC_CTDR = decimal.Parse(RRP_CLASS_CRC_CTDRPF);
                    }
                    repre.RSR_TIPO_CRC_CTDR = RRP_TIPO_CRC_CTDRPF;
                    repre.Update(cp);
                }
            }
        }

        

        public void gravarDadosEnderecoCorrespondencia(OracleTransaction cp, psc.Ruc.Tablelas.Helper.Endereco cc, string pProtocolo, string pCNPJEmpresa, DataTable DtTipoLogra, string pCodServico)
        {
            //psc.Ruc.Tablelas.DAL.wsRfbRegin.endereco end = new psc.Ruc.Tablelas.DAL.wsRfbRegin.endereco();
            //end = (psc.Ruc.Tablelas.DAL.wsRfbRegin.endereco)CreateObject(pXml35, end);
            StringBuilder sqlD = new StringBuilder();
            if (cc.Bairro != null && cc.Cep != null && cc.Bairro != "" && cc.Cep != "")
            {
                //endereco end = new endereco();

             
                ////psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                //cc.Bairro = end.bairro == null ? "" : end.bairro;
                //cc.Cep = end.cep == null ? "" : end.cep;
                //cc.Codigo_municipio = end.codMunicipio == null ? "" : end.codMunicipio;
                //cc.Complemento = end.complementoLogradouro == null ? "" : end.complementoLogradouro;
                //cc.Logradouro = end.logradouro == null ? "" : end.logradouro;
                //cc.Numero = end.numLogradouro == null ? "" : end.numLogradouro;
                //cc.Pais = end.codPais == null ? "" : end.codPais;
                //cc.TipLogradoro = end.codTipoLogradouro == null ? "" : end.codTipoLogradouro;
                //cc.Uf = end.uf == null ? "" : end.uf;

                cc.TrataEndereco(ref cc, DtTipoLogra);


                using (OracleCommand cmdToExecuteSql = new OracleCommand())
                {
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;

                    StringBuilder Sql = new StringBuilder();

                    Sql.AppendLine(" update ruc_comp Set ");

                    Sql.AppendLine(" RCO_NUME = :v_RPR_NUME, ");
                    Sql.AppendLine(" RCO_IDENT_COMP = :v_RPR_IDENT_COMP, ");
                    Sql.AppendLine(" RCO_TTL_TIP_LOGRADORO = :v_rpr_ttl_tip_logradoro, ");
                    Sql.AppendLine(" RCO_DIRECCION = :v_rpr_direccion, ");
                    Sql.AppendLine(" RCO_URBANIZACION = :v_RPR_URBANIZACION, ");
                    Sql.AppendLine(" RCO_TES_COD_ESTADO = :v_RPR_TES_COD_ESTADO, ");
                    Sql.AppendLine(" RCO_ZONA_POSTAL = :v_RPR_ZONA_POSTAL, ");
                    Sql.AppendLine(" RCO_TMU_COD_MUN = :v_RPR_TMU_COD_MUN, ");
                    Sql.AppendLine(" RCO_ORIGEM_ENDERECO = 1 ");

                    Sql.AppendLine(" where	RCO_RGE_PRA_PROTOCOLO = :v_rpr_rge_pra_protocolo");
                  

                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_nume", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Numero));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_ident_comp", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Complemento));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_ttl_tip_logradoro", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.TipLogradoro));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_direccion", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Logradouro));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_urbanizacion", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Bairro));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_tes_cod_estado", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Uf));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_zona_postal", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Cep));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_tmu_cod_mun", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Codigo_municipio));

                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_rge_pra_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
              
                    cmdToExecuteSql.CommandText = Sql.ToString();
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;
                    cmdToExecuteSql.ExecuteNonQuery();
                }
            }
        }


        public void gravar218CorreioWs35(OracleTransaction cp, string pXml35, string pProtocolo, string pCNPJEmpresa, string pCodServico)
        {

            psc.Ruc.Tablelas.DAL.wsRfbRegin.contato dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.contato();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.contato)CreateObject(pXml35, dados);

            using (OracleCommand cmdToExecuteSql = new OracleCommand())
            {
                
                cmdToExecuteSql.Transaction = cp;
                cmdToExecuteSql.Connection = cp.Connection;

                string pCorreioEletronico = "";
                if (dados != null && dados != null && dados.correioEletronico != null)
                {
                    pCorreioEletronico = validaNulo(dados.correioEletronico);
                }
                
                if (pCorreioEletronico != "")
                {
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;

                    StringBuilder sqlD = new StringBuilder();
                    sqlD.AppendLine(" update TAB_INFORM_EXTRA_JUNTA ");
                    sqlD.AppendLine(" set	 TIE_CPF_CNPJ = :v_TIE_CPF_CNPJ, ");
                    sqlD.AppendLine(" 		 TIE_EMAIL = :v_TIE_EMAIL ");
                    sqlD.AppendLine(" where	TIE_PROTOCOLO = :v_TIE_PROTOCOLO ");
                    sqlD.AppendLine(" and	TIE_TIPO_RELACAO = 4 ");

                    cmdToExecuteSql.Parameters.Clear();
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_CPF_CNPJ", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCNPJEmpresa));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_EMAIL", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCorreioEletronico));

                    cmdToExecuteSql.CommandText = sqlD.ToString();
                    cmdToExecuteSql.CommandType = CommandType.Text;
                    if (cmdToExecuteSql.ExecuteNonQuery() == 0)
                    {
                        sqlD = new StringBuilder();
                        sqlD.AppendLine(" Insert into TAB_INFORM_EXTRA_JUNTA (TIE_PROTOCOLO, TIE_TIPO_RELACAO, TIE_CPF_CNPJ, TIE_EMAIL) ");
                        sqlD.AppendLine(" Values (:v_TIE_PROTOCOLO, 4, :v_TIE_CPF_CNPJ, :v_TIE_EMAIL) ");
                        cmdToExecuteSql.CommandText = sqlD.ToString();
                        cmdToExecuteSql.CommandType = CommandType.Text;
                        cmdToExecuteSql.ExecuteNonQuery();

                    }

                    using (Ruc_Gen_Protocolo gc = new Ruc_Gen_Protocolo())
                    {
                        DateTime DtHoje = DateTime.Now;
                        gc.rgp_rge_pra_protocolo = pProtocolo;
                        gc.rgp_tge_tip_tab = 902;
                        gc.rgp_tge_cod_tip_tab = 3;
                        gc.rgp_valor = validaNulo(pCorreioEletronico);
                        gc.rgp_tus_cod_usr = "REGIN";
                        gc.rgp_fec_actl = DtHoje;
                        if (gc.rgp_valor != "")
                        {
                            gc.Update(cp); 
                        }
                    }

                    
                }
            }
        }

        public void gravar214TelefoneWs35(OracleTransaction cp, string pXml35, string pProtocolo, string pCNPJEmpresa, string pCodServico)
        {

            //psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse();
            //dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse)CreateObject(pXml35, dados);

            psc.Ruc.Tablelas.DAL.wsRfbRegin.contato dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.contato();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.contato)CreateObject(pXml35, dados);


            using (OracleCommand cmdToExecuteSql = new OracleCommand())
            {

                cmdToExecuteSql.Transaction = cp;
                cmdToExecuteSql.Connection = cp.Connection;

                string pTelefone = "";
                string pDdd = "";

                string pTelefone2 = "";
                string pDdd2 = "";

                //if (dados.dadosRedesim.fcpj != null && dados.dadosRedesim.fcpj.contato != null && dados.dadosRedesim.fcpj.contato.telefone1 != null)
                //{
                //    if (dados.dadosRedesim.fcpj.contato.telefone1 != null)
                //    {
                //        pTelefone = validaNulo(dados.dadosRedesim.fcpj.contato.telefone1);
                //    }
                //}
                //if (dados.dadosRedesim.fcpj != null && dados.dadosRedesim.fcpj.contato != null && dados.dadosRedesim.fcpj.contato.dddTelefone1 != null)
                //{
                //    if (dados.dadosRedesim.fcpj.contato.dddTelefone1 != null)
                //    {
                //        pDdd = validaNulo(dados.dadosRedesim.fcpj.contato.dddTelefone1);
                //    }
                //}

                if (dados != null && dados.telefone1 != null)
                {
                    if (dados.telefone1 != null)
                    {
                        pTelefone = validaNulo(dados.telefone1);
                    }
                }
                if (dados != null && dados.dddTelefone1 != null)
                {
                    if (dados.dddTelefone1 != null)
                    {
                        pDdd = validaNulo(dados.dddTelefone1);
                    }
                }

                if (dados != null && dados.telefone2 != null)
                {
                    if (dados.telefone2 != null)
                    {
                        pTelefone2 = validaNulo(dados.telefone2);
                    }
                }
                if (dados != null && dados.dddTelefone2 != null)
                {
                    if (dados.dddTelefone2 != null)
                    {
                        pDdd2 = validaNulo(dados.dddTelefone2);
                    }
                }

                if (pTelefone != "")
                {
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;

                    StringBuilder sqlD = new StringBuilder();
                    sqlD.AppendLine(" update TAB_INFORM_EXTRA_JUNTA ");
                    sqlD.AppendLine(" set	 TIE_CPF_CNPJ = :v_TIE_CPF_CNPJ, ");
                    sqlD.AppendLine(" 		 TIE_FONE1 = :v_TIE_FONE1, ");
                    sqlD.AppendLine(" 		 TIE_DDD_FONE1 = :v_TIE_DDD_FONE1");
                    sqlD.AppendLine(" where	TIE_PROTOCOLO = :v_TIE_PROTOCOLO ");
                    sqlD.AppendLine(" and	TIE_TIPO_RELACAO = 4 ");

                    cmdToExecuteSql.Parameters.Clear();
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_CPF_CNPJ", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCNPJEmpresa));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_FONE1", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pTelefone));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_DDD_FONE1", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pDdd));

                    cmdToExecuteSql.CommandText = sqlD.ToString();
                    cmdToExecuteSql.CommandType = CommandType.Text;
                    if (cmdToExecuteSql.ExecuteNonQuery() == 0)
                    {
                        sqlD = new StringBuilder();
                        sqlD.AppendLine(" Insert into TAB_INFORM_EXTRA_JUNTA (TIE_PROTOCOLO, TIE_TIPO_RELACAO, TIE_CPF_CNPJ, TIE_FONE1, TIE_DDD_FONE1) ");
                        sqlD.AppendLine(" Values (:v_TIE_PROTOCOLO, 4, :v_TIE_CPF_CNPJ, :v_TIE_FONE1, :v_TIE_DDD_FONE1 ) ");
                        cmdToExecuteSql.CommandText = sqlD.ToString();
                        cmdToExecuteSql.CommandType = CommandType.Text;
                        cmdToExecuteSql.ExecuteNonQuery();

                    }

                    using (Ruc_Gen_Protocolo gc = new Ruc_Gen_Protocolo())
                    {
                        DateTime DtHoje = DateTime.Now;
                        gc.rgp_rge_pra_protocolo = pProtocolo;
                        gc.rgp_tge_tip_tab = 902;
                        gc.rgp_tge_cod_tip_tab = 2;
                        gc.rgp_valor = validaNulo(pTelefone);
                        gc.rgp_tus_cod_usr = "REGIN";
                        gc.rgp_fec_actl = DtHoje;
                        if (gc.rgp_valor != "")
                        {
                            gc.Update(cp); //falta para por para atualizar
                        }
                    }
                }

                if (pTelefone2 != "")
                {
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;

                    StringBuilder sqlD = new StringBuilder();
                    sqlD.AppendLine(" update TAB_INFORM_EXTRA_JUNTA ");
                    sqlD.AppendLine(" set	 TIE_CPF_CNPJ = :v_TIE_CPF_CNPJ, ");
                    sqlD.AppendLine(" 		 TIE_FONE2 = :v_TIE_FONE1, ");
                    sqlD.AppendLine(" 		 TIE_DDD_FONE2 = :v_TIE_DDD_FONE1");
                    sqlD.AppendLine(" where	TIE_PROTOCOLO = :v_TIE_PROTOCOLO ");
                    sqlD.AppendLine(" and	TIE_TIPO_RELACAO = 4 ");

                    cmdToExecuteSql.Parameters.Clear();
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_CPF_CNPJ", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCNPJEmpresa));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_FONE1", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pTelefone2));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_DDD_FONE1", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pDdd2));

                    cmdToExecuteSql.CommandText = sqlD.ToString();
                    cmdToExecuteSql.CommandType = CommandType.Text;
                    if (cmdToExecuteSql.ExecuteNonQuery() == 0)
                    {
                        sqlD = new StringBuilder();
                        sqlD.AppendLine(" Insert into TAB_INFORM_EXTRA_JUNTA (TIE_PROTOCOLO, TIE_TIPO_RELACAO, TIE_CPF_CNPJ, TIE_FONE1, TIE_DDD_FONE1) ");
                        sqlD.AppendLine(" Values (:v_TIE_PROTOCOLO, 4, :v_TIE_CPF_CNPJ, :v_TIE_FONE1, :v_TIE_DDD_FONE1 ) ");
                        cmdToExecuteSql.CommandText = sqlD.ToString();
                        cmdToExecuteSql.CommandType = CommandType.Text;
                        cmdToExecuteSql.ExecuteNonQuery();

                    }
                }
            }
        }


        public void gravar216FaxWs35(OracleTransaction cp, string pXml35, string pProtocolo, string pCNPJEmpresa, string pCodServico)
        {

            psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse)CreateObject(pXml35, dados);

     //       psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse ws17v2 = new psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse();
     //       ws17v2 = (psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse)CreateObject(pXml35, ws17v2);
            

            using (OracleCommand cmdToExecuteSql = new OracleCommand())
            {

                cmdToExecuteSql.Transaction = cp;
                cmdToExecuteSql.Connection = cp.Connection;

                string pFax = "";
                string pDdd = "";

                if (dados.dadosRedesim.fcpj != null && dados.dadosRedesim.fcpj.contato != null)
                {
                    if (dados.dadosRedesim.fcpj.contato.fax != null)
                    {
                        pFax = validaNulo(dados.dadosRedesim.fcpj.contato.fax);
                    }
                    if (dados.dadosRedesim.fcpj.contato.dddFax != null)
                    {
                        pDdd = validaNulo(dados.dadosRedesim.fcpj.contato.dddFax);
                    }
                }
                if (pFax != "")
                {
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;

                    StringBuilder sqlD = new StringBuilder();
                    sqlD.AppendLine(" update TAB_INFORM_EXTRA_JUNTA ");
                    sqlD.AppendLine(" set	 TIE_CPF_CNPJ = :v_TIE_CPF_CNPJ, ");
                    sqlD.AppendLine(" 		 TIE_FAX = :v_TIE_FAX, ");
                    sqlD.AppendLine(" 		 TIE_DDD_FAX = :v_TIE_DDD_FAX");
                    sqlD.AppendLine(" where	TIE_PROTOCOLO = :v_TIE_PROTOCOLO ");
                    sqlD.AppendLine(" and	TIE_TIPO_RELACAO = 4 ");

                    cmdToExecuteSql.Parameters.Clear();
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_CPF_CNPJ", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCNPJEmpresa));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_FAX", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pFax));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_DDD_FAX", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pDdd));

                    cmdToExecuteSql.CommandText = sqlD.ToString();
                    cmdToExecuteSql.CommandType = CommandType.Text;
                    if (cmdToExecuteSql.ExecuteNonQuery() == 0)
                    {
                        sqlD = new StringBuilder();
                        sqlD.AppendLine(" Insert into TAB_INFORM_EXTRA_JUNTA (TIE_PROTOCOLO, TIE_TIPO_RELACAO, TIE_CPF_CNPJ, TIE_FAX, TIE_DDD_FAX) ");
                        sqlD.AppendLine(" Values (:v_TIE_PROTOCOLO, 4, :v_TIE_CPF_CNPJ, :v_TIE_FAX, :v_TIE_DDD_FAX ) ");
                        cmdToExecuteSql.CommandText = sqlD.ToString();
                        cmdToExecuteSql.CommandType = CommandType.Text;
                        cmdToExecuteSql.ExecuteNonQuery();

                    }

                }
            }
        }

        public void gravar257AlteraNumeroOrgaRegistro35(OracleTransaction cp, string pProtocolo, string pNumeroOrgaoRegistroWs11)
        {
            //257	Alteração do número de registro no órgão competente
            //psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse();
            //dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse)CreateObject(pXml35, dados);

            using (OracleCommand cmdToExecuteSql = new OracleCommand())
            {

                cmdToExecuteSql.Transaction = cp;
                cmdToExecuteSql.Connection = cp.Connection;


                string pNire = validaNulo(pNumeroOrgaoRegistroWs11);

                if (pNire != "")
                {
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;

                    StringBuilder sqlD = new StringBuilder();
                    sqlD.AppendLine(" update RUC_COMP ");
                    sqlD.AppendLine(" set	 RCO_NUM_REG_MERC = :v_RCO_NUM_REG_MERC ");
                    sqlD.AppendLine(" where	 RCO_RGE_PRA_PROTOCOLO = :v_TIE_PROTOCOLO ");
                    sqlD.AppendLine(" And	 nvl(length(trim(RCO_NUM_REG_MERC)),0) < 3 ");

                    cmdToExecuteSql.Parameters.Clear();
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_RCO_NUM_REG_MERC", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pNire));

                    cmdToExecuteSql.CommandText = sqlD.ToString();
                    cmdToExecuteSql.CommandType = CommandType.Text;
                    cmdToExecuteSql.ExecuteNonQuery();

                    //Atualiza psc_protocolo
                    sqlD = new StringBuilder();
                    sqlD.AppendLine(" update PSC_IDENT_PROTOCOLO ");
                    sqlD.AppendLine(" set	 PIP_NIRE = :v_RCO_NUM_REG_MERC ");
                    sqlD.AppendLine(" where	 PIP_PRO_PROTOCOLO = :v_TIE_PROTOCOLO ");
                    sqlD.AppendLine(" And	 nvl(length(trim(PIP_NIRE)),0) < 3 ");

                    cmdToExecuteSql.Parameters.Clear();
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_RCO_NUM_REG_MERC", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pNire));

                    cmdToExecuteSql.CommandText = sqlD.ToString();
                    cmdToExecuteSql.CommandType = CommandType.Text;
                    cmdToExecuteSql.ExecuteNonQuery();
                    

                }
            }
        }

        public void gravar517DadosBaixaWs35(OracleTransaction cp, string pXml35, string pProtocolo, string pCNPJEmpresa, string pCodServico)
        {
            psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse)CreateObject(pXml35, dados);
            if (dados.dadosRedesim.fcpj == null)
            {
                return;
            }
            StringBuilder sqlD = new StringBuilder();
            using (OracleCommand cmdToExecuteSql = new OracleCommand())
            {

                cmdToExecuteSql.Transaction = cp;
                cmdToExecuteSql.Connection = cp.Connection;

                string MotivoBaixa = dados.dadosRedesim.fcpj.codMotivoSituacaoCadastral == null ? "" : dados.dadosRedesim.fcpj.codMotivoSituacaoCadastral;

                sqlD = new StringBuilder();
                sqlD.AppendLine(" update RUC_GENERAL ");
                sqlD.AppendLine(" set	 RGE_MOT_BAIXA_RFB = '" + MotivoBaixa + "'");
                sqlD.AppendLine(" where	 RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");

                cmdToExecuteSql.Parameters.Clear();
                cmdToExecuteSql.CommandText = sqlD.ToString();
                cmdToExecuteSql.CommandType = CommandType.Text;
                cmdToExecuteSql.ExecuteNonQuery();

            }
        }

        public void gravarDadosEnderecoResponsavel(OracleTransaction cp, string pXml35, string pProtocolo, string pCNPJEmpresa, DataTable DtTipoLogra, string pCodServico)
        {
            psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse)CreateObject(pXml35, dados);
            StringBuilder sqlD = new StringBuilder();
            if (dados.dadosRedesim.fcpj != null && dados.dadosRedesim.fcpj.endResponsavel != null)
            {
                endereco end = new endereco();

                end = dados.dadosRedesim.fcpj.endResponsavel;

                psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                cc.Bairro = end.bairro == null ? "" : end.bairro;
                cc.Cep = end.cep == null ? "" : end.cep;
                cc.Codigo_municipio = end.codMunicipio == null ? "" : end.codMunicipio;
                cc.Complemento = end.complementoLogradouro == null ? "" : end.complementoLogradouro;
                cc.Logradouro = end.logradouro == null ? "" : end.logradouro;
                cc.Numero = end.numLogradouro == null ? "" : end.numLogradouro;
                cc.Pais = end.codPais == null ? "" : end.codPais;
                cc.TipLogradoro = end.codTipoLogradouro == null ? "" : end.codTipoLogradouro;
                cc.Uf = end.uf == null ? "" : end.uf;

                cc.TrataEndereco(ref cc, DtTipoLogra);



                string cpfResponsavel = dados.dadosRedesim.fcpj.cpfResponsavel;
                using (OracleCommand cmdToExecuteSql = new OracleCommand())
                {
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;

                    StringBuilder Sql = new StringBuilder();

                    Sql.AppendLine(" update ruc_prof Set ");

                    Sql.AppendLine(" RPR_NUME = :v_RPR_NUME, ");
                    Sql.AppendLine(" RPR_IDENT_COMP = :v_RPR_IDENT_COMP, ");
                    Sql.AppendLine(" RPR_TTL_TIP_LOGRADORO = :v_rpr_ttl_tip_logradoro, ");
                    Sql.AppendLine(" RPR_DIRECCION = :v_rpr_direccion, ");
                    Sql.AppendLine(" RPR_URBANIZACION = :v_RPR_URBANIZACION, ");
                    Sql.AppendLine(" RPR_TES_COD_ESTADO = :v_RPR_TES_COD_ESTADO, ");
                    Sql.AppendLine(" RPR_ZONA_POSTAL = :v_RPR_ZONA_POSTAL, ");
                    Sql.AppendLine(" RPR_TMU_COD_MUN = :v_RPR_TMU_COD_MUN ");

                    Sql.AppendLine(" where	rpr_rge_pra_protocolo = :v_rpr_rge_pra_protocolo");
                    Sql.AppendLine(" and    trim(rpr_cgc_cpf_secd) = :v_rpr_cgc_cpf_secd");


                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_nume", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Numero));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_ident_comp", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Complemento));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_ttl_tip_logradoro", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.TipLogradoro));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_direccion", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Logradouro));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_urbanizacion", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Bairro));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_tes_cod_estado", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Uf));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_zona_postal", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Cep));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_tmu_cod_mun", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cc.Codigo_municipio));
                
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_rge_pra_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rpr_cgc_cpf_secd", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cpfResponsavel.Trim()));

                    cmdToExecuteSql.CommandText = Sql.ToString();
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;
                    cmdToExecuteSql.ExecuteNonQuery();
                }
            }
        }


        public void gravarDadosProcesso(OracleTransaction cp, string pXml, string pProtocolo, string pCNPJEmpresa, string pCodServico)
        {
            psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse)CreateObject(pXml, dados);

            string numViabilidadeAssociada = "";
            string NumeroProtocoloUnico = "";
            string codMunicOrigem = "";
            #region Buscar data node RedeSim
            if (dados.dadosRedesim.numViabilidadeAssociada != null && dados.dadosRedesim.numViabilidadeAssociada != "")
            {
                NumeroProtocoloUnico = dados.dadosRedesim.numViabilidadeAssociada.Trim();
                if (NumeroProtocoloUnico.Substring(2, 1) == "P" || NumeroProtocoloUnico.Substring(2, 1) == "B")
                    numViabilidadeAssociada = NumeroProtocoloUnico;
            }
            #endregion


            #region buscar Municipio Anterior
            if (dados.dadosRedesim.fcpj != null && dados.dadosRedesim.fcpj.codMunicOrigem != null && dados.dadosRedesim.fcpj.codMunicOrigem != "")
            {
                codMunicOrigem = dados.dadosRedesim.fcpj.codMunicOrigem;
                codMunicOrigem = CalDvMunicipio(codMunicOrigem);
            }
            #endregion

            StringBuilder Sql = new StringBuilder();

            if (codMunicOrigem != "")
            {

                using (OracleCommand cmdToExecuteSql = new OracleCommand())
                {
                    Sql = new StringBuilder();
                    

                    Sql.AppendLine(" update psc_protocolo  ");
                    Sql.AppendLine(" Set    PRO_TMU_COD_MUN_ANTERIOR = :v_PRO_NR_UNICO ");
                    Sql.AppendLine(" where	pro_protocolo = :v_RGE_PRA_PROTOCOLO");
                    Sql.AppendLine(" and    PRO_TMU_COD_MUN_ANTERIOR is null ");


                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_PRO_NR_UNICO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, codMunicOrigem));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_RGE_PRA_PROTOCOLO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                    cmdToExecuteSql.CommandText = Sql.ToString();
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;
                    cmdToExecuteSql.ExecuteNonQuery();
                }
            }


            if (NumeroProtocoloUnico != "")
            {
                using (OracleCommand cmdToExecuteSql = new OracleCommand())
                {
                    Sql = new StringBuilder();
                    
                    Sql.AppendLine(" update psc_protocolo  ");
                    Sql.AppendLine(" Set    PRO_NR_UNICO = :v_PRO_NR_UNICO ");
                    Sql.AppendLine(" where	pro_protocolo = :v_RGE_PRA_PROTOCOLO");
                    Sql.AppendLine(" and    PRO_NR_UNICO is null ");


                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_PRO_NR_UNICO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, NumeroProtocoloUnico));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_RGE_PRA_PROTOCOLO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                    cmdToExecuteSql.CommandText = Sql.ToString();
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;
                    cmdToExecuteSql.ExecuteNonQuery();
                }
            }
            
            if (numViabilidadeAssociada != "")
            {

                using (OracleCommand cmdToExecuteSql = new OracleCommand())
                {

                    Sql = new StringBuilder();
                    
                    Sql.AppendLine(" update psc_protocolo  ");
                    Sql.AppendLine(" Set    PRO_VPV_COD_PROTOCOLO = :v_PRO_NR_UNICO ");
                    Sql.AppendLine(" where	pro_protocolo = :v_RGE_PRA_PROTOCOLO");
                    Sql.AppendLine(" and    PRO_VPV_COD_PROTOCOLO is null ");


                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_PRO_NR_UNICO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, numViabilidadeAssociada));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_RGE_PRA_PROTOCOLO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                    cmdToExecuteSql.CommandText = Sql.ToString();
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;
                    cmdToExecuteSql.ExecuteNonQuery();
                }
            }
        }

        public void gravarDataSituacaoCadastalRucGeneral(OracleTransaction cp, string pXml, string pProtocolo, string pCNPJEmpresa, string pCodServico)
        {
            psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse)CreateObject(pXml, dados);

            DateTime pDataEvento = DateTime.MinValue;

            #region Buscar data node RedeSim
            if (dados.dadosRedesim.fcpj != null && dados.dadosRedesim.fcpj.dataEvento != null && dados.dadosRedesim.fcpj.dataEvento.Length > 0)
            {
                string iString = "";
                string DataDeferimento = "";
                try
                {
                    DataDeferimento = dados.dadosRedesim.fcpj.dataEvento[0];
                    if (DataDeferimento.Length > 5)
                    {
                        string ano = DataDeferimento.Substring(0, 4);
                        string mes = DataDeferimento.Substring(4).Remove(2);
                        string dia = DataDeferimento.Substring(6);

                        iString = ano + mes + dia;
                        pDataEvento = DateTime.ParseExact(iString, "yyyyMMdd", null);
                    }
                }
                catch
                {
                    throw new Exception(" Erro gravarDataSituacaoCadastalRucGeneral " + DataDeferimento);
                }
            }
            #endregion

            #region Buscar data node Socio
            if (pDataEvento == DateTime.MinValue && dados.dadosRedesim.socios != null && dados.dadosRedesim.socios[0].dataEvento != null && dados.dadosRedesim.socios[0].dataEvento.Length > 0)
            {
                string iString = "";
                string DataDeferimento = "";
                try
                {
                    DataDeferimento = dados.dadosRedesim.socios[0].dataEvento;
                    if (DataDeferimento.Length > 5)
                    {
                        string ano = DataDeferimento.Substring(0, 4);
                        string mes = DataDeferimento.Substring(4).Remove(2);
                        string dia = DataDeferimento.Substring(6);

                        iString = ano + mes + dia;
                        pDataEvento = DateTime.ParseExact(iString, "yyyyMMdd", null);
                    }
                }
                catch
                {
                    throw new Exception(" Erro gravarDataSituacaoCadastalRucGeneral " + DataDeferimento);
                }
            }
            #endregion
            StringBuilder sqlD = new StringBuilder();
            if (pDataEvento != DateTime.MinValue)
            {

                using (OracleCommand cmdToExecuteSql = new OracleCommand())
                {
       
                    StringBuilder Sql = new StringBuilder();

                    Sql.AppendLine(" update ruc_general  ");
                    Sql.AppendLine(" Set    RGE_FEC_SIT_CAD = :v_RGE_FEC_SIT_CAD ");
                    Sql.AppendLine(" where	RGE_PRA_PROTOCOLO = :v_RGE_PRA_PROTOCOLO");
                    Sql.AppendLine(" and    RGE_FEC_SIT_CAD is null ");


                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_RGE_FEC_SIT_CAD", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pDataEvento));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_RGE_PRA_PROTOCOLO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                    cmdToExecuteSql.CommandText = Sql.ToString();
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;
                    cmdToExecuteSql.ExecuteNonQuery();
                }
            }
        }

        public void gravarDadosSucessoraWs35(OracleTransaction cp, string pXml35, string pProtocolo, string pCNPJEmpresa, string pCodServico)
        {
            psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse)CreateObject(pXml35, dados);
            StringBuilder sqlD = new StringBuilder();
            if (dados.dadosRedesim.fcpj != null && dados.dadosRedesim.fcpj.cnpjSucessora != null)
            {
                using (OracleCommand cmdToExecuteSql = new OracleCommand())
                {
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;

                    sqlD = new StringBuilder();
                    sqlD.AppendLine(" delete RUC_EMPRESAS_VINCULADAS ");
                    sqlD.AppendLine(" where	 REV_PROTOCOLO = '" + pProtocolo + "'");
                    sqlD.AppendLine(" And    REV_TIPO = 1 ");

                    cmdToExecuteSql.Parameters.Clear();
                    cmdToExecuteSql.CommandText = sqlD.ToString();
                    cmdToExecuteSql.CommandType = CommandType.Text;
                    cmdToExecuteSql.ExecuteNonQuery();

                    foreach (string cnpjSucessora in dados.dadosRedesim.fcpj.cnpjSucessora)
                    {
                        if (cnpjSucessora != null && cnpjSucessora != "")
                        {
                            sqlD = new StringBuilder();
                            sqlD.AppendLine(" Insert into RUC_EMPRESAS_VINCULADAS (REV_PROTOCOLO, REV_TIPO, REV_CNPJ_VINCULADA) ");
                            sqlD.AppendLine(" Values (:v_TIE_PROTOCOLO, :v_REV_TIPO, :v_REV_CNPJ_VINCULADA) ");

                            cmdToExecuteSql.Parameters.Clear();
                            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_REV_TIPO", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, 1));
                            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_REV_CNPJ_VINCULADA", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cnpjSucessora));


                            cmdToExecuteSql.CommandText = sqlD.ToString();
                            cmdToExecuteSql.CommandType = CommandType.Text;
                            cmdToExecuteSql.ExecuteNonQuery();
                        }
                    }
                }


            }
        }

        private void gravaEvento(OracleTransaction cp, string pEvento, string pProtocolo)
        {
            StringBuilder sqlD = new StringBuilder();
            using (OracleCommand cmdToExecuteSql = new OracleCommand())
            {
                cmdToExecuteSql.Transaction = cp;
                cmdToExecuteSql.Connection = cp.Connection;

                sqlD = new StringBuilder();
                sqlD.AppendLine(" Select count(*) ");
                sqlD.AppendLine(" from   mac_eventos_rfb ");
                sqlD.AppendLine(" where	 mer_cod_evento = " + pEvento);
                sqlD.AppendLine(" And    mer_evento_s08 = 1 ");

                cmdToExecuteSql.Parameters.Clear();
                cmdToExecuteSql.CommandText = sqlD.ToString();
                cmdToExecuteSql.CommandType = CommandType.Text;
                int QtdEventoTratado = int.Parse(cmdToExecuteSql.ExecuteScalar().ToString());


                sqlD = new StringBuilder();
                sqlD.AppendLine(" Select count(*) ");
                sqlD.AppendLine(" from   psc_prot_evento_rfb ");
                sqlD.AppendLine(" where	 pev_pro_protocolo = '" + pProtocolo + "'");
                sqlD.AppendLine(" And    pev_cod_evento = " + pEvento);

                cmdToExecuteSql.Parameters.Clear();
                cmdToExecuteSql.CommandText = sqlD.ToString();
                cmdToExecuteSql.CommandType = CommandType.Text;
                int Qtd = int.Parse(cmdToExecuteSql.ExecuteScalar().ToString());

                if (Qtd == 0 && QtdEventoTratado > 0)
                {

                    sqlD = new StringBuilder();
                    sqlD.AppendLine(" Insert into psc_prot_evento_rfb (pev_pro_protocolo, pev_cod_evento) ");
                    sqlD.AppendLine(" Values (:v_TIE_PROTOCOLO, :v_pev_cod_evento) ");

                    cmdToExecuteSql.Parameters.Clear();
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_pev_cod_evento", OracleType.Number, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pEvento));

                    cmdToExecuteSql.CommandText = sqlD.ToString();
                    cmdToExecuteSql.CommandType = CommandType.Text;
                    cmdToExecuteSql.ExecuteNonQuery();

                }
            }
        }

        public void gravarEventosFaltantesDBEEventoWs35(OracleTransaction cp, string pXml35, string pProtocolo, string pCNPJEmpresa, string pCodServico)
        {
            psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.serviceResponse)CreateObject(pXml35, dados);
            StringBuilder sqlD = new StringBuilder();

            if (dados.dadosRedesim.fcpj != null)
            {

                if (dados.dadosRedesim.fcpj.codEvento == null && dados.dadosRedesim.socios != null)
                {
                    gravaEvento(cp, "202", pProtocolo);
                }


                if (dados.dadosRedesim.fcpj.codEvento != null && pCNPJEmpresa.Trim() == dados.dadosRedesim.cnpj.Trim())
                {
                    using (OracleCommand cmdToExecuteSql = new OracleCommand())
                    {
                        cmdToExecuteSql.Transaction = cp;
                        cmdToExecuteSql.Connection = cp.Connection;

                        foreach (string Evento in dados.dadosRedesim.fcpj.codEvento)
                        {
                            gravaEvento(cp, Evento, pProtocolo);
                        }
                    }
                }
                //Isto e para pegar os processos que sao de matriz con dbe e caso seja uma filial dessa matriz, por o evento 220
                if (dados.dadosRedesim.fcpj.codEvento != null && pCNPJEmpresa.Trim() != dados.dadosRedesim.cnpj.Trim() 
                    && pCNPJEmpresa.Trim().Substring(0, 8) == dados.dadosRedesim.cnpj.Trim().Substring(0, 8))
                {
                    using (OracleCommand cmdToExecuteSql = new OracleCommand())
                    {
                        cmdToExecuteSql.Transaction = cp;
                        cmdToExecuteSql.Connection = cp.Connection;

                        foreach (string Evento in dados.dadosRedesim.fcpj.codEvento)
                        {
                            if (Evento == "220" || Evento == "225")
                                gravaEvento(cp, Evento, pProtocolo);
                        }
                    }
                }
            }
        }
        #endregion

        public void GravaRepresentanteDoCNPJRFNB(OracleTransaction bd, string Xmlws11, string pProtocolo, DataTable DtTipoLogra)
        {

            psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim dados = new psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim();
            dados = (psc.Ruc.Tablelas.DAL.wsRfbRegin.retornoWS11Redesim)CreateObject(Xmlws11, dados);

            #region RUC_REPRSETENTANTE

            decimal ValorCapitalEmpresa = 0;

            if (dados.dadosCNPJ[0].cpfRepresentante != null && dados.dadosCNPJ[0].cpfRepresentante != "")
            {
                RUC_REPRESENTANTES_CO_ORACLE repre = new RUC_REPRESENTANTES_CO_ORACLE();
                dadosCPF dados09 = new dadosCPF();
                repre.rsr_pra_protocolo = pProtocolo;
                repre.rsr_cgc_cpf_princ = dados.dadosCNPJ[0].cnpj;
                repre.rsr_cgc_cpf_secd = dados.dadosCNPJ[0].cpfRepresentante;
                repre.rsr_tipo = 6;
                repre.rsr_nomb = dados.dadosCNPJ[0].nomeRepresentante;
                repre.rsr_tge_tcod_qual = 23;
                repre.rsr_tge_vcod_qual = decimal.Parse(dados.dadosCNPJ[0].qualificacaoRepresentante);
                repre.rsr_tge_ttip_pers = 233;
                repre.rsr_tge_vtip_pers = 1;

                ServiceReginRFB ws09 = new ServiceReginRFB();
                Retorno ws0911Socio = new Retorno();
                ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                ws0911Socio = ws09.ServiceWs09(dados.dadosCNPJ[0].cpfRepresentante);

                if (ws0911Socio.status == "OK")
                {
                    dados09 = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                }
                else
                {
                    throw new Exception("Erro ao tentar fados do Responavel perante ao cnpj cpf " + dados.dadosCNPJ[0].cpfRepresentante + " no ws09 na dll psc.ruc.tabelas.dall");
                }

                if (ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0] != null &&
                                        ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco != null)
                {
                    psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                    cc.Bairro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.bairro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.bairro;
                    cc.Cep = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.cep == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.cep;
                    cc.Codigo_municipio = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codMunicipio == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codMunicipio;
                    cc.Complemento = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.complementoLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.complementoLogradouro;
                    cc.Logradouro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.logradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.logradouro;
                    cc.Numero = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.numLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.numLogradouro;
                    cc.Pais = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codPais == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codPais;
                    cc.TipLogradoro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codTipoLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codTipoLogradouro;
                    cc.Uf = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.uf == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.uf;

                    cc.TrataEndereco(ref cc, DtTipoLogra);

                    repre.rsr_nomb = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].nome;


                    repre.rsr_direccion = cc.Logradouro;
                    repre.rsr_nume = cc.Numero;
                    repre.rsr_tge_vpais = cc.Pais;
                    repre.rsr_ttl_tip_logradoro = cc.TipLogradoro;
                    repre.rsr_urbanizacion = cc.Bairro;
                    repre.rsr_tes_cod_estado = cc.Uf;
                    repre.rsr_zona_postal = cc.Cep;
                    repre.rsr_ident_comp = cc.Complemento;
                    repre.rsr_tmu_cod_mun = cc.Codigo_municipio;
                    repre.rsr_tge_vpais = cc.Pais;
                }
                repre.Update(bd);
            }

            
            

            #endregion
        }

        #region RUC_CARD
        
        public void Update_RUC_CARD(OracleTransaction cp, string v_protocolo,
                           string v_cpfsolicitante,
                           string v_indqualifsolicitante,
                           string v_cnpj,
                           string v_ufselecionada,
                           string v_municipioselecionado,
                           string v_eventos,
                           string v_indinscrreativatualizestado,
                           string v_indsubsttrib,
                           string v_indccfe,
                           string v_indste,
                           string v_indcoe,
                           string v_indedgteee,
                           string v_inscrmunlocal,
                           string v_inscrmunvinc,
                           string v_inscroutromun,
                           string v_numviabilidade,
                           string v_inscricaomunicipal)
        {
            string Sql = "";

            using (OracleCommand cmdToExecuteSql = new OracleCommand())
            {
                cmdToExecuteSql.Transaction = cp;
                cmdToExecuteSql.Connection = cp.Connection;

                cmdToExecuteSql.Parameters.Clear();
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_protocolo));
         

                Sql = @"Delete   ruc_card_rfb p
                        where    protocolo = :v_protocolo";

                cmdToExecuteSql.CommandText = Sql.ToString();
                cmdToExecuteSql.CommandType = CommandType.Text;
                cmdToExecuteSql.ExecuteNonQuery();


                Sql = @"insert into ruc_card_rfb
                          (protocolo,
                           cpfsolicitante,
                           indqualifsolicitante,
                           cnpj,
                           ufselecionada,
                           municipioselecionado,
                           eventos,
                           indinscrreativatualizestado,
                           indsubsttrib,
                           indccfe,
                           indste,
                           indcoe,
                           indedgteee,
                           inscrmunlocal,
                           inscrmunvinc,
                           inscroutromun,
                           numviabilidade,
                           inscricaomunicipal)
                        values
                          (:v_protocolo,
                           :v_cpfsolicitante,
                           :v_indqualifsolicitante,
                           :v_cnpj,
                           :v_ufselecionada,
                           :v_municipioselecionado,
                           :v_eventos,
                           :v_indinscrreativatualizestado,
                           :v_indsubsttrib,
                           :v_indccfe,
                           :v_indste,
                           :v_indcoe,
                           :v_indedgteee,
                           :v_inscrmunlocal,
                           :v_inscrmunvinc,
                           :v_inscroutromun,
                           :v_numviabilidade,
                           :v_inscricaomunicipal)";

                cmdToExecuteSql.Parameters.Clear();
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_protocolo));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_cpfsolicitante", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_cpfsolicitante));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_indqualifsolicitante", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_indqualifsolicitante));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_cnpj", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_cnpj));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_ufselecionada", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_ufselecionada));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_municipioselecionado", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_municipioselecionado));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_eventos", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_eventos));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_indinscrreativatualizestado", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_indinscrreativatualizestado));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_indsubsttrib", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_indsubsttrib));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_indccfe", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_indccfe));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_indste", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_indste));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_indcoe", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_indcoe));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_indedgteee", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_indedgteee));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_inscrmunlocal", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_inscrmunlocal));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_inscrmunvinc", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_inscrmunvinc));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_inscroutromun", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_inscroutromun));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_numviabilidade", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_numviabilidade));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_inscricaomunicipal", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_inscricaomunicipal));


                
                cmdToExecuteSql.CommandText = Sql.ToString();
                cmdToExecuteSql.CommandType = CommandType.Text;
                cmdToExecuteSql.ExecuteNonQuery();

                if (v_numviabilidade != "")
                {
                    cmdToExecuteSql.Parameters.Clear();
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_protocolo));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("v_numviabilidade", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_numviabilidade));


                    Sql = @"update  psc_protocolo p
                            set     p.pro_vpv_cod_protocolo = :v_numviabilidade
                           where    p.pro_protocolo = :v_protocolo";

                    cmdToExecuteSql.CommandText = Sql.ToString();
                    cmdToExecuteSql.CommandType = CommandType.Text;
                    cmdToExecuteSql.ExecuteNonQuery();

                }
            }
        }
        #endregion

        

    }
}

