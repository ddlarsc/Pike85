<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Rediger_Fangstrapport.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Medlemmer.RedigerFangstrapport" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    RuntimeMasterPageFile="~/Skabelon/Medlem.master" 
    Title="Medlem rediger fangstrapport side." %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentMedlemRedigerFangstRapport" runat="Server">
    <h3 style="text-align: center">Rediger&nbsp;Fangstrapport</h3>
    <div style="text-align: left">
        <p>
            <asp:Label ID="Label_Output" runat="server" ForeColor="Blue" Font-Bold="True" />
            <asp:ValidationSummary runat="server" />
        </p>
        <p>
            Denne side giver mulighed for at redigere fangst rapport.
            <p>
                Dato&nbsp;for fangst :
            </p>
            <p>
                Dag :&nbsp;
                <asp:DropDownList ID="DropDownList_Dag" TabIndex="1" runat="server">
                    <asp:ListItem Value="01">01</asp:ListItem>
                    <asp:ListItem Value="02">02</asp:ListItem>
                    <asp:ListItem Value="03">03</asp:ListItem>
                    <asp:ListItem Value="04">04</asp:ListItem>
                    <asp:ListItem Value="05">05</asp:ListItem>
                    <asp:ListItem Value="06">06</asp:ListItem>
                    <asp:ListItem Value="07">07</asp:ListItem>
                    <asp:ListItem Value="08">08</asp:ListItem>
                    <asp:ListItem Value="09">09</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                    <asp:ListItem Value="13">13</asp:ListItem>
                    <asp:ListItem Value="14">14</asp:ListItem>
                    <asp:ListItem Value="15">15</asp:ListItem>
                    <asp:ListItem Value="16">16</asp:ListItem>
                    <asp:ListItem Value="17">17</asp:ListItem>
                    <asp:ListItem Value="18">18</asp:ListItem>
                    <asp:ListItem Value="19">19</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="21">21</asp:ListItem>
                    <asp:ListItem Value="22">22</asp:ListItem>
                    <asp:ListItem Value="23">23</asp:ListItem>
                    <asp:ListItem Value="24">24</asp:ListItem>
                    <asp:ListItem Value="25">25</asp:ListItem>
                    <asp:ListItem Value="26">26</asp:ListItem>
                    <asp:ListItem Value="27">27</asp:ListItem>
                    <asp:ListItem Value="28">28</asp:ListItem>
                    <asp:ListItem Value="29">29</asp:ListItem>
                    <asp:ListItem Value="30">30</asp:ListItem>
                    <asp:ListItem Value="31">31</asp:ListItem>
                </asp:DropDownList>
                &nbsp; Måned :&nbsp;
                <asp:DropDownList ID="DropDownList_Maaned" TabIndex="2" runat="server">
                    <asp:ListItem Value="01">01</asp:ListItem>
                    <asp:ListItem Value="02">02</asp:ListItem>
                    <asp:ListItem Value="03">03</asp:ListItem>
                    <asp:ListItem Value="04">04</asp:ListItem>
                    <asp:ListItem Value="05">05</asp:ListItem>
                    <asp:ListItem Value="06">06</asp:ListItem>
                    <asp:ListItem Value="07">07</asp:ListItem>
                    <asp:ListItem Value="08">08</asp:ListItem>
                    <asp:ListItem Value="09">09</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                År
                <asp:TextBox ID="TextBox_Aar" TabIndex="3" runat="server" Width="49px">&#229;&#229;&#229;&#229;</asp:TextBox>
                &nbsp;
            </p>
            <p>
                Art :
                <asp:DropDownList ID="DropDownList_Art" TabIndex="4" runat="server" />                
            </p>
            <p>
                Vægt :
                <asp:TextBox ID="TextBox_Vaegt" TabIndex="5" runat="server" Width="72px" />kg.
                <asp:RequiredFieldValidator ControlToValidate="TextBox_Vaegt" ErrorMessage="Vægt skal indtastes" runat="server">*</asp:RequiredFieldValidator>
                &nbsp;&nbsp;&nbsp;
                Længde :
                <asp:TextBox ID="TextBox_Laengde" runat="server" TabIndex="6" Width="72px" />cm.
            </p>
            <p>
                Fanger/medlem :
                <asp:DropDownList ID="DropDownList_Fanger" runat="server" />
            </p>
            <p>
                Fiskevand :
                <asp:TextBox ID="TextBox_Fiskevand" runat="server" TabIndex="8" Width="200px" />
            </p>
            <p>
                Placering på fiskevand :
                <asp:TextBox ID="TextBox_Placering" runat="server" TabIndex="9" Width="300px" />
            </p>
            <p>
                Land&nbsp;:
                <asp:TextBox ID="Textbox_Land" runat="server" TabIndex="10" Width="300px">Danmark</asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="Textbox_Land" ErrorMessage="Land skal indtastes" runat="server">*</asp:RequiredFieldValidator>
            </p>
            <p>
                Fangst redskab :
                <asp:TextBox ID="TextBox_Redskab" runat="server" TabIndex="11" Width="200px" />
            </p>
            <p>
                Vejr :
                <asp:TextBox ID="TextBox_Vejr" runat="server" TabIndex="12" Width="500px" />
            </p>
            <p>
                Beskrivelse af fangsten
                <br />
                <asp:TextBox Height="100px" ID="TextBox_Beskrivelse" runat="server" TabIndex="13" TextMode="MultiLine" Width="600px" />
            </p>
            <p>
                Evt. Link :
                <strong>http://</strong>
                <asp:TextBox ID="TextBox_Link" runat="server" TabIndex="14" Width="500px" />
            </p>
            <p>
                &nbsp;
            </p>
            <p>
                Billed 1&nbsp;:
                <input id="FileUpload1" name="FileUpload1" runat="server" tabindex="15" type="file" />
                &nbsp;
                <img alt="Liggende" src="../Billeder/skabelon/Liggende.gif" style="vertical-align:bottom">
                <asp:RadioButton Checked="True" GroupName="Retning_Billed1" ID="RadioButton_Billed1_Liggende" runat="server" TabIndex="16" Text="Liggende" />
                &nbsp;
                <img alt="Stående" src="../Billeder/skabelon/Staaende.gif" style="vertical-align: bottom" />
                <asp:RadioButton GroupName="Retning_Billed1" ID="RadioButton_Billed1_Staaende" runat="server" TabIndex="17" Text="Stående" />
                <asp:Image ID="Image1" runat="server" />
            </p>
            <p>
                Billed&nbsp;2&nbsp;:
                <input id="FileUpload2" name="FileUpload2" runat="server" tabindex="18" type="file" />
                &nbsp;
                <img alt="Liggende" src="../Billeder/skabelon/Liggende.gif" style="vertical-align:bottom" />
                <asp:RadioButton Checked="True" GroupName="Retning_Billed2" ID="RadioButton_Billed2_Liggende" runat="server" TabIndex="19" Text="Liggende" />
                &nbsp;
                <img alt="Stående" src="../Billeder/skabelon/Staaende.gif" style="vertical-align: bottom" />
                <asp:RadioButton GroupName="Retning_Billed2" ID="RadioButton_Billed2_Staaende" runat="server" TabIndex="20" Text="Stående" />
                <asp:Image ID="Image2" runat="server" />
            </p>
            <p>
                Billed&nbsp;3&nbsp;:
                <input id="FileUpload3" name="FileUpload3" runat="server" tabindex="21" type="file" />
                &nbsp;
                <img alt="Liggende" src="../Billeder/skabelon/Liggende.gif" style="vertical-align: bottom"/>
                <asp:RadioButton Checked="True" GroupName="Retning_Billed3" runat="server" TabIndex="22" Text="Liggende"/>
                &nbsp;
                <img alt="Stående" src="../Billeder/skabelon/Staaende.gif" style="vertical-align:bottom" />
                <asp:RadioButton GroupName="Retning_Billed3" ID="RadioButton_Billed3_Staaende" runat="server" TabIndex="23" Text="Stående"/>
                <asp:Image ID="Image3" runat="server"/>
            </p>
            <p>
                <asp:CheckBox Checked="True" ID="CheckBox_Offentlig" runat="server" TabIndex="24" Text="Skal fangstrapporten være offentlig tilgængelig"/>
            </p>
            <p>
                <asp:CheckBox Checked="True" ID="CheckBox_YearFish" runat="server" TabIndex="25" Text="Er dette årets fisk?"/>
            </p>
            <p>
                <asp:Button OnClick="ButtonUploadClick" runat="server" TabIndex="26" Text="Opdater fangst"/>
            </p>
        </p>
    </div>
</asp:Content>
