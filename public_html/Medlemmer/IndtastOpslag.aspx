<%@ Page 
    AutoEventWireup="true" 
    CodeFile="IndtastOpslag.aspx.cs" 
    Inherits="Medlemmer.IndtastOpslag" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentIndtastFangstRapport" runat="server">
    <script type="text/javascript" src="../ScriptLib/JAVA/AnchorPosition.js"></script>
    <script type="text/javascript" src="../ScriptLib/JAVA/CalendarPopup.js"></script>
    <script type="text/javascript" src="../ScriptLib/JAVA/date.js"></script>
    <script type="text/javascript" src="../ScriptLib/JAVA/PopupWindow.js"></script>
    <script type="text/javascript">
        var cal1 = new CalendarPopup();
        cal1.offsetX = 20;
        cal1.offsetY = 20;
        cal1.setWeekStartDay(1);
    </script>
    <table width="600px">
        <tr>
            <td style="width: 150px"></td>
            <td style="text-align: center; width: 300px"><h4>Indtast / Rediger / Slet opslag</h4></td>
            <td style="width: 150px"></td>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:DropDownList ID="drpOpslag" runat="server" Width="300px" TabIndex="48" AutoPostBack="True" onselectedindexchanged="DrpOpslagSelectedIndexChanged" /></td>
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
            <td style="text-align: left">Dato:</td>
            <td><asp:Textbox ID="txtDato" runat="server" Width="300px" TabIndex="51" /></td>
            <td style="text-align: left">
                <a id="anchorCal" runat="server" />
                <img alt="Open Calendar" id="pickDate1" runat="server" src="" onmouseout="this.style.cursor='pointer'" onmouseover="this.style.cursor='hand'" />
                <asp:RequiredFieldValidator ID="rfvDato" runat="server" />
                <asp:RegularExpressionValidator ID="revDato" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">Overskrift:</td>
            <td><asp:Textbox ID="txtOverskrift" runat="server" Width="300px" TabIndex="52" /></td>
            <td style="text-align: left"><asp:RequiredFieldValidator ID="rfvOverskrift" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align: left">Beskrivelse:<br/>(max. 500 tegn)</td>
            <td><asp:Textbox ID="txtBeskrivelse" runat="server" MaxLength="500" Rows="5" TextMode="MultiLine" Width="300px" TabIndex="53" /></td>
            <td style="text-align: left"><asp:RequiredFieldValidator ID="rfvBeskrivelse" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align: left">Link:</td>
            <td><asp:Textbox ID="txtLink" runat="server" Width="300px" TabIndex="54" /></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Billed 1:</td>
            <td><asp:FileUpload ID="fuBilled" runat="server" TabIndex="55" Width="300px"/></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <div style="border-style: solid; border-width: thin; border-color: InactiveCaption; text-align: center; padding: 2px">
                    <asp:RadioButton ID="rbnBilledLiggende" runat="server" Checked="True" Text="Liggende" GroupName="grpBilled1" TabIndex="56" />&nbsp;
                    <asp:RadioButton ID="rbnBilledStaaende" runat="server" Text="Stående" GroupName="grpBilled1" TabIndex="57" />
                </div>
            </td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Offentlig:</td>
            <td style="text-align: left"><asp:CheckBox ID="chkOffentlig" runat="server" TabIndex="58" /></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3"><br/></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnInsert" runat="server" Width="300px" onclick="BtnInsertClick" TabIndex="59" Text="Opret" /></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnUpdate" runat="server" Width="300px" onclick="BtnUpdateClick" TabIndex="60" Text="Gem" /></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnDelete" runat="server" Width="300px" onclick="BtnDeleteClick" TabIndex="61" Text="Slet" /></td>
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
    <script type="text/javascript">
    function getDate(field)
    {
        return field.defaultValue;
    }
    </script>
</asp:Content>