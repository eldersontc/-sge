using NHibernate;
using NHibernate.Criterion;
using SGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Base
{
    public abstract class daBase<T>
    {
        public ISession sesion;
        public ITransaction transaccion;
        public ISessionFactory fabricaSesiones;

        public ISessionFactory getfabricaSesiones()
        {
            if (fabricaSesiones == null)
            {
                var configuracion = new NHibernate.Cfg.Configuration().Configure(ConfigurationManager.AppSettings["ARCHIVO_CONFIGURACION"]);
                fabricaSesiones = configuracion.BuildSessionFactory();
            }
            return fabricaSesiones;
        }

        public void AbrirSesion()
        {
            sesion = getfabricaSesiones().OpenSession();
        }

        public void CerrarSesion()
        {
            if (sesion != null && sesion.IsOpen) sesion.Close();
        }

        public void AsignarSesion(dynamic dal)
        {
            this.sesion = dal.sesion;
        }

        public void IniciarTransaccion()
        {
            if (sesion == null)
                AbrirSesion();
            transaccion = sesion.BeginTransaction();
        }

        public void ConfirmarTransaccion()
        {
            transaccion.Commit();
        }

        public void AbortarTransaccion()
        {
            transaccion.Rollback();
        }

        public void Agregar(Object obj)
        {
            sesion.Save(obj);
        }

        public void Actualizar(Object obj)
        {
            sesion.Update(obj);
        }

        public T ObtenerPorId(int id)
        {
            return (T)sesion.Get(typeof(T), id);
        }

        public void EliminarPorId(int id, string esquema)
        {
            Ejecutar(string.Format("DELETE " + esquema + "." + typeof(T).Name + " WHERE id" + typeof(T).Name + "= {0}", id));
        }

        public void Ejecutar(string sql)
        {
            var dbCommand = sesion.Connection.CreateCommand();
            sesion.Transaction.Enlist(dbCommand);
            dbCommand.CommandText = sql;
            dbCommand.ExecuteNonQuery();
        }

        public IList<T> ObtenerTodos()
        {
            return sesion.CreateCriteria(typeof(T)).
                List<T>();
        }

        public List<Object[]> ObtenerArray(String sql)
        {
            return (List<Object[]>)sesion.CreateSQLQuery(sql).List();
        }

        public List<T> ObtenerLista(String sql)
        {
            return (List<T>)sesion.CreateSQLQuery(sql).AddEntity(typeof(T)).List();
        }

        public List<T> ObtenerLista(List<Object[]> filtros)
        {
            ICriteria criteria = sesion.CreateCriteria(typeof(T));
            foreach (Object[] filtro in filtros)
            {
                criteria.Add(Restrictions.Eq(filtro[0].ToString(), filtro[1]));
            }
            return (List<T>)criteria.List<T>();
        }

        public object[] ObtenerPaginacion(List<Object[]> filtros, Paginacion paginacion, Orden orden)
        {
            ICriteria criteria = sesion.CreateCriteria(typeof(T));
            foreach (Object[] filtro in filtros)
            {
                criteria.Add(Restrictions.Eq(filtro[0].ToString(), filtro[1]));
            }

            if (!string.IsNullOrEmpty(orden.columna)) {
                if (orden.asc)
                {
                    criteria.AddOrder(Order.Asc(orden.columna));
                }
                else {
                    criteria.AddOrder(Order.Desc(orden.columna));
                }
            }

            List<T> lista = (List<T>)criteria.SetFirstResult((paginacion.pagActual -1) * paginacion.nroRegistros).SetMaxResults(paginacion.nroRegistros).List<T>();
            
            return new object[]{ lista, ObtenerTotal(filtros) };
        }

        public int ObtenerTotal(List<Object[]> filtros) {
            ICriteria criteria = sesion.CreateCriteria(typeof(T));
            foreach (Object[] filtro in filtros)
            {
                criteria.Add(Restrictions.Eq(filtro[0].ToString(), filtro[1]));
            }
            return criteria.SetProjection(Projections.RowCount()).UniqueResult<int>();
        }
    }
}
