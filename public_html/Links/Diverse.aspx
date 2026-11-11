<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Diverse.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Links.DiverseLinks" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Links.master" 
    RuntimeMasterPageFile="~/Skabelon/Links.master" 
    Title="Diverse Links" %>

<asp:Content ID="ContentFangster" ContentPlaceHolderID="LinksBody" runat="Server">
    <table>
        <tr>
            <td class="indhold" style="width:100%; vertical-align: top" rowspan="2">
                <h3 style="text-align: center">Link til Diverse sider</h3>
                <p>
                    <asp:Label ID="Label_Output" runat="server" Font-Bold="True" ForeColor="Blue" />
                </p>
                <p>
                    <asp:DataGrid AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ID="DataGrid_Diversesider" runat="server">
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
                    </asp:DataGrid>
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
