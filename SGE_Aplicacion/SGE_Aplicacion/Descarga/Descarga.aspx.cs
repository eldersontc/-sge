using SGE.Entidades.Administracion;
using SGE.Negocios.Administracion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SGE.Aplicacion.Descarga
{
    public partial class Descarga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idReporte = HttpContext.Current.Request.Form["r"];
            string id = HttpContext.Current.Request.Form["i"];

            blReporte blReporte = new blReporte(new Sesion());
            Reporte reporte = blReporte.ObtenerPorId(int.Parse(idReporte));

            WebClient httpclient = new WebClient();

            httpclient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["USUARIO_REPORTES"], ConfigurationManager.AppSettings["PASSWORD_REPORTES"]);
            httpclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            string requestXml;
            requestXml = string.Format("<resourceDescriptor name=\"{0}\" wsType=\"reportUnit\" uriString=\"{1}\"", reporte.descripcion, reporte.ubicacion);
            requestXml += " isNew=\"false\">";

            if (!string.IsNullOrEmpty(id))
            {
                requestXml += string.Format("   <parameter name=\"id\">{0}</parameter>", id);
            }

            foreach (ReporteItem item in reporte.items)
            {
                requestXml += string.Format("   <parameter name=\"{0}\">{1}</parameter>", item.nombre, item.valor);
            }

            requestXml += "</resourceDescriptor>";

            string requestAllResult = httpclient.UploadString(ConfigurationManager.AppSettings["SERVIDOR_REPORTES"] + string.Format("/rest/report{0}?RUN_OUTPUT_FORMAT=PDF", reporte.ubicacion), "PUT", requestXml);

            string session = httpclient.ResponseHeaders.Get("Set-Cookie");

            httpclient.Headers.Add("Cookie", session);

            XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(requestAllResult);
            XmlNode node = doc.DocumentElement.SelectSingleNode("uuid");
            string uuid = node.InnerText;

            string reportUrl = ConfigurationManager.AppSettings["SERVIDOR_REPORTES"] + "/rest/report/";
            reportUrl += uuid;
            reportUrl += "?file=report";

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.pdf", reporte.descripcion));
            Response.ClearContent();

            byte[] bytes = httpclient.DownloadData(reportUrl);

            Response.OutputStream.Write(bytes, 0, bytes.Length);
            Response.Flush();
            Response.Close();
        }
    }
}