using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using proyectofinal.Data;
using proyectofinal.Models;
using proyectofinal.Models.AccountViewModels;

namespace proyectofinal.Controllers
{
    public class ProductoesController : Controller
    {
        private Inventario inventario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProductoesController(ApplicationDbContext context, UserManager<ApplicationUser> _userManager)
        {
            this._userManager = _userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            List<Producto> productos = new List<Producto>();
             var consulta = from empresa in _context.Empresas
                           join producto in _context.Productos on empresa.Id equals producto.EmpresaId into gruop
                           where empresa.RegisterId == _userManager.GetUserId(HttpContext.User)
                           select gruop;
            foreach (var item in consulta)
            {
                foreach (var item2 in item)
                {
                    productos.Add(new Producto { Id=item2.Id,Nombre = item2.Nombre, Tipo = item2.Tipo, Categoria=item2.Categoria, Peso = item2.Peso,
                        Descripcion =item2.Descripcion,Cantidad=item2.Cantidad,FechaIngreso=item2.FechaIngreso, EmpresaId = item2.EmpresaId});
                }
            }
            return View(productos.ToList());
        }

        [Authorize(Roles="Admi")]
        public async Task<IActionResult> IndexAdmi() {
            return View(await _context.Productos.ToListAsync());
        }


        public ActionResult Inventario() {
            inventario = new Models.Inventario(_context);
            return View(inventario.Obtener_Inventarios(_userManager.GetUserId(HttpContext.User)));
        }


        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .SingleOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }
        [HttpGet]
        public string ObtenerId()
        {
            var UserId = _userManager.GetUserId(HttpContext.User);
            Producto producto = new Producto(_context, UserId);
            return JsonConvert.SerializeObject(producto.Obtener_id_empresa());
        }
        // GET: Productoes/Create
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Tipo,Categoria,Peso,Descripcion,Cantidad,Precio,FechaIngreso,EmpresaId,RegisterId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                var UserId = _userManager.GetUserId(HttpContext.User);
                HttpContext.Session.SetString("Id", UserId);
                producto.RegisterId = HttpContext.Session.GetString("Id");
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.SingleOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Tipo,Categoria,Peso,Descripcion,Cantidad,Precion,FechaIngreso,RegisterId,EmpresaId")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    producto.RegisterId = HttpContext.Session.GetString("Id");
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .SingleOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.SingleOrDefaultAsync(m => m.Id == id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
    
}
