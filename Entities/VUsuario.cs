using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [MetadataType(typeof(VUsuario))]
    public partial class Usuario
    {
        class VUsuario
        {
            [Required(ErrorMessage = "Campo requerido")]
            [StringLength(50)]
            [DisplayName("Nombre(s)")]
            public string Nombres { get; set; }

            //[Required(ErrorMessage = "Campo requerido")]
            [StringLength(50)]
            [DisplayName("Apellido paterno")]
            public string ApellidoPaterno { get; set; }

            [Required(ErrorMessage = "Campo requerido")]
            [StringLength(50)]
            [DisplayName("Apellido materno")]
            public string ApellidoMaterno { get; set; }

            //[Required(ErrorMessage = "Campo requerido")]
            [StringLength(20)]
            [DisplayName("Nombre de usuario")]
            public string UserName { get; set; }

            //[Required(ErrorMessage = "Campo requerido")]
            [StringLength(50)]
            [DataType(DataType.EmailAddress)]
            [DisplayName("E-mail")]
            public string Correo { get; set; }

            [DisplayName("Fecha de nacimiento")]
            public Nullable<System.DateTime> FechaNacimiento { get; set; }

            //[Required(ErrorMessage = "Campo requerido")]
            [StringLength(20)]
            [DataType(DataType.Password)]
            [DisplayName("Contraseña")]
            public string Contraseña { get; set; }

            //[Required(ErrorMessage = "Campo requerido")]
            [DisplayName("Ocupación")]
            public string Ocupacion { get; set; }

            //[Required(ErrorMessage = "Campo requerido")]
            [StringLength(5)]
            [DisplayName("Rol")]
            public string Rol { get; set; }

            [Required(ErrorMessage = "Sexo requerido")]
            [DisplayName("Sexo")]
            public int Sexo { get; set; }

            [Required(ErrorMessage = "Estatus requerido")]
            [DisplayName("Carrera")]
            public string Carrera { get; set; }

            //[Required(ErrorMessage = "Campo requerido")]
            [DisplayName("Estatus")]
            public int Estatus { get; set; }

            [DisplayName("Foto de perfil")]
            public byte[] Foto { get; set; }

        }
    }
}
