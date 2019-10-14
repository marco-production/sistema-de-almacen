using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyectofinal.Data;
using proyectofinal.Models;
using proyectofinal.Models.AccountViewModels;

namespace proyectofinal.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public EmpresaController(UserManager<ApplicationUser> _userManager, ApplicationDbContext context)
        {
           this. _userManager = _userManager;
            _context = context;
        }

        // GET: Empresa
        public async Task<IActionResult> Index(String id)
        {
            
            var UserId = _userManager.GetUserId(HttpContext.User);
            HttpContext.Session.SetString("Id", UserId);
            var ListEmpresa = _context.Empresas.Where(x=> x.RegisterId==UserId);
            return View(ListEmpresa);
        }

        // GET: Empresa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpContext.Session.GetString("Id");
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .SingleOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresa/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Tipo,Pais,Estado,Ciudad,PhoneNumber,Email,RegisterId")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                empresa.RegisterId = HttpContext.Session.GetString("Id");
                _context.Add(empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empresa);
        }

        // GET: Empresa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.SingleOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }
            return View(empresa);
        }

        // POST: Empresa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Tipo,Pais,Estado,Ciudad,PhoneNumber,Email,RegisterId")] Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    empresa.RegisterId = HttpContext.Session.GetString("Id");
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
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
            return View(empresa);
        }

        // GET: Empresa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .SingleOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Eliminar_Productos = await _context.Productos.SingleOrDefaultAsync(m => m.EmpresaId == id);
            var empresa = await _context.Empresas.SingleOrDefaultAsync(m => m.Id == id);

            if (Eliminar_Productos != null)
            {
                _context.Productos.Remove(Eliminar_Productos);
            }

            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.Id == id);
        }
    }
}
