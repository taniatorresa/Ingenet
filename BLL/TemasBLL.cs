using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TemasBLL
    {
        public Tema Create(Tema tema)
        {
            Tema Result = null;
            using (var r = new Repository<Tema>())
            {
                Tema tmp = r.Retrieve(
                    p => p.NombreTema == tema.NombreTema);
                if (tmp == null)
                {
                    Result = r.Create(tema);
                }
                else
                {
                    throw (new Exception("La categoría ya existe"));
                }
            }
            return Result;
        }

        public bool Update(Tema tema)
        {
            bool Result = false;
            using (var r = new Repository<Tema>())
            {
                Tema tmp = r.Retrieve(
                    p => p.NombreTema == tema.NombreTema &&
                    p.TemaID != tema.TemaID);
                if (tmp == null)
                {
                    Result = r.Update(tema);
                }
                else
                {
                    throw (new Exception("La categoría ya existe"));
                }
            }
            return Result;
        }

        public Tema Retrieve(int id)
        {
            Tema Result = null;
            using (var r = new Repository<Tema>())
            {
                Result = r.Retrieve(p => p.TemaID == id);
            }
            return Result;
        }

        public List<Tema> RetrieveAll()
        {
            List<Tema> Result = null;
            using (var r = new Repository<Tema>())
            {
                Result = r.RetrieveAllOrder(p => p.NombreTema);
            }
            return Result;
        }

        public bool Delete(int id)
        {
            bool Result = false;
            var tema = Retrieve(id);
            if (tema != null)
            {
                using (var r = new Repository<Tema>())
                {
                    Result = r.Delete(tema);
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