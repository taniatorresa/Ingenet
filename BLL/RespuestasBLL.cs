using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class RespuestasBLL
    {
        public Respuesta Create(Respuesta respuesta)
        {
            Respuesta Result = null;
            using (var r = new Repository<Respuesta>())
            {
                Respuesta tmp = r.Retrieve(
                    p => p.ContenidoRespuesta == respuesta.ContenidoRespuesta);
                if (tmp == null)
                {
                    Result = r.Create(respuesta);
                }
                else
                {
                    throw (new Exception("La respuesta ya existe "));
                }
            }
            return Result;
        }

        public bool Update(Respuesta respuesta)
        {
            bool Result = false;
            using (var r = new Repository<Respuesta>())
            {
                Respuesta tmp = r.Retrieve(
                    p => p.ContenidoRespuesta == respuesta.ContenidoRespuesta&&
                    p.RespuestaID != respuesta.RespuestaID);
                if (tmp == null)
                {
                    Result = r.Update(respuesta);
                }
                else
                {
                    throw (new Exception("La respuesta ya existe "));
                }
            }
            return Result;
        }

        public Respuesta Retrieve(int id)
        {
            Respuesta Result = null;
            using (var r = new Repository<Respuesta>())
            {
                Result = r.Retrieve(p => p.RespuestaID == id);
            }
            return Result;
        }

        public List<Respuesta> RetrieveAll()
        {
            List<Respuesta> Result = null;
            using (var r = new Repository<Respuesta>())
            {
                Result = r.RetrieveAllOrder(p => p.ContenidoRespuesta);
            }
            return Result;
        }
        public List<Respuesta> FilterRespuestasByPreguntaID(int preguntaID)
        {
            List<Respuesta> Result = null;
            using (var r = new Repository<Respuesta>())
            {
                Result = r.Filter(p => p.PreguntaID == preguntaID);
            }
            return Result;
        }

        public bool Delete(int id)
        {
            bool Result = false;
            var respuesta = Retrieve(id);
            if (respuesta != null)
            {
                using (var r = new Repository<Respuesta>())
                {
                    Result = r.Delete(respuesta);
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
