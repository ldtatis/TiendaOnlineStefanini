using Negocio.DTO;
using Negocio.ReglasNego;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Comunes
{
    public class Logs
    {
        public static void ReturnThrow(string sMensaje, Exception ex)
        {
            string sMensajRet = sMensaje;
            sMensajRet += "\n Mensaje: " + ex.Message;
            if (ex.InnerException != null)
            {
                sMensajRet += "\n InnerException: " + ex.InnerException;
            }
            throw new Exception(sMensajRet);
        }
        public static void WriteLogDB(string sMensaje, Exception ex)
        {
            string sMensajRet = sMensaje;
            sMensajRet += "\n Mensaje: " + ex.Message;
            if (ex.InnerException != null)
            {
                sMensajRet += "\n InnerException: " + ex.InnerException;
            }
        }
        public static int WriteLogDB(string sMensaje, string Usuario, Exception ex = null)
        {
            int IdLog = 0;
            LogDTO DtoLog = new LogDTO();
            if (ex != null)
            {
                DtoLog = new LogDTO()
                {
                    Fecha = DateTime.Now,
                    Usuario = Usuario,
                    Message = ex.Message,
                    Source = ex.Source,
                    InnerException = ex.InnerException != null ? ex.InnerException.Message : string.Empty,
                    TargetSite = ex.TargetSite != null ? ex.TargetSite.Name : string.Empty,
                    success = false
                };
            }
            else
            {
                DtoLog = new LogDTO()
                {
                    Fecha = DateTime.Now,
                    Usuario = Usuario,
                    Message = sMensaje,
                    success = true
                };
            }
            LogRN RnLog = new LogRN();
            IdLog = RnLog.Insertar(DtoLog);
            return IdLog;
        }
    }
}
