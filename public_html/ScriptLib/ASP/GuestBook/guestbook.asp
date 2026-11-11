<%  response.buffer=true  %> 

<%  
'declaring all database variables and opening connections  

Set dc = Server.CreateObject("ADODB.Connection")  
Set rs = Server.CreateObject("ADODB.Recordset")  

dc.Open "DBQ=" & Server.Mappath("database") & ";Driver={Microsoft Access Driver (*.mdb)};"  
%>

<%  
' this script adds a record to database  

openMethod = request.form ("openMethod")  
if openMethod = "postMessage" then  'this line checks if the ASP file should post a message 

USER_DATE = FormatDateTime(NOW)  'records the post date and time
USER_NAME = request.Form ("USER_NAME")   
USER_CITY = request.Form ("USER_CITY")   
USER_MAIL = request.Form ("USER_MAIL")   
USER_URL = request.Form ("USER_URL")   
USER_MESSAGE = request.Form ("USER_MESSAGE")  

if (USER_NAME = "" or USER_MESSAGE = "" or USER_MAIL = "") then  
	response.write ("<font size=+2>NAME, MAIL and MESSAGE fields must be filled!</font><br>") & VBCRLF  
End IF

if USER_CITY = "" then USER_CITY = "nowhere" End If  
if USER_URL = "" then USER_URL = "none" End If  

if len(USER_MESSAGE) > 4096 then USER_MESSAGE = Left(USER_MESSAGE, 4096)  End If 'this sets the message size limit to 4 kb: if it's more then 4kb, a part of it will be erased  
if USER_URL <>  "" then if Left(UCase(USER_URL),7) <> "HTTP://" then USER_URL = "http://" & USER_URL End IF  'if user writes his url as "www.someurl.com" then it's replaced with "http://www.someurl.com"
 
MYSQL = "SELECT GBOOK.* FROM GBOOK"  'selects the guestbook table in the database   

if NOT (USER_NAME = "" or USER_MESSAGE = "" or USER_MAIL = "") then  'if all fields are valid, adds a record to the DB
   
rs.Open MYSQL, dc, 1, 3    'opens the database

rs.AddNew  
rs.Fields("USER_DATE") = USER_DATE  
rs.Fields("USER_NAME") = USER_NAME  
rs.Fields("USER_CITY") = USER_CITY  
rs.Fields("USER_MAIL") = USER_MAIL  
rs.Fields("USER_URL") = USER_URL  
rs.Fields("USER_MESSAGE") = USER_MESSAGE  
rs.Update  

'this code saves your name, mail, url and city as a cookie,
' so that users don't have to enter them the next time they use the guestbook  
response.Cookies("server")("name") = USER_NAME
response.Cookies("server")("email") = USER_MAIL
response.Cookies("server")("URL") = USER_URL
response.Cookies("server")("city") = USER_CITY
response.Cookies("server").Expires = DateAdd("m",1,Date)

response.redirect ("guestbook.asp") 'redirects to the guestbook page

End If

End If
%>  

<form action="guestbook.asp" method="post">  
	<input type=hidden name=openMethod value=postMessage>  
<table align=center border=0 cellspacing=0 cellpadding=0>  
	<tr>  <td>Name*:</td><td align=right width=300><input size=35 type=text name=USER_NAME value="<%=request.Cookies("server")("name")%>"</td>  </tr>  
	<tr>  <td>E-Mail*:</td><td align=right><input size=35 type=text name=USER_MAIL value="<%=request.Cookies("server")("email")%>"></td>  </tr>  
	<tr>  <td>City:</td><td align=right><input size=35 type=text name=USER_CITY value="<%=request.Cookies("server")("city")%>"></td> 	</tr>  
	<tr>  <td>URL:</td><td align=right><input size=35 type=text name=USER_URL value="<%=request.Cookies("server")("URL")%>"></td>  </tr>  
	<tr>  <td valign=top>Message*:</td><td align=right><textarea cols=30 rows=4 name=USER_MESSAGE></textarea></td>  </tr>  
	<tr>  <td>&nbsp;</td><td height=50 valign=bottom align=right><input type=submit value="Go on, Post it!">&nbsp;<input type=reset value="Reset it, plz"></td>  </tr>  
</table>  

</form>  

<%  
MYSQL = "SELECT ID, USER_DATE, USER_NAME, USER_CITY, USER_MAIL, USER_URL, USER_MESSAGE FROM GBOOK ORDER by ID DESC"  

rs.Open MySQL, dc, 3, 1 
%>  

<%    
'this is the records counter and navigation stuff

currentPage = request.queryString ("id")  'check what page to dysplay (this is used for nav)
If currentPage = "" then  
	currentPage = "1"  
End If  

pageSize = 10   'pageSize is the ammout of records per page to be dysplayed 

Records = rs.RecordCount			 'this is the total ammount of records in the DB

if NOT (Records = 0) then  			 'if there are no records in the DB, server may report an error.  I used the IF structure to prevent this error.  
	rs.pageSize = pageSize 
	rs.AbsolutePage = currentPage  		 'absolutePage is the page to be dysplayed - don't change it: it depeds on the user's choice  
End If

pageNext = currentPage + 1
pagePrev = currentPage - 1
Pages = Records \ pageSize  
if Records mod pageSize then 
Pages = Pages + 1
End If

%>

<table width=100% border=0 cellspacing=0 cellpadding=0>
	<tr>	
		<td valign=top align=left width=50%>Pages:&nbsp; <% if NOT pagePrev < 1 then %><a href=gbook.asp?id=<% = pagePrev %>>Prev</a> <% else %> Prev <% End If %> | <%  While NOT pageNumber = Pages pageNumber = pageNumber + 1  %>  <% if CInt (pageNumber) = CInt (currentPage)  then %>  <% = pageNumber %> |  <% else %>   <a href=gbook.asp?id=<% = pageNumber %>> <% = pageNumber %> </a> |  <% End If %>  <% Wend %>  <% if NOT pages < pageNext then %> <a href=gbook.asp?id=<% = pageNext %>>Next</a> <% else %> Next <% End If %> </td>  
		<td valign=top align=right width=50%>Total entries:&nbsp;<% = Records %></td>
	</tr>

</table>  

<%  
if NOT (CInt(Records) = 0) then  'if there are no records in the database it doesn't go on dysplaying then and only sais that there are no records  

Do While (Not rs.EOF) and (currentRecords < pageSize)  'runs through the records and dysplays them
currentRecords = currentRecords + 1  'counts currently dysplayed records
%>

	<hr noshade>  

	<table width=100% border=0 cellspacing=0 cellpadding=0>  
		<tr>  
			<td valign=top align=left width=50%>Name:&nbsp;<%= rs("USER_NAME") %>,&nbsp;&nbsp;City:&nbsp;<%= rs("USER_CITY") %><br>E-mail:&nbsp;<a href=mailto:<%= rs("USER_MAIL")%>><%= rs("USER_MAIL")%></a><br>URL:&nbsp;<a href=<%= rs("USER_URL") %>><%= rs("USER_URL") %></a></td>  
			<td valign=top align=right width=50%>Posted:&nbsp;<%= rs("USER_DATE") %></td>  
		</tr>  
	</table>

	<P align=justify>Message:&nbsp;<%= rs("USER_MESSAGE") %></P>  

<%  
	rs.MoveNext  
	Loop  
else   
	response.write ("<P align=center><font size=+1>No records found, be the first to post a comment!</font></center><br>") & VBCRLF  
End If
%>  

<%  
'closing connections and resetting all database variables  

rs.Close
Set rs = Nothing
dc.Close
Set dc = Nothing
%>  
