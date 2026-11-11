<%@ Page 
    AutoEventWireup="true" 
    CodeFile="IndtastArrangement.aspx.cs" 
    Inherits="Medlemmer.IndtastArrangement" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    Title="Tilmelding til arrangementer" %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentTilmeldArrangementer" Runat="Server">
    <table width="600px">
        <tr>
            <td style="width:150px"></td>
            <td style="width:300px"><h4 style="text-align: center">Indtast / Rediger / Slet Arrangement</h4></td>
            <td style="width:150px"></td>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:DropDownList ID="drpSelectArrangement" runat="server" Width="100%" onselectedindexchanged="DrpArrangementSelectedIndexChanged" AutoPostBack="True" TabIndex="1" /></td>
            <td align="left"><asp:Label ID="lblUniqueKey" runat="server" Visible="false"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3"><br/><hr /><br/></td>
        </tr>
        <tr>
            <td></td>
            <td align="center">
                <div style="border: thin solid #9999FF; width:227px; padding-left:70px; text-align:left ">
                    <asp:RadioButtonList CausesValidation="false" Enabled="true" ID="rblAnticipate" runat="server" TabIndex="10" TextAlign="Right" Visible="true" onselectedindexchanged="RblAnticipateSelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Enabled="true" Selected="False" Text="Jeg deltager." Value="0" />
                        <asp:ListItem Enabled="true" Selected="False" Text="Jeg deltager IKKE." Value="1" />
                    </asp:RadioButtonList>
                </div>
            </td>
            <td></td>
        </tr>
        <tr>
            <td align="left" valign="top">Jeg deltager fra:</td>
            <td><asp:DropDownList ID="drpArrivalDate" runat="server" Width="100%" /></td>
            <td><asp:RequiredFieldValidator ID="rfvldArrivalDate" runat="server" /></td>
        </tr>
        <tr>
            <td align="left" valign="top">Jeg ankommer:</td>
            <td><asp:DropDownList ID="drpArrivalOnDay" runat="server" Width="100%" /></td>
            <td><asp:RequiredFieldValidator ID="rvArrivalOnDay" runat="server" /></td>
        </tr>
        <tr>
            <td align="left" valign="top">Jeg deltager til:</td>
            <td><asp:DropDownList ID="drpDepartureDate" runat="server" Width="100%" /></td>
            <td><asp:RequiredFieldValidator ID="rfvldDepartureDate" runat="server" /></td>
        </tr>
        <tr>
            <td align="left" valign="top">Jeg smutter:</td>
            <td><asp:DropDownList ID="drpDepartureOnDay" runat="server" Width="100%" /></td>
            <td><asp:RequiredFieldValidator ID="rvDepartureOnDay" runat="server" /></td>
        </tr>
        <tr>
            <td align="left" valign="top">Nogen beskeder til de andre:</td>
            <td><asp:TextBox ID="txtMessageAny" runat="server" TextMode="MultiLine" Rows="3" MaxLength="255" Width="100%"/></td>
            <td></td>
        </tr>
        <tr>
            <td align="left" valign="top">Jeg har følgende båd:</td>
            <td><asp:TextBox ID="txtMessageBoat" runat="server" TextMode="MultiLine" Rows="3" MaxLength="255" Width="100%"/></td>
            <td></td>
        </tr>
        <tr>
            <td align="left" valign="top">Jeg har følgende overnatning:</td>
            <td><asp:TextBox ID="txtMessageBed" runat="server" TextMode="MultiLine" Rows="3" MaxLength="255" Width="100%"/></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnCommit" runat="server" Text="Gem svar" Width="100%" onclick="BtnCommitClick" /></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label ID="lblMessage" runat="server" Width="100%"/>
                <br />
                <asp:ValidationSummary ID="vldSumery" runat="server" Width="100%"/>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
