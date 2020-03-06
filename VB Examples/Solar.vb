Module Solar

    ' Windows Visual Basic Sample Console Application: Solar
    '
    ' ------------------------------------------------------------------------
    '               Vaguely adapted from Solar.vbs
    '               Copyright Software Bisque 
    '           
    '               Converted by:  R.McAlister 2014
    '
    ' ------------------------------------------------------------------------
    '
    'Very simple demonstration application that targets the telescope on the Sun -- careful now.
    '
    '  Note:  this is basically simplification of "ListSearch.vb" where the only target is the Sun.
    '
    '

    'If you want your script to run all night regardless of errors, set bIgnoreErrors = True
    Public bIgnoreErrors As Boolean = False

    'Global TSX COM Objects
    Public tsx_sc As Object
    Public tsx_ts As Object
    Public tsx_oi As Object

    'Target Sun

    Public target As String = "Sun"

    Sub Main()

        If Not Welcome() Then
            Return
        End If

        'Create Objects
        tsx_sc = CreateObject("TheSkyX.Sky6StarChart")
        tsx_ts = CreateObject("TheSkyX.Sky6RASCOMTele")
        tsx_oi = CreateObject("TheSkyX.Sky6ObjectInformation")

        'Connect telescope
        tsx_ts.Connect()

        Dim iError As Boolean

        tsx_sc.Find(target)
        tsx_oi.Property(theskyxLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_ALT)
        Dim dAlt As Double = tsx_oi.ObjInfoPropOut
        tsx_oi.Property(theskyxLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_AZM)
        Dim dAz As Double = tsx_oi.ObjInfoPropOut

        Try
            tsx_ts.SlewToAzAlt(dAz, dAlt, target)
        Catch ex As Exception
            If Not bIgnoreErrors Then
                iError = MsgBox("An error has occurred running slew: " + ex.Message +
                                      vbCrLf + vbCrLf + "Exit Script?",
                                      vbYesNo + vbInformation)
                If iError = vbYes Then
                    Exit Try
                End If
            End If
        End Try
        MsgBox("The Sun's location is at: Altitude: " + Str(dAlt) + "  Azimuth: " + Str(dAz))

        'Disconnect telesope
        tsx_ts.Disconnect()

        'Clean up
        tsx_sc = Nothing
        tsx_ts = Nothing
        tsx_oi = Nothing

    End Sub

    Function Welcome() As Boolean

        ' Returns True if user wants to continue, False is abort

        Dim intDoIt = MsgBox("This script demonstrates how to target the Sun." + vbCrLf + vbCrLf,
                            vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return (False)
        End If

        Return (True)
    End Function

End Module



