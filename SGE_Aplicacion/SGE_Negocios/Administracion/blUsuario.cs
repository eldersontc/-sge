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
    public class blUsuario : blBase
    {
        public blUsuario(Sesion sesion) { base.sesion = sesion; }

        daUsuario daUsuario;

        public IList<Usuario> ObtenerTodos()
        {
            IList<Usuario> usuarios;
            try
            {
                daUsuario = new daUsuario();
                daUsuario.AbrirSesion();
                usuarios = daUsuario.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daUsuario.CerrarSesion();
            }
            return usuarios;
        }

        public bool Agregar(Usuario usuario) {
            try
            {
                daUsuario = new daUsuario();
                daUsuario.IniciarTransaccion();
                daUsuario.Agregar(usuario);
                daUsuario.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daUsuario.AbortarTransaccion();
                throw;
            } finally {
                daUsuario.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Usuario usuario)
        {
            try
            {
                daUsuario = new daUsuario();
                daUsuario.IniciarTransaccion();
                Usuario usuario_ = daUsuario.ObtenerPorId(usuario.idUsuario);
                usuario_.usuario = usuario.usuario;
                usuario_.clave = usuario.clave;
                usuario_.perfil = usuario.perfil;
                usuario_.activo = usuario.activo;
                daUsuario.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daUsuario.AbortarTransaccion();
                throw;
            }
            finally
            {
                daUsuario.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idUsuario)
        {
            try
            {
                daUsuario = new daUsuario();
                daUsuario.IniciarTransaccion();
                daUsuario.EliminarPorId(idUsuario, constantes.esquemas.Administracion);
                daUsuario.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daUsuario.AbortarTransaccion();
                throw;
            }
            finally
            {
                daUsuario.CerrarSesion();
            }
            return true;
        }
    }
}
