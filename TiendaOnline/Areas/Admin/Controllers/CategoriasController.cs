using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Negocio.DTO;
using TiendaOnline.Models;
using TiendaOnline.Controllers;

namespace TiendaOnline.Areas.Admin.Controllers
{
    public class CategoriasController : BaseController
    {

        // GET: Admin/Categorias
        public ActionResult Index()
        {
            try
            {
                List<CategoriasDTO> app = CategoRN.GetCategorias();
                return View(app);
            }
            catch (Exception ex)
            {
                MensajeFail("No se pudo Index las aplicaciones." + ex.Message);
            }
            return View(new List<CategoriasDTO>());
        }


        // GET: Admin/Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCategoria,NombreCategoria")] CategoriasDTO categoriasDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CategoRN.Insertar(ref categoriasDTO);
                    MensajeSuccess("Registro guardado.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                MensajeFail("No se pudo crear el registro." + ex.Message);
            }
            return View(categoriasDTO);
        }

        // GET: Admin/Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriasDTO categoriasDTO = CategoRN.GetCategorias().FirstOrDefault(x => x.IdCategoria == (int)id);
            if (categoriasDTO == null)
            {
                return HttpNotFound();
            }
            return View(categoriasDTO);
        }

        // POST: Admin/Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCategoria,NombreCategoria")] CategoriasDTO categoriasDTO)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    CategoRN.Modificar(categoriasDTO);
                    MensajeSuccess("Registro modificado.");
                }
                else
                {
                    MensajeModelState(ModelState.Values);
                }
            }
            catch (Exception ex)
            {
                MensajeFail("No se pudo modificar el registro." + ex.Message);
            }
            return View(categoriasDTO);
        }

        // GET: Admin/Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CategoriasDTO categoriasDTO = CategoRN.GetCategorias().FirstOrDefault(x => x.IdCategoria == (int)id);
                if (categoriasDTO == null)
                {
                    return HttpNotFound();
                }
                return View(categoriasDTO);
            }
            catch (Exception ex)
            {
                MensajeFail("No se pudo obtener el registro a eliminar." + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                CategoRN.Eliminar(id);
                MensajeSuccess("Registro eliminado.");
            }
            catch (Exception ex)
            {
                MensajeFail("No se pudo eliminar el registro." + ex.Message);
            }
            return RedirectToAction("Index");
        }

    }
}
