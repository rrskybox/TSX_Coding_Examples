Module PierSide

    ' Windows Visual Basic Sample Console Application: PierSide
    '
    ' ------------------------------------------------------------------------
    '               Original Example

    '			    Developed 2015, R.McAlister
    '
    ' ------------------------------------------------------------------------
    '
    ' This Visual Basic console application reads the pier side position of the current
    '   telescope and shows the value.

    Sub Main()

        Dim tsx_tele = CreateObject("theskyx.sky6RASCOMTele")

        tsx_tele.DoCommand(11, "")
        Dim pierstring = tsx_tele.DoCommandOutput

        MsgBox("Pier Side: " + pierstring)
	Return
    End Sub

End Module
