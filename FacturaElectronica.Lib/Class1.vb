Public Class FacturaFactory

    '<FeCabReq> 
    '    <CantReg>int</CantReg>
    '    <PtoVta>int</PtoVta> 
    '    <CbteTipo>int</CbteTipo> 
    '</FeCabReq>

    Private _puntoDeVenta As Integer
    Private _tipoComprobante As Integer 'enum ??

    Public ReadOnly Property PuntoDeVenta() As Integer
        Get
            Return _puntoDeVenta
        End Get
    End Property

    Public ReadOnly Property TipoComprobante() As Integer
        Get
            Return _tipoComprobante
        End Get
    End Property

    Public Sub New(ByVal pPuntoDeVenta As Integer, ByVal pTipoComprobante As Integer)
        _puntoDeVenta = pPuntoDeVenta
        _tipoComprobante = pTipoComprobante
    End Sub

    Public Function CrearFactura() As Factura
        Return Nothing
    End Function

    


End Class
