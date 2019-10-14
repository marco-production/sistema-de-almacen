using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proyectofinal.Models.AccountViewModels
{
    public class Empresa
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(100), Display(Name = "Nombre")]
        public String Nombre { get; set; }

        [Required, MaxLength(200), Display(Name = "Tipo de empresa")]
        public String Tipo { get; set; }

        [Required, MaxLength(250), Display(Name = "Pais")]
        public String Pais { get; set; }

        [Required, MaxLength(250), Display(Name = "Estado")]
        public String Estado { get; set; }

        [Required, MaxLength(250), Display(Name = "Ciudad")]
        public String Ciudad { get; set; }

        [Phone]
        [Display(Name = "Numero de telefono")]
        public string PhoneNumber { get; set; }

        [Required, EmailAddress, MaxLength(255), Display(Name = "Correo Electronico")]
        public String Email { get; set; }

        public String RegisterId { get; set; }

    }
}
