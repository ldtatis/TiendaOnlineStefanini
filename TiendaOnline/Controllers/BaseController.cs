using Negocio.Comunes;
using Negocio.DTO;
using Negocio.ReglasNego;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using TiendaOnline.Models;
using static Negocio.Comunes.enums;

namespace TiendaOnline.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public MotorDb MotorBd;
        protected LogRN LogRN;
        protected CategoriaRN CategoRN;
        protected ClientesRN ClientRN;
        protected ProductosRN ProducRN;
        protected PedidosRN PedidRN;

        protected CategoriasDTO oCategoDto;
        protected ClientesDTO oClientDto;
        protected ProductosDTO oProducDto;
        protected PedidosDTO oPedidosDto;

        public BaseController()
        {
            try
            {
                this.ObtenerMotorBd();
                //Dtos
                oCategoDto = new CategoriasDTO();
                oClientDto = new ClientesDTO();
                oProducDto = new ProductosDTO();
                oPedidosDto = new PedidosDTO();
                //Reglas de Negocio
                LogRN = new LogRN();
                CategoRN = new CategoriaRN();
                ClientRN = new ClientesRN();
                ProducRN = new ProductosRN();
                PedidRN = new PedidosRN();
            }
            catch (System.StackOverflowException OV)
            {
                Logs.WriteLogDB("Error StackOverflowException en BaseController", OV);

            }
            catch (Exception ex)
            {
                Logs.WriteLogDB("Error en BaseController", ex);
            }

        }
        private void ObtenerMotorBd()
        {
            Configuration cfgWeb = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringsSection section = cfgWeb.GetSection("connectionStrings") as ConnectionStringsSection;
            if (section == null)
                throw new Exception("No contiene connectionStrings en el config");
            ConnectionStringSettings sectionConString = section.ConnectionStrings["Modelo"];
            if (sectionConString == null)
                throw new Exception("No contiene un string de conección llamado 'Modelo'");
            if (sectionConString.ProviderName == "System.Data.SqlClient")
                MotorBd = MotorDb.MSSQL;
            else if (sectionConString.ProviderName == "Oracle.ManagedDataAccess.Client")
                MotorBd = MotorDb.ORACLE;
            else
                throw new Exception("No contiene un proveedor valido");
            Negocio.Comunes.Base.MotorBd = MotorBd;

        }
        public void MensajeSuccess(string sMensaje)
        {
            try
            {
                Logs.WriteLogDB("No se pudo cambiar los datos de la conexión.\n", GetDataUser());
            }
            catch (Exception)
            {
            }

            TempData["UserMessage"] = new MessageVM() { CssClassName = "alert-success", Title = "Éxito!", Message = sMensaje };
        }
        public void MensajeFail(string sMensaje)
        {
            try
            {
                //Insertar log en BD o Pc
                if (sMensaje.Contains("Mensaje: "))
                {
                    int index = sMensaje.LastIndexOf("Mensaje: ");
                    sMensaje = sMensaje.Substring(index + 9, sMensaje.Length - index - 9);
                }
                Logs.WriteLogDB("No se pudo cambiar los datos de la conexión.\n", GetDataUser(), new Exception(sMensaje));
            }
            catch (Exception)
            {

            }

            TempData["UserMessage"] = new MessageVM() { CssClassName = "alert-danger", Title = "Error!", Message = sMensaje };
        }
        public static string GetDataUser()
        {
            string DataUser = string.Empty;
            try
            {

                DataUser = "Nombre del equipo: " + System.Environment.MachineName;
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        DataUser += "\n Ip: " + ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception)
            {

            }
            return DataUser;
        }

        public void MensajeModelState(ICollection<ModelState> Values)
        {
            List<string> lErrorMessage = Values.SelectMany(m => m.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToList();

            string retErrorMessage = string.Join(",", lErrorMessage.ToArray());
            string Mensaje = retErrorMessage;
            lErrorMessage = Values.SelectMany(m => m.Errors).Where(x => x.Exception != null)
                              .Select(e => e.Exception.Message)
                              .ToList();
            retErrorMessage = string.Join(",", lErrorMessage.ToArray());
            Mensaje += retErrorMessage;
            LogRN.WriteLogEventViwer(Mensaje, System.Diagnostics.EventLogEntryType.Error);
        }
    }
}