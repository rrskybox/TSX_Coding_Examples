Module ImageLink

    ' Windows Visual Basic Sample Console Application: ImageLink
    '
    ' ------------------------------------------------------------------------
    '               Vaguely adapted from ImageLink.vbs  (Visual Basic Script)
    '               Copyright (C) Software Bisque (2013)
    '
    '				Converted 2015, R.McAlister
    '
    ' ------------------------------------------------------------------------
    '
    ' This Visual Basic console application demonstrates how to perform an image link on a photo.
    '  The path to the FITS image is set to a user defined location.  In this case, I used
    '   C:\Users\Rick\Documents\Software Bisque\TheSkyX Professional Edition\Camera AutoSave\Temp
    '
    ' The application executes an ImageLink on the target file, then prints the RA/Dec coordinates.
    '
    'FITS pathname
    Public PathName = "C:\Users\Rick\Documents\Software Bisque\TheSkyX Professional Edition\Camera AutoSave\Temp\temp.fit"

    'Global Parameters
    Public dExposure As Double = 3.0 'seconds
    Public ifilter As Integer = 3  '4nd filter slot, probably clear/lumenscent

    'Global TSX COM Objects
    Public tsx_pc As Object
    Public tsx_il As Object
    Public tsx_ilr As Object


    Sub Main()

        If Not Welcome() Then
            Return
        End If

        'Create camera object and connect
        tsx_il = CreateObject("TheSkyX.ImageLink")
        tsx_ilr = CreateObject("TheSkyX.ImageLinkResults")

        'Set path to file
        tsx_il.PathToFITS = PathName

        'Run ImageLink
        tsx_il.execute()

        'Check on result
        If tsx_ilr.Succeeded = 0 Then
            MsgBox("Error: " + Str(tsx_ilr.ErrorCode) + "  " + tsx_ilr.errortext)
            Return
        End If

        'Print the image center location
        MsgBox("RA: " + Str(tsx_ilr.imageCenterRAJ2000) + "  Dec: " + Str(tsx_ilr.imageCenterDecJ2000))

        'Clean up
        tsx_il = Nothing
        tsx_ilr = Nothing
        Return

    End Sub

    Function Welcome() As Boolean

        ' Returns True if user wants to continue, False is abort

        Dim intDoIt = MsgBox("This script demonstrates how to execute ImageLink and ImageLinkResults." + vbCrLf + vbCrLf,
                            vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return (False)
        End If

        Return (True)
    End Function

End Module