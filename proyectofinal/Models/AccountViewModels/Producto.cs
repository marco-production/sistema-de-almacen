using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using proyectofinal.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace proyectofinal.Models.AccountViewModels
{
    public class Producto
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(50), Display(Name = "Nombre producto")]
        public String Nombre { get; set; }

        [Required, MaxLength(250), Display(Name = "Tipo")]
        public String Tipo { get; set; }

        [Required, MaxLength(50), Display(Name = "Categoria")]
        public String Categoria { get; set; }

        [Required, Display(Name = "Peso")]
        public Double Peso { get; set; }

        [Required, MaxLength(400), Display(Name = "Descripcion")]
        public String Descripcion { get; set; }

        [Required, Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Required,Display(Name = "Precio")]
        public Double Precio { get; set; }

        [Required,DataType(DataType.Date), Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }

        public String RegisterId { get; set; }

        public int EmpresaId { get; set; }

        [NotMapped]
        public  List<int> listId { get; set; }

        private string Id_usuario { get; set; }

        private  ApplicationDbContext context= new ApplicationDbContext();

        public Producto()
        {

        }
        public Producto(ApplicationDbContext context, string Id_usuario)
        {
            this.Id_usuario = Id_usuario;
            this.context = context;
        }
        public List<int> Obtener_id_empresa()
        {
            listId = new List<int>();
            var consulta = from empresa in context.Empresas
                           join user in context.Users on empresa.RegisterId  equals user.Id
                           where empresa.RegisterId == Id_usuario
                           select new { EmpresaId = empresa.Id };
            foreach (var item in consulta)
            {
                listId.Add(item.EmpresaId);
            }
            return listId;
        }
    }
}
