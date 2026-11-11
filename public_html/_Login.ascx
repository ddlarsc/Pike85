<%@ Control Language="c#" Inherits="UserControls.Login" CodeFile="Login.ascx.cs" %>
<p>&nbsp;</p>
<div style="text-align: right">
    <asp:Label ID="Label_Fejl" runat="server" Font-Names="Verdana" Font-Size="10px" Font-Bold="True"></asp:Label>
    <asp:Label ID="Label_BrugerId" runat="server" Font-Names="Verdana" Font-Size="10px">BrugerId</asp:Label>
    <asp:TextBox ID="TextBox_Login" runat="server" Width="50px" Font-Names="Verdana" Font-Size="10px" TabIndex="101"></asp:TextBox><br />
    <asp:Label ID="Label_Kodeord" runat="server" Font-Names="Verdana" Font-Size="10px">Kodeord</asp:Label>
    <asp:TextBox ID="TextBox_Kodeord" runat="server" Width="50px" TextMode="Password" Font-Names="Verdana" Font-Size="10px" TabIndex="102"></asp:TextBox><br />
</div>
<div style="text-align: center">
    <asp:LinkButton ID="LinkButton_Login" runat="server" OnClick="LinkButtonLoginClick" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" ForeColor="#CD0000" TabIndex="103">Login</asp:LinkButton>
    <asp:LinkButton ID="LinkButton_Logud" runat="server" Visible="False" OnClick="LinkButtonLogudClick" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" ForeColor="#CD0000" CausesValidation="false" TabIndex="104">Logud</asp:LinkButton></div>
