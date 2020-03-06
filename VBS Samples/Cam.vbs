' Windows VBScript Example: Cam
'
' ------------------------------------------------------------------------
'               Adapted from Cam.vbs  (Visual Basic Script)
'               Copyright (C) Software Bisque (2013)
'
'				Converted 2014, R.McAlister
'
' ------------------------------------------------------------------------
'
' This script demonstrates the key elements of how to take images using the primary camera.
'
'TSX Enumeration
cdLight = 1
cdAutoDark = 3

'Create the camera object
set tsx_cam = CreateObject("TheSkyX.ccdSoftCamera")

'Connect TSX to the camera
tsx_cam.Connect()

'Set the exposure time
tsx_cam.ExposureTime = 1.0
'SEt an exposure delay
tsx_cam.Delay = 0.0
'Set a frame type
tsx_cam.Frame = cdLight
'Set for autodark
tsx_cam.ImageReduction = cdAutoDark
'Set for synchronous imaging (this app will wait until done or error)
tsx_cam.Asynchronous = False
'Take image
iCamStatus = tsx_cam.TakeImage()
If iCamStatus <> 0 Then
Msgbox ("Camera Error: " & FormatNumber(iCamStatus))
End If
'Disconnect Camera

set tsx_img = CreateObject("TheSkyX.ccdSoftImage")
tsx_img.AttachToActive()

tsx_img.Save()

tsx_cam.Disconnect()

