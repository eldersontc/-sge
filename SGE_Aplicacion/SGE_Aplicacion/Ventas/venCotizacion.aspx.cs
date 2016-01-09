using SGE.Entidades.Administracion;
using SGE.Entidades.Ventas;
using SGE.Negocios.Ventas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.Ventas
{
    public partial class venCotizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion, Paginacion paginacion, Orden orden)
        {
            object resultado = new { };
            try
            {
                blCotizacion blCotizacion = new blCotizacion(sesion);
                object[] datos = blCotizacion.ObtenerTodos(paginacion, orden);
                resultado = new { correcto = true, cotizaciones = datos[0], total = datos[1] };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object ObtenerPorId(Sesion sesion, int idCotizacion)
        {
            object resultado = new { };
            try
            {
                blCotizacion blCotizacion = new blCotizacion(sesion);
                Cotizacion cotizacion = blCotizacion.ObtenerPorId(idCotizacion);
                resultado = new { correcto = true, cotizacion = cotizacion };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Cotizacion cotizacion)
        {
            object resultado = new { };
            try
            {
                blCotizacion blCotizacion = new blCotizacion(sesion);
                blCotizacion.Agregar(cotizacion);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Cotizacion cotizacion)
        {
            object resultado = new { };
            try
            {
                blCotizacion blCotizacion = new blCotizacion(sesion);
                blCotizacion.Actualizar(cotizacion);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, List<int> ids)
        {
            object resultado = new { };
            try
            {
                blCotizacion blCotizacion = new blCotizacion(sesion);
                blCotizacion.Eliminar(ids);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object GenerarGraficoPrecorte(Sesion sesion, CotizacionItem item)
        {
            object resultado = new { };
            try
            {
                blCotizacion blCotizacion = new blCotizacion(sesion);
                string imgBase64 = GenerarGraficoPrecorte(item);
                resultado = new { correcto = true, imgBase64 = imgBase64 };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        public static string GenerarGraficoPrecorte(CotizacionItem item)
        {
            string imgBase64 = string.Empty;

            var xMAT = Convert.ToInt32(item.material.alto * 10);
            var yMAT = Convert.ToInt32(item.material.largo * 10);

            var xPZ = 0;
            var yPZ = 0;

            if (item.flagGPR)
            {
                xPZ = Convert.ToInt32(item.valYFI * 10);
                yPZ = Convert.ToInt32(item.valXFI * 10);
            }
            else
            {
                xPZ = Convert.ToInt32(item.valXFI * 10);
                yPZ = Convert.ToInt32(item.valYFI * 10);
            }

            Bitmap b;
            b = new Bitmap(xMAT, yMAT);

            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);

            Pen p = new Pen(System.Drawing.Color.Black, 1);

            g.DrawRectangle(p, new Rectangle(0, 0, yMAT - 1, yMAT - 1));

            for (int x = xPZ; x <= xMAT; x += xPZ)
            {
                for (int y = yPZ; y <= yMAT; y += yPZ)
                {
                    g.DrawRectangle(p, new Rectangle(x - xPZ, y - yPZ, xPZ, yPZ));
                }
            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            b.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            byte[] byteImage = ms.ToArray();
            imgBase64 = Convert.ToBase64String(byteImage);

            return imgBase64;
        }
    }
}