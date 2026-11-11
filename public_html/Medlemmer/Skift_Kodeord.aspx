<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Skift_Kodeord.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Medlemmer.SkiftKodeord" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    RuntimeMasterPageFile="~/Skabelon/Medlem.master" 
    Title="Medlem Skift Kodeord" %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentMedlemSkiftKodeord" runat="Server">
    <h4 style="text-align: center">Skift kodeord</h4>
    <table id="Table1" style="border-width: 1; padding: 1; text-align: left; padding: 1" width="100%">
        <tr>
            <td style="height: 19px" width="150">
                Bruger :
            </td>
            <td style="height: 19px">
                <asp:Label ID="Label_Bruger" runat="server" />
                <asp:DropDownList ID="DropDownList_BrugerId" runat="server" TabIndex="1" />
            </td>
        </tr>
        <tr>
            <td style="height: 19px" width="150">
                Tast gamle kodeord :
            </td>
            <td style="height: 19px">
                <asp:TextBox Font-Names="Verdana" ID="Textbox_GamleKodeord" MaxLength="50" runat="server" TabIndex="2" TextMode="Password" Width="272px" />
                <asp:RequiredFieldValidator ControlToValidate="Textbox_GamleKodeord" ErrorMessage="'Gammelt kodeord' skal udfyldes" Font-Names="Verdana" ID="Requiredfieldvalidator3" runat="server">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="150" style="height: 25px">
                Nyt kodeord :
            </td>
            <td style="height: 25px">
                <p align="left">
                    <asp:TextBox Font-Names="Verdana" ID="TextBox_NytKodeord" MaxLength="50" runat="server" TabIndex="3" TextMode="Password" Width="272px" />
                    <asp:RequiredFieldValidator ControlToValidate="TextBox_NytKodeord" ErrorMessage="'Nyt kodeord' skal udfyldes" Font-Names="Verdana" runat="server">*</asp:RequiredFieldValidator>
                </p>
            </td>
        </tr>
        <tr>
            <td width="150">
                Nyt kodeord igen :
            </td>
            <td>
                <p align="left">
                    <asp:TextBox Font-Names="Verdana" ID="TextBox_NytKodeordIgen" MaxLength="50" runat="server" TabIndex="4" TextMode="Password" Width="272px" />
                    <asp:RequiredFieldValidator ControlToValidate="TextBox_NytKodeordIgen" ErrorMessage="'Nyt kodeord igen' skal udfyldes" Font-Names="Verdana" runat="server">*</asp:RequiredFieldValidator>
                </p>
            </td>
        </tr>
        <tr>
            <td width="150">
            </td>
            <td>
                <p>
                    <asp:Button OnClick="ButtonSkiftKodeordClick" runat="server" TabIndex="5" Text="Skift kodeord"/>
                </p>
                <asp:ValidationSummary runat="server" Width="496px" />
                <asp:Label Font-Names="Verdana" ID="Label_Status" runat="server" />
                <asp:CompareValidator ControlToCompare="TextBox_NytKodeord" ControlToValidate="TextBox_NytKodeordIgen" ErrorMessage="Kodeord er ikke ens, prøv igen" runat="server"/>
                <p>
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
