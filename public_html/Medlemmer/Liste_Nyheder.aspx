<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Liste_Nyheder.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Medlemmer.ListeNyheder" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    RuntimeMasterPageFile="~/Skabelon/Medlem.master" 
    Title="Medlem Liste Nyheder side." %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentMedlemListeNyheder" runat="Server">
    <h3 style="text-align: center">Liste over Nyheder</h3>
    <div style="text-align: left">
        <p>
            <asp:Label ID="Label_Output" runat="server" ForeColor="Blue" Font-Bold="True" />
        </p>
        <p>
            År :&nbsp;
            <asp:DropDownList AutoPostBack="True" ID="DropDownList_Aar" OnSelectedIndexChanged="DropDownListAarSelectedIndexChanged" runat="server" TabIndex="1">
                <asp:ListItem Value="2005" Selected="True">2005</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            <asp:DataGrid AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" HorizontalAlign="Center" ID="DataGrid_Nyheder" runat="server">
                <FooterStyle BackColor="#CCCCCC" />
                <SelectedItemStyle Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#000099" />
                <EditItemStyle Wrap="False" />
                <AlternatingItemStyle Wrap="False" BackColor="#CCCCCC" />
                <ItemStyle Wrap="False" />
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#758DC7" />
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="Id" />
                    <asp:BoundColumn DataField="Dato" DataFormatString="{0:d}" HeaderText="Dato">
                        <HeaderStyle Width="80px" />
                        <ItemStyle Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Overskrift" HeaderText="Overskrift">
                        <HeaderStyle Width="300px" />
                    </asp:BoundColumn>
                    <asp:EditCommandColumn ButtonType="PushButton" CancelText="" EditText="Rediger" HeaderText="Rediger" UpdateText="" />
                    <asp:ButtonColumn Text="Slet" ButtonType="PushButton" HeaderText="Slet" CommandName="Delete" />
                </Columns>
                <PagerStyle BackColor="Silver" ForeColor="#CC0000" HorizontalAlign="Center" Mode="NumericPages" PageButtonCount="5" Position="TopAndBottom" />
            </asp:DataGrid>
        </p>
    </div>
</asp:Content>
