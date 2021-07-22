using Crud.Data;
using Crud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UsuariosController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            List<Usuario> usuarios = new List<Usuario>();
            usuarios = _applicationDbContext.Usuario.ToList();
            return View(usuarios);
        }

        public IActionResult Create()
        {
            return View();
        }
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
                return View(usuario);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            Usuario usuario = _applicationDbContext.Usuario.Where(x => x.Codigo == id).FirstOrDefault();
            //Usuario usuario = _applicationDbContext.Usuario.Find(id);
            if (usuario == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(int id, Usuario usuario)
        {
            if (id != usuario.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                usuario.Estado = 1;
                _applicationDbContext.Update(usuario);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return View(usuario);
            }
            return RedirectToAction("Index");
        }

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

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            Usuario usuario = _applicationDbContext.Usuario.Find(id);
            try
            {
                _applicationDbContext.Remove(usuario);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
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
