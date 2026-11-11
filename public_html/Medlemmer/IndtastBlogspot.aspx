<%@ Page 
    AutoEventWireup="true" 
    CodeFile="IndtastBlogspot.aspx.cs" 
    Inherits="Medlemmer.MedlemmerIndtastBlogspot" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    Title="Blogspot setup" %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentIndtastBlogspot" Runat="Server">
    <table style="width:600px">
        <tr>
            <td style="width:150px" />
            <td style="width:300px"><h4 style="text-align: center">Indtast / Rediger / Slet Blogspot</h4></td>
            <td style="width:150px" />
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td />
            <td><asp:DropDownList AutoPostBack="True" ID="drpSelect" onselectedindexchanged="DrpSelectedIndexChanged" runat="server" TabIndex="40" Width="100%" /></td>
            <td />
        </tr>
        <tr>
            <td colspan="3"><br /><hr /><br /></td>
        </tr>
        <tr>
            <td style="text-align: left">Id:</td>
            <td style="text-align: left">
                <asp:Textbox Enabled="false" ID="txtId" runat="server" Width="50px" TabIndex="48" />
                <asp:Textbox Enabled="False" ID="txtUser" runat="server" style="margin-bottom: 0px" TabIndex="49" Width="246px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Dato:</td>
            <td><asp:Textbox Enabled="False" ID="txtDate" runat="server" TabIndex="52" Width="100%" /></td>
            <td />
        </tr>
        <tr>
            <td style="text-align:left">Brugernavn :</td>
            <td><asp:TextBox ID="txtUsername" MaxLength="50" runat="server" TabIndex="53" Width="100%" /></td>
            <td><asp:RequiredFieldValidator ID="rfvUsername" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align:left">Kode :</td>
            <td><asp:TextBox ID="txtPassword" MaxLength="50" runat="server" TabIndex="54" TextMode="Password" Width="100%" /></td>
            <td><asp:RequiredFieldValidator ID="rfvPassword" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align:left">Link/URL :</td>
            <td><asp:TextBox ID="txtUrl" MaxLength="100" runat="server" TabIndex="55" Width="100%" /></td>
            <td>
                <asp:RequiredFieldValidator ID="rfvUrl" runat="server" />
                <asp:RegularExpressionValidator ID="revUrl" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="3"><br/></td>
        </tr>
        <tr>
            <td />
            <td><asp:Button ID="btnInsert" onclick="BtnInsertClick" runat="server" TabIndex="60" Text="Opret" Width="300px" /></td>
            <td />
        </tr>
        <tr>
            <td />
            <td><asp:Button ID="btnUpdate" onclick="BtnUpdateClick" runat="server" TabIndex="61" Text="Gem" Width="300px" /></td>
            <td />
        </tr>
        <tr>
            <td />
            <td><asp:Button ID="btnDelete" onclick="BtnDeleteClick" runat="server" TabIndex="62" Text="Slet" Width="300px" /></td>
            <td />
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td />
            <td>
                <asp:Label ID="lblMessage" runat="server" Width="100%" />
                <br />
                <asp:ValidationSummary ID="vsMessage" runat="server" Width="100%" />
            </td>
            <td />
        </tr>
    </table>
</asp:Content>

