Public Class Factura
    '--> DETALLE (FeDetReq) Puede haber N
    '<Concepto>int</Concepto> 
    '<DocTipo>int</DocTipo> 
    '<DocNro>long</DocNro> 
    '<CbteDesde>long</CbteDesde> 
    '<CbteHasta>long</CbteHasta> 
    '<CbteFch>string</CbteFch> 
    '<ImpTotal>double</ImpTotal> 
    '<ImpTotConc>double</ImpTotConc> 
    '<ImpNeto>double</ImpNeto> 
    '<ImpOpEx>double</ImpOpEx> 
    '<ImpTrib>double</ImpTrib>  --> CALCULADO
    '<ImpIVA>double</ImpIVA>    --> CALCULADO
    '<FchServDesde>string</FchServDesde> 'NO OBLIGATORIO
    '<FchServHasta>string</FchServHasta> 'NO OBLIGATORIO
    '<FchVtoPago>string</FchVtoPago>  'NO OBLIGATORIO
    '<MonId>string</MonId> 
    '<MonCotiz>double</MonCotiz>

    Private _concepto As Integer 'enum??
    Private _documentoTipo As Integer 'enum??
    Private _documentoNro As Long
    Private _comprobanteDesde As Long 'una misma clase con "hasta"??
    Private _comprobanteHasta As Long 'una misma clase con "desde"??
    Private _comprobanteFecha As String
    Private _importeTotal As Double
    Private _importeTotalConc As Double 'Conc ??
    Private _importeNeto As Double
    Private _importeOpEx As Double 'Importe Exento
    Private _fechaServicioDesde As String
    Private _fechaServicioHasta As String
    Private _fechaServicioVtoPago As String
    Private _monedaID As String 'Consultar método FEParamGetTiposMonedas para valores posibles
    Private _monedaCotizacion As String 'Cotización de la moneda informada. Para PES, pesos argentinos la misma debe ser 1

    Public Property Concepto() As Integer
        Get
            Return _concepto
        End Get
        Set(ByVal value As Integer)
            _concepto = value
        End Set
    End Property
    Public Property DocumentoTipo() As Integer
        Get
            Return _documentoTipo
        End Get
        Set(ByVal value As Integer)
            _documentoTipo = value
        End Set
    End Property
    Public Property DocumentoNro() As Long
        Get
            Return _documentoNro
        End Get
        Set(ByVal value As Long)
            _documentoNro = value
        End Set
    End Property
    Public Property ComprobanteDesde() As Long
        Get
            Return _comprobanteDesde
        End Get
        Set(ByVal value As Long)
            _comprobanteDesde = value
        End Set
    End Property
    Public Property ComprobanteHasta() As Long
        Get
            Return _comprobanteHasta
        End Get
        Set(ByVal value As Long)
            _comprobanteHasta = value
        End Set
    End Property
    Public Property ComprobanteFecha() As String
        Get
            Return _comprobanteFecha
        End Get
        Set(ByVal value As String)
            _comprobanteFecha = value
        End Set
    End Property
    Public Property ImporteTotal() As Double
        Get
            Return _importeTotal
        End Get
        Set(ByVal value As Double)
            _importeTotal = value
        End Set
    End Property
    Public Property ImporteTotalConc() As Double
        Get
            Return _importeTotalConc
        End Get
        Set(ByVal value As Double)
            _importeTotalConc = value
        End Set
    End Property
    Public Property ImporteNeto() As Double
        Get
            Return _importeNeto
        End Get
        Set(ByVal value As Double)
            _importeNeto = value
        End Set
    End Property
    Public Property ImporteOpEx() As Double
        Get
            Return _importeOpEx
        End Get
        Set(ByVal value As Double)
            _importeOpEx = value
        End Set
    End Property
    Public Property FechaServicioDesde() As String
        Get
            Return _fechaServicioDesde
        End Get
        Set(ByVal value As String)
            _fechaServicioDesde = value
        End Set
    End Property
    Public Property FechaServicioHasta() As String
        Get
            Return _fechaServicioHasta
        End Get
        Set(ByVal value As String)
            _fechaServicioHasta = value
        End Set
    End Property
    Public Property FechaServicioVtoPago() As String
        Get
            Return _fechaServicioVtoPago
        End Get
        Set(ByVal value As String)
            _fechaServicioVtoPago = value
        End Set
    End Property
    Public Property MonedaID() As String
        Get
            Return _monedaID
        End Get
        Set(ByVal value As String)
            _monedaID = value
        End Set
    End Property
    Public Property MonedaCotizacion() As String
        Get
            Return _monedaCotizacion
        End Get
        Set(ByVal value As String)
            _monedaCotizacion = value
        End Set
    End Property
End Class

