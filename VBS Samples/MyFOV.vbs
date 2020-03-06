' Windows VBScript Example: MyFOV
'
' ------------------------------------------------------------------------
'
'               Author: R.McAlister (2014)
'
' ------------------------------------------------------------------------
'
' This script demonstrates some of the functionality of the MyFOV class
'  
'
'Globals
'
'TSX Enumerations
sk6MyFOVProp_PositionAngleDegrees = 1

'Which FOV definition by index
iFOV = 0            'First FOV definition

'Welcome Window
intDoIt = MsgBox("This script demonstrates some usage of the MyFOV class." & vbCrLf & vbCrLf, _
	vbOKCancel & vbInformation, _
	"Windows Visual Basic Sample")
If intDoIt = vbCancel Then
	Return
End If

'Create FOV object
set tsx_fov = CreateObject("TheSkyX.Sky6MyFOVs")
set tsx_sc = CreateObject("TheSkyX.Sky6StarChart")

'First FOV definition, additional definitions at successive indicies
'Get FOV name and Postition Angle
tsx_fov.Name(iFOV)
fovname = tsx_fov.OutString
tsx_fov.Property fovname, 0, sk6MyFOVProp_PositionAngleDegrees
fovPA  = tsx_fov.OutVar
MsgBox("FOV Name: " & fovname & "  FOV PA: " & FormatNumber(fovPA))

