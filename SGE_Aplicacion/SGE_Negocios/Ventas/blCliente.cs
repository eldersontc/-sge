using SGE.AccesoDatos.Ventas;
using SGE.Entidades.Administracion;
using SGE.Entidades.Ventas;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Ventas
{
    public class blCliente : blBase
    {
        public blCliente(Sesion sesion) { base.sesion = sesion; }

        daCliente daCliente;
        daClienteDireccion daClienteDireccion;
        daClienteContacto daClienteContacto;

        public IList<Cliente> ObtenerTodos()
        {
            IList<Cliente> clientes;
            try
            {
                daCliente = new daCliente();
                daCliente.AbrirSesion();
                clientes = daCliente.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daCliente.CerrarSesion();
            }
            return clientes;
        }

        public IList<Cliente> ObtenerActivos()
        {
            IList<Cliente> clientes;
            try
            {
                daCliente = new daCliente();
                daCliente.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                clientes = daCliente.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daCliente.CerrarSesion();
            }
            return clientes;
        }

        public IList<ClienteContacto> ObtenerContactos(int idCliente)
        {
            IList<ClienteContacto> contactos;
            try
            {
                daClienteContacto = new daClienteContacto();
                daClienteContacto.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idCliente", idCliente });
                contactos = daClienteContacto.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daClienteContacto.CerrarSesion();
            }
            return contactos;
        }

        public Cliente ObtenerPorId(int idCliente)
        {
            Cliente cliente;
            try
            {
                daCliente = new daCliente();
                cliente = daCliente.ObtenerPorId(idCliente);
                daClienteDireccion = new daClienteDireccion();
                daClienteDireccion.AsignarSesion(daCliente);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idCliente", idCliente });
                cliente.direcciones = daClienteDireccion.ObtenerLista(filtros);
                daClienteContacto = new daClienteContacto();
                daClienteContacto.AsignarSesion(daCliente);
                cliente.contactos = daClienteContacto.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daCliente.CerrarSesion();
            }
            return cliente;
        }

        public bool Agregar(Cliente cliente)
        {
            try
            {
                daCliente = new daCliente();
                daCliente.IniciarTransaccion();
                daCliente.Agregar(cliente);
                daClienteDireccion = new daClienteDireccion();
                daClienteDireccion.AsignarSesion(daCliente);
                foreach (ClienteDireccion direccion in cliente.direcciones)
                {
                    daClienteDireccion.Agregar(direccion);
                }
                daClienteContacto = new daClienteContacto();
                daClienteContacto.AsignarSesion(daCliente);
                foreach (ClienteContacto contacto in cliente.contactos)
                {
                    daClienteContacto.Agregar(contacto);
                }
                daCliente.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daCliente.AbortarTransaccion();
                throw;
            }
            finally
            {
                daCliente.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Cliente cliente)
        {
            try
            {
                daCliente = new daCliente();
                daCliente.IniciarTransaccion();
                Cliente cliente_ = daCliente.ObtenerPorId(cliente.idCliente);
                cliente_.razonSocial = cliente.razonSocial;
                cliente_.documentoIdentidad = cliente.documentoIdentidad;
                cliente_.nombreComercial = cliente.nombreComercial;
                cliente_.telefono = cliente.telefono;
                cliente_.correo = cliente.correo;
                cliente_.vendedor = cliente.vendedor;
                cliente_.activo = cliente.activo;
                daClienteDireccion = new daClienteDireccion();
                daClienteDireccion.AsignarSesion(daCliente);
                foreach (ClienteDireccion direccion in cliente.direcciones)
                {
                    if (direccion.idClienteDireccion == 0)
                    {
                        daClienteDireccion.Agregar(direccion);
                    }
                    else {
                        ClienteDireccion direccion_ = daClienteDireccion.ObtenerPorId(direccion.idClienteDireccion);
                        direccion_.departamento = direccion.departamento;
                        direccion_.provincia = direccion.provincia;
                        direccion_.distrito = direccion.distrito;
                        direccion_.direccion = direccion.direccion;
                    }
                }
                foreach (int idDireccion in cliente.idsDirecciones)
                {
                    daClienteDireccion.EliminarPorId(idDireccion, constantes.esquemas.Ventas);
                }
                daClienteContacto = new daClienteContacto();
                daClienteContacto.AsignarSesion(daCliente);
                foreach (ClienteContacto contacto in cliente.contactos)
                {
                    if (contacto.idClienteContacto == 0)
                    {
                        daClienteContacto.Agregar(contacto);
                    }
                    else {
                        ClienteContacto contacto_ = daClienteContacto.ObtenerPorId(contacto.idClienteContacto);
                        contacto_.nombre = contacto.nombre;
                        contacto_.cargo = contacto.cargo;
                        contacto_.correo = contacto.correo;
                        contacto_.telefono = contacto.telefono;
                    }
                }
                foreach (int idContacto in cliente.idsContactos)
                {
                    daClienteContacto.EliminarPorId(idContacto, constantes.esquemas.Ventas);
                }
                daCliente.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daCliente.AbortarTransaccion();
                throw;
            }
            finally
            {
                daCliente.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idCliente)
        {
            try
            {
                daCliente = new daCliente();
                daCliente.IniciarTransaccion();
                daCliente.EliminarPorId(idCliente, constantes.esquemas.Administracion);
                daClienteDireccion = new daClienteDireccion();
                daClienteDireccion.AsignarSesion(daCliente);
                daClienteDireccion.EliminarPorIdCliente(idCliente);
                daClienteContacto = new daClienteContacto();
                daClienteContacto.AsignarSesion(daCliente);
                daClienteContacto.EliminarPorIdCliente(idCliente);
                daCliente.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daCliente.AbortarTransaccion();
                throw;
            }
            finally
            {
                daCliente.CerrarSesion();
            }
            return true;
        }
    }
}
