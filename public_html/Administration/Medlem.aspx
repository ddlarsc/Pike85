<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Medlem.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Administration.Medlem" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    Title="Rediger Medlem side." %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentOpretArrangement" runat="server">
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
    <table width="600px" style="border-spacing: 2px">
        <tr>
            <td style="width: 150px"/>
            <td style="width: 300px"><h4 style="text-align:center">Opret nyt medlem / Rediger medlem</h4></td>
            <td style="width: 150px"/>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr style="text-align:left">
            <td/>
            <td><asp:DropDownList AutoPostBack="True" ID="drpUserList" OnSelectedIndexChanged="UserIdSelectedIndexChanged" runat="server" TabIndex="49" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td colspan="3"><br/><hr /><a id="anchorCal" runat="server" /><br/></td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align:top">Id:</td>
            <td><asp:TextBox ID="txtBrugerId" MaxLength="20" runat="server" TabIndex="50" Width="50px" /></td>
            <td><asp:RequiredFieldValidator ID="rfvBrugerId" runat="server" /></td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top">Navn :</td>
            <td><asp:TextBox ID="txtNavn" MaxLength="50" runat="server" TabIndex="51" Width="300px" /></td>
            <td><asp:RequiredFieldValidator ID="rfvNavn" runat="server" /></td>
        </tr>
        <tr>
            <td style="vertical-align: top">Kort navn:</td>
            <td><asp:TextBox ID="txtKortNavn" MaxLength="20" runat="server" TabIndex="52" Width="300px" /></td>
            <td><asp:RequiredFieldValidator ID="rfvKortNavn" runat="server" /></td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align:top">Indmeldelses dato :</td>
            <td><asp:TextBox ID="txtMembership" runat="server" TabIndex="53" Width="300px" /></td>
            <td>
                <img alt="Open Calendar" id="pickDate1" onmouseout="this.style.cursor='pointer'" onmouseover="this.style.cursor='hand'" runat="server" src="" />
                <asp:RequiredFieldValidator ID="rfvMembership" runat="server" />
            </td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align:top">Fødselsdag :</td>
            <td><asp:TextBox ID="txtBirthDay" runat="server" Width="300px" TabIndex="54" /></td>
            <td>
                <img alt="Open Calendar" id="pickDate2" runat="server" src="" onmouseout="this.style.cursor='pointer'" onmouseover="this.style.cursor='hand'" />
                <asp:RequiredFieldValidator ID="rfvBirthday" runat="server" />
            </td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align:top">Beskrivelse af medlem :</td>
            <td><asp:TextBox Height="100px" ID="txtDescription" runat="server" TabIndex="55" TextMode="MultiLine" Width="300px" /></td>
            <td><asp:RequiredFieldValidator ID="rfvDescription" runat="server" /></td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top">Email :</td>
            <td><asp:TextBox ID="txtEmail1" MaxLength="100" runat="server" TabIndex="56" Width="300px" /></td>
            <td>
                <asp:RequiredFieldValidator ID="rfvEmail1" runat="server" />
                <asp:RegularExpressionValidator ID="revEmail1" runat="server" />
            </td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top">Email 2 :</td>
            <td><asp:TextBox ID="txtEmail2" MaxLength="100" runat="server" TabIndex="57" Width="300px" /></td>
            <td><asp:RegularExpressionValidator ID="revEmail2" runat="server" /></td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top">Adresse :</td>
            <td><asp:TextBox ID="txtAdresse" MaxLength="50" runat="server" TabIndex="58" Width="300px" /></td>
            <td><asp:RequiredFieldValidator ID="rfvAddress" runat="server" /></td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top">By :</td>
            <td><asp:TextBox ID="txtCity" MaxLength="30" runat="server" TabIndex="59" Width="300px" /></td>
            <td><asp:RequiredFieldValidator ID="rfvCity" runat="server" /></td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top">PostNr :</td>
            <td><asp:TextBox ID="txtZipCode" MaxLength="4" runat="server" TabIndex="60" Width="300px" /></td>
            <td>
                <asp:RequiredFieldValidator ID="rfvPostnr" runat="server" />
                <asp:RegularExpressionValidator ID="revZipCode" runat="server" />
            </td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top">Hjem Tlf. :</td>
            <td><asp:TextBox ID="txtHjemTlf" MaxLength="30" runat="server" TabIndex="61" Width="300px" /></td>
            <td/>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top">Mobil Tlf.</td>
            <td><asp:TextBox ID="txtMobilPhone" MaxLength="30" runat="server" TabIndex="62" Width="300px" /></td>
            <td><asp:RequiredFieldValidator ID="rfvMobilTlf" runat="server" /></td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top">Arb Tlf :</td>
            <td><asp:TextBox ID="txtArbTlf" MaxLength="30" runat="server" TabIndex="63" Width="300px" /></td>
            <td style="vertical-align: top" rowspan="4"><asp:Image ID="imgMedlem" runat="server" Visible="false" /></td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top">Billed :</td>
            <td><asp:FileUpload ID="FileUpload" runat="server" TabIndex="64" Width="300px"></asp:FileUpload></td>
        </tr>
        <tr style="text-align:left">
            <td>Roller :</td>
            <td>
                <asp:DropDownList ID="drpUserLevel" runat="server" TabIndex="65" Width="300px">
                    <asp:ListItem Value="">-- vælg --</asp:ListItem>
                    <asp:ListItem Value="Administrator">Administrator</asp:ListItem>
                    <asp:ListItem Value="Medlem">Medlem</asp:ListItem>
                    <asp:ListItem Value="Anonym">Anonym</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="text-align:left">
            <td>Offentlig :</td>
            <td><asp:CheckBox ID="ckbOffentlig" runat="server" Checked="True" TabIndex="66" /></td>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top"></td>
            <td><asp:Button ID="btnInsertUser" OnClick="BtnCreateUserClick" runat="server" TabIndex="67" Text="Opret" width="300px" /></td>
            <td/>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top"/>
            <td><asp:Button ID="btnUpdateUser" OnClick="BtnUpdateUserClick" runat="server" TabIndex="68" Text="Gem" width="300px" /></td>
            <td/>
        </tr>
        <tr style="text-align:left">
            <td style="vertical-align: top"/>
            <td><asp:Button ID="btnDeleteUser" OnClick="BtnDeleteUserClick" runat="server" TabIndex="69" Text="Slet" width="300px" /></td>
            <td/>
        </tr>
        <tr style="text-align:left">
           <td colspan="3">
            <asp:Label id="lblMessage" runat="server" />
            <br />
            <asp:ValidationSummary ID="vldSummery" runat="server" />
          </td> 
        </tr>
    </table>
    <script type="text/javascript">
    function getDate(field)
    {
        return field.defaultValue;
    }
    </script>
</asp:Content>
