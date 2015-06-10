Public Class Cfg


    Public Shared Function UrlServicio() As String
        Return "http://wswhomo.afip.gov.ar/wsfev1/service.asmx"
    End Function

    Public Shared Function UrlServicioMonedas() As String
        Return "https://wswhomo.afip.gov.ar/wsfev1/service.asmx?op= FEParamGetTiposMonedas"
    End Function

End Class
