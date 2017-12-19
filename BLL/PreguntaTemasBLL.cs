using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class PreguntaTemasBLL
    {
        public PreguntaTema Create(PreguntaTema preguntaTema)
        {
            PreguntaTema Result = null;
            using (var r = new Repository<PreguntaTema>())
            {
                PreguntaTema tmp = r.Retrieve(
                    p => p.PreguntaTemasID == preguntaTema.PreguntaTemasID);
                if (tmp == null)
                {
                    Result = r.Create(preguntaTema);
                }
                else
                {
                    throw (new Exception("La preguntaTema ya existe "));
                }
            }
            return Result;
        }

        public bool Update(PreguntaTema preguntaTema)
        {
            bool Result = false;
            using (var r = new Repository<PreguntaTema>())
            {
                PreguntaTema tmp = r.Retrieve(
                    p => p.PreguntaTemasID == preguntaTema.PreguntaTemasID );
                if (tmp == null)
                {
                    Result = r.Update(preguntaTema);
                }
                else
                {
                    throw (new Exception("La preguntaTema ya existe "));
                }
            }
            return Result;
        }

        public PreguntaTema Retrieve(int id)
        {
            PreguntaTema Result = null;
            using (var r = new Repository<PreguntaTema>())
            {
                Result = r.Retrieve(p => p.PreguntaTemasID == id);
            }
            return Result;
        }

        public List<PreguntaTema> RetrieveAll()
        {
            List<PreguntaTema> Result = null;
            using (var r = new Repository<PreguntaTema>())
            {
                Result = r.RetrieveAllOrder(p => p.PreguntaTemasID.ToString());
            }
            return Result;
        }


        public bool Delete(int id)
        {
            bool Result = false;
            var preguntaTema = Retrieve(id);
            if (preguntaTema != null)
            {
                using (var r = new Repository<PreguntaTema>())
                {
                    Result = r.Delete(preguntaTema);
                }

            }
            else
            {
                throw (new Exception("Error al eliminar"));
            }
            return Result;
        }
        public List<PreguntaTema> FilterPreguntasTemaByID(int preguntaID)
        {
            List<PreguntaTema> Result = null;
            using (var r = new Repository<PreguntaTema>())
            {
                Result = r.Filter(p => p.PreguntaID == preguntaID);
            }
            return Result;
        }
        public List<PreguntaTema> FilterPreguntasporTema(int temaID)
        {
            List<PreguntaTema> Result = null;
            using (var r = new Repository<PreguntaTema>())
            {
                Result = r.Filter(p => p.TemaID == temaID);
            }
            return Result;
        }
    }
}
