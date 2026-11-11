<%@ Control 
    AutoEventWireup="true" 
    CodeFile="UcCalendar.ascx.cs" 
    Inherits="UserControls.UcCalendar" 
    Language="C#" %>
    
<div style="border: Gray; border-width: thin; text-align: center">
    <br />
    <asp:HyperLink NavigateUrl="~/Medlemmer/Arrangement/View.aspx" runat="server">Se Arrangementer</asp:HyperLink>
    <br />
    <br />
    <asp:Calendar BorderStyle="None" ID="Calendar1" OnDayRender="Calendar1DayRender" OnPreRender="Calendar1PreRender" runat="server" Width="120px" />
    <br />
    <asp:Calendar BorderStyle="None" ID="Calendar2" OnDayRender="Calendar2DayRender" runat="server" Width="120px" />
    <br />
    <asp:Calendar BorderStyle="None" ID="Calendar3" OnDayRender="Calendar3DayRender" runat="server" Width="120px" />
</div>
<br />
<div align="center">
    <table style="font-size: 8px;">
        <tr>
            <td style="width: 10px; background-color: Gray;" />
            <td>Venter på svar!</td>
        </tr>
        <tr>
            <td style="width: 10px; background-color: DarkGreen;" />
            <td>Du deltager!</td>
        </tr>
        <tr>
            <td style="width: 10px; background-color: DarkRed;" />
            <td>Du deltager ikke!</td>
        </tr>
    </table>
</div>
