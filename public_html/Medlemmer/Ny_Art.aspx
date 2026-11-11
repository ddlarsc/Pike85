<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Ny_Art.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Medlemmer.NyArt" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    RuntimeMasterPageFile="~/Skabelon/Medlem.master" 
    Title="Medlem Ny Art Side" %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentMedlemNyArt" runat="Server">
    <h4 style="text-align: center">Opret ny fiskeart</h4>
    <table id="Table1" style="text-align: left; border-width: 0; padding: 1; border-spacing: 1" width="100%">
        <tr>
            <td style="width: 122px; height: 25px">
                Navn på ny art :
            </td>
            <td style="height: 25px">
                <asp:TextBox Font-Names="Verdana" ID="TextBox_ArtNavn" MaxLength="50" runat="server" TabIndex="1" Width="232px" />
                <asp:RequiredFieldValidator ControlToValidate="TextBox_ArtNavn" ErrorMessage="Navn på art skal udfyldes" Font-Names="Verdana" runat="server">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 122px">
                <asp:Button OnClick="ButtonOpretArtClick" runat="server" TabIndex="2" Text="Opret art" />
            </td>
            <td>
                <asp:Label ID="Label_Status" runat="server" Font-Names="Verdana" />
            </td>
        </tr>
        <tr>
            <td style="height: 6px" colspan="2">
                <p align="left">&nbsp;</p>
            </td>
        </tr>
    </table>
</asp:Content>
