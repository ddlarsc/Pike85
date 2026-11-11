<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Generalforsamling.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Klubben.Medlemmer.Generalforsamling" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Klubben.master" 
    Title="Referater af generalforsamlinger" %>
    
<%@ Reference Control="~/UserControls/UcGeneralforsamling.ascx" %>

<asp:Content ID="ContentMainPage" ContentPlaceHolderID="KlubbenBody" runat="Server">
    <table id="tblReferat" style="text-align: center; width: 300px">
        <tr>
            <td><h3>Generalforsamlinger</h3></td>
        </tr>
        <tr>
            <td><asp:PlaceHolder ID="phUcGeneralforsamling" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblMessage" runat="server" /></td>
        </tr>
    </table>
</asp:Content>