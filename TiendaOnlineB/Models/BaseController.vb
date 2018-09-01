
Imports Negocio.ReglasNego
Imports Negocio.DTO
Imports Negocio.Comunes.enums
Imports Negocio.Comunes

Public Class BaseController
    Inherits System.Web.UI.Page

    Private LogRN As LogRN
    Private MotorBd As MotorDb
    Protected CategoRN As CategoriaRN
    Protected ClientRN As ClientesRN
    Protected ProducRN As ProductosRN
    Protected PedidRN As PedidosRN

    Protected oCategoDto As CategoriasDTO
    Protected oClientDto As ClientesDTO
    Protected oProducDto As ProductosDTO
    Protected oPedidosDto As PedidosDTO



    Public Sub New()
        Try
            MotorBd = MotorDb.MSSQL
            Negocio.Comunes.Base.MotorBd = MotorBd

            oCategoDto = New CategoriasDTO()
            oClientDto = New ClientesDTO()
            oProducDto = New ProductosDTO()
            oPedidosDto = New PedidosDTO()

            LogRN = New LogRN()
            CategoRN = New CategoriaRN()
            ClientRN = New ClientesRN()
            ProducRN = New ProductosRN()
            PedidRN = New PedidosRN()


        Catch OV As StackOverflowException
            Logs.WriteLogDB("Error StackOverflowException en BaseController", OV)
        Catch ex As Exception
            Logs.WriteLogDB("Error en BaseController", ex)
        End Try
    End Sub


End Class
