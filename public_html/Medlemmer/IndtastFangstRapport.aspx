<%@ Page 
    AutoEventWireup="true" 
    CodeFile="IndtastFangstRapport.aspx.cs" 
    Inherits="Medlemmer.IndtastFangstRapport" 
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
            <td style="text-align: center; width: 300px"><h4>Indtast / Rediger / Slet Fangster</h4></td>
            <td style="width: 150px"></td>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:DropDownList ID="drpFangster" runat="server" Width="300px" TabIndex="40" AutoPostBack="True" onselectedindexchanged="DrpOpslagSelectedIndexChanged" /></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3"><br/><hr/><br/></td>
        </tr>
        <tr>
            <td style="text-align: left">Id:</td>
            <td style="text-align: left">
                <asp:Textbox Enabled="false" ID="txtId" runat="server" Width="50px" TabIndex="48" />
                <asp:Textbox Enabled="False" ID="txtFanger" runat="server" style="margin-bottom: 0px" TabIndex="49" Width="246px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Dato:</td>
            <td><asp:Textbox ID="txtDato" runat="server" Width="300px" TabIndex="50" /></td>
            <td style="text-align: left">
                <a id="anchorCal" runat="server" />
                <img alt="Open Calendar" id="pickDate1" runat="server" src="" onmouseout="this.style.cursor='pointer'" onmouseover="this.style.cursor='hand'" />
                <asp:RequiredFieldValidator ID="rfvDato" runat="server" />
                <asp:RegularExpressionValidator ID="revDato" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">Art:</td>
            <td><asp:DropDownList ID="drpArt" runat="server" Width="300px" TabIndex="51"/></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Vægt: (kg.)</td>
            <td><asp:Textbox ID="txtVægt" runat="server" Width="300px" TabIndex="52" /></td>
            <td style="text-align: left"><asp:RequiredFieldValidator ID="rfvVægt" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align: left">Længde: (cm.)</td>
            <td><asp:Textbox ID="txtLængde" runat="server" Width="300px" TabIndex="53" /></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Fanger:</td>
            <td><asp:DropDownList ID="drpFanger" runat="server" TabIndex="54" Width="300px" /></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Fiskevand:</td>
            <td><asp:Textbox ID="txtFiskevand" MaxLength="100" runat="server" TabIndex="55" Width="300px" /></td>
            <td style="text-align: left"><asp:RequiredFieldValidator ID="rfvFiskevand" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align: left">Placering på vandet:</td>
            <td><asp:Textbox ID="txtPlacering" runat="server" MaxLength="100" Width="300px" TabIndex="56" /></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Land:</td>
            <td><asp:Textbox ID="txtLand" runat="server" MaxLength="30" Width="300px" TabIndex="57" /></td>
            <td style="text-align: left"><asp:RequiredFieldValidator ID="rfvLand" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align: left">Fangstredskab:</td>
            <td><asp:Textbox ID="txtRedskab" runat="server" MaxLength="100" Width="300px" TabIndex="58" /></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Vejr:</td>
            <td><asp:Textbox ID="txtVejr" runat="server" MaxLength="250" Width="300px" Rows="2" TextMode="MultiLine" TabIndex="59" /></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Beskrivelse:</td>
            <td><asp:Textbox ID="txtBeskrivelse" runat="server" Rows="4" Width="300px" TextMode="MultiLine" TabIndex="60"/></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Evt. link: (http:\\)</td>
            <td><asp:Textbox ID="txtLink" runat="server" MaxLength="250" Width="300px" TabIndex="61"/></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Billed 1:</td>
            <td style="text-align: left"><asp:FileUpload ID="fuBilled1" runat="server" TabIndex="62" Width="300px"/></td>
            <td rowspan="2"><asp:Image ID="imgBilled1" runat="server"/></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <div style="border-style: solid; border-width: thin; border-color: InactiveCaption; text-align: center; padding: 2px">
                    <asp:RadioButton ID="rbnBilled1Liggende" runat="server" Checked="True" Text="Liggende" GroupName="grpBilled1" TabIndex="63" />
                    <asp:RadioButton ID="rbnBilled1Staaende" runat="server" Text="Stående" GroupName="grpBilled1" TabIndex="64" />
                </div>
            </td>
            
        </tr>
        <tr>
            <td style="text-align: left">Billed 2:</td>
            <td style="text-align: left"><asp:FileUpload ID="fuBilled2" runat="server" TabIndex="65" Width="300px"/></td>
            <td rowspan="2"><asp:Image ID="imgBilled2" runat="server"/></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <div style="border-style: solid; border-width: thin; border-color: InactiveCaption; text-align: center; padding: 2px">
                    <asp:RadioButton ID="rbnBilled2Liggende" runat="server" Checked="True" Text="Liggende" GroupName="grpBilled2" TabIndex="66" />
                    <asp:RadioButton ID="rbnBilled2Staaende" runat="server" Text="Stående" GroupName="grpBilled2" TabIndex="67" />
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">Billed 3:</td>
            <td style="text-align: left"><asp:FileUpload ID="fuBilled3" runat="server" TabIndex="68" Width="300px"/></td>
            <td rowspan="2"><asp:Image ID="imgBilled3" runat="server"/></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <div style="border-style: solid; border-width: thin; border-color: InactiveCaption; text-align: center; padding: 2px">
                    <asp:RadioButton ID="rbnBilled3Liggende" runat="server" Checked="True" Text="Liggende" GroupName="grpBilled3" TabIndex="69" />
                    <asp:RadioButton ID="rbnBilled3Staaende" runat="server" Text="Stående" GroupName="grpBilled3" TabIndex="70" />
                </div>
            </td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left">Offentlig:</td>
            <td style="text-align: left"><asp:CheckBox ID="chkOffentlig" runat="server" TabIndex="71" /></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: left"><asp:Label ID="lblYearFish" runat="server" /></td>
            <td style="text-align: left"><asp:CheckBox ID="chkYearFish" runat="server" TabIndex="72" /></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3"><br/></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnInsert" runat="server" Width="300px" onclick="BtnInsertClick" TabIndex="73" Text="Opret" /></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnUpdate" runat="server" Width="300px" onclick="BtnUpdateClick" TabIndex="74" Text="Gem" /></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnDelete" runat="server" Width="300px" onclick="BtnDeleteClick" TabIndex="75" Text="Slet" /></td>
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