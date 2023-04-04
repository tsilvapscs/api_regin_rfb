using psc.Receita.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using WsRFBReginV2.Models;

namespace WsRFBReginV2
{
    /// <summary>
    /// Descrição resumida de Service0506
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class Service0506 : System.Web.Services.WebService
    {

        public Service0506()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        #region ws05
        /// <summary>
        /// Codigo de retorno
        /// *** Os códigos de erro acima de "90" são relativos a procedimentos internos. Recebendo algum deles será necessário informar à RFB.
        /// Segue a lista de retorno do S05:
        /// * MENSAGENS DE RETORNO PROGRAMADA
        /// * 00 - 'SUCESSO'
        /// * 01 - 'REQUISICAO INVALIDA'
        /// * 02 - 'SOLICITACAO ESTA INDEFERIDA'
        /// * 03 - 'SOLICITACAO ESTA CANCELADA PELO CONTRIBUINTE' OU * 'SOLICITACAO ESTA CANCELADA DE OFICIO PELA RFB' OU * 'SOLICITACAO ESTA CANCELADA POR DECURSO DE PRAZO DE 90 DIAS'
        /// * 04 - 'SOLICITACAO JA ESTA DEFERIDA'
        /// * 05 - 'DBE AINDA NAO FOI DISPONIBILIZADO'
        /// * 06 - 'SOLICITACAO NAO E DE DEFERIMENTO DA JUNTA COMERCIAL'
        /// * 07 - EVENTO X NJ NAO PODE SER DEFERIDA PELA JUNTA COMERCIAL VIA WEBSERVICE
        /// * 08 - 'DBE INDISPONIBILIZADO'
        /// * 09 - 'SISTEMA FECHADO PARA ACEITACAO DA DOC - TENTE MAIS TARDE'
        /// * 10 - 'NUMERO DE SOLICITACAO INEXISTENTE'
        /// * 12 - 'DBE JA RECEPCIONADO PELO ORGAO DE REGISTRO'
        /// </summary>
        /// <param name="Identificacao"></param>
        /// <param name="Recibo"></param>
        /// <param name="cpfRecepcionador"></param>
        /// <returns></returns>
        [WebMethod(Description = @"Retorno status <br>
                                 OK -- Sucesso <br>
                                 NOK -- Sem Sucesso 
                        <br>Retorno codretorno: 
                        MENSAGENS DE RETORNO(codretorno) PROGRAMADA <br>
                        00 - 'SUCESSO' <br>
                        01 - 'REQUISICAO INVALIDA' <br>
                        02 - 'SOLICITACAO ESTA INDEFERIDA' <br>
                        03 - 'SOLICITACAO ESTA CANCELADA PELO CONTRIBUINTE' OU * 'SOLICITACAO ESTA CANCELADA DE OFICIO PELA RFB' OU * 'SOLICITACAO ESTA CANCELADA POR DECURSO DE PRAZO DE 90 DIAS' <br>
                        04 - 'SOLICITACAO JA ESTA DEFERIDA' <br>
                        05 - 'DBE AINDA NAO FOI DISPONIBILIZADO' <br>
                        06 - 'SOLICITACAO NAO E DE DEFERIMENTO DA JUNTA COMERCIAL' <br>
                        07 - EVENTO X NJ NAO PODE SER DEFERIDA PELA JUNTA COMERCIAL VIA WEBSERVICE <br>
                        08 - 'DBE INDISPONIBILIZADO' <br>
                        09 - 'SISTEMA FECHADO PARA ACEITACAO DA DOC - TENTE MAIS TARDE' <br>
                        10 - 'NUMERO DE SOLICITACAO INEXISTENTE' <br>
                        12 - 'DBE JA RECEPCIONADO PELO ORGAO DE REGISTRO' <br>
                        99 - Outros Erros")]
        public Retorno ServiceWs05(string Identificacao, string Recibo, string cpfRecepcionador, string numeroServentia, string cnpjOrgaoRegistro)
        {
            Retorno result = new Retorno();
            WsServicesReginRFB.Retorno resultIn = new WsServicesReginRFB.Retorno();

            try
            {

                GlobalV1.ValidaReques();

                cpfRecepcionador = cpfRecepcionador.Trim();
                Identificacao = Identificacao.Trim();
                Recibo = Recibo.Trim();
                cpfRecepcionador = cpfRecepcionador.Trim();
                numeroServentia = numeroServentia.Trim();
                cnpjOrgaoRegistro = cnpjOrgaoRegistro.Trim();

                string val = "";

                if (Recibo == null || Recibo.Length != 10)
                {
                    val = "Nulo";
                    if (Recibo != null)
                        val = Recibo;

                    throw new Exception("Recibo Invalido: " + val);
                }

                if (Identificacao == null || Identificacao.Length != 14)
                {
                    val = "Nulo";
                    if (Identificacao != null)
                        val = Identificacao;

                    throw new Exception("Identificacao Invalido: " + val);
                }

                if (cpfRecepcionador == null || cpfRecepcionador.Length != 11)
                {
                    val = "Nulo";
                    if (cpfRecepcionador != null)
                        val = cpfRecepcionador;

                    throw new Exception("cpfRecepcionador Invalido: " + val);
                }

                if (cnpjOrgaoRegistro == null || cnpjOrgaoRegistro.Length != 14)
                {
                    val = "Nulo";
                    if (cnpjOrgaoRegistro != null)
                        val = cnpjOrgaoRegistro;
                    throw new Exception("cnpjOrgaoRegistro Invalido: " + val);
                }

                WsServicesReginRFB.ServiceReginRFB se = new WsServicesReginRFB.ServiceReginRFB();

                se.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                resultIn = se.ServiceWs05V2(Identificacao, Recibo, cpfRecepcionador, numeroServentia, cnpjOrgaoRegistro);

                result.Cnpj = resultIn.Cnpj;
                result.codretorno = resultIn.codretorno;
                result.descricao = resultIn.descricao;
                result.Nire = resultIn.Nire;
                result.status = resultIn.status;

            }
            catch (Exception ex)
            {
                string XmlDados = GlobalV1.CreateXML(result);
                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = XmlDados;
                    e35.t73307_erro = ex.Message;
                    e35.t73307_ide_solicitacao = Identificacao;
                    e35.t73307_rec_solicitacao = "WS05EXT";
                    e35.Update();
                }

                result.status = "NOK";
                result.codretorno = "99";
                result.descricao = ex.Message;
            }
            return result;
        }
        [WebMethod(Description = @"Retorno status <br>
                                 OK -- Sucesso <br>
                                 NOK -- Sem Sucesso 
                        <br>Retorno codretorno: 
                        MENSAGENS DE RETORNO(codretorno) PROGRAMADA <br>
                        00 - 'SUCESSO' <br>
                        01 - 'REQUISICAO INVALIDA' <br>
                        02 - 'SOLICITACAO ESTA INDEFERIDA' <br>
                        03 - 'SOLICITACAO ESTA CANCELADA PELO CONTRIBUINTE' OU * 'SOLICITACAO ESTA CANCELADA DE OFICIO PELA RFB' OU * 'SOLICITACAO ESTA CANCELADA POR DECURSO DE PRAZO DE 90 DIAS' <br>
                        04 - 'SOLICITACAO JA ESTA DEFERIDA' <br>
                        05 - 'DBE AINDA NAO FOI DISPONIBILIZADO' <br>
                        06 - 'SOLICITACAO NAO E DE DEFERIMENTO DA JUNTA COMERCIAL' <br>
                        07 - EVENTO X NJ NAO PODE SER DEFERIDA PELA JUNTA COMERCIAL VIA WEBSERVICE <br>
                        08 - 'DBE INDISPONIBILIZADO' <br>
                        09 - 'SISTEMA FECHADO PARA ACEITACAO DA DOC - TENTE MAIS TARDE' <br>
                        10 - 'NUMERO DE SOLICITACAO INEXISTENTE' <br>
                        12 - 'DBE JA RECEPCIONADO PELO ORGAO DE REGISTRO' <br>
                        99 - Outros Erros")]
        public Retorno ServiceWs05EX(string Identificacao, string Recibo, string cpfRecepcionador, string numeroServentia, string cnpjOrgaoRegistro)
        {
            Retorno result = new Retorno();
            WsServicesReginRFB.Retorno resultIn = new WsServicesReginRFB.Retorno();

            try
            {

                GlobalV1.ValidaReques();

                cpfRecepcionador = cpfRecepcionador.Trim();
                Identificacao = Identificacao.Trim();
                Recibo = Recibo.Trim();
                cpfRecepcionador = cpfRecepcionador.Trim();
                numeroServentia = numeroServentia.Trim();
                cnpjOrgaoRegistro = cnpjOrgaoRegistro.Trim();

                string val = "";

                if (Recibo == null || Recibo.Length != 10)
                {
                    val = "Nulo";
                    if (Recibo != null)
                        val = Recibo;

                    throw new Exception("Recibo Invalido: " + val);
                }

                if (Identificacao == null || Identificacao.Length != 14)
                {
                    val = "Nulo";
                    if (Identificacao != null)
                        val = Identificacao;

                    throw new Exception("Identificacao Invalido: " + val);
                }

                if (cpfRecepcionador == null || cpfRecepcionador.Length != 11)
                {
                    val = "Nulo";
                    if (cpfRecepcionador != null)
                        val = cpfRecepcionador;

                    throw new Exception("cpfRecepcionador Invalido: " + val);
                }

                if (cnpjOrgaoRegistro == null || cnpjOrgaoRegistro.Length != 14)
                {
                    val = "Nulo";
                    if (cnpjOrgaoRegistro != null)
                        val = cnpjOrgaoRegistro;
                    throw new Exception("cnpjOrgaoRegistro Invalido: " + val);
                }

                WsServicesReginRFB.ServiceReginRFB se = new WsServicesReginRFB.ServiceReginRFB();

                se.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                resultIn = se.ServiceWs05EX(Identificacao, Recibo, cpfRecepcionador, numeroServentia, cnpjOrgaoRegistro);

                result.Cnpj = resultIn.Cnpj;
                result.codretorno = resultIn.codretorno;
                result.descricao = resultIn.descricao;
                result.Nire = resultIn.Nire;
                result.status = resultIn.status;

            }
            catch (Exception ex)
            {
                string XmlDados = GlobalV1.CreateXML(result);
                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = XmlDados;
                    e35.t73307_erro = ex.Message;
                    e35.t73307_ide_solicitacao = Identificacao;
                    e35.t73307_rec_solicitacao = "WS05EXT";
                    e35.Update();
                }

                result.status = "NOK";
                result.codretorno = "99";
                result.descricao = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 
        /// *** Os códigos de erro acima de "90" são relativos a procedimentos internos. Recebendo algum deles será necessário informar à RFB.
        /// Segue a lista de retorno do S05:
        /// * MENSAGENS DE RETORNO PROGRAMADA
        /// * 00 - 'SUCESSO'
        /// * 01 - 'REQUISICAO INVALIDA'
        /// * 02 - 'SOLICITACAO ESTA INDEFERIDA'
        /// * 03 - 'SOLICITACAO ESTA CANCELADA PELO CONTRIBUINTE' OU * 'SOLICITACAO ESTA CANCELADA DE OFICIO PELA RFB' OU * 'SOLICITACAO ESTA CANCELADA POR DECURSO DE PRAZO DE 90 DIAS'
        /// * 04 - 'SOLICITACAO JA ESTA DEFERIDA'
        /// * 05 - 'DBE AINDA NAO FOI DISPONIBILIZADO'
        /// * 06 - 'SOLICITACAO NAO E DE DEFERIMENTO DA JUNTA COMERCIAL'
        /// * 07 - EVENTO X NJ NAO PODE SER DEFERIDA PELA JUNTA COMERCIAL VIA WEBSERVICE
        /// * 08 - 'DBE INDISPONIBILIZADO'
        /// * 09 - 'SISTEMA FECHADO PARA ACEITACAO DA DOC - TENTE MAIS TARDE'
        /// * 10 - 'NUMERO DE SOLICITACAO INEXISTENTE'
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
        [WebMethod(Description = @"Retorno status <br>
                                 OK -- Sucesso <br>
                                 NOK -- Sem Sucesso 
                        <br>Retorno codretorno: 
                        MENSAGENS DE RETORNO(codretorno) PROGRAMADA <br>
                         00 - 'SUCESSO' <br>
                         01 - 'REQUISICAO INVALIDA' <br>
                         02 - 'SOLICITACAO ESTA INDEFERIDA' <br>
                         03 - 'SOLICITACAO ESTA CANCELADA PELO CONTRIBUINTE' OU * 'SOLICITACAO ESTA CANCELADA DE OFICIO PELA RFB' OU * 'SOLICITACAO ESTA CANCELADA POR DECURSO DE PRAZO DE 90 DIAS' <br>
                         04 - 'SOLICITACAO JA ESTA DEFERIDA' <br>
                         05 - 'DBE AINDA NAO FOI DISPONIBILIZADO' <br>
                         06 - 'SOLICITACAO NAO E DE DEFERIMENTO DA JUNTA COMERCIAL' <br>
                         07 - 'EVENTO X NJ NAO PODE SER DEFERIDA PELA JUNTA COMERCIAL VIA WEBSERVICE' <br>
                         08 - 'DBE INDISPONIBILIZADO' <br>
                         09 - 'SISTEMA FECHADO PARA ACEITACAO DA DOC - TENTE MAIS TARDE' <br>
                         10 - 'NUMERO DE SOLICITACAO INEXISTENTE <br>
                         99 - Outros Erros")]
        public Retorno ServiceWs06(DadosWs06 dados)
        {
            Retorno result = new Retorno();
            WsServicesReginRFB.Retorno resultIn = new WsServicesReginRFB.Retorno();
            WsServicesReginRFB.DadosWs06 dadosIn = new WsServicesReginRFB.DadosWs06();
            try
            {

                GlobalV1.ValidaReques();

                if (dados.reciboSolicitacao == null || dados.reciboSolicitacao.Length != 10)
                {
                    throw new Exception("reciboSolicitacao Invalido");
                }

                if (dados.identificacaoSolicitacao == null || dados.identificacaoSolicitacao.Length != 14)
                {
                    throw new Exception("identificacaoSolicitacao Invalido");
                }

                if (dados.resultadoRegistroIntegradorEstadual != "01" && dados.resultadoRegistroIntegradorEstadual != "02")
                {
                    throw new Exception("resultadoRegistroIntegradorEstadual Invalido: " + dados.resultadoRegistroIntegradorEstadual);
                }

                if (dados.numeroRegistroCartorio != null && dados.numeroRegistroCartorio != "" && dados.numeroNire != null && dados.numeroNire != "")
                {
                    throw new Exception("numeroRegistroCartorio: " + dados.numeroRegistroCartorio + " e numero numeroNire: " + dados.numeroNire + ", não pode ser enviados preenchidos na mesma requisição " + dados.numeroServentia);
                }

                if (dados.numeroRegistroOab != null && dados.numeroRegistroOab != "" && dados.numeroNire != null && dados.numeroNire != "")
                {
                    throw new Exception("numeroRegistroOab: " + dados.numeroRegistroOab + " e numero numeroNire: " + dados.numeroNire + ", não pode ser enviados preenchidos na mesma requisição " + dados.numeroServentia);
                }

                if (dados.numeroRegistroCartorio != null && dados.numeroRegistroCartorio != "" && dados.numeroRegistroOab != null && dados.numeroRegistroOab != "")
                {
                    throw new Exception("numeroRegistroCartorio: " + dados.numeroRegistroCartorio + " e numero numeroRegistroOab: " + dados.numeroRegistroOab + ", não pode ser enviados preenchidos na mesma requisição " + dados.numeroServentia);
                }

                if (dados.numeroServentia != null && dados.numeroServentia != "" && dados.numeroNire != null && dados.numeroNire != "")
                {
                    throw new Exception("numeroServentia: " + dados.numeroServentia + " e numero numeroNire: " + dados.numeroNire + ", não pode ser enviados preenchidos na mesma requisição " + dados.numeroServentia);
                }

                if (dados.numeroServentia != null && dados.numeroServentia != "" && dados.numeroServentia.Length != 6)
                {
                    throw new Exception("numeroServentia Invalido: tem que ter 6 posiçoes " + dados.numeroServentia);
                }

                if (dados.numeroServentia != null && dados.numeroServentia.Length >= 2)
                {
                    if (dados.numeroRegistroCartorio == null || dados.numeroRegistroCartorio == "" || dados.numeroRegistroCartorio.Length == 0)
                    {
                        throw new Exception("numeroRegistroCartorio Invalido: " + dados.numeroRegistroCartorio);
                    }
                }


                if (dados.resultadoRegistroIntegradorEstadual == "02")
                {
                    if (dados.incompRegistroIntegradorEstadual == null || dados.incompRegistroIntegradorEstadual.Length == 0)
                    {
                        throw new Exception("incompRegistroIntegradorEstadual Invalido: Tem que ser enviado uma justificativa para o Indeferimento");
                    }
                }

                //WsServicesReginRFB.DadosWs06 dados = (WsServicesReginRFB.DadosWs06)dadosIn;
                WsServicesReginRFB.ServiceReginRFB se = new WsServicesReginRFB.ServiceReginRFB();

                dadosIn.identificacaoSolicitacao = dados.identificacaoSolicitacao;
                dadosIn.reciboSolicitacao = dados.reciboSolicitacao;
                dadosIn.numeroNire = dados.numeroNire;
                dadosIn.numeroRegistroCartorio = dados.numeroRegistroCartorio;
                dadosIn.dataRegistro = dados.dataRegistro;// DateTime.Today.ToString("yyyyMMdd");
                dadosIn.cpfResponsavelDeferimento = dados.cpfResponsavelDeferimento;
                dadosIn.numeroServentia = dados.numeroServentia;
                dadosIn.numeroRegistroOab = dados.numeroRegistroOab;
                dadosIn.Uf = dados.Uf;

                if (dados.nomeEmpresarial != "")
                {
                    dadosIn.nomeEmpresarial = dados.nomeEmpresarial;
                }

                if (dados.numeroNire246 != "")
                {
                    dados.numeroNire246 = dados.numeroNire246;
                }

                dadosIn.resultadoRegistroIntegradorEstadual = dados.resultadoRegistroIntegradorEstadual;

                if (dados.resultadoRegistroIntegradorEstadual == "02")
                {
                    if (dados.incompRegistroIntegradorEstadual != null && dados.incompRegistroIntegradorEstadual.Length > 0)
                    {
                        WsServicesReginRFB.incompRegistroIntegradorEstadual[] inconpa = new WsServicesReginRFB.incompRegistroIntegradorEstadual[20];

                        foreach (incompRegistroIntegradorEstadual _dadosWs06 in dados.incompRegistroIntegradorEstadual)
                        {
                            if (_dadosWs06 != null && _dadosWs06.codigo != "")
                            {
                                int i = 0;
                                WsServicesReginRFB.incompRegistroIntegradorEstadual inconpa2 = new WsServicesReginRFB.incompRegistroIntegradorEstadual();
                                inconpa2.codigo = _dadosWs06.codigo;
                                inconpa2.mensagem = _dadosWs06.mensagem;
                                inconpa.SetValue(inconpa2, i);
                                i++;
                            }
                        }

                        dadosIn.incompRegistroIntegradorEstadual = inconpa;
                    }
                }

                se.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                resultIn = se.ServiceWs06(dadosIn);

                result.Cnpj = resultIn.Cnpj;
                result.codretorno = resultIn.codretorno;
                result.descricao = resultIn.descricao;
                result.Nire = resultIn.Nire;
                result.status = resultIn.status;

                if (result.status == "NOK")
                {
                    string XmlDados = GlobalV1.CreateXML(dadosIn);
                    using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    {
                        e35.t73307_arquivo_RFB = XmlDados;
                        e35.t73307_erro = result.descricao;
                        e35.t73307_ide_solicitacao = dados.identificacaoSolicitacao;
                        e35.t73307_rec_solicitacao = "WS06EXT";
                        e35.Update();
                    }
                }


            }
            catch (Exception ex)
            {
                string XmlDados = GlobalV1.CreateXML(dados);
                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = XmlDados;
                    e35.t73307_erro = ex.Message;
                    e35.t73307_ide_solicitacao = dados.identificacaoSolicitacao;
                    e35.t73307_rec_solicitacao = "WS06EXT";
                    e35.Update();
                }

                result.status = "NOK";
                result.codretorno = "99";
                result.descricao = ex.Message;
            }

            return result;

        }
        #endregion

    }
}
