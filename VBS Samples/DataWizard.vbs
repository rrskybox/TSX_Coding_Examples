' Windows VBScript Sample: DataWizard
'
' ------------------------------------------------------------------------
'                          
'               Authored:  R.McAlister 2016
'
' ------------------------------------------------------------------------
'
'This script demonstrates the usage of the Data Wizard and
' Object Information classes.
'
'The application opens and runs a built-in Observing List search then accesses a few 
' of the return properties, for no particularly good reason.
'
'The application uses the standard query "Bright objects visible now.dbq"
'
'TSX Enumerations
sk6ObjInfoProp_NAME1 = 0
sk6ObjInfoProp_RA_2000 = 56
sk6ObjInfoProp_DEC_2000 = 57

'Username for directory
sUserName = "Rick"  'Ohter realtime ways to do this as well...

sname = ""

intDoIt = MsgBox("This script demonstrates how to use the Data Wizard class." & vbCrLf & vbCrLf, _
vbOKCancel & vbInformation, _
"Windows Visual Basic Sample")
If intDoIt = vbCancel Then
	Return
	End If

'Create object information and datawizard objects
set tsx_dw = CreateObject("TheSkyX.Sky6DataWizard")
'set tsx_oi = CreateObject("TheSkyX.Sky6ObjectInformation")

'Set query path to a user-defined test query database for this example.  If you don't have it, make it.  Best if the Messier catalog is selected.
tsx_dw.Path = "C:\Users\" & sUserName & "\Documents\Software Bisque\TheSkyX Professional Edition\Database Queries\Bright objects visible now.dbq"
tsx_dw.Open()
set tsx_oi = tsx_dw.RunQuery
'

'tsx_oi is an array of object information indexed by the tsx_oi.Index property
'
'For each object information in the list, get the name, perform a "Find" and look for the catalog ID.  If there is one, print it.
For i = 0 to To tsx_oi.Count - 1
	tsx_oi.Index = i
	tsx_oi.Property(sk6ObjInfoProp_NAME1)
	sname = tsx_oi.ObjInfoPropOut
	tsx_oi.Property(sk6ObjInfoProp_RA_2000)
	sRA = tsx_oi.ObjInfoPropOut
	tsx_oi.Property(sk6ObjInfoProp_DEC_2000)
	sDec = tsx_oi.ObjInfoPropOut
	MsgBox("Name: " & sname & "   RA: " & sRA & "  Dec:  " & sDec)
	Next
