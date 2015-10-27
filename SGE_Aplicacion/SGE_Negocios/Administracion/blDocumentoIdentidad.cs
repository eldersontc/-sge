using SGE.AccesoDatos.Administracion;
using SGE.Entidades.Administracion;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Administracion
{
    public class blDocumentoIdentidad
    {
        daDocumentoIdentidad daDocumentoIdentidad;

        public IList<DocumentoIdentidad> ObtenerTodos()
        {
            IList<DocumentoIdentidad> documentosIdentidad;
            try
            {
                daDocumentoIdentidad = new daDocumentoIdentidad();
                daDocumentoIdentidad.AbrirSesion();
                documentosIdentidad = daDocumentoIdentidad.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daDocumentoIdentidad.CerrarSesion();
            }
            return documentosIdentidad;
        }

        public IList<DocumentoIdentidad> ObtenerActivos()
        {
            IList<DocumentoIdentidad> documentosIdentidad;
            try
            {
                daDocumentoIdentidad = new daDocumentoIdentidad();
                daDocumentoIdentidad.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                documentosIdentidad = daDocumentoIdentidad.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daDocumentoIdentidad.CerrarSesion();
            }
            return documentosIdentidad;
        }

        public bool Agregar(DocumentoIdentidad documentoIdentidad)
        {
            try
            {
                daDocumentoIdentidad = new daDocumentoIdentidad();
                daDocumentoIdentidad.IniciarTransaccion();
                daDocumentoIdentidad.Agregar(documentoIdentidad);
                daDocumentoIdentidad.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daDocumentoIdentidad.AbortarTransaccion();
                throw;
            }
            finally
            {
                daDocumentoIdentidad.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(DocumentoIdentidad documentoIdentidad)
        {
            try
            {
                daDocumentoIdentidad = new daDocumentoIdentidad();
                daDocumentoIdentidad.IniciarTransaccion();
                DocumentoIdentidad documentoIdentidad_ = daDocumentoIdentidad.ObtenerPorId(documentoIdentidad.idDocumentoIdentidad);
                documentoIdentidad_.nombre = documentoIdentidad.nombre;
                documentoIdentidad_.abreviacion = documentoIdentidad.abreviacion;
                documentoIdentidad_.activo = documentoIdentidad.activo;
                daDocumentoIdentidad.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daDocumentoIdentidad.AbortarTransaccion();
                throw;
            }
            finally
            {
                daDocumentoIdentidad.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idDocumentoIdentidad)
        {
            try
            {
                daDocumentoIdentidad = new daDocumentoIdentidad();
                daDocumentoIdentidad.IniciarTransaccion();
                daDocumentoIdentidad.EliminarPorId(idDocumentoIdentidad, constantes.esquemas.Administracion);
                daDocumentoIdentidad.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daDocumentoIdentidad.AbortarTransaccion();
                throw;
            }
            finally
            {
                daDocumentoIdentidad.CerrarSesion();
            }
            return true;
        }
    }
}
