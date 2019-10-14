using Microsoft.AspNetCore.Mvc.Rendering;
using proyectofinal.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace proyectofinal.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(250), Display(Name = "Nombre Completo")]
        public String CompletoNombre { get; set; }

        [Required, EmailAddress, MaxLength(255), Display(Name = "Correo Electronico")]
        public String Email { get; set; }

        [Required, DataType(DataType.Password), 
        StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", 
        MinimumLength = 6), Display(Name = "Contraseña")]
        public String Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public List<Empresa> Empresas { get; set; }

        public List<Producto> Productos { get; set; }

        /*Agregando roles*/
        [NotMapped]
        public List<SelectListItem> Roles { get; set; }
        public string Role { get; set; }

        public RegisterViewModel() {
            Roles = new List<SelectListItem>();
        }

        public void getRoles(ApplicationDbContext _context) {
            var roles = from r in _context.identityRole select r;
            var listRole = roles.ToList();

            foreach (var Data in listRole)
            {
                Roles.Add(new SelectListItem()
                {
                    Value = Data.Id,
                    Text = Data.Name
                });
            }
        }
        }
    }
