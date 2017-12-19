using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public class Login
    {
        [Required(ErrorMessage = "Correo requerido")]
        [Display(Name = "Correo Electrónico")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Display(Name = "Recordarme")]
        public bool Rememberme { get; set; }
    }
}
