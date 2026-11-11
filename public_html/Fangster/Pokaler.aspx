<%@ Page Language="C#" MasterPageFile="~/Skabelon/Pike85.master" AutoEventWireup="true"
    CodeFile="Pokaler.aspx.cs" Inherits="Pike85.Pokaler" Title="Pokaler" CodeFileBaseClass="SkabelonPage"
    RuntimeMasterPageFile="~/Skabelon/Fangster.master" %>

<asp:Content ID="ContentPokaler" ContentPlaceHolderID="FangsterBody" runat="Server">
    <table>
        <tr>
            <td class="indhold" valign="top" style="width: 100%">
                <h3 style="text-align: center">
                    Pokal vindere</h3>
                <asp:Label ID="Label_Output" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label><br />
                <asp:GridView ID="GridViewPokaler" runat="server" AutoGenerateColumns="False" ForeColor="Black"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" GridLines="Vertical">
                    <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                    <SelectedRowStyle Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#000099" />
                    <EditRowStyle Wrap="False" />
                    <AlternatingRowStyle Wrap="False" BackColor="#CCCCCC" />
                    <RowStyle Wrap="False" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#758DC7" />
                    <PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#C6C3C6" />
                    <Columns>
                        <asp:BoundField DataField="Year" HeaderText="&#197;r">
                            <ItemStyle Height="35px" HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pike" HeaderText="Gedde" HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Salmon" HeaderText="Laks" HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Cod" HeaderText="Torsk" HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="YearFish" HeaderText="&#197;rets Fisk" HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
