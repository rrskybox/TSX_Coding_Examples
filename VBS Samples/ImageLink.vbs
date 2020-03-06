' Windows VBScript Sample: ImageLink
'
' ------------------------------------------------------------------------
'               Vaguely adapted from ImageLink.vbs  (Visual Basic Script)
'               Copyright (C) Software Bisque (2013)
'
'				Converted 2016, R.McAlister
'
' ------------------------------------------------------------------------
'
' This script demonstrates how to perform an image link on a photo.
'  The path to the FITS image is set to a user defined location.  In this case, I used
'   C:\Users\Rick\Documents\Software Bisque\TheSkyX Professional Edition\Camera AutoSave\Temp
'
' The application executes an ImageLink on the target file, then prints the RA/Dec coordinates.
'
'
'Username for directory -- change accordingly
sUserName = "Rick"

'FITS pathname
sPathName = "C:\Users\" & sUserName & "\Documents\Software Bisque\TheSkyX Professional Edition\Camera AutoSave\Temp\temp.fit"

'Global Parameters
dExposure  = 3.0 'seconds
ifilter  = 3  '4nd filter slot, probably clear/lumenscent

'Create camera object and connect
set tsx_il = CreateObject("TheSkyX.ImageLink")
set tsx_ilr = CreateObject("TheSkyX.ImageLinkResults")

'Set path to file
tsx_il.PathToFITS = sPathName

'Run ImageLink
tsx_il.execute()

'Check on result
If tsx_ilr.Succeeded = 0 Then
	MsgBox("Error: " + FormatNumber(tsx_ilr.ErrorCode) & "  " & tsx_ilr.errortext)
	Return
End If

'Print the image center location
MsgBox("RA: " + FormatNumber(tsx_ilr.imageCenterRAJ2000) + "  Dec: " + FormatNumber(tsx_ilr.imageCenterDecJ2000))


