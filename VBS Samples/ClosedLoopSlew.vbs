' Windows Visual Basic Script: Closed Loop Slew
'
' ------------------------------------------------------------------------
'               Adapted from Cam.vbs  (Visual Basic Script)
'               Copyright (C) Software Bisque (2013)
'
'				Converted 2016, R.McAlister
'
' ------------------------------------------------------------------------
'
' This script demonstrates the key elements of how to take images 
'	using the primary camera.

'TSX Enumeration
cdLight = 1
cdAutoDark = 3

'Set connection to star chart and perform a find on M39
set tsx_sc = CreateObject("TheSkyX.Sky6StarChart")
tsx_sc.Find("M39")

'Create the camera object
set tsx_cam = CreateObject("TheSkyX.ccdSoftCamera")

'Connect TSX to the camera
tsx_cam.Connect()

'Create closed loop slew object
set tsx_cls = CreateObject("TheSkyX.ClosedLoopSlew")

'Set the exposure time
tsx_cam.ExposureTime = 10.0
'SEt an exposure delay
tsx_cam.Delay = 5.0
'Set a frame type
tsx_cam.Frame = cdLight
'Set for autodark
tsx_cam.ImageReduction = cdAutoDark
'Set for synchronous imaging (this app will wait until done or error)
tsx_cam.Asynchronous = False
'Take image
iCamStatus = tsx_cam.TakeImage()
If iCamStatus <> 0 Then
MsgBox("Camera Error: " & FormatNumber(iCamStatus))
End If

'Execute
 set clsstat = tsx_cls.exec()
 if clsstat <> 0 then
 MsgBox("Closed Loop Slew failure: " + FormatNumber(tsx_cls.lastError))
