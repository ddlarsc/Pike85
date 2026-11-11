<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Rediger_Opslag.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Medlemmer.RedigerOpslag" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    RuntimeMasterPageFile="~/Skabelon/Medlem.master" 
    Title="Medlem Rediger Opslag side." %>

<asp:Content ID="ContentMedlemRedigerOpslag" ContentPlaceHolderID="MedlemBody" runat="Server">
    <h3 style="text-align: center">Rediger Opslag</h3>
    <div style="text-align: left">
        <p>
            <asp:Label ForeColor="Red" ID="Label_Output" runat="server" />
            Denne side giver mulighed for at&nbsp;redigere fiske opslag.
        </p>
        <p>
            Dato for opslag
            <asp:Calendar BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" ID="Kalender_Dato" NextPrevFormat="FullMonth" runat="server" TabIndex="1" Width="350px">
                <TodayDayStyle BackColor="#CCCCCC"/>
                <NextPrevStyle Font-Size="8pt" Font-Bold="True" ForeColor="#333333" VerticalAlign="Bottom"/>
                <DayHeaderStyle Font-Size="8pt" Font-Bold="True"/>
                <SelectedDayStyle ForeColor="White" BackColor="#333399"/>
                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                <OtherMonthDayStyle ForeColor="#999999"/>
            </asp:Calendar>
        </p>
        <p>
            &nbsp;
        </p>
        <p>
            Overskrift til opslag :
            <asp:TextBox ID="TextBox_Overskrift" runat="server" TabIndex="2" Width="490px"/>
        </p>
        <p>
            Beskrivelse af opslaget<br />
            <asp:TextBox Height="100px" ID="TextBox_Beskrivelse" runat="server" TabIndex="3" TextMode="MultiLine" Width="600px"/>
        </p>
        <p>
            Evt. link : <strong>http://</strong>
            <asp:TextBox ID="TextBox_Link" runat="server" TabIndex="4" Width="520px" />
        </p>
        <p>
            Evt. Billed :&nbsp;&nbsp;&nbsp;&nbsp;
            <input id="FileUpload1" name="FileUpload1" runat="server" tabindex="5" type="file" />
            &nbsp;
            <img alt="Liggende" src="../Billeder/skabelon/Liggende.gif" style="vertical-align:bottom" />
            <asp:RadioButton Checked="True" GroupName="Retning_Billed1" runat="server" TabIndex="6" Text="Liggende"/>
            &nbsp;
            <img alt="Stående" src="../Billeder/skabelon/Staaende.gif" style="vertical-align: bottom" />
            <asp:RadioButton GroupName="Retning_Billed1" ID="RadioButton_Billed1_Staaende" runat="server" TabIndex="7" Text="Stående" />
            &nbsp;
            <asp:Image ID="Image1" runat="server" />
        </p>
        <p>
            <asp:CheckBox Checked="True" ID="CheckBox_Offentlig" runat="server" TabIndex="14" Text="Skal opslaget være offentlig tilgængelig" />
        </p>
        <p>
            <asp:Button OnClick="ButtonUploadClick" runat="server" TabIndex="15" Text="Opdater opslag" />
        </p>
    </div>
</asp:Content>
