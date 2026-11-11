<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Indtast_Nyhed.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Medlemmer.IndtastNyhed" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    RuntimeMasterPageFile="~/Skabelon/Medlem.master" 
    Title="Medlem indtast nyhed side" %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentMedlemIndtastNyhed" runat="Server">
    <h3 style="text-align: center">Indtast Nyhed</h3><div style="text-align: left">
        <p>
            <asp:Label ID="Label_Output" runat="server" ForeColor="Red"/>Denne side giver mulighed for at indtaste fiske nyhed.
        </p>
        <p>
            Dato&nbsp;for&nbsp;nyhed&nbsp;
            <asp:Calendar BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" ID="Kalender_Dato" NextPrevFormat="FullMonth" runat="server" TabIndex="16" Width="350px">
                <TodayDayStyle BackColor="#CCCCCC" />
                <NextPrevStyle Font-Size="8pt" Font-Bold="True" ForeColor="#333333" VerticalAlign="Bottom" />
                <DayHeaderStyle Font-Size="8pt" Font-Bold="True" />
                <SelectedDayStyle ForeColor="White" BackColor="#333399" />
                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                <OtherMonthDayStyle ForeColor="#999999" />
            </asp:Calendar>
        </p>
        <p>
            &nbsp;
        </p>
        <p>
            Overskrift til nyhed :
            <asp:TextBox ID="TextBox_Overskrift" runat="server" Width="490px" TabIndex="17" />
        </p>
        <p>
            Beskrivelse af nyheden
            <br />
            <asp:TextBox Height="100px" ID="TextBox_Beskrivelse" runat="server" TabIndex="18" TextMode="MultiLine" Width="600px" />
        </p>
        <p>
            Evt. link : <strong>http://</strong>
            <asp:TextBox ID="TextBox_Link" runat="server" Width="520px" TabIndex="19" />
        </p>
        <p>
            Evt. Billed 1 :&nbsp;&nbsp;&nbsp;&nbsp;<input id="FileUpload1" name="FileUpload1" runat="server" tabindex="20" type="file" />
            &nbsp;
            <img alt="Liggende" src="../Billeder/skabelon/Liggende.gif" style="vertical-align: bottom" />
            <asp:RadioButton Checked="True" GroupName="Retning_Billed1" ID="RadioButton_Billed1_Liggende" runat="server" TabIndex="21" Text="Liggende" />
            &nbsp;
            <img alt="Stående" src="../Billeder/skabelon/Staaende.gif" style="vertical-align: bottom" />
            <asp:RadioButton GroupName="Retning_Billed1" ID="RadioButton_Billed1_Staaende" runat="server" TabIndex="22" Text="Stående" />
            &nbsp;
        </p>
        <p>
            Evt. Billed&nbsp;2&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;
            <input id="FileUpload2" name="FileUpload2" runat="server" size="20" tabindex="23" type="file" />
            &nbsp;
            <img alt="Liggende" src="../Billeder/skabelon/Liggende.gif" style="vertical-align: bottom" />
            <asp:RadioButton Checked="True" GroupName="Retning_Billed2" ID="RadioButton_Billed2_Liggende" runat="server" TabIndex="24" Text="Liggende" />
            &nbsp;
            <img alt="Stående" src="../Billeder/skabelon/Staaende.gif" style="vertical-align: bottom" />
            <asp:RadioButton GroupName="Retning_Billed2" ID="RadioButton_Billed2_Staaende" runat="server" TabIndex="25" Text="Stående" />
        </p>
        <p>
            Evt. Billed 3&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;
            <input id="FileUpload3" name="FileUpload3" runat="server" tabindex="26" type="file" />
            &nbsp;
            <img alt="Liggende" src="../Billeder/skabelon/Liggende.gif" style="vertical-align: bottom" />
            <asp:RadioButton Checked="True" GroupName="Retning_Billed3" ID="RadioButton_Billed3_Liggende" runat="server" TabIndex="27" Text="Liggende" />
            &nbsp;
            <img alt="stående" src="../Billeder/skabelon/Staaende.gif" style="vertical-align: bottom" />
            <asp:RadioButton GroupName="Retning_Billed3" ID="RadioButton_Billed3_Staaende" runat="server" TabIndex="28" Text="Stående" />
        </p>
        <p>
            <asp:CheckBox Checked="True" ID="CheckBox_Offentlig" runat="server" TabIndex="29" Text="Skal nyheden være offentlig tilgængelig" />
        </p>
        <p>
            <asp:Button ID="ButtonUpload" OnClick="ButtonUploadClick" runat="server" TabIndex="30" Text="Gem nyhed" />
        </p>
    </div>
</asp:Content>
