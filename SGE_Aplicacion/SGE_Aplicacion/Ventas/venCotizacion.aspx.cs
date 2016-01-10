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
        public static object GenerarPrecorte(Sesion sesion, CotizacionItem item)
        {
            object resultado = new { };
            try
            {
                blCotizacion blCotizacion = new blCotizacion(sesion);
                object[] datos = GenerarPrecorte(item);
                resultado = new { correcto = true, imgBase64 = datos[0], valPZSP = datos[1] };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object OptimizarPrecorte(Sesion sesion, CotizacionItem item)
        {
            object resultado = new { };
            try
            {
                blCotizacion blCotizacion = new blCotizacion(sesion);
                List<object[]> opciones = OptimizarPrecorte(item);
                resultado = new { correcto = true, opciones = opciones };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        public static object[] GenerarPrecorte(CotizacionItem item)
        {
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

            int valPZSP = 0;

            for (int x = xPZ; x <= xMAT; x += xPZ)
            {
                for (int y = yPZ; y <= yMAT; y += yPZ)
                {
                    g.DrawRectangle(p, new Rectangle(x - xPZ, y - yPZ, xPZ, yPZ));
                    valPZSP++;
                }
            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            b.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            byte[] byteImage = ms.ToArray();
            string imgBase64 = Convert.ToBase64String(byteImage);

            return new object[] { imgBase64, valPZSP };
        }

        public static List<object[]> OptimizarPrecorte(CotizacionItem item)
        {
            List<object[]> opciones = new List<object[]>();

            var xMAT = Convert.ToInt32(item.material.alto * 10);
            var yMAT = Convert.ToInt32(item.material.largo * 10);

            var xPZ = Convert.ToInt32(item.valXFI * 10);
            var yPZ = Convert.ToInt32(item.valYFI * 10);

            var xPZG = yPZ;
            var yPZG = xPZ;

            int cantidad_columnas = Math.Max(yMAT, xMAT) / Math.Min(yPZ, xPZ);

            List<bool[]> columnas = new List<bool[]>();

            for (int i = 0; i < cantidad_columnas; i++)
            {
                bool[] obj = new bool[cantidad_columnas];
                for (int j = 0; j < cantidad_columnas; j++)
                {
                    if (j <= i)
                    {
                        obj[j] = true;
                    }
                    else
                    {
                        obj[j] = false;
                    }
                }
                columnas.Add(obj);
            }

            foreach (bool[] array in columnas)
            {
                Bitmap b;
                b = new Bitmap(xMAT, yMAT);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.White);

                Pen MyPen = new Pen(System.Drawing.Color.Black, 1);
                //Pen MyPen2 = new Pen(System.Drawing.Color.Red, 1);
                g.DrawRectangle(MyPen, new Rectangle(0, 0, xMAT - 1, yMAT - 1));

                int incremento_x = 0;
                int incremento_y = 0;

                int cantidad_piezas = 0;
                int numero_columna = 0;

                int sum_x = 0;
                int sum_y = 0;

                while (sum_x + incremento_x <= xMAT)
                {
                    incremento_x = array[numero_columna] ? xPZG : xPZ;
                    incremento_y = array[numero_columna] ? yPZG : yPZ;
                    while (sum_y + incremento_y <= yMAT)
                    {
                        g.DrawRectangle(MyPen, new Rectangle(sum_x, sum_y, incremento_x, incremento_y));
                        sum_y += incremento_y;
                        cantidad_piezas++;
                    }
                    sum_x += incremento_x;
                    numero_columna++;
                    sum_y = 0;
                    if (numero_columna < array.Length)
                    {
                        incremento_x = array[numero_columna] ? xPZG : xPZ;
                    }
                }

                if (cantidad_piezas > item.valPZSP) {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    b.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    byte[] byteImage = ms.ToArray();
                    string imgBase64 = Convert.ToBase64String(byteImage);

                    opciones.Add(new object[] { cantidad_piezas, imgBase64 });
                }
            }

            //var opciones1 = opciones.Distinct().ToList();

            return opciones;
        }
    }
}