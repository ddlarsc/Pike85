<%

' Variable Declaration
Dim objFSO, objCountFile  ' object vars for FSO and File
Dim strCountFileName      ' filename of count text file
Dim iCount                ' count variable
Dim bUseImages            ' boolean whether or not to use images
Dim I                     ' standard looping var


' whether use images or plain text

bUseImages = CBool(Request.QueryString("images"))

strCountFileName = Server.MapPath(Request.ServerVariables("SCRIPT_NAME") & ".cnt")

' Create FileSystemObject to deal with file access
Set objFSO = Server.CreateObject("Scripting.FileSystemObject")

' Open the file as a text stream (1 = ForReading, True = Create)
Set objCountFile = objFSO.OpenTextFile(strCountFileName, 1, True)

' Read the current count from the file
If Not objCountFile.AtEndOfStream Then
	' Set value to contents of the file
	iCount = CLng(objCountFile.ReadAll)
Else
	' If no file exists or it's empty start at 0
	iCount = 0
End If

' Close the file and destroy the object
objCountFile.Close
Set objCountFile = Nothing


' Increment the count
iCount = iCount + 1

' Overwrite existing file and get a text stream to new one
Set objCountFile = objFSO.CreateTextFile(strCountFileName, True)

' Write updated count
objCountFile.Write iCount

' Close the file and destroy the object
objCountFile.Close
Set objCountFile = Nothing


' Destroy the FSO object
Set objFSO = Nothing


' to display the results

If bUseImages Then
	
	For I = 1 to Len(iCount)
		' Output the IMG tag using the right digit
		Response.Write "<img src=""./images/digit_"
		Response.Write Mid(iCount, I, 1)
		Response.Write ".gif"" alt="""
		Response.Write Mid(iCount, I, 1)
		Response.Write """ width=""20"" height=""27"" />"
	Next 'I
Else
	' show the variable
	Response.Write iCount
End If
%>

