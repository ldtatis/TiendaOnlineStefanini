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
    public class ClientesRN : Base
    {
        public List<ClientesDTO> GetClientes()
        {
            List<ClientesDTO> objectListDTO = new List<ClientesDTO>();

            try
            {
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Clientes> repoMsSql = new Repositorio<Clientes>(new Modelo());

                    if (repoMsSql.All.Any())
                    {
                        List<Clientes> objectList = new List<Clientes>();
                        objectList = repoMsSql.All.OrderBy(p => p.NombreCliente).ToList();
                        objectListDTO = objectList.Select(data => Mapper.Map<ClientesDTO>(data)).ToList();
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
                if (objectListDTO == null)
                    objectListDTO = new List<ClientesDTO>();
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
                Logs.ReturnThrow("No se pudo obtener los registros de los Clientes", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener los registros de los Clientes", ex);
            }
            return objectListDTO;
        }

        /// <summary>
        /// Insertar un nueva Cliente, Es necesario el referencia para devolver su id ya que es auto incrementable
        /// </summary>
        public void Insertar(ref ClientesDTO objecDTO)
        {
            try
            {
                string Cliente = objecDTO.NombreCliente;
                string Email = objecDTO.Correo;
                if (GetClientes().Any(x => x.NombreCliente == Cliente))
                {
                    throw new Exception("Ya existe un Cliente con el nombre de  " + Cliente);
                }
                else if (GetClientes().Any(x => x.Correo == Email))
                {
                    throw new Exception("Ya existe un Cliente con el correo  " + Email);
                }
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Clientes> rpObjectMsSq = new Repositorio<Clientes>(new Modelo());

                    Clientes NewObj = Mapper.Map<Clientes>(objecDTO);
                    rpObjectMsSq.Insert(NewObj);
                    rpObjectMsSq.Commit();
                    objecDTO = Mapper.Map<ClientesDTO>(NewObj);
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
                Logs.ReturnThrow("No se pudo obtener los registros de los Clientes", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo obtener los registros de los Clientes", ex);
            }
        }

        /// <summary>
        /// Modificar el Cliente, Es necesario el original y el nueva para detectar si hubieron cambios
        /// </summary>
        public void Modificar(ClientesDTO objecDTO)
        {
            try
            {
                string Cliente = objecDTO.NombreCliente;
                if (GetClientes().Any(x => x.NombreCliente == Cliente && x.IdCliente != objecDTO.IdCliente))
                {
                    throw new Exception("Ya existe un Cliente con el nombre de Cliente: " + Cliente);
                }
                bool Modificado = false;
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Clientes> rpClienteMsSq = new Repositorio<Clientes>(new Modelo());

                    Clientes NewObj = Mapper.Map<Clientes>(objecDTO);
                    Clientes OldObj = rpClienteMsSq.All.FirstOrDefault(X => X.IdCliente == objecDTO.IdCliente);
                    rpClienteMsSq.Update(NewObj, OldObj, out Modificado);
                    if (Modificado)
                        rpClienteMsSq.Commit();
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
                Logs.ReturnThrow("No se pudo modificar los registros de los Clientes", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo modificar los registros de los Clientes", ex);
            }
        }

        public void Eliminar(int ID)
        {
            try
            {
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Clientes> rpClienteMsSq = new Repositorio<Clientes>(new Modelo());
                    Clientes OldObj = rpClienteMsSq.All.FirstOrDefault(x => x.IdCliente == ID);
                    if (OldObj != null)
                    {
                        rpClienteMsSq.Delete(OldObj);
                        rpClienteMsSq.Commit();
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
                Logs.ReturnThrow("No se pudo modificar los registros de los Clientes", new Exception(sMensaje));
            }
            catch (Exception ex)
            {
                Logs.ReturnThrow("No se pudo modificar los registros de los Clientes", ex);
            }
        }
    }
}
