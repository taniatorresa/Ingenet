using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuariosBLL
    {
        public Usuario Create(Usuario usuario)
        {
            Usuario Result = null;
            using (var r = new Repository<Usuario>())
            {
                Usuario tmp = r.Retrieve(
                    p => p.UserName == usuario.UserName);
                if (tmp == null)
                {
                    Result = r.Create(usuario);
                }
                else
                {
                    throw (new Exception("El usuario ya existe"));
                }
            }
            return Result;
        }

        public bool Update(Usuario usuario)
        {
            bool Result = false;
            using (var r = new Repository<Usuario>())
            {
                Usuario tmp = r.Retrieve(
                    p => p.UserName == usuario.UserName &&
                    p.UsuarioID != usuario.UsuarioID);
                if (tmp == null)
                {
                    Result = r.Update(usuario);
                }
                else
                {
                    throw (new Exception("El usuario ya existe"));
                }
            }
            return Result;
        }


        public Usuario Retrieve(int id)
        {
            Usuario Result = null;
            using (var r = new Repository<Usuario>())
            {
                Result = r.Retrieve(p => p.UsuarioID == id);
            }
            return Result;
        }

        public List<Usuario> RetrieveAll()
        {
            List<Usuario> Result = null;
            using (var r = new Repository<Usuario>())
            {
                Result = r.RetrieveAllOrder(p => p.UserName);
            }
            return Result;
        }

        public bool Delete(int id)
        {
            bool Result = false;
            var usuario = Retrieve(id);
            if (usuario != null)
            {
                using (var r = new Repository<Usuario>())
                {
                    Result = r.Delete(usuario);
                }

            }
            else
            {
                throw (new Exception("Error al eliminar"));
            }
            return Result;
        }
    }
}
