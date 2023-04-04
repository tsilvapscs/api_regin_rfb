using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace WsRFBReginV2
{
    /// <summary>
    /// Descrição resumida de ClientDocumento
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class ClientDocumento : System.Web.Services.WebService
    {

        public ClientDocumento()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }
        //private static string apiBaseUri = ConfigurationManager.AppSettings.Get("apiDocumentosJucerja") != null ?
        //    ConfigurationManager.AppSettings.Get("apiDocumentosJucerja").ToString() 
        //    : "https://www.jucerja.rj.gov.br/IntegracaoJuntas/api/Documento/ObterUrlDocumento";

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public static async Task<RetornoBasico> AppClientObterUrlDocumento(string uf, string protocoloRedesim, string nire)
        {
            RetornoBasico result = new RetornoBasico();
            try
            {
                WebRequestHandler handler = new WebRequestHandler();
                var certificadoCliente = new ServiceReginRFB().getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());
                if (certificadoCliente != null)
                {
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    handler.ClientCertificates.Add(certificadoCliente);
                }
                else
                {
                    result.status = "NOK";
                    result.descricao = "Erro ao obter certificado.";
                }
                handler.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
                using (var client = new HttpClient(handler))
                {
                    string apiBaseUri = ConfigurationManager.AppSettings.Get("apiDocumentosJucerja") != null ? ConfigurationManager.AppSettings.Get("apiDocumentosJucerja").ToString() : "https://www.jucerja.rj.gov.br/IntegracaoJuntas/api/Documento/ObterUrlDocumento";
                    apiBaseUri = apiBaseUri + "?uf=" + uf + "&numeroProtocoloRfb=" + protocoloRedesim + "&nire=" + nire;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var response = await client
                        .GetAsync(apiBaseUri)
                        .ConfigureAwait(false))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var conteudoResponse = response.Content.ReadAsStringAsync().Result;

                            // retornoJason doc = (retornoJason)JsonConvert.DeserializeXmlNode(conteudoResponse);

                            if (!string.IsNullOrWhiteSpace(conteudoResponse))
                            {
                                result.status = "OK";
                                result.codretorno = response.StatusCode.ToString();
                                //result.url = JsonConvert.DeserializeObject<string>(conteudoResponse);
                                //result.url = JsonConvert.DeserializeObject<retornoJason>(conteudoResponse);
                                retornoJson retorno = JsonConvert.DeserializeObject<retornoJson>(conteudoResponse);
                                result.url = retorno.URL;
                            }
                        }
                        else
                        {
                            result.status = "NOK";
                            result.codretorno = response.StatusCode.ToString();
                            result.descricao = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.status = "NOK";
                result.descricao = ex.Message;
            }
            return result;
        }

        [WebMethod]
        public RetornoBasico ClientObterUrlDocumento(string uf, string protocoloRedesim, string nire)
        {
            return AppClientObterUrlDocumento(uf, protocoloRedesim, nire).Result;
            //return AppClientObterUrlDocumento("PA", "PAP1900082324", "15600295284").Result;
        }

    }

    public class retornoJson
    {
        public string URL { get; set; }
        public string Token { get; set; }
        public string Identificador { get; set; }
    }
}
