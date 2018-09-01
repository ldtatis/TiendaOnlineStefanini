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
    public class CategoriaRN : Base
    {
        public List<CategoriasDTO> GetCategorias()
        {
            List<CategoriasDTO> objectListDTO = new List<CategoriasDTO>();

            try
            {
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Categorias> repoMsSql = new Repositorio<Categorias>(new Modelo());

                    if (repoMsSql.All.Any())
                    {
                        List<Categorias> objectList = new List<Categorias>();
                        objectList = repoMsSql.All.OrderBy(p => p.NombreCategoria).ToList();
                        objectListDTO = objectList.Select(data => Mapper.Map<CategoriasDTO>(data)).ToList();
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
                if (objectListDTO == null)
                    objectListDTO = new List<CategoriasDTO>();
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
                Logs.ReturnThrow("No se pudo obtener los registros de las Categorias", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener los registros de las Categorias", ex);
            }
            return objectListDTO;
        }

        /// <summary>
        /// Insertar una nueva categoria, Es necesario la referencia para devolver su id ya que es auto incrementable
        /// </summary>
        public void Insertar(ref CategoriasDTO objecDTO)
        {
            try
            {
                string Categoria = objecDTO.NombreCategoria;
                if (GetCategorias().Any(x => x.NombreCategoria == Categoria))
                {
                    throw new Exception("Ya existe una categoria con el nombre de  " + Categoria);
                }
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Categorias> rpObjectMsSq = new Repositorio<Categorias>(new Modelo());

                    Categorias NewObj = Mapper.Map<Categorias>(objecDTO);
                    rpObjectMsSq.Insert(NewObj);
                    rpObjectMsSq.Commit();
                    objecDTO = Mapper.Map<CategoriasDTO>(NewObj);
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
                Logs.ReturnThrow("No se pudo obtener los registros de las Categorias", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener los registros de las Categorias", ex);
            }
        }

        /// <summary>
        /// Modificar la Categoria, Es necesario la original y la nueva para detectar si hubieron cambios
        /// </summary>
        public void Modificar(CategoriasDTO objecDTO)
        {
            try
            {
                string Categoria = objecDTO.NombreCategoria;
                if (GetCategorias().Any(x => x.NombreCategoria == Categoria && x.IdCategoria != objecDTO.IdCategoria))
                {
                    throw new Exception("Ya existe un Categoria con el nombre de Categoria: " + Categoria);
                }
                bool Modificado = false;
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Categorias> rpCategoriaMsSq = new Repositorio<Categorias>(new Modelo());

                    Categorias NewObj = Mapper.Map<Categorias>(objecDTO);
                    Categorias OldObj = rpCategoriaMsSq.All.FirstOrDefault(X => X.IdCategoria == objecDTO.IdCategoria);
                    rpCategoriaMsSq.Update(NewObj, OldObj, out Modificado);
                    if (Modificado)
                        rpCategoriaMsSq.Commit();
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
                Logs.ReturnThrow("No se pudo modificar los registros de las Categorias", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo modificar los registros de las Categorias", ex);
            }
        }

        public void Eliminar(int ID)
        {
            try
            {
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Categorias> rpCategoriaMsSq = new Repositorio<Categorias>(new Modelo());
                    Categorias OldObj = rpCategoriaMsSq.All.FirstOrDefault(x => x.IdCategoria == ID);
                    if (OldObj != null)
                    {
                        EliminarProdcXCatego(OldObj.IdCategoria);
                        rpCategoriaMsSq.Delete(OldObj);
                        rpCategoriaMsSq.Commit();
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
                Logs.ReturnThrow("No se pudo modificar los registros de las Categorias", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo modificar los registros de las Categorias", ex);
            }
        }


        public List<CategoriasDTO> GetCategoPorProducto(int IdProduc)
        {
            List<CategoriasDTO> objectListDTO = new List<CategoriasDTO>();

            try
            {
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Cateoria_Producto> repoCatego = new Repositorio<Cateoria_Producto>(new Modelo());
                    Repositorio<Categorias> repoMsSql = new Repositorio<Categorias>(new Modelo());

                    if (repoCatego.All.Where(x => x.IdProducto == IdProduc).Any())
                    {
                        List<Cateoria_Producto> categoList = new List<Cateoria_Producto>();
                        categoList = repoCatego.All.ToList();
                        foreach (Cateoria_Producto item in categoList)
                        {
                            Categorias categ = repoMsSql.All.FirstOrDefault(x => x.IdCategoria == item.IdCatego);
                            CategoriasDTO catDTO = Mapper.Map<CategoriasDTO>(categ);
                            objectListDTO.Add(catDTO);
                        }
                    }
                }
                if (objectListDTO == null)
                    objectListDTO = new List<CategoriasDTO>();
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
                Logs.ReturnThrow("No se pudo obtener las Categorias por producto", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener las Categorias por producto", ex);
            }
            return objectListDTO;
        }
        private void EliminarProdcXCatego(int IdCatego)
        {
            try
            {
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Cateoria_Producto> repoMsSql = new Repositorio<Cateoria_Producto>(new Modelo());

                    List<Cateoria_Producto> GrUsr = repoMsSql.All.Where(x => x.IdCatego == IdCatego).ToList();
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
    }
}
