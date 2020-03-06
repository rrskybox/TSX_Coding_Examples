Module AutoImageSet

    ' Windows Visual Basic Sample Console Application: AutoImageSet
    '
    ' ------------------------------------------------------------------------
    '
    '               Author: R.McAlister (2014)
    '
    ' ------------------------------------------------------------------------
    '
    ' Example program that shows how to read (and by inference) how to set AutomatedImageLinkSettings
    '
    '
    Sub Main()

        'Welcome Window
        Dim intDoIt = MsgBox("This script demonstrates how to use the AutomatedImageLinkSettings class." + vbCrLf + vbCrLf,
                            vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return
        End If

        'Open Automated Image Link Settings object
        Dim tsx_ails = CreateObject("TheSkyX.AutomatedImageLinkSettings")

        'This property holds the image scale. 
        Dim dIScale As Double = tsx_ails.imageScale

        'This property holds the position angle.
        Dim dPA As Double = tsx_ails.positionAngle

        'This property holds the exposure time for Closed Loop Slew and T-point runs
        Dim dExposure As Double = tsx_ails.exposureTimeAILS

        'This property holds the number of field of views to search while Image Linking
        Dim iFOVSearch As Integer = tsx_ails.fovsToSearch

        'This property holds the number of ImageLink retries upon failure.
        Dim iRetry As Integer = tsx_ails.retries

        MsgBox("Automated Image Link Settings:" + vbCrLf + vbCrLf +
               "Scale: " + Str(dIScale) + vbCrLf +
               "Position Angle: " + Str(dPA) + vbCrLf +
               "Exposure: " + Str(dExposure) + vbCrLf +
               "FOVs to Search: " + Str(iFOVSearch) + vbCrLf +
               "Retries: " + Str(iRetry))

    End Sub

End Module
