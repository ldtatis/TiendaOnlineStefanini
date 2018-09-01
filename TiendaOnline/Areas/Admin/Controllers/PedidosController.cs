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
    public class PedidosController : BaseController
    {

        // GET: Admin/Pedidos
        public ActionResult Index()
        {
            try
            {
                List<PedidosDTO> pedid = PedidRN.GetPedidos();
                List<ClientesDTO> Clien = ClientRN.GetClientes();
                for (int i = 0; i < pedid.Count; i++)
                {
                    pedid[i].ClientesAsoc = Clien.FirstOrDefault(x => x.IdCliente == pedid[i].IdCliente);
                }
                return View(pedid);
            }
            catch (Exception ex)
            {
                MensajeFail("No se pudo Index las aplicaciones." + ex.Message);
            }
            return View(new List<PedidosDTO>());
        }


        // GET: Admin/Pedidos/Create
        public ActionResult Create()
        {
            PedidosXProductos model = PedidRN.ObtenerPedidosXProductos();
            return View(model);
        }

        // POST: Admin/Pedidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PedidosXProductos ProducPediDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PedidRN.InsertarPedidoXProductos(ProducPediDTO);
                    MensajeSuccess("Registro guardado.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                MensajeFail("No se pudo crear el registro." + ex.Message);

            }
            return View(ProducPediDTO);
        }

        public ActionResult ProductosPedidos(int Id)
        {
            try
            {

                List<ProductosDTO> model = PedidRN.ObtenerPedidosXProductos(Id).Productos.Where(x => x.Selected == true).Select(x => x.Producto).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                MensajeFail("No se pudo Index las aplicaciones." + ex.Message);
            }
            return View(new List<PedidosDTO>());
        }


    }
}
