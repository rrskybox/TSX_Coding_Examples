' Windows VBScript Example: AutomatedSearch
'
' ------------------------------------------------------------------------
'               Adapted from AutomatedSearch.vbs  (Visual Basic Script)
'               Copyright (C) Software Bisque (199?)
'
'				Converted 2016, R.McAlister
'
' ------------------------------------------------------------------------
'
' This script reads an Exported Observing List file for targets
'   For each target, the telescope is slewed to the target and an image taken.
'
'Global settings
sYourUserName = "Rick" 'Change to current username to create correct save file location

'Use TheSky to generate a text file of mapping points
szPathToMapFile  = "C:\Users\" & sYourUserName & "\Documents\Software Bisque\TheSkyX Professional Edition\Exported Data\map.txt"

'Set the exposure time for the image
dExposure = 1.0

' ********************************************************************************
' *
' * Below is the flow of program execution
' * See the subroutine TargetLoop to see where the real work is done

'Create Objects
set tsx_tele = CreateObject("TheSkyX.Sky6RASCOMTele")
set tsx_cam = CreateObject("TheSkyX.ccdSoftCamera")
set tsx_util = CreateObject("TheSkyX.Sky6Utils")

'Connect Objects
tsx_tele.Connect()
tsx_cam.Connect()

'Run the target loop
TargetLoop()

'Disconnect objects
tsx_tele.Disconnect()
tsx_cam.Disconnect()


' ********************************************************************************
' *
' * Below are all the subroutines used in this sample
' *

Sub TargetLoop()

'Dim LineFromFile As String
'  Dim sname As String
'  Dim dAz As Double
'  Dim dAlt As Double
'  Dim iError As Boolean

'Open the observing list export file for targets
set MyFileObject = CreateObject("Scripting.FileSystemObject")
set MyFile = MyFileObject.GetFile(szPathToMapFile)
set MyFileStream = MyFile.OpenAsTextStream  'Stream object for Export Data text file

'Get the first line -- headers
LineFromFile = MyFileStream.ReadLine
'Exit if the file is empty
If LineFromFile = "" Then
	Return
End If

iRAindex = InStr(LineFromFile, "RA")
iDecindex = InStr(LineFromFile, "Dec")

Do

LineFromFile = MyFileStream.ReadLine
MsgBox("RA: " + Mid(LineFromFile, iRAindex, 13) + "  Dec: " + Mid(LineFromFile, iDecindex, 13))

sname = Left(LineFromFile, 12)
tsx_util.ConvertStringToRA(Mid(LineFromFile, iRAindex, 13))
dAz = tsx_util.dOut0
tsx_util.ConvertStringToDec(Mid(LineFromFile, iDecindex, 13))
dAlt = tsx_util.dOut0
'Slew to object

tsx_tele.SlewToAzAlt dAz, dAlt, sname

'Set exposure time and try for image, exit if error
tsx_cam.ExposureTime = dExposure
tsx_cam.AutoSaveOn = True

tsx_cam.TakeImage()

Loop

End Sub
