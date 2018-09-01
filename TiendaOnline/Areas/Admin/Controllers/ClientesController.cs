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
    public class ClientesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Clientes
        public ActionResult Index()
        {
            try
            {
                List<ClientesDTO> app = ClientRN.GetClientes();
                return View(app);
            }
            catch (Exception ex)
            {
                MensajeFail("No se pudo Index las aplicaciones." + ex.Message);
            }
            return View(new List<ClientesDTO>());
        }


        // GET: Admin/Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCliente,NombreCliente,Direccion,Telefono,Correo")] ClientesDTO clientesDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClientRN.Insertar(ref clientesDTO);
                    MensajeSuccess("Registro guardado.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                MensajeFail("No se pudo crear el registro." + ex.Message);

            }
            return View(clientesDTO);
        }

        // GET: Admin/Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientesDTO ClientesDTO = ClientRN.GetClientes().FirstOrDefault(x => x.IdCliente == (int)id);
            if (ClientesDTO == null)
            {
                return HttpNotFound();
            }
            return View(ClientesDTO);
        }

        // POST: Admin/Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCliente,NombreCliente,Direccion,Telefono,Correo")] ClientesDTO clientesDTO)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    ClientRN.Modificar(clientesDTO);
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
            return View(clientesDTO);
        }

        // GET: Admin/Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ClientesDTO ClientesDTO = ClientRN.GetClientes().FirstOrDefault(x => x.IdCliente == (int)id);
                if (ClientesDTO == null)
                {
                    return HttpNotFound();
                }
                return View(ClientesDTO);
            }
            catch (Exception ex)
            {
                MensajeFail("No se pudo obtener el registro a eliminar." + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                ClientRN.Eliminar(id);
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
