Public Class SolicitudDetalle
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

    '<Opcionales> <Opcional> <Id>string</Id> <Valor>string</Valor> </Opcional> </Opcionales>
    Private _listOpcionales As New List(Of ItemOpcional)
    Friend Class ItemOpcional
        Dim _id As String
        Dim _valor As String

        Public Sub New(ByVal pId As String, ByVal pValor As String)
            _id = pId
            _valor = pValor
        End Sub
        Public ReadOnly Property ID As String
            Get
                Return _id
            End Get
        End Property

        Public ReadOnly Property Valor As String
            Get
                Return _valor
            End Get
        End Property
    End Class
    Public Sub AgregarItemOpcional(ByVal pId As String, ByVal pValor As String)
        _listOpcionales.Add(New ItemOpcional(pId, pValor))
    End Sub

    '<Iva> 
    '    <AlicIva>
    '       <Id>short</Id> 
    '       <BaseImp>double</BaseImp> 
    '       <Importe>double</Importe> 
    '    </AlicIva> 
    '</Iva>
    Private _listIva As New List(Of ItemIVAAlicuota)
    Friend Class ItemIVAAlicuota
        Dim _id As Short
        Dim _baseImp As Double
        Dim _importe As Double

        Public Sub New(ByVal pId As Short, ByVal pBaseImponible As Double, ByVal pImporte As Double)
            _id = pId
            _baseImp = pBaseImponible
            _importe = pImporte
        End Sub
        Public ReadOnly Property ID As Short
            Get
                Return _id
            End Get
        End Property
        Public ReadOnly Property BaseImponible As Double
            Get
                Return _baseImp
            End Get
        End Property
        Public ReadOnly Property Importe As Double
            Get
                Return _importe
            End Get
        End Property
    End Class
    Public Sub AgregarItemIva(ByVal pId As Short, ByVal pBaseImponible As Double, ByVal pImporte As Double)
        _listIva.Add(New ItemIVAAlicuota(pId, pBaseImponible, pImporte))
    End Sub

    'Array para informar los tributos asociados a un comprobante <Tributo>.
    '<Tributos> 
    '    <Tributo> 
    '        <Id>short</Id> 
    '        <Desc>string</Desc> 
    '        <BaseImp>double</BaseImp> 
    '        <Alic>double</Alic> 
    '        <Importe>double</Importe> 
    '    </Tributo> 
    '</Tributos>
    Private _listTributos As New List(Of ItemTributo)
    Friend Class ItemTributo
        Dim _id As Short
        Dim _descripcion As String
        Dim _alicuota As Double
        Dim _baseImp As Double
        Dim _importe As Double

        Public Sub New(ByVal pId As Short, _
                        ByVal pDescripcion As String, _
                        ByVal pAlicuota As Double,
                        ByVal pBaseImponible As Double,
                        ByVal pImporte As Double)
            _id = pId
            _descripcion = pDescripcion
            _alicuota = pAlicuota
            _baseImp = pBaseImponible
            _importe = pImporte
        End Sub
        Public ReadOnly Property ID As Short
            Get
                Return _id
            End Get
        End Property
        Public ReadOnly Property Descripcion As String
            Get
                Return _descripcion
            End Get
        End Property
        Public ReadOnly Property Alicuota As Double
            Get
                Return _alicuota
            End Get
        End Property
        Public ReadOnly Property BaseImponible As Double
            Get
                Return _baseImp
            End Get
        End Property
        Public ReadOnly Property Importe As Double
            Get
                Return _importe
            End Get
        End Property
    End Class
    Public Sub AgregarItemTributo(ByVal pId As Short, _
                                  ByVal pDescripcion As String, _
                                  ByVal pAlicuota As Double, _
                                  ByVal pBaseImponible As Double, _
                                  ByVal pImporte As Double)
        _listTributos.Add(New ItemTributo(pId, pDescripcion, pAlicuota, pBaseImponible, pImporte))
    End Sub

    'Array para informar los comprobantes asociados <CbteAsoc>
    '<CbtesAsoc> 
    '    <CbteAsoc> 
    '        <Tipo>short</Tipo> 
    '        <PtoVta>int</PtoVta> 
    '        <Nro>long</Nro> 
    '    </CbteAsoc> 
    '</CbtesAsoc>
    Private _listComprobantes As New List(Of ItemComprobante)
    Friend Class ItemComprobante
        Dim _tipo As Short
        Dim _ptoVenta As Integer
        Dim _nro As Long

        Public Sub New(ByVal pTipo As Short, _
                       ByVal pPtoDeVenta As Integer, _
                       ByVal pNro As Long)
            _tipo = pTipo
            _ptoVenta = pPtoDeVenta
            _nro = pNro
        End Sub
        Public ReadOnly Property Tipo As Short
            Get
                Return _tipo
            End Get
        End Property
        Public ReadOnly Property PuntoDeVenta As Integer
            Get
                Return _ptoVenta
            End Get
        End Property
        Public ReadOnly Property Nro As Long
            Get
                Return _nro
            End Get
        End Property
    End Class
    Public Sub AgregarItemComprobantes(ByVal pTipo As Short, _
                                       ByVal pPtoVenta As Integer, _
                                       ByVal pNro As Long)
        _listComprobantes.Add(New ItemComprobante(pTipo, pPtoVenta, pNro))
    End Sub

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

    Public Function ImporteIva() As Double
        Dim _importeSalida As Double = 0.0
        For Each it As ItemIVAAlicuota In _listIva
            _importeSalida += it.Importe
        Next
        Return _importeSalida
    End Function
    Public Function ImporteTributos() As Double
        Dim _importeSalida As Double = 0.0
        For Each it As ItemTributo In _listTributos
            _importeSalida += it.Importe
        Next
        Return _importeSalida
    End Function
End Class

