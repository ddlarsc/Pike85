<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Fiskesider.aspx.cs" 
    CodeFileBaseClass="SkabelonPage"
    Inherits="Links.FiskeLinks" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Links.master"
    RuntimeMasterPageFile="~/Skabelon/Links.master" 
    Title="Fiskesider Links" %>

<asp:Content ContentPlaceHolderID="LinksBody" ID="ContentFangster" runat="Server">
    <h3 style="text-align: center">
        Link til Fiskehjemmesider</h3>
    <p>
        <asp:Label ID="Label_Output" runat="server" Font-Bold="True" ForeColor="Blue" />
        <br />
        Vælg land :
        <asp:DropDownList ID="DropDownList_Land" TabIndex="1" runat="server" AutoPostBack="True">
            <asp:ListItem Value="Danmark">Danmark</asp:ListItem>
            <asp:ListItem Value="Sverige">Sverige</asp:ListItem>
            <asp:ListItem Value="Norge">Norge</asp:ListItem>
            <asp:ListItem Value="Diverse">Diverse</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
    </p>
    <p>
        <asp:DataGrid AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ID="DataGrid_Fiskesider" runat="server">
            <FooterStyle BackColor="#CCCCCC" />
            <SelectedItemStyle Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#000099" />
            <EditItemStyle Wrap="False" />
            <AlternatingItemStyle Wrap="False" BackColor="#CCCCCC" />
            <ItemStyle Wrap="False" />
            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#758DC7" />
            <Columns>
                <asp:HyperLinkColumn DataNavigateUrlField="Adresse" DataNavigateUrlFormatString="http://{0}" DataTextField="Navn" HeaderText="Link" Target="_blank">
                    <HeaderStyle Width="150px" />
                    <ItemStyle VerticalAlign="Top" />
                </asp:HyperLinkColumn>
                <asp:BoundColumn DataField="Beskrivelse" HeaderText="Beskrivelse">
                    <HeaderStyle Width="400px" />
                    <ItemStyle VerticalAlign="Top" />
                </asp:BoundColumn>
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        </asp:DataGrid></p>
</asp:Content>
