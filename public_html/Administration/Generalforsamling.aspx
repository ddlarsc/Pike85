<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Generalforsamling.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Administration.Admin" 
    Language="C#" 
    MasterpageFile="~/Skabelon/Klubben.master" 
    Title="Gem referater fra generalforsamling"%>

<asp:Content ContentPlaceHolderID="KlubbenBody" ID="ContentMainPage" runat="server">
    <table id="tblSave" style="border-spacing: 2px" width="600px">
        <tr style="text-align: center">
            <td style="width: 150px"/>
            <td style="width: 300px"><h4>Opret referat / Rediger referat</h4></td>
            <td style="width: 150px"/>
        </tr>
        <tr style="text-align: left">
            <td/>
            <td><asp:DropDownList AutoPostBack="True" ID="drpGetReferat" onselectedindexchanged="DrpGetReferatSelectedIndexChanged" runat="server" TabIndex="50" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td colspan="3"><br/><hr/><br/></td>
        </tr>
        <tr style="text-align: left">
            <td>Overskrift:</td>
            <td><asp:TextBox ID="txtHeader" runat="server" TabIndex="51" Width="300px" /></td>
            <td><asp:RequiredFieldValidator ID="rfvHeader" runat="server" /></td>
        </tr>
        <tr style="text-align: left">
            <td>Tekst:</td>
            <td><asp:TextBox ID="txtBody" Rows="4" runat="server" TabIndex="52" TextMode="MultiLine" Width="300px" /></td>
            <td><asp:RequiredFieldValidator ID="rfvBody" runat="server" /></td>
        </tr>
        <tr style="text-align: left">
            <td>Link:</td>
            <td><asp:FileUpload ID="fuLink" runat="server" TabIndex="53" Width="300px" /></td>
            <td><asp:Label ID="lblLink" runat="server" Visible="false" /></td>
        </tr>
        <tr style="text-align: left">
            <td>Link tekst:</td>
            <td><asp:TextBox ID="txtLinkText" runat="server" TabIndex="54" Width="300px" /></td>
            <td><asp:RequiredFieldValidator ID="rfvLinkText" runat="server" /></td>
        </tr>
        <tr style="text-align: left">
            <td>Efterskrift:</td>
            <td><asp:TextBox ID="txtFooter" runat="server" TabIndex="55" Width="300px" /></td>
            <td><asp:RequiredFieldValidator ID="rfvFooter" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="3"><br/></td>
        </tr>
        <tr>
            <td/>
            <td><asp:Button ID="btnCreate" onclick="BtnCreateClick" runat="server" TabIndex="56" Text="Opret" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td/>
            <td><asp:Button ID="btnUpdate" onclick="BtnUpdateClick" runat="server" TabIndex="57" Text="Gem" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td/>
            <td><asp:Button ID="btnDelete" onclick="BtnDeleteClick" runat="server" TabIndex="58" Text="Slet" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td colspan="3"><br/><asp:Label ID="lblPrimaryKey" runat="server" Visible="false"/></td>
        </tr>
        <tr>
            <td colspan="3"><asp:ValidationSummary ID="vsGeneralforsamling" runat="server" /></td>
        </tr>
    </table>
    <br/>
    <asp:Label ID="lblMessage" runat="server" Width="100%" />
</asp:Content>