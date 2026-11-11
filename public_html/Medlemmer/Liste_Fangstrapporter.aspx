<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Liste_Fangstrapporter.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Medlemmer.ListeFangstrapporter" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    RuntimeMasterPageFile="~/Skabelon/Medlem.master" 
    Title="Medlem Fangstrapport Liste side" %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentMedlemFangstRapportListe" runat="Server">
    <h3 style="text-align: center">Liste over Fangstrapporter</h3>
    <div style="text-align: left">
        <p>
            <asp:Label ID="Label_Output" runat="server" Font-Bold="True" ForeColor="Blue" />
        </p>
        <p>
            År :&nbsp;
            <asp:DropDownList AutoPostBack="True" ID="DropDownList_Aar" OnSelectedIndexChanged="DropDownListAarSelectedIndexChanged" runat="server" TabIndex="1">
                <asp:ListItem Selected="True" Value="2005">2005</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            <asp:DataGrid AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ID="DataGrid_Fangster" runat="server">
                <FooterStyle BackColor="#CCCCCC" />
                <SelectedItemStyle Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#000099" />
                <EditItemStyle Wrap="False" />
                <AlternatingItemStyle Wrap="False" BackColor="#CCCCCC" />
                <ItemStyle Wrap="False" />
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#758DC7" />
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="FangstId" />
                    <asp:BoundColumn DataField="Dato" HeaderText="Dato" DataFormatString="{0:d}">
                        <HeaderStyle Width="80px" />
                        <ItemStyle Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Art" HeaderText="Art" />
                    <asp:BoundColumn DataField="Vaegt" HeaderText="V&#230;gt" DataFormatString="{0:N3}">
                        <HeaderStyle Width="80px" />
                        <ItemStyle Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Fanger" HeaderText="Fanger">
                        <HeaderStyle Width="80px" />
                        <ItemStyle Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Fiskevand" HeaderText="Fiskevand">
                        <HeaderStyle Width="80px" />
                        <ItemStyle Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Land" HeaderText="Land" />
                    <asp:EditCommandColumn ButtonType="PushButton" CancelText="" EditText="Rediger" HeaderText="Rediger" UpdateText="" />
                    <asp:ButtonColumn Text="Slet" ButtonType="PushButton" HeaderText="Slet" CommandName="Delete" />
                </Columns>
                <PagerStyle BackColor="Silver" ForeColor="#CC0000" HorizontalAlign="Center" Mode="NumericPages" PageButtonCount="5" Position="TopAndBottom" />
            </asp:DataGrid>
        </p>
    </div>
</asp:Content>
