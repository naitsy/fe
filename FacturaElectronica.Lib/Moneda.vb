Public Class Moneda
    Private _id As String
    Private _descripcion As String
    Private _fechaDesde As String
    Private _fechaHasta As String

    Public ReadOnly Property FechaHasta As String
        Get
            Return _fechaHasta
        End Get
    End Property

    Public ReadOnly Property FechaDesde As String
        Get
            Return _fechaDesde
        End Get
    End Property

    Public ReadOnly Property Descripcion As String
        Get
            Return _descripcion
        End Get
    End Property

    Public ReadOnly Property Id As String
        Get
            Return _id
        End Get
    End Property

    Public Sub New(ByVal pId As String, ByVal pDescripcion As String, ByVal pFechaDesde As String, ByVal pFechaHasta As String)
        _id = pId
        _descripcion = pDescripcion
        _fechaDesde = pFechaDesde
        _fechaHasta = pFechaHasta
    End Sub

End Class
