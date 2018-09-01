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
    public class ProductosController : BaseController
    {

        // GET: Admin/Productos
        public ActionResult Index()
        {
            try
            {
                List<ProductosDTO> app = ProducRN.GetProductos();
                return View(app);
            }
            catch (Exception ex)
            {
                MensajeFail("No se pudo Index las aplicaciones." + ex.Message);
            }
            return View(new List<ProductosDTO>());
        }


        // GET: Admin/Productos/Create
        public ActionResult Create()
        {
            ProductoXCategorias model = ProducRN.ObtenerProductoXCategorias();
            return View(model);
        }

        // POST: Admin/Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoXCategorias producCategoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProducRN.InsertarProductoXCategorias(producCategoDTO);
                    MensajeSuccess("Registro guardado.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                MensajeFail("No se pudo crear el registro." + ex.Message);

            }
            return View(producCategoDTO);
        }

        // GET: Admin/Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoXCategorias productosDTO = ProducRN.ObtenerProductoXCategorias((int)id);

            if (productosDTO == null)
            {
                return HttpNotFound();
            }
            return View(productosDTO);
        }
        // POST: Admin/Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductoXCategorias producCategoDTO)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    ProducRN.ModificarProductoXCategorias(producCategoDTO);
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
            return View(producCategoDTO);
        }

        // GET: Admin/Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ProductosDTO productosDTO = ProducRN.GetProductos().FirstOrDefault(x => x.IdProducto == (int)id);
                if (productosDTO == null)
                {
                    return HttpNotFound();
                }
                return View(productosDTO);
            }
            catch (Exception ex)
            {
                MensajeFail("No se pudo obtener el registro a eliminar." + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                ProducRN.Eliminar(id);
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
