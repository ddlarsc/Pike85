<%@ Page 
    AutoEventWireup="true" 
    CodeFile="IndtastLink.aspx.cs" 
    Inherits="Medlemmer.MedlemmerIndtastLink" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    Title="Opret/Indtast/Slet links" %>

<asp:Content ID="Link" ContentPlaceHolderID="MedlemBody" Runat="Server">
    <table width="600px">
        <tr>
            <td style="width: 150px"></td>
            <td style="text-align: center; width: 300px"><h4>Indtast / Rediger / Slet link</h4></td>
            <td style="width: 150px"></td>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:DropDownList ID="drpLink" runat="server" Width="300px" TabIndex="50" AutoPostBack="True" onselectedindexchanged="DrpLinkSelectedIndexChanged" /></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3"><br/><hr/><br/></td>
        </tr>
        <tr>
            <td style="text-align: left">Id:</td>
            <td style="text-align: left">
                <asp:Textbox Enabled="false" ID="txtId" runat="server" Width="50px" TabIndex="48" />
                <asp:Textbox Enabled="False" ID="txtForfatter" runat="server" style="margin-bottom: 0px" TabIndex="49" Width="246px" />
            </td>
            <td></td>            
        </tr>
        <tr>
            <td style="text-align: left">Navn på link:</td>
            <td><asp:Textbox ID="txtNavn" runat="server" Width="300px" TabIndex="53" /></td>
            <td style="text-align: left"><asp:RequiredFieldValidator ID="rfvNavn" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align: left">Adresse: (http://)</td>
            <td><asp:Textbox ID="txtAdresse" runat="server" Width="300px" TabIndex="54" /></td>
            <td style="text-align: left"><asp:RequiredFieldValidator ID="rfvAdresse" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top">Beskrivelse:</td>
            <td><asp:Textbox ID="txtBeskrivelse" runat="server" Rows="5" TextMode="MultiLine" Width="300px" TabIndex="55" /></td>
            <td style="text-align: left"><asp:RequiredFieldValidator ID="rfvBeskrivelse" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align: left">Kategori:</td>
            <td><asp:DropDownList ID="drpKategori" runat="server" Width="300px" TabIndex="56" /></td>
            <td style="text-align: left"><asp:RequiredFieldValidator ID="rfvKategori" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align: left">Land:</td>
            <td><asp:DropDownList ID="drpLand" runat="server" Width="300px" TabIndex="57" /></td>
            <td style="text-align: left"><asp:RequiredFieldValidator ID="rfvLand" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align: left">Billed:</td>
            <td><asp:FileUpload ID="fuBilled" runat="server" TabIndex="58" Width="300px"/></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td style="text-align: center">
                <div style="border-style: solid; border-width: thin; border-color: InactiveCaption; text-align: center; padding: 2px">
                    <asp:RadioButton ID="rbnBilledLiggende" runat="server" Checked="True" Text="Liggende" GroupName="grpBilled1" TabIndex="59" />&nbsp;
                    <asp:RadioButton ID="rbnBilledStaaende" runat="server" Text="Stående" GroupName="grpBilled1" TabIndex="60" />
                </div>
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3"><br/></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnInsert" runat="server" Width="300px" onclick="BtnInsertClick" TabIndex="61" Text="Opret" /></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnUpdate" runat="server" Width="300px" onclick="BtnUpdateClick" TabIndex="62" Text="Gem" /></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnDelete" runat="server" Width="300px" onclick="BtnDeleteClick" TabIndex="63" Text="Slet" /></td>
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

