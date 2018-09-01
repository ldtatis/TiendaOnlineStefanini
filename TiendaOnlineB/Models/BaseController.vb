
Imports Negocio.ReglasNego
Imports Negocio.DTO
Imports Negocio.Comunes.enums
Imports Negocio.Comunes

Public Class BaseController
    Inherits System.Web.UI.Page

    Private LogRN As LogRN
    Private MotorBd As MotorDb
    Protected CategoRN As CategoriaRN
    Protected oCategoDto As CategoriasDTO


    Public Sub New()
        Try
            MotorBd = MotorDb.MSSQL
            Negocio.Comunes.Base.MotorBd = MotorBd

            oCategoDto = New CategoriasDTO()
            LogRN = New LogRN()
            CategoRN = New CategoriaRN()
        Catch OV As StackOverflowException
            Logs.WriteLogDB("Error StackOverflowException en BaseController", OV)
        Catch ex As Exception
            Logs.WriteLogDB("Error en BaseController", ex)
        End Try
    End Sub


End Class
