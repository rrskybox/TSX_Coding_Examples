Module MyFOV
    ' Windows Visual Basic Sample Console Application: MyFOV
    '
    ' ------------------------------------------------------------------------
    '
    '               Author: R.McAlister (2014)
    '
    ' ------------------------------------------------------------------------
    '
    ' This application demonstrates some of the functionality of the MyFOV class
    '  
    '
    'Globals
    '
    'Which FOV definition by index
    Dim iFOV = 0            'First FOV definition

    Sub Main()

        'Welcome Window
        Dim intDoIt = MsgBox("This script demonstrates some usage of the MyFOV class." + vbCrLf + vbCrLf,
                            vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return
        End If

        'Create FOV object
        Dim tsx_fov = CreateObject("TheSkyX.Sky6MyFOVs")
        Dim tsx_sc = CreateObject("TheSkyX.Sky6StarChart")

        'First FOV definition, additional definitions at successive indicies
        'Get FOV name and Postition Angle
        tsx_fov.Name(iFOV)
        Dim fovname = tsx_fov.OutString
        tsx_fov.Property(fovname, 0, theskyxLib.sk6MyFOVProperty.sk6MyFOVProp_PositionAngleDegrees)
        Dim fovPA As Double = tsx_fov.OutVar
        MsgBox("FOV Name: " + fovname + "  FOV PA: " + Str(fovPA))

        Return

    End Sub

End Module
