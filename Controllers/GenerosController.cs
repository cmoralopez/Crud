using Crud.Data;
using Crud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud.Controllers
{
    [Authorize]
    public class GenerosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GenerosController (ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Owner,Final")]
        // GET: GenerosController
        public ActionResult Index()
        {
            List<Genero> ltsGenero = _context.Generos.ToList();
            return View(ltsGenero);
        }

        [Authorize(Roles = "Owner,Final")]
        // GET: GenerosController/Details/5
        public ActionResult Details(int id)
        {
            Genero genero = _context.Generos.FirstOrDefault(c=>c.Codigo==id);
            return View();
        }

        [Authorize(Roles = "Owner")]
        // GET: GenerosController/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Owner")]
        // POST: GenerosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genero genero)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(genero);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(genero);
            }
        }

        [Authorize(Roles = "Owner")]
        // GET: GenerosController/Edit/5
        public ActionResult Edit(int id)
        {
            Genero genero = _context.Generos.FirstOrDefault(c => c.Codigo == id);
            return View(genero);
        }

        [Authorize(Roles = "Owner")]
        // POST: GenerosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Genero genero)
        {
            if (id != genero.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(genero);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(genero);
            }
        }

        //[Authorize(Roles = "Owner,Final")]
        //public ActionResult Details(int Codigo)
        //{
        //    Genero genero = _context.Generos.Where(x => x.Codigo == Codigo).FirstOrDefault();

        //    if (Codigo == 0)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    if (genero == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(genero);
        //}

        [Authorize(Roles = "Owner")]
        public IActionResult Desactivar(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            Genero usuario = _context.Generos.Find(id);
            try
            {
                usuario.Estado = 0;
                _context.Update(usuario);
                _context.SaveChanges();
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
            Genero usuario = _context.Generos.Find(id);
            try
            {
                usuario.Estado = 1;
                _context.Update(usuario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
