using proyectofinal.Data;
using proyectofinal.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyectofinal.Models
{
    public class Inventario
    {
            //Aqui van todas las propiedades que tendra el inventario
            public List <Empresa> Nombre_Empresa { get; set; }
            public List<Producto> listproducto { get; set; }
        private ApplicationDbContext context;
        public Inventario()
        {

        }
        public Inventario(ApplicationDbContext dbContext)
        {
            this.context = dbContext;

        }
        public List<Inventario> Obtener_Inventarios(string Id_usuario)
        {
            List<Empresa> listempresa = new List<Empresa>();
            List<Inventario> list = new List<Inventario>();
            List<Producto> listProducto = new List<Producto>();
            var consulta = from empresa in context.Empresas
                           join producto in context.Productos on empresa.Id equals producto.EmpresaId into gruop
                           where empresa.RegisterId == Id_usuario 
                           orderby empresa.Id
                           select new { Id_empresa = empresa.Id, Productos = gruop, NombreEmpresa = empresa.Nombre };
            foreach (var item in consulta)
            {
                listempresa.Add(new Empresa { Id=item.Id_empresa, Nombre=item.NombreEmpresa});
                foreach (var item2 in item.Productos)
                {
                    listProducto.Add(new Producto { Nombre = item2.Nombre,
                                                    Tipo = item2.Tipo,
                                                    Categoria = item2.Categoria,
                                                    Peso = item2.Peso,
                                                    Descripcion = item2.Descripcion,
                                                    Cantidad = item2.Cantidad,
                                                    Precio = item2.Precio,
                                                    FechaIngreso = item2.FechaIngreso,
                                                    EmpresaId =item2.EmpresaId });
                }
            }
            return new List<Inventario> { new Inventario() { Nombre_Empresa = listempresa, listproducto=listProducto } };
        }
    }
}
