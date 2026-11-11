<%@ Page 
    AutoEventWireup="true" 
    CodeFile="~/Klubben/Medlemmer.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Klubben.Medlemmer" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Klubben.master" 
    RuntimeMasterPageFile="~/Skabelon/Klubben.master" 
    Title="Pike 85 Klubben Medlemmer" %>

<asp:Content ID="ContentMainPage" ContentPlaceHolderID="KlubbenBody" runat="Server">
    <table>
        <tr>
            <td class="indhold" style="vertical-align: top; text-align:left">
                <h4 style="text-align: center">Pike 85 medlemmer</h4>
                <div style="text-align: center">
                <img alt="Fælles billed af Pike 85 medlemmerne" src="../Billeder/Medlemmer/Faelles_billed_alle_mand.jpg" width="550" />
                </div>
                <span id="medlemmer" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
