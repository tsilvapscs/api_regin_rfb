using AcessoService;
using psc.Receita;
using psc.Receita.Entities;
using psc.Ruc.Tablelas.ConnectionBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Xml;
using WsRFBReginV2.Models;

namespace WsRFBReginV2
{
    /// <summary>
    /// Descrição resumida de ServicosConsultas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicosConsultas : System.Web.Services.WebService
    {

        public ServicosConsultas()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }
        #region ConsultaEmpresa

        [WebMethod]
        public Retorno ConsultaEmpresa(Entrada dadosEntrada)
        {
            Retorno resul = new Retorno();

            WsServicesReginRFB.Retorno resulWsRegin = new WsServicesReginRFB.Retorno();


            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            string ipComputer = HttpContext.Current.Request.UserHostAddress;

            //bool ipValido = true;
            //if (ConfigurationManager.AppSettings["IpsAutorizados"] != null && ConfigurationManager.AppSettings["IpsAutorizados"].ToString() != "")
            //{
            //    ipValido = false;
            //    string[] IpsAutorizados = ConfigurationManager.AppSettings["IpsAutorizados"].ToString().Split('-');

            //    foreach (string ip in IpsAutorizados)
            //    {
            //        if (ip == ipComputer)
            //        {
            //            ipValido = true;
            //            break;
            //        }
            //    }
            //}

            //if (!ipValido)
            //{
            //    resul.codretorno = "90";
            //    resul.status = "NOK";
            //    resul.descricao = "Ip não valido para acesso ao Ws " + ipComputer;
            //    return resul;
            //}

            if (dadosEntrada.CnpjOrgaoConsulta == "")
            {
                resul.codretorno = "90";
                resul.status = "NOK";
                resul.descricao = "CNPJ do Orgao de registro Inválido " + dadosEntrada.CnpjOrgaoConsulta;

                return resul;
            }

            if (dadosEntrada.CpfCnpj == "")
            {
                resul.codretorno = "90";
                resul.status = "NOK";
                resul.descricao = "CNPJ da empresa Inválido " + dadosEntrada.CpfCnpj;
                return resul;
            }




            string informacao = ipComputer;

            string retorno = "";
            retorno += " DADOS DO CERTIFICADO " + Environment.NewLine;

            retorno += "ipComputer = " + ipComputer + Environment.NewLine;
            retorno += "CnpjOrgaoConsulta = " + dadosEntrada.CnpjOrgaoConsulta + Environment.NewLine;
            retorno += "Senha = " + dadosEntrada.Senha + Environment.NewLine;
            retorno += "Usuario = " + dadosEntrada.Usuario + Environment.NewLine;
            retorno += "CpfCnpj = " + dadosEntrada.CpfCnpj + Environment.NewLine;

            Random random = new Random();
            int arquivo = random.Next(201, 400);

            GlobalV1.salvaDados(retorno, arquivo.ToString() + ".txt");


            WsServicesReginRFB.ServiceReginRFB c = new WsServicesReginRFB.ServiceReginRFB();

            c.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

            resulWsRegin = c.ServiceWs11(dadosEntrada.CpfCnpj);

            resul.Cnpj = resulWsRegin.Cnpj;
            resul.codretorno = resulWsRegin.codretorno;
            resul.descricao = resulWsRegin.descricao;
            resul.Nire = resulWsRegin.Nire;
            resul.status = resulWsRegin.status;
            resul.XmlDBE = resulWsRegin.XmlDBE;

            resul.oCNPJResponse = null;
            resul.oCPFResponse = null;
            resul.oWs35Response = null;

            using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
            {
                e35.t73307_ide_solicitacao = dadosEntrada.CnpjOrgaoConsulta;
                e35.t73307_rec_solicitacao = dadosEntrada.CpfCnpj;
                e35.t73307_arquivo_RFB = ipComputer;
                e35.UpdateConsultaEmpresa();
            }

            return resul;


        }

        #endregion

        #region Consulta Pessoa Fisica

        [WebMethod]
        public Retorno ConsultaPessoaFisica(Entrada dadosEntrada)
        {
            Retorno resul = new Retorno();

            WsServicesReginRFB.Retorno resulWsRegin = new WsServicesReginRFB.Retorno();

            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            string ipComputer = HttpContext.Current.Request.UserHostAddress;

            //bool ipValido = true;
            //if (ConfigurationManager.AppSettings["IpsAutorizados"] != null && ConfigurationManager.AppSettings["IpsAutorizados"].ToString() != "")
            //{
            //    ipValido = false;
            //    string[] IpsAutorizados = ConfigurationManager.AppSettings["IpsAutorizados"].ToString().Split('-');

            //    foreach (string ip in IpsAutorizados)
            //    {
            //        if (ip == ipComputer)
            //        {
            //            ipValido = true;
            //            break;
            //        }
            //    }
            //}

            //if (!ipValido)
            //{
            //    resul.codretorno = "90";
            //    resul.status = "NOK";
            //    resul.descricao = "Ip não valido para acesso ao Ws " + ipComputer;
            //    return resul;
            //}

            if (dadosEntrada.CnpjOrgaoConsulta == "")
            {
                resul.codretorno = "90";
                resul.status = "NOK";
                resul.descricao = "CNPJ do Orgao de registro Inválido " + dadosEntrada.CnpjOrgaoConsulta;

                return resul;
            }

            if (dadosEntrada.CpfCnpj == "")
            {
                resul.codretorno = "90";
                resul.status = "NOK";
                resul.descricao = "CNPJ da empresa Inválido " + dadosEntrada.CpfCnpj;
                return resul;
            }




            string informacao = ipComputer;

            string retorno = "";
            retorno += " DADOS DO CERTIFICADO " + Environment.NewLine;

            retorno += "ipComputer = " + ipComputer + Environment.NewLine;
            retorno += "CnpjOrgaoConsulta = " + dadosEntrada.CnpjOrgaoConsulta + Environment.NewLine;
            retorno += "Senha = " + dadosEntrada.Senha + Environment.NewLine;
            retorno += "Usuario = " + dadosEntrada.Usuario + Environment.NewLine;
            retorno += "CpfCnpj = " + dadosEntrada.CpfCnpj + Environment.NewLine;

            Random random = new Random();
            int arquivo = random.Next(201, 400);

            GlobalV1.salvaDados(retorno, arquivo.ToString() + ".txt");


            WsServicesReginRFB.ServiceReginRFB c = new WsServicesReginRFB.ServiceReginRFB();

            c.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

            resulWsRegin = c.ServiceWs09(dadosEntrada.CpfCnpj);

            resul.Cnpj = dadosEntrada.CpfCnpj;
            resul.codretorno = resulWsRegin.codretorno;
            resul.descricao = resulWsRegin.descricao;
            resul.Nire = resulWsRegin.Nire;
            resul.status = resulWsRegin.status;
            resul.XmlDBE = resulWsRegin.XmlDBE;

            resul.oCNPJResponse = null;
            resul.oCPFResponse = null;
            resul.oWs35Response = null;

            using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
            {
                e35.t73307_ide_solicitacao = dadosEntrada.CnpjOrgaoConsulta;
                e35.t73307_rec_solicitacao = dadosEntrada.CpfCnpj;
                e35.t73307_arquivo_RFB = ipComputer;
                e35.UpdateConsultaEmpresa();
            }

            return resul;


        }

        #endregion

        #region Valida XML

        private void adicionaErroNaLista(PSC_CONTROL_DETALHE erro, ref List<PSC_CONTROL_DETALHE> _listaErro)
        {
            PSC_CONTROL_DETALHE erroNovo = new PSC_CONTROL_DETALHE();

            erroNovo.pcd_motivo = erro.pcd_motivo;
            erroNovo.pcd_pcc_id = erro.pcd_pcc_id;
            erroNovo.pcq_cnpj = erro.pcq_cnpj;
            erroNovo.pcq_cnpj_or = erro.pcq_cnpj_or;
            erroNovo.pcq_nire = erro.pcq_nire;
            erroNovo.pcq_protocolo = erro.pcq_protocolo;

            _listaErro.Add(erroNovo);
        }

        [WebMethod]
        public Retorno ValidaXMLRegin(string pXmlIn, string pProtocolo, string tipoDeProtocolo, string CNPJOrgaoRegistro)
        {
            Retorno resul = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                if (tipoDeProtocolo != "1" && tipoDeProtocolo != "2" && tipoDeProtocolo != "51")
                {
                    if (CNPJOrgaoRegistro != "10054583000197")
                    {
                        resul.status = "OK";
                        return resul;
                    }
                }

                //So para validar tipo 51 em PE
                if (CNPJOrgaoRegistro != "10054583000197" && tipoDeProtocolo == "51")
                {
                    resul.status = "OK";
                    return resul;
                }

                //return resul;

                List<PSC_CONTROL_DETALHE> _listaErro = new List<PSC_CONTROL_DETALHE>();
                PSC_CONTROL_DETALHE erro = new PSC_CONTROL_DETALHE();

                erro.pcq_protocolo = pProtocolo;
                erro.pcq_cnpj_or = CNPJOrgaoRegistro;

                WsServicesReginRFB.Retorno result11 = new WsServicesReginRFB.Retorno();

                string ipComputer = HttpContext.Current.Request.UserHostAddress;

                #region Valida IP
                bool ipValido = true;
                if (ConfigurationManager.AppSettings["IpsAutorizadosValidaRegin"] != null && ConfigurationManager.AppSettings["IpsAutorizadosValidaRegin"].ToString() != "")
                {
                    ipValido = false;
                    string[] IpsAutorizados = ConfigurationManager.AppSettings["IpsAutorizadosValidaRegin"].ToString().Split('-');

                    foreach (string ip in IpsAutorizados)
                    {
                        if (ip == ipComputer)
                        {
                            ipValido = true;
                            break;
                        }
                    }
                }
                #endregion

                bParametroValidaXml _parametro = new bParametroValidaXml(CNPJOrgaoRegistro);

                if (_parametro.ListaParam.Count == 0)
                {
                    resul.status = "OK";
                    resul.codretorno = "03";
                    resul.descricao = "Nao se encontraram validações a fazer para esse Orgao de Registro";
                    return resul;
                }


                WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();
                WsServicesReginRFB.dadosCNPJ dados = new WsServicesReginRFB.dadosCNPJ();


                XmlDocument doc = new XmlDocument();
                doc.Load(new StringReader(pXmlIn));

                //XmlNode nodePSC_PROTOCOLO = doc.SelectNodes("//Ruc//ROWSET//PSC_PROTOCOLO");

                erro.pcq_nire = doc.SelectSingleNode("//Ruc//ROWSET//PSC_PROTOCOLO")["NIRE"].InnerText.Trim().ToLower();
                erro.pcq_cnpj = doc.SelectSingleNode("//Ruc//ROWSET//PSC_PROTOCOLO")["CNPJ"].InnerText.Trim().ToLower();

                string urlWs11 = dHelperQuery.getUrlOR(CNPJOrgaoRegistro);

                regin.Url = urlWs11;

                result11 = regin.ServiceWs11(erro.pcq_cnpj);
                if (result11.status != "OK")
                {
                    resul.status = result11.status;
                    resul.descricao = result11.descricao;
                    return resul;
                }

                string nomeempresaXml = doc.SelectSingleNode("//Ruc//ROWSET//RUC_GENERAL")["RGE_NOMB"].InnerText.Trim().ToUpper();

                string pValorMensagem = "";
                dados = result11.oCNPJResponse.dadosCNPJ[0];

                #region SE O NIRE DE FILIAL NA JUNTA E NA RFB E UMA MATRIZ
                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_TIPO_MATRIZ_FILIAL_RFB_JUNTA);
                if (pValorMensagem != "")
                {
                    string dgNire = erro.pcq_nire.Substring(2, 1);
                    if (dgNire == "9" && dados.indMatrizFilial == "1")
                    {
                        erro.pcd_motivo = "Nire e de filial " + erro.pcq_nire + " E o CNPJ da empresa na RFB e uma Matriz";
                        erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_TIPO_MATRIZ_FILIAL_RFB_JUNTA;
                        adicionaErroNaLista(erro, ref _listaErro);
                    }

                    if (dgNire != "9" && dados.indMatrizFilial != "1")
                    {
                        erro.pcd_motivo = "Nire e de Matriz " + erro.pcq_nire + " E o CNPJ da empresa na RFB e uma Filial";
                        erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_TIPO_MATRIZ_FILIAL_RFB_JUNTA;
                        adicionaErroNaLista(erro, ref _listaErro);
                    }

                }

                #endregion

                #region Valida PORTE EMPRESA
                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_EMPRESA_PORTE);
                if (pValorMensagem != "")
                {

                    string[] tbPorte = new string[3] { "ME", "EPP", "NORMAL" };

                    string VariavelXML = doc.SelectSingleNode("//Ruc//ROWSET//RUC_GENERAL")["RGE_TGE_VTAMANHO"].InnerText.Trim().ToUpper();
                    string VariavelRFB = dados.porte;

                    if (VariavelXML != "1" && VariavelXML != "2")
                    {
                        VariavelXML = "3"; //Normal
                    }

                    if (VariavelRFB != "01" && VariavelRFB != "03")
                    {
                        VariavelRFB = "3"; //Normal
                    }
                    if (VariavelRFB == "01")
                    {
                        VariavelRFB = "1"; // ME
                    }
                    if (VariavelRFB == "03")
                    {
                        VariavelRFB = "2"; //EPP
                    }

                    if (VariavelXML != VariavelRFB)
                    {
                        erro.pcd_motivo = pValorMensagem;
                        erro.pcd_motivo = "Porte: " + tbPorte[int.Parse(VariavelXML) - 1] + " Diferente á RFB: " + tbPorte[int.Parse(VariavelRFB) - 1];
                        erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_EMPRESA_PORTE;
                        adicionaErroNaLista(erro, ref _listaErro);
                    }
                }

                #endregion

                #region Valida ENDEREÇO EMPRESA
                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_ENDERECO_EMPRESA_MUNICIPIO);
                if (pValorMensagem != "")
                {
                    string CodMunicipioXML = doc.SelectSingleNode("//Ruc//ROWSET//RUC_ESTAB")["RES_TMU_COD_MUN"].InnerText.Trim().ToUpper();
                    string CodMunicipioRFB = dados.endereco.codMunicipio;

                    if (CodMunicipioRFB.Length > 1)
                    {
                        CodMunicipioRFB = CodMunicipioRFB + psc.Framework.General.CalculateVerificationDigit(CodMunicipioRFB, 11).ToString();
                    }

                    if (int.Parse(CodMunicipioXML) != int.Parse(CodMunicipioRFB))
                    {
                        erro.pcd_motivo = pValorMensagem;
                        erro.pcd_motivo = "Municipio: " + CodMunicipioXML + " Diferente á RFB: " + CodMunicipioRFB;
                        erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_ENDERECO_EMPRESA_MUNICIPIO;
                        adicionaErroNaLista(erro, ref _listaErro);
                    }
                }

                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_ENDERECO_EMPRESA_BAIRRO);
                if (pValorMensagem != "")
                {
                    string BairroXML = doc.SelectSingleNode("//Ruc//ROWSET//RUC_ESTAB")["RES_URBANIZACION"].InnerText.Trim().ToUpper();
                    BairroXML = DadosViabilidade.TiraAcentoNomeEmpresarial(BairroXML, "");
                    BairroXML = retiraCarateresEspeciais(BairroXML);
                    BairroXML = retProt(BairroXML);
                    BairroXML = TiraCaracteres(BairroXML);
                    string BairroRFB = "";
                    if (dados.endereco != null && dados.endereco.bairro != null)
                    {
                        BairroRFB = DadosViabilidade.TiraAcentoNomeEmpresarial(dados.endereco.bairro, "");
                    }
                    BairroRFB = BairroRFB.Trim().ToUpper();
                    BairroRFB = retiraCarateresEspeciais(BairroRFB);
                    BairroRFB = retProt(BairroRFB);
                    BairroRFB = TiraCaracteres(BairroRFB);

                    if (BairroXML != BairroRFB)
                    {
                        erro.pcd_motivo = pValorMensagem;
                        erro.pcd_motivo = "Bairro: " + BairroXML + " Diferente á RFB: " + BairroRFB;
                        erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_ENDERECO_EMPRESA_BAIRRO;
                        adicionaErroNaLista(erro, ref _listaErro);
                    }
                }

                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_ENDERECO_EMPRESA_LOGRADOURO);
                if (pValorMensagem != "")
                {
                    string VariavelXML = doc.SelectSingleNode("//Ruc//ROWSET//RUC_ESTAB")["RES_DIRECCION"].InnerText.Trim().ToUpper();
                    VariavelXML = DadosViabilidade.TiraAcentoNomeEmpresarial(VariavelXML, "");
                    VariavelXML = retiraCarateresEspeciais(VariavelXML);
                    VariavelXML = retProt(VariavelXML);
                    VariavelXML = TiraCaracteres(VariavelXML);


                    string VariavelRFB = DadosViabilidade.TiraAcentoNomeEmpresarial(dados.endereco.logradouro, "");
                    VariavelRFB = retiraCarateresEspeciais(VariavelRFB);
                    VariavelRFB = retProt(VariavelRFB);
                    VariavelRFB = TiraCaracteres(VariavelRFB);


                    if (VariavelXML != VariavelRFB)
                    {
                        erro.pcd_motivo = pValorMensagem;
                        erro.pcd_motivo = "Logradouro: " + VariavelXML + " Diferente á RFB: " + VariavelRFB;
                        erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_ENDERECO_EMPRESA_LOGRADOURO;
                        adicionaErroNaLista(erro, ref _listaErro);
                    }
                }

                #endregion

                #region Compara QSA com RFB
                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_CPFCNPJ_QSA);
                if (pValorMensagem != "")
                {
                    bool CpfCnpjOk = false;
                    string CpfCnpjRegin = "";
                    string CpfCnpjRFB = "";

                    #region Valida QSA Orgao de Registro com RFB
                    foreach (XmlNode nodeQsa in doc.SelectNodes("//Ruc//ROWSET//RUC_RELAT_PROF"))
                    {
                        string TipoRelat = nodeQsa["RRP_TGE_VTIP_RELAC"].InnerText.Trim().ToLower();
                        if (TipoRelat == "2")
                        {
                            CpfCnpjOk = false;
                            CpfCnpjRegin = nodeQsa["RRP_CGC_CPF_SECD"].InnerText.Trim().ToLower();
                            CpfCnpjRFB = "";
                            if (dados.naturezaJuridica == "2135")
                            {
                                CpfCnpjRFB = dados.cpfRepresentante.Trim().ToLower();

                                if (CpfCnpjRegin == CpfCnpjRFB)
                                {
                                    CpfCnpjOk = true;
                                    break;
                                }
                            }
                            if (dados.dadosSocio != null)
                            {
                                foreach (WsServicesReginRFB.dadosSocio socio in dados.dadosSocio)
                                {
                                    CpfCnpjRFB = socio.cpfCnpj;

                                    if (CpfCnpjRegin == CpfCnpjRFB)
                                    {
                                        CpfCnpjOk = true;
                                        break;
                                    }
                                }
                            }

                            if (!CpfCnpjOk)
                            {
                                erro.pcd_motivo = "QSA Junta " + CpfCnpjRegin + " não existe na RFB";
                                erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_CPFCNPJ_QSA;
                                adicionaErroNaLista(erro, ref _listaErro);
                                CpfCnpjOk = false;
                            }
                        }
                    }
                    #endregion

                    #region Valida QSA RFB com QSA Orgao de Registro

                    CpfCnpjOk = false;
                    CpfCnpjRegin = "";
                    CpfCnpjRFB = "";
                    if (dados.naturezaJuridica == "2135")
                    {
                        CpfCnpjRFB = dados.cpfRepresentante.Trim().ToLower();

                        foreach (XmlNode nodeQsa in doc.SelectNodes("//Ruc//ROWSET//RUC_RELAT_PROF"))
                        {
                            string TipoRelat = nodeQsa["RRP_TGE_VTIP_RELAC"].InnerText.Trim().ToLower();
                            if (TipoRelat == "2")
                            {
                                CpfCnpjRegin = nodeQsa["RRP_CGC_CPF_SECD"].InnerText.Trim().ToLower();
                                if (CpfCnpjRegin == CpfCnpjRFB)
                                {
                                    CpfCnpjOk = true;
                                    break;
                                }
                            }
                        }
                        if (!CpfCnpjOk)
                        {
                            erro.pcd_motivo = "QSA RFB " + CpfCnpjRFB + " não existe na Junta";
                            erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_CPFCNPJ_QSA;
                            adicionaErroNaLista(erro, ref _listaErro);
                            CpfCnpjOk = false;
                        }

                    }
                    if (dados.dadosSocio != null)
                    {
                        foreach (WsServicesReginRFB.dadosSocio socio in dados.dadosSocio)
                        {
                            CpfCnpjRFB = socio.cpfCnpj;
                            CpfCnpjOk = false;
                            foreach (XmlNode nodeQsa in doc.SelectNodes("//Ruc//ROWSET//RUC_RELAT_PROF"))
                            {
                                string TipoRelat = nodeQsa["RRP_TGE_VTIP_RELAC"].InnerText.Trim().ToLower();
                                if (TipoRelat == "2")
                                {
                                    CpfCnpjRegin = nodeQsa["RRP_CGC_CPF_SECD"].InnerText.Trim().ToLower();
                                    if (CpfCnpjRegin == CpfCnpjRFB)
                                    {
                                        CpfCnpjOk = true;
                                        break;
                                    }
                                }
                            }

                            if (!CpfCnpjOk)
                            {
                                erro.pcd_motivo = "QSA RFB " + CpfCnpjRFB + " não existe na Junta";
                                erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_CPFCNPJ_QSA;
                                adicionaErroNaLista(erro, ref _listaErro);
                                CpfCnpjOk = false;
                            }
                        }
                    }



                    #endregion
                }
                #endregion

                #region Valida NomeEmpresarial
                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_NOME_EMPRESARIAL);
                if (pValorMensagem != "" && dados.indMatrizFilial == "1")
                {
                    string nomeempresaXmlSemAcento = DadosViabilidade.TiraAcentoNomeEmpresarial(nomeempresaXml, "");
                    nomeempresaXmlSemAcento = RetiraTipoEnquadramento(nomeempresaXmlSemAcento);

                    string nomeempresaRFB = DadosViabilidade.TiraAcentoNomeEmpresarial(dados.nomeEmpresarial, "");
                    nomeempresaRFB = RetiraTipoEnquadramento(nomeempresaRFB);

                    if (nomeempresaXmlSemAcento != nomeempresaRFB)
                    {
                        erro.pcd_motivo = pValorMensagem;
                        erro.pcd_motivo = "Nome: " + nomeempresaXmlSemAcento + " Diferente á RFB: " + nomeempresaRFB;
                        erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_NOME_EMPRESARIAL;
                        adicionaErroNaLista(erro, ref _listaErro);
                    }
                }
                #endregion

                #region Valida Natureza Juridica
                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_NATUREZA_JURIDICA);
                if (pValorMensagem != "" && dados.indMatrizFilial == "1")
                {
                    string pCodNaturezaXmlRegin = doc.SelectSingleNode("//Ruc//ROWSET//RUC_COMP")["RCO_TNC_COD_NATUR"].InnerText.Trim().ToUpper();

                    string pCodNaturezaXmlRFB = dados.naturezaJuridica.ToUpper().Trim();


                    if (pCodNaturezaXmlRegin != pCodNaturezaXmlRFB)
                    {
                        erro.pcd_motivo = pValorMensagem;
                        erro.pcd_motivo = "Natureza: " + pCodNaturezaXmlRegin + " Diferente á RFB: " + pCodNaturezaXmlRFB;
                        erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_NATUREZA_JURIDICA;
                        adicionaErroNaLista(erro, ref _listaErro);
                    }
                }
                #endregion

                #region Valida Empresa sem QSA
                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_QSA_SEM_SOCIO);
                if (pValorMensagem != "")
                {
                    bool encontroQsa = false;
                    foreach (XmlNode nodeQsa in doc.SelectNodes("//Ruc//ROWSET//RUC_RELAT_PROF"))
                    {
                        string TipoRelat = nodeQsa["RRP_TGE_VTIP_RELAC"].InnerText.Trim().ToLower();
                        if (TipoRelat == "2")
                        {
                            encontroQsa = true;
                            break;
                        }
                    }
                    if (!encontroQsa)
                    {
                        erro.pcd_motivo = pValorMensagem;
                        erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_QSA_SEM_SOCIO;
                        adicionaErroNaLista(erro, ref _listaErro);
                        //_listaErro.Add(erro);
                    }
                }
                #endregion

                #region VALIDA_CNAE_PRINCIPAL_DIFERENTE_RFB
                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_CNAE_PRINCIPAL_DIFERENTE_RFB);
                bool cnaeOk = false;
                if (pValorMensagem != "")
                {
                    string cnae = "";
                    foreach (XmlNode nodeCnae in doc.SelectNodes("//Ruc//ROWSET//RUC_ACTV_ECON"))
                    {
                        cnae = nodeCnae["RAE_TAE_COD_ACTVD"].InnerText.Trim().ToLower();
                        string tipo = nodeCnae["RAE_CALIF_ACTV"].InnerText.Trim().ToLower();
                        if (tipo == "1")
                        {
                            if (cnae == dados.cnaePrincipal)
                            {
                                cnaeOk = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            cnae = "";
                        }
                    }
                    if (!cnaeOk)
                    {
                        if (cnae == "")
                        {
                            erro.pcd_motivo = "Cnae principal Não existe no Orgao de Registro ";
                            erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_CNAE_PRINCIPAL_DIFERENTE_RFB;
                            adicionaErroNaLista(erro, ref _listaErro);
                        }
                        else
                        {
                            erro.pcd_motivo = "Cnae principal diferente da RFB: CNAE Junta " + cnae + " CNAE RFB " + dados.cnaePrincipal;
                            erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_CNAE_PRINCIPAL_DIFERENTE_RFB;
                            adicionaErroNaLista(erro, ref _listaErro);
                            //_listaErro.Add(erro);
                        }
                    }
                }
                #endregion

                #region VALIDA_CNAE_DIFERENTE_RFB_OR
                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_CNAE_DIFERENTE_RFB_OR);
                if (pValorMensagem != "")
                {
                    bool cnaeValida = true;
                    //Verifica Total de Cnae na segundaria na RFB
                    int TotalCnaeSecundariaRFB = 0;
                    if (dados.cnaeSecundaria != null)
                    {
                        foreach (string pCNAE in dados.cnaeSecundaria)
                        {
                            if (pCNAE != null && pCNAE != "0000000")
                            {
                                TotalCnaeSecundariaRFB++;
                            }
                        }
                    }
                    //Comentei para nao entrar ja que vou validar todas as cnaes indiferentemente se esta diferente o total ou nao
                    //if (TotalCnaeSecundariaRFB != doc.SelectNodes("//Ruc//ROWSET//RUC_ACTV_ECON").Count - 1)
                    //{
                    //    erro.pcd_motivo = "Quantidade de Cnae principal diferente entre entes";
                    //    erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_CNAE_DIFERENTE_RFB_OR;
                    //    adicionaErroNaLista(erro, ref _listaErro);
                    //    //_listaErro.Add(erro);
                    //    cnaeValida = false;
                    //}

                    #region valida Cnae segundaria de Junta com RFB
                    if (cnaeValida)
                    {
                        foreach (XmlNode nodeCnae in doc.SelectNodes("//Ruc//ROWSET//RUC_ACTV_ECON"))
                        {
                            string cnaeJunta = "";
                            cnaeOk = false;
                            cnaeJunta = nodeCnae["RAE_TAE_COD_ACTVD"].InnerText.Trim().ToLower();
                            string tipo = nodeCnae["RAE_CALIF_ACTV"].InnerText.Trim().ToLower();
                            if (dados.cnaeSecundaria != null)
                            {
                                foreach (string pCNAERFB in dados.cnaeSecundaria)
                                {
                                    if (pCNAERFB != null && pCNAERFB != "0000000")
                                    {
                                        if (cnaeJunta == pCNAERFB || cnaeJunta == dados.cnaePrincipal)
                                        {
                                            cnaeOk = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        cnaeOk = true;
                                        break;
                                    }
                                }
                            }
                            if (!cnaeOk)
                            {
                                //erro.pcd_motivo = pValorMensagem;
                                erro.pcd_motivo = "Cnae Junta " + cnaeJunta + " não existe na RFB";
                                erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_CNAE_DIFERENTE_RFB_OR;
                                adicionaErroNaLista(erro, ref _listaErro);
                                //_listaErro.Add(erro);
                                cnaeValida = false;
                                //break;
                            }
                        }

                    }
                    #endregion

                    #region Compara RFB com Junta
                    /*
                     * ams aqui nao chega porque se nao passa pelas duas validaçoes anteriores aqui nao chega
                       Agora compara RFB com relação a Junta
                     * tambem vou entrar porque to  validando mesmo que tenha erro de junta para rfb
                     * vou validar RFB para junta tb (if (cnaeValida || !cnaeValida))
                     */
                    if (cnaeValida || !cnaeValida)
                    {
                        string pCNAERFBMostra = "";
                        if (dados.cnaeSecundaria != null)
                        {
                            foreach (string pCNAERFB in dados.cnaeSecundaria)
                            {
                                pCNAERFBMostra = pCNAERFB;
                                cnaeOk = true;
                                if (pCNAERFB != null && pCNAERFB != "0000000")
                                {
                                    cnaeOk = false;
                                    foreach (XmlNode nodeCnae in doc.SelectNodes("//Ruc//ROWSET//RUC_ACTV_ECON"))
                                    {
                                        string cnaeJunta = "";
                                        cnaeOk = false;
                                        cnaeJunta = nodeCnae["RAE_TAE_COD_ACTVD"].InnerText.Trim().ToLower();
                                        if (cnaeJunta == pCNAERFB)
                                        {
                                            cnaeOk = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }

                                if (!cnaeOk)
                                {
                                    erro.pcd_motivo = "Cnae RFB " + pCNAERFBMostra + " não existe na Junta";
                                    erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_CNAE_DIFERENTE_RFB_OR;
                                    adicionaErroNaLista(erro, ref _listaErro);
                                    //_listaErro.Add(erro);
                                    cnaeValida = false;
                                    //break;
                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion

                #region Valida se precisa de Representanta
                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_QSA_SEM_REPRESENTANTE);
                if (pValorMensagem != "")
                {
                    bool encontrouRepresentante = false;
                    foreach (XmlNode nodeQsa in doc.SelectNodes("//Ruc//ROWSET//RUC_RELAT_PROF"))
                    {
                        string TipoRelat = nodeQsa["RRP_TGE_VTIP_RELAC"].InnerText.Trim().ToLower();
                        string CodQualificacao = nodeQsa["RRP_TGE_VCOD_QUAL"].InnerText.Trim().ToLower().Trim();
                        /*
                        Mastis 0002661: [JUCERJA - Controle de Qualidade] Enviar protocolos cuja qualificação do sócio PJ for Cotas de Tesouraria
                        Descrição
                        Quando o QSA da empresa conter "sócio PJ" com qualificação 63 - Cotas de Tesouraria, o sistema 
                         * não deverá incluir em pendência de validação pela falta de representante. 
                         * Essa qualificação não tem representação, nem aqui na Jucerja, nem na RFB.
                         * 
                         * dia 16/01/2019 email enviado pla Tatiana da JUCERJA
                         *  no sistema para não exigir representantes a exemplo do que ocorre na JUCERJA e Receita Federal
                         * 20 – Sociedade Consorciada
                         * 
                        */
                        if (TipoRelat == "2" && CodQualificacao != "63" && CodQualificacao != "20")
                        {
                            string CpfCnpjRucRelaProf = nodeQsa["RRP_CGC_CPF_SECD"].InnerText.Trim().ToLower();
                            foreach (XmlNode nodeProf in doc.SelectNodes("//Ruc//ROWSET//RUC_PROF"))
                            {
                                string DataNacimento = nodeProf["RPR_FEC_CONST_NASC"].InnerText.Trim().ToLower();
                                string CpfCnpjRucProf = nodeProf["RPR_CGC_CPF_SECD"].InnerText.Trim().ToLower();
                                string EMANCIPACAO = "";
                                if (nodeProf["RPR_EMANCIPACAO"] != null)
                                    EMANCIPACAO = GlobalV1.valNuloBranco(nodeProf["RPR_EMANCIPACAO"].InnerText.Trim().ToUpper());//.InnerText.Trim().ToLower(); 

                                encontrouRepresentante = false;
                                if (CpfCnpjRucProf == CpfCnpjRucRelaProf)
                                {
                                    if (ValidaPrecisaRepresentante(DataNacimento, EMANCIPACAO, CpfCnpjRucRelaProf))
                                    {
                                        foreach (XmlNode nodeRepre in doc.SelectNodes("//Ruc//ROWSET//RUC_REPRESENTANTES"))
                                        {
                                            string CpfCnpjQsaRucRepresentante = nodeRepre["RSR_CGC_CPF_PRINC"].InnerText.Trim().ToLower();
                                            if (CpfCnpjRucProf == CpfCnpjQsaRucRepresentante)
                                            {
                                                encontrouRepresentante = true;
                                                break;
                                            }
                                        }
                                        if (encontrouRepresentante)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            erro.pcd_motivo = pValorMensagem;
                                            erro.pcd_motivo = "QSA " + CpfCnpjRucRelaProf + " tem que ter um representante ";
                                            erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_QSA_SEM_REPRESENTANTE;
                                            adicionaErroNaLista(erro, ref _listaErro);
                                            //_listaErro.Add(erro);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion


                resul.XmlDBE += "<root>";
                resul.XmlDBE += "<atualizacaogrupo>";

                #region Valida CEP:  Empresa - QSA - Representante, Atualiza Cadastro
                pValorMensagem = _parametro.getValor(bParametroValidaXml.Valores.VALIDA_FAIXA_DE_CEP_ENDERECO);
                if (pValorMensagem != "")
                {

                    string cepEmpresa = doc.SelectSingleNode("//Ruc//ROWSET//RUC_ESTAB")["RES_ZONA_POSTAL"].InnerText.Trim().ToLower();
                    string cep = doc.SelectSingleNode("//Ruc//ROWSET//RUC_ESTAB")["RES_ZONA_POSTAL"].InnerText.Trim().ToLower();
                    string uf = doc.SelectSingleNode("//Ruc//ROWSET//RUC_ESTAB")["RES_TES_COD_ESTADO"].InnerText.Trim().ToUpper();
                    string CodMunc = doc.SelectSingleNode("//Ruc//ROWSET//RUC_ESTAB")["RES_TMU_COD_MUN"].InnerText.Trim().ToLower();

                    bool ValidaCep = dHelperQuery.ValidaCepFaixaMunicipio(uf, CodMunc, ref cep);
                    if (!ValidaCep)
                    {
                        resul.XmlDBE += "<atualizacao>";
                        resul.XmlDBE += "<tipo>cep</tipo>";
                        resul.XmlDBE += "<posicao>ruc_estab</posicao>";
                        resul.XmlDBE += "<valor>" + cep + "</valor>";
                        resul.XmlDBE += "<cpfcnpj></cpfcnpj>";
                        resul.XmlDBE += "</atualizacao>";

                        erro.pcd_motivo = "Cep da empresa " + cepEmpresa + " não esta na faixa de cep do municipio ";
                        erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_FAIXA_DE_CEP_ENDERECO;
                        adicionaErroNaLista(erro, ref _listaErro);
                        //_listaErro.Add(erro);
                    }

                    #region ValidaCepQSA
                    foreach (XmlNode nodeQsa in doc.SelectNodes("//Ruc//ROWSET//RUC_RELAT_PROF"))
                    {
                        string TipoRelat = nodeQsa["RRP_TGE_VTIP_RELAC"].InnerText.Trim().ToLower();
                        if (TipoRelat == "2")
                        {
                            string CpfCnpjRucRelaProf = nodeQsa["RRP_CGC_CPF_SECD"].InnerText.Trim().ToLower();
                            foreach (XmlNode nodeProf in doc.SelectNodes("//Ruc//ROWSET//RUC_PROF"))
                            {
                                string cepQSA = nodeProf["RPR_ZONA_POSTAL"].InnerText.Trim().ToLower();
                                cep = nodeProf["RPR_ZONA_POSTAL"].InnerText.Trim().ToLower();
                                string CpfCnpjRucProf = nodeProf["RPR_CGC_CPF_SECD"].InnerText.Trim().ToLower();
                                uf = nodeProf["RPR_TES_COD_ESTADO"].InnerText.Trim().ToUpper();
                                CodMunc = nodeProf["RPR_TMU_COD_MUN"].InnerText.Trim().ToLower();
                                if (CpfCnpjRucProf == CpfCnpjRucRelaProf)
                                {
                                    ValidaCep = dHelperQuery.ValidaCepFaixaMunicipio(uf, CodMunc, ref cep);
                                    if (!ValidaCep)
                                    {
                                        resul.XmlDBE += "<atualizacao>";
                                        resul.XmlDBE += "<tipo>cep</tipo>";
                                        resul.XmlDBE += "<posicao>ruc_prof</posicao>";
                                        resul.XmlDBE += "<cpfcnpj>" + CpfCnpjRucRelaProf + "</cpfcnpj>";
                                        resul.XmlDBE += "<valor>" + cep + "</valor>";
                                        resul.XmlDBE += "</atualizacao>";

                                        erro.pcd_motivo = "Cep " + cepQSA + " do QSA " + CpfCnpjRucProf + " não esta na faixa de cep do municipio ";
                                        erro.pcd_pcc_id = (decimal)bParametroValidaXml.Valores.VALIDA_FAIXA_DE_CEP_ENDERECO;
                                        adicionaErroNaLista(erro, ref _listaErro);
                                        //_listaErro.Add(erro);
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion
                string nroid = "0";
                #region atualiza control_detalhe com os dados das validaçoes
                if (_listaErro.Count > 0)
                {
                    using (ConnectionProviderORACLE cp = new ConnectionProviderORACLE())
                    {
                        cp.OpenConnection();
                        cp.BeginTransaction();

                        nroid = "0";
                        foreach (PSC_CONTROL_DETALHE t01 in _listaErro)
                        {
                            t01.MainConnectionProvider = cp;
                            t01.pcd_pcq_id = decimal.Parse(nroid);

                            nroid = t01.Update();
                        }

                        //cp.RollbackTransaction(""); //Commit Oracle
                        cp.CommitTransaction(); //Commit Oracle
                    }
                }
                #endregion

                resul.XmlDBE += "</atualizacaogrupo>";
                resul.XmlDBE += "</root>";

                if (_listaErro.Count > 0)
                {
                    resul.status = "NOK";
                    resul.descricao = "Validações pendentes ou dados faltantes no XML";
                }
                else
                {
                    resul.status = "OK";
                    resul.descricao = "";
                    dHelperQuery c = new dHelperQuery();
                    c.UpdateStatusControlQualidade(pProtocolo, erro.pcq_cnpj_or, "2");
                }

                //UpdateCamposTabelasRuc(resul.XmlDBE, pProtocolo);

                /*
                 <root>
      <atualizacaogrupo>
        <atualizacao>
          <tipo>cep</tipo>
          <posicao>empresa</posicao>
          <valor>22793395</valor>
        </atualizacao>
        <atualizacao>
          <tipo>cep</tipo>
          <posicao>qsa</posicao>
          <cpfcnpj>23131</cpfcnpj>
          <valor>22793395</valor>
        </atualizacao>
        <atualizacao>
          <tipo>cep</tipo>
          <posicao>representante</posicao>
          <cpfcnpjqsa>23131</cpfcnpjqsa>
          <cpfcnpjrepresentante>23131</cpfcnpjrepresentante>
          <valor>22793395</valor>
        </atualizacao>
      </atualizacaogrupo>
    </root>

                 */




                return resul;
            }
            catch (Exception ex)
            {
                resul.status = "NOK";
                resul.descricao = "Erro ao chamar ao ws de valida XML " + ex.Message;
                return resul;

            }

        }


        public string retProt(string valor)
        {
            string temp = valor;
            temp = temp.Replace("-", "");
            temp = temp.Replace("/", "");
            temp = temp.Replace("_", "");
            temp = temp.Replace(".", "");
            return temp;

        }
        public string TiraCaracteres(string valor)
        {
            string temp = valor;
            temp = temp.Replace(":", "");
            temp = temp.Replace(";", "");
            temp = temp.Replace(".", "");
            temp = temp.Replace(",", "");
            return temp;

        }

        private string retiraCarateresEspeciais(string pValue)
        {
            /*
            32   64 @  96 ` 162 ¢ 194 Â 226 â
            33 ! 65 A  97 a 163 £ 195 Ã 227 ã
            34 " 66 B  98 b 164 ¤ 196 Ä 228 ä
            35 # 67 C  99 c 165 ¥ 197 Å 229 å
            36 $ 68 D 100 d 166 ¦ 198 Æ 230 æ
            37 % 69 E 101 e 167 § 199 Ç 231 ç
            38 & 70 F 102 f 168 ¨ 200 È 232 è
            39 ' 71 G 103 g 169 © 201 É 233 é
            40 ( 72 H 104 h 170 ª 202 Ê 234 ê
            41 ) 73 I 105 i 171 « 203 Ë 235 ë
            42 * 74 J 106 j 172 ¬ 204 Ì 236 ì
            43 + 75 K 107 k 173 − 205 Í 237 í
            44 , 76 L 108 l 174 ® 206 Î 238 î
            45 − 77 M 109 m 175 ¯ 207 Ï 239 ï
            46 . 78 N 110 n 176 ° 208 Ð 240 ð
            47 / 79 O 111 o 177 ± 209 Ñ 241 ñ
            48 0 80 P 112 p 178 ² 210 Ò 242 ò
            49 1 81 Q 113 q 179 ³ 211 Ó 243 ó
            50 2 82 R 114 r 180 ´ 212 Ô 244 ô
            51 3 83 S 115 s 181 µ 213 Õ 245 õ
            52 4 84 T 116 t 182 ¶ 214 Ö 246 ö
            53 5 85 U 117 u 183 o 215 × 247 ÷
            54 6 86 V 118 v 184 ¸ 216 Ø 248 ø
            55 7 87 W 119 w 185 ¹ 217 Ù 249 ù
            56 8 88 X 120 x 186 º 218 Ú 250 ú
            57 9 89 Y 121 y 187 » 219 Û 251 û
            58 : 90 Z 122 z 188 ¼ 220 Ü 252 ü
            59 ; 91 [ 123 { 189 ½ 221 Ý 253 ý
            60 < 92 \ 124 | 190 ¾ 222 Þ 254 þ
            61 = 93 ] 125 } 191 ¿ 223 ß 255 ÿ
            62 > 94 ^ 126 ~ 192 À 224 à
            63 ? 95 _ 161 ¡ 193 Á 225 á 
           */

            string parametro = "[^ -;?-_a-zA-Z0-9¿-ÿ,={}!]";
            //txtResposta.Text = Regex.Replace(txtXml.Text, "[^0-9a-zA-Z.&,'@/ ]+", " ");
            return Regex.Replace(pValue, parametro, " ");
        }
        private string RetiraTipoEnquadramento(string wTexto)
        {
            string[] tbEnquadramento = new string[12] { " - ME ", " ME.", "- ME ", " -ME ", "-ME ", " ME ", " - EPP", "- EPP ", " -EPP ", "-EPP ", " EPP ", " EPP." };
            wTexto = wTexto.Trim();

            //if (!wTexto.Contains(" "))
            //{
            //    return wTexto;
            //}

            string wAux = wTexto + " ";
            string wEnquadramento = string.Empty;

            for (int wii = 0; wii <= tbEnquadramento.Length - 1; wii++)
            {
                wEnquadramento = tbEnquadramento[wii];
                if (wAux.Length > tbEnquadramento[wii].Length)
                {
                    int ii = wAux.IndexOf(tbEnquadramento[wii], wAux.Length - tbEnquadramento[wii].Length);
                    if (ii != -1)
                    {
                        wTexto = wTexto.Substring(0, wTexto.Length - tbEnquadramento[wii].Length + 1);
                        break;
                    }
                }
            }

            return wTexto;

        }

        private Boolean ValidaPrecisaRepresentante(string pDtNacimento, string Emancipado, string CPFCNPJ)
        {
            Boolean wChave = false;
            try
            {
                if (CPFCNPJ.Trim().Length == 14)
                {
                    return true;
                }

                /*
                    Aqui e porque o QSA e emancipado, com isso nao precisa de Representante
                 */
                if (Emancipado == "1" ||
                    Emancipado == "2" ||
                    Emancipado == "3" ||
                    Emancipado == "4" ||
                    Emancipado == "5" ||
                    Emancipado == "6" ||
                    Emancipado == "7"
                    )
                {
                    return false;
                }

                if (pDtNacimento == "")
                {
                    return false;
                }

                //string date = "20100102";
                DateTime dataNascimento = DateTime.ParseExact(pDtNacimento, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                TimeSpan diff;

                // Usando Subtract
                diff = DateTime.Now.Subtract(dataNascimento);

                if ((diff.Days / 365) < 18)
                    wChave = true;
                else
                    wChave = false;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar se Precisa Representante");
            }
            return wChave;
        }
        #endregion


    }
}
