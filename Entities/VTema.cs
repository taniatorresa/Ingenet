using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities
{
    [MetadataType(typeof(VTema))]
    public partial class Tema
    {
        class VTema
        {
            [Required(ErrorMessage = "Campo requerido")]
            [StringLength(50)]
            [DisplayName("Nombre del tema")]
            public string NombreTema { get; set; }

        }
    }
}
