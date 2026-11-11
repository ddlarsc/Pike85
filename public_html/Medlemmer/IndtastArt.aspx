<%@ Page 
    AutoEventWireup="true" 
    CodeFile="IndtastArt.aspx.cs" 
    Inherits="Medlemmer.IndtastArt" 
    Language="C#"
    MasterPageFile="~/Skabelon/Medlem.master" %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentIndtastArt" runat="server">
    <table width="600px">
        <tr>
            <td style="width: 150px"></td>
            <td style="text-align: center; width: 300px"><h4>Indtast / Rediger / Slet Art</h4></td>
            <td style="width: 150px"></td>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:DropDownList ID="drpArt" runat="server" Width="300px" TabIndex="50" AutoPostBack="True" onselectedindexchanged="DrpOpslagSelectedIndexChanged" /></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3"><br/><hr/><br/></td>
        </tr>
        <tr>
            <td style="text-align: left">Id:</td>
            <td style="text-align: left"><asp:Textbox Enabled="false" ID="txtArtId" runat="server" Width="51" TabIndex="49" /></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Art:</td>
            <td><asp:Textbox ID="txtArtNavn" runat="server" Width="300px" TabIndex="52" /></td>
            <td style="text-align: left"><asp:RequiredFieldValidator ID="rfvArtNavn" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="3"><br/></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnInsert" runat="server" Width="300px" onclick="BtnInsertClick" TabIndex="53" Text="Opret" /></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnUpdate" runat="server" Width="300px" onclick="BtnUpdateClick" TabIndex="54" Text="Gem" /></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnDelete" runat="server" Width="300px" onclick="BtnDeleteClick" TabIndex="55" Text="Slet" /></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3"><br/></td>
        </tr>
        <tr style="text-align: center">
            <td colspan="3"><asp:Label ID="lblMessage" runat="server" /></td>
        </tr>
        <tr style="text-align: center">
            <td colspan="3"><asp:ValidationSummary ID="vsMessage" runat="server"/></td>
        </tr>
    </table>
</asp:Content>