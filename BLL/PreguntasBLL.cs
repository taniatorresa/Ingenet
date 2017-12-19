using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PreguntasBLL
    {
        public Pregunta Create(Pregunta pregunta)
        {
            Pregunta Result = null;
            using (var r = new Repository<Pregunta>())
            {
                Pregunta tmp = r.Retrieve(
                    p => p.TituloPregunta == pregunta.TituloPregunta);
                if (tmp == null)
                {
                    Result = r.Create(pregunta);
                }
                else
                {
                    throw (new Exception("La pregunta ya existe "));
                }
            }
            return Result;
        }

        public bool Update(Pregunta pregunta)
        {
            bool Result = false;
            using (var r = new Repository<Pregunta>())
            {
                Pregunta tmp = r.Retrieve(
                    p => p.TituloPregunta == pregunta.TituloPregunta &&
                    p.PreguntaID != pregunta.PreguntaID);
                if (tmp == null)
                {
                    Result = r.Update(pregunta);
                }
                else
                {
                    throw (new Exception("La pregunta ya existe "));
                }
            }
            return Result;
        }

        public Pregunta Retrieve(int id)
        {
            Pregunta Result = null;
            using (var r = new Repository<Pregunta>())
            {
                Result = r.Retrieve(p => p.PreguntaID == id);
            }
            return Result;
        }

        public List<Pregunta> RetrieveAll()
        {
            List<Pregunta> Result = null;
            using (var r = new Repository<Pregunta>())
            {
                Result = r.RetrieveAllOrder(p => p.TituloPregunta);
            }
            return Result;
        }

 
        public bool Delete(int id)
        {
            bool Result = false;
            var pregunta = Retrieve(id);
            if (pregunta != null)
            {
                using (var r = new Repository<Pregunta>())
                {
                    Result = r.Delete(pregunta);
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