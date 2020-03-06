' Windows VBScript: ListSearch
'
' ------------------------------------------------------------------------
'               Vaguely adapted from ErrorHandling.vbs  (Visual Basic Script)
'               Copyright (C) Software Bisque (2013)
'
'				Converted 2016, R.McAlister
'
' ------------------------------------------------------------------------
'
' This script demonstrates how to run the telescope through a list of targets as defined
'   by a list of names.
'
'  Note:  The gist of the orginal VBS script was entitled "ErrorHandling.vbs".  However, that
'  script, however labeled, performed the functions as adapted to VB herein.
'
'TSX Enumerations
sk6ObjInfoProp_ALT = 58
sk6ObjInfoProp_AZM = 59

'Set the exposure time for the image
dExposure = 1.0

'Target List
dim targetlist(6)
targetlist(1) = "NGC1348"
targetlist(2) = "NGC1491"
targetlist(3) = "NGC179"
targetlist(4) = "NGC1798"
targetlist(5) = "M39"
targetlist(6) = "NGC2165"

'Create objects
set objChrt = CreateObject("TheSkyX.Sky6StarChart")
set objTele = CreateObject("TheSkyX.Sky6RASCOMTele")
set objCam = CreateObject("TheSkyX.ccdSoftCamera")
set objUtil = CreateObject("TheSkyX.Sky6Utils")
set objInfo = CreateObject("TheSkyX.Sky6ObjectInformation")

'Connect Objects
objTele.Connect()
objCam.Connect()

For i = 1 to 6
	tname = targetlist(i)
	objChrt.Find(tname)
	objInfo.Property(sk6ObjInfoProp_ALT)
	dAlt = objInfo.ObjInfoPropOut
	objInfo.Property(sk6ObjInfoProp_AZM)
	dAz = objInfo.ObjInfoPropOut
	
	objTele.SlewToAzAlt dAz, dAlt, tname
	
	'Set exposure time and try for image, exit if error
	objCam.ExposureTime = dExposure
	
	objCam.TakeImage()
	Next

'Disconnect telescope and camera
objTele.Disconnect()
objCam.Disconnect()

