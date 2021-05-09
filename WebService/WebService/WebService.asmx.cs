using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebService
{
    /// <summary>
    /// Descrição resumida de WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        [WebMethod]
        public string UploadArquivo(String nomeDoArquivo, byte[] arquivoByte)
        {
            try
            {
                String arquivo = Server.MapPath("~/Arquivos/") + nomeDoArquivo;
                System.IO.File.WriteAllBytes(arquivo, arquivoByte);
            }
            catch (Exception ex)
            {
                return "Erro ao realizar o Upload! (" + ex.Message + ")";
            }
            return "Upload realizado com sucesso!";
        }
    }
}
