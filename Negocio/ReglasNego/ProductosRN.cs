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
    public class ProductosRN : Base
    {

        public List<ProductosDTO> GetProductos()
        {
            List<ProductosDTO> objectListDTO = new List<ProductosDTO>();

            try
            {
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Productos> repoMsSql = new Repositorio<Productos>(new Modelo());
                    if (repoMsSql.All.Any())
                    {
                        List<Productos> objectList = new List<Productos>();
                        objectList = repoMsSql.All.OrderBy(p => p.Titulo).ToList();
                        objectListDTO = objectList.Select(data => Mapper.Map<ProductosDTO>(data)).ToList();
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
                if (objectListDTO == null)
                    objectListDTO = new List<ProductosDTO>();
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
                Logs.ReturnThrow("No se pudo obtener los registros de las Productos", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener los registros de las Productos", ex);
            }
            return objectListDTO;
        }

        public ProductoXCategorias ObtenerProductoXCategorias(int productoid = 0)
        {
            ProductoXCategorias oProdCateg = new ProductoXCategorias();
            Repositorio<Productos> repoMsSql = new Repositorio<Productos>(new Modelo());
            Repositorio<Categorias> repoCategoriasMsSql = new Repositorio<Categorias>(new Modelo());
            if (productoid != 0)
            {

                Productos prd = repoMsSql.All.FirstOrDefault(x => x.IdProducto == productoid);
                oProdCateg.Producto = GetProductos().FirstOrDefault(x => x.IdProducto == productoid);

                foreach (var d in prd.Cateoria_Producto)
                {
                    CategoriaCheckbox model = new CategoriaCheckbox
                    {
                        Categoria = Mapper.Map<CategoriasDTO>(d.Categorias),
                        Selected = true
                    };

                    oProdCateg.Categorias.Add(model);
                }
            }
            int[] prdCategorias = oProdCateg.Categorias.Select(m => m.Categoria).Select(x => x.IdCategoria).ToArray();
            var categorias = repoCategoriasMsSql.All.Where(m => !prdCategorias.Contains(m.IdCategoria));

            foreach (var itm in categorias)
            {
                CategoriaCheckbox model = new CategoriaCheckbox
                {
                    Categoria = Mapper.Map<CategoriasDTO>(itm),
                    Selected = false
                };

                oProdCateg.Categorias.Add(model);
            }
            return oProdCateg;
        }

        /// <summary>
        /// Insertar una nueva producto, Es necesario la referencia para devolver su id ya que es auto incrementable
        /// </summary>
        private void Insertar(ref ProductosDTO objecDTO)
        {
            try
            {
                string Producto = objecDTO.Titulo;
                int NumeroProducto = objecDTO.NumeroProducto;
                if (GetProductos().Any(x => x.Titulo == Producto))
                {
                    throw new Exception("Ya existe un producto con el nombre de  " + Producto);
                }
                else if (GetProductos().Any(x => x.NumeroProducto == NumeroProducto))
                {
                    throw new Exception("Ya existe un producto con el numero de  " + Producto);
                }
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Productos> repoMsSql = new Repositorio<Productos>(new Modelo());

                    Productos NewObj = Mapper.Map<Productos>(objecDTO);
                    repoMsSql.Insert(NewObj);
                    repoMsSql.Commit();
                    objecDTO = Mapper.Map<ProductosDTO>(NewObj);
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
                Logs.ReturnThrow("No se pudo obtener los registros de las Productos", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener los registros de las Productos", ex);
            }
        }

        /// <summary>
        /// Modificar la Producto, Es necesario la original y la nueva para detectar si hubieron cambios
        /// </summary>
        private void Modificar(ProductosDTO objecDTO)
        {
            try
            {
                string Producto = objecDTO.Titulo;
                if (GetProductos().Any(x => x.Titulo == Producto && x.IdProducto != objecDTO.IdProducto))
                {
                    throw new Exception("Ya existe un Producto con el nombre de Producto: " + Producto);
                }
                bool Modificado = false;
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Productos> rpProductoMsSq = new Repositorio<Productos>(new Modelo());

                    Productos NewObj = Mapper.Map<Productos>(objecDTO);
                    Productos OldObj = rpProductoMsSq.All.FirstOrDefault(X => X.IdProducto == objecDTO.IdProducto);
                    rpProductoMsSq.Update(NewObj, OldObj, out Modificado);
                    if (Modificado)
                        rpProductoMsSq.Commit();
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
                Logs.ReturnThrow("No se pudo modificar los registros de las Productos", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo modificar los registros de las Productos", ex);
            }
        }

        public void Eliminar(int ID)
        {
            try
            {
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Productos> rpProductoMsSq = new Repositorio<Productos>(new Modelo());
                    Productos OldObj = rpProductoMsSq.All.FirstOrDefault(x => x.IdProducto == ID);
                    if (OldObj != null)
                    {
                        EliminarProdcXCatego(OldObj.IdProducto);
                        rpProductoMsSq.Delete(OldObj);
                        rpProductoMsSq.Commit();
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
                Logs.ReturnThrow("No se pudo modificar los registros de las Productos", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo modificar los registros de las Productos", ex);
            }
        }

        /// <summary>
        /// Insertar una nueva producto, Es necesario la referencia para devolver su id ya que es auto incrementable
        /// </summary>
        private void InsertProdCatego(ref Cateoria_ProductoDTO objecDTO)
        {
            try
            {

                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Cateoria_Producto> repoMsSql = new Repositorio<Cateoria_Producto>(new Modelo());
                    Cateoria_Producto NewObj = Mapper.Map<Cateoria_Producto>(objecDTO);
                    repoMsSql.Insert(NewObj);
                    repoMsSql.Commit();
                    objecDTO = Mapper.Map<Cateoria_ProductoDTO>(NewObj);
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
                Logs.ReturnThrow("No se pudo obtener los registros de las categorias por producto", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener los registros de las categorias por producto", ex);
            }
        }

        private void EliminarProdcXCatego(int IdProducto)
        {
            try
            {
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Cateoria_Producto> repoMsSql = new Repositorio<Cateoria_Producto>(new Modelo());

                    List<Cateoria_Producto> GrUsr = repoMsSql.All.Where(x => x.IdProducto == IdProducto).ToList();
                    if (GrUsr.Any())
                    {
                        foreach (Cateoria_Producto item in GrUsr)
                        {
                            repoMsSql.Delete(item);
                            repoMsSql.Commit();
                        }
                    }
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
                Logs.ReturnThrow("No se pudo eliminar los registros de las categorias por productos", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo eliminar los registros de las categorias por productos", ex);
            }
        }

        private void EliminarProdcXCatego(int IdProducto, int IdCatego)
        {
            try
            {
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Cateoria_Producto> repoMsSql = new Repositorio<Cateoria_Producto>(new Modelo());
                    Cateoria_Producto OldObj = repoMsSql.All.FirstOrDefault(x => x.IdProducto == IdProducto && x.IdCatego == IdCatego);
                    if (OldObj != null)
                    {
                        repoMsSql.Delete(OldObj);
                        repoMsSql.Commit();
                    }
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
                Logs.ReturnThrow("No se pudo eliminar los registros de las categorias por productos", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo eliminar los registros de las categorias por productos", ex);
            }
        }

        public void InsertarProductoXCategorias(ProductoXCategorias objecDTO)
        {
            try
            {
                List<CategoriasDTO> ListCatego = objecDTO.Categorias.Where(x => x.Selected == true).Select(x => x.Categoria).ToList();
                if (ListCatego.Count == 0)
                {
                    throw new Exception("El producto debe tener al menos una categoria");
                }
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    ProductosDTO Producto = objecDTO.Producto;
                    Insertar(ref Producto);
                    foreach (CategoriasDTO item in ListCatego)
                    {
                        Cateoria_ProductoDTO CateProdc = new Cateoria_ProductoDTO()
                        {
                            IdCatego = item.IdCategoria,
                            IdProducto = Producto.IdProducto
                        };
                        InsertProdCatego(ref CateProdc);
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
                Logs.ReturnThrow("No se pudo obtener los registros de las Productos", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener los registros de las Productos", ex);
            }
        }

        public void ModificarProductoXCategorias(ProductoXCategorias objecDTO)
        {
            try
            {
                List<CategoriasDTO> ListCatego = objecDTO.Categorias.Where(x => x.Selected == true).Select(x => x.Categoria).ToList();
                if (ListCatego.Count == 0)
                {
                    throw new Exception("El producto debe tener al menos una categoria");
                }
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    ProductosDTO Producto = objecDTO.Producto;
                    Modificar(Producto);

                    Repositorio<Cateoria_Producto> repoMsSql = new Repositorio<Cateoria_Producto>(new Modelo());
                    List<Cateoria_Producto> lObjectBD = repoMsSql.All.Where(p => p.IdProducto == Producto.IdProducto).ToList();


                    if (lObjectBD.Count > 0)
                    {
                        #region Eliminar
                        List<Cateoria_Producto> lPropDel = (from tb1 in lObjectBD
                                                            join tb2 in ListCatego
                                                            on tb1.IdCatego equals tb2.IdCategoria into ProdDel
                                                            from tb3 in ProdDel.DefaultIfEmpty()
                                                            where tb3 == null
                                                            select tb1).ToList();
                        foreach (Cateoria_Producto item in lPropDel)
                        {
                            EliminarProdcXCatego(Producto.IdProducto, (int)item.IdCatego);
                        }



                        #endregion
                    }

                    #region Insertar

                    List<CategoriasDTO> lPropNew = (from tb1 in ListCatego
                                                    join tb2 in lObjectBD
                                                    on tb1.IdCategoria equals tb2.IdCatego into PropNew
                                                    from tb3 in PropNew.DefaultIfEmpty()
                                                    where tb3 == null
                                                    select tb1).ToList();

                    foreach (CategoriasDTO item in lPropNew)
                    {
                        Cateoria_ProductoDTO CateProdc = new Cateoria_ProductoDTO()
                        {
                            IdCatego = item.IdCategoria,
                            IdProducto = Producto.IdProducto
                        };
                        InsertProdCatego(ref CateProdc);
                    }

                    #endregion

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
                Logs.ReturnThrow("No se pudo modificar los registros de las Productos", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo modificar los registros de las Productos", ex);
            }
        }

    }
}
