using AutoMapper;
using ConexionBD.MsSql;
using Negocio.Comunes;
using Negocio.DbManager;
using Negocio.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.ReglasNego
{
    public class LogRN : Base
    {
        public int Insertar(LogDTO objecDTO)
        {
            try
            {
                string Usuario = objecDTO.Usuario;
                if (MotorBd == enums.MotorDb.MSSQL)
                {
                    Repositorio<Log> rpUsuarioMsSq = new Repositorio<Log>(new Modelo());

                    Log NewObj = Mapper.Map<Log>(objecDTO);
                    rpUsuarioMsSq.Insert(NewObj);
                    rpUsuarioMsSq.Commit();
                    objecDTO = Mapper.Map<LogDTO>(NewObj);
                }
            }
            catch (DbEntityValidationException ex)
            {
                string sMensaje = objecDTO.Message + "\n No se pudo insertar el log en la base de datos: \n";
                foreach (var itemA in ex.EntityValidationErrors)
                {
                    foreach (var itemB in itemA.ValidationErrors)
                    {
                        sMensaje += itemB.ErrorMessage + "\n";
                    }
                }
                WriteLogEventViwer(sMensaje, EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                string sMensaje = objecDTO.Message;
                sMensaje += "\n No se pudo insertar el log en la base de datos: " + ex.Message;
                WriteLogEventViwer(sMensaje, EventLogEntryType.Error);
            }
            return objecDTO.IdLog;
        }
        public void WriteLogEventViwer(string Mensaje, EventLogEntryType TipoEvento)
        {
            try
            {
                string NameLog = "Microcolsa ApiUsuarios";
                if (!EventLog.Exists(NameLog))
                {
                    EventLog.CreateEventSource(NameLog, "ApiUsuarios");
                }
                EventLog log = new EventLog("ApiUsuarios");
                log.Source = NameLog;
                log.WriteEntry(Mensaje, TipoEvento);
                log.Close();
                log.Dispose();
            }
            catch (Exception ex)
            {
                GrabarLogErrorNote(Mensaje + "\n\n No se pudo escribir en el visor de eventos: " + ex.Message, EventLogEntryType.Error);
            }
        }
        private static void GrabarLogErrorNote(string Mensaje, EventLogEntryType tipoEvento)
        {
            try
            {
                string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
                string directoryName = Path.GetDirectoryName(assemblyFile);
                string sRut = directoryName + "//LogsTiendaOnline//";
                if (!Directory.Exists(sRut))
                    Directory.CreateDirectory(sRut);
                Guid gu = Guid.NewGuid();
                string sName = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "_" + gu.ToString();
                StreamWriter sw = new StreamWriter(sRut + sName + ".txt");
                sw.Write(Mensaje);
                sw.Close();
            }
            catch (Exception ex)
            {
            }
        }

    }
}
