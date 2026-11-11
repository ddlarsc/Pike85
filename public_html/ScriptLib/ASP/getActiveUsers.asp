' global.asa file

<SCRIPT LANGUAGE="VBScript" RUNAT="Server">
Sub Application_OnStart
	Application("ActiveUsers") = 0
End Sub

Sub Session_OnStart
	Application.Lock
	Application("ActiveUsers") = Application("ActiveUsers") + 1
	Application.UnLock
End Sub

Sub Session_OnEnd
	Application.Lock
		Application("ActiveUsers") = Application("ActiveUsers") - 1
	Application.UnLock
End Sub
</SCRIPT>

' use this code in your scripts
<%=Application("Active")%>
