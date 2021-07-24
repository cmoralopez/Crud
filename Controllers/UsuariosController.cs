using Crud.Data;
using Crud.Models;
using Crud.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UsuariosController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Authorize(Roles = "Owner,Final")]
        public IActionResult Index()
        {
            List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();
            usuarios = _applicationDbContext.Usuario.Select(c=> new UsuarioViewModel
            {
                Codigo = c.Codigo,
                Nombre = c.Nombre,
                Apellido= c.Apellido,
                Direccion = c.Direccion,
                Estado = c.Estado,
                DescripcionGenero = c.CodigoGeneroNavigation.Detalles
            }).ToList();
            return View(usuarios);
        }

        [Authorize(Roles = "Owner")]
        public IActionResult Create()
        {
            ViewData["CodigoGenero"] = new SelectList(_applicationDbContext.Generos.Where(c=>c.Estado == 1).ToList(),"Codigo","Descripción");
            return View();
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            try
            {
                usuario.Estado = 1;
                _applicationDbContext.Add(usuario);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {
                ViewData["CodigoGenero"] = new SelectList(_applicationDbContext.Generos.Where(c => c.Estado == 1).ToList(), "Codigo", "Descripción",usuario.CodigoGenero);
                return View(usuario);
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Owner")]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            Usuario usuario = _applicationDbContext.Usuario.Where(x => x.Codigo == id).FirstOrDefault();

            if (usuario == null)
                return RedirectToAction("Index");
            ViewData["CodigoGenero"] = new SelectList(_applicationDbContext.Generos.Where(c => c.Estado == 1).ToList(), "Codigo", "Descripción", usuario.CodigoGenero);
            return View(usuario);
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        public IActionResult Edit(int id, Usuario usuario)
        {
            if (id != usuario.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _applicationDbContext.Update(usuario);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {
                ViewData["CodigoGenero"] = new SelectList(_applicationDbContext.Generos.Where(c => c.Estado == 1).ToList(), "Codigo", "Descripción", usuario.CodigoGenero);
                return View(usuario);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Owner,Final")]
        public IActionResult Details(int Codigo)
        {
            Usuario usuario = _applicationDbContext.Usuario.Where(x => x.Codigo == Codigo).FirstOrDefault();
            
            if (Codigo == 0)
            {
                return RedirectToAction("Index");
            }
            
            if(usuario == null)
            {
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        [Authorize(Roles = "Owner")]
        public IActionResult Desactivar(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            Usuario usuario = _applicationDbContext.Usuario.Find(id);
            try
            {
                usuario.Estado = 0;
                _applicationDbContext.Update(usuario);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Owner")]
        public IActionResult Activar(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            Usuario usuario = _applicationDbContext.Usuario.Find(id);
            try
            {
                usuario.Estado = 1;
                _applicationDbContext.Update(usuario);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
