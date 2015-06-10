Public Class Autenticacion
    Private _cuit As String
    Private _token As String
    Private _firma As String

    Public ReadOnly Property Firma As String
        Get
            Return _firma
        End Get
    End Property
    Public ReadOnly Property Token As String
        Get
            Return _token
        End Get
    End Property
    Public ReadOnly Property Cuit As String
        Get
            Return _cuit
        End Get
    End Property

End Class
