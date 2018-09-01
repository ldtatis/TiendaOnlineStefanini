using AutoMapper;
using ConexionBD.MsSql;
using Negocio.Comunes;
using Negocio.DbManager;
using Negocio.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.ReglasNego
{
    public class PedidosRN : Base
    {
        public List<PedidosDTO> GetPedidos()
        {
            List<PedidosDTO> objectListDTO = new List<PedidosDTO>();

            try
            {
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Pedidos> repoMsSql = new Repositorio<Pedidos>(new Modelo());

                    if (repoMsSql.All.Any())
                    {
                        List<Pedidos> objectList = new List<Pedidos>();
                        objectList = repoMsSql.All.OrderByDescending(p => p.Fecha).ToList();
                        objectListDTO = objectList.Select(data => Mapper.Map<PedidosDTO>(data)).ToList();
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
                if (objectListDTO == null)
                    objectListDTO = new List<PedidosDTO>();
            }
            catch (DbEntityValidationException ex)
            {
                string sMensaje = string.Empty;
                foreach (var itemA in ex.EntityValidationErrors)
                {
                    foreach (var itemB in itemA.ValidationErrors)
                    {
                        sMensaje += itemB.ErrorMessage + "\n";
                    }
                }
                Logs.ReturnThrow("No se pudo obtener los registros de las Pedidos", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener los registros de las Pedidos", ex);
            }
            return objectListDTO;
        }

        /// <summary>
        /// Insertar una nueva categoria, Es necesario la referencia para devolver su id ya que es auto incrementable
        /// </summary>
        public void Insertar(ref PedidosDTO objecDTO)
        {
            try
            {
                //string Pedido = objecDTO.NombrePedido;
                //if (GetPedidos().Any(x => x.NombrePedido == Pedido))
                //{
                //    throw new Exception("Ya existe una categoria con el nombre de  " + Pedido);
                //}
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Pedidos> rpObjectMsSq = new Repositorio<Pedidos>(new Modelo());

                    Pedidos NewObj = Mapper.Map<Pedidos>(objecDTO);
                    rpObjectMsSq.Insert(NewObj);
                    rpObjectMsSq.Commit();
                    objecDTO = Mapper.Map<PedidosDTO>(NewObj);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (DbEntityValidationException ex)
            {
                string sMensaje = string.Empty;
                foreach (var itemA in ex.EntityValidationErrors)
                {
                    foreach (var itemB in itemA.ValidationErrors)
                    {
                        sMensaje += itemB.ErrorMessage + "\n";
                    }
                }
                Logs.ReturnThrow("No se pudo obtener los registros de las Pedidos", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener los registros de las Pedidos", ex);
            }
        }


        public PedidosXProductos ObtenerPedidosXProductos(int pedidoid = 0)
        {
            PedidosXProductos oPedProd = new PedidosXProductos();
            Repositorio<Pedidos> repoMsSql = new Repositorio<Pedidos>(new Modelo());
            Repositorio<Productos> repoProductosMsSql = new Repositorio<Productos>(new Modelo());
            if (pedidoid != 0)
            {

                Pedidos prd = repoMsSql.All.FirstOrDefault(x => x.IdPedido == pedidoid);
                oPedProd.Pedido = GetPedidos().FirstOrDefault(x => x.IdPedido == pedidoid);

                foreach (var d in prd.Producto_Pedido)
                {
                    ProductoCheckbox model = new ProductoCheckbox
                    {
                        Producto = Mapper.Map<ProductosDTO>(d.Productos),
                        Selected = true
                    };

                    oPedProd.Productos.Add(model);
                }
            }
            else
            {
                PedidosDTO pedidosDTO = new PedidosDTO();
                ClientesRN ClientRN = new ClientesRN();
                pedidosDTO.ClientesDto = ClientRN.GetClientes();
                pedidosDTO.Fecha = DateTime.Now;
                pedidosDTO.Iva = 19;
                oPedProd.Pedido = pedidosDTO;

            }
            int[] prdProductos = oPedProd.Productos.Select(m => m.Producto).Select(x => x.IdProducto).ToArray();
            var productos = repoProductosMsSql.All.Where(m => !prdProductos.Contains(m.IdProducto));

            foreach (var itm in productos)
            {
                ProductoCheckbox model = new ProductoCheckbox
                {
                    Producto = Mapper.Map<ProductosDTO>(itm),
                    Selected = false
                };

                oPedProd.Productos.Add(model);
            }

            return oPedProd;
        }

        public void InsertarPedidoXProductos(PedidosXProductos objecDTO)
        {
            try
            {
                List<ProductosDTO> ListProduc = objecDTO.Productos.Where(x => x.Selected == true).Select(x => x.Producto).ToList();
                if (ListProduc.Count == 0)
                {
                    throw new Exception("El pedido debe tener al menos un producto");
                }
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    PedidosDTO Pedido = objecDTO.Pedido;
                    Insertar(ref Pedido);
                    foreach (ProductosDTO item in ListProduc)
                    {
                        Producto_PedidoDTO ProdcPed = new Producto_PedidoDTO()
                        {
                            IdProducto = item.IdProducto,
                            IdPedido = Pedido.IdPedido
                        };
                        InsertPediCatego(ref ProdcPed);
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (DbEntityValidationException ex)
            {
                string sMensaje = string.Empty;
                foreach (var itemA in ex.EntityValidationErrors)
                {
                    foreach (var itemB in itemA.ValidationErrors)
                    {
                        sMensaje += itemB.ErrorMessage + "\n";
                    }
                }
                Logs.ReturnThrow("No se pudo obtener los registros de las Pedidos", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener los registros de las Pedidos", ex);
            }
        }

        private void InsertPediCatego(ref Producto_PedidoDTO objecDTO)
        {
            try
            {

                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Producto_Pedido> repoMsSql = new Repositorio<Producto_Pedido>(new Modelo());
                    Producto_Pedido NewObj = Mapper.Map<Producto_Pedido>(objecDTO);
                    repoMsSql.Insert(NewObj);
                    repoMsSql.Commit();
                    objecDTO = Mapper.Map<Producto_PedidoDTO>(NewObj);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (DbEntityValidationException ex)
            {
                string sMensaje = string.Empty;
                foreach (var itemA in ex.EntityValidationErrors)
                {
                    foreach (var itemB in itemA.ValidationErrors)
                    {
                        sMensaje += itemB.ErrorMessage + "\n";
                    }
                }
                Logs.ReturnThrow("No se pudo obtener los registros de los productos por categoria", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener los registros de los productos por categoria", ex);
            }
        }

    }
}
