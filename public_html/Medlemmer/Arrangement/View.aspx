 <%@ Page 
    AutoEventWireup="true" 
    CodeFile="View.aspx.cs" 
    Inherits="Medlemmer.Arrangement.View" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" %>
    
<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentSeArrangementListe" runat="Server">
    <table width="600px">
        <tr>
            <td style="width: 150px" />
            <td style="text-align: center; width: 300px" ><h4>Deltagerliste til arrangement</h4><br/></td>
            <td style="width: 150px" />
        </tr>
        <tr>
            <td colspan="3" />
        </tr>
        <tr>
            <td />
            <td><asp:DropDownList AutoPostBack="True" ID="drpArrangement" onselectedindexchanged="DrpSelectedIndexChanged" runat="server" TabIndex="50" Width="100%" /></td>
            <td />
        </tr>
        <tr>
            <td colspan="3"><br/><hr /><br/></td>
        </tr>
        <tr>
            <td colspan="3" style="text-align:center">
                <br/>
                <asp:DataGrid AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ID="dgArrangementer" runat="server">
                    <FooterStyle BackColor="#CCCCCC"/>
                    <SelectedItemStyle Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#000099"/>
                    <AlternatingItemStyle Wrap="False" BackColor="#CCCCCC"/>
                    <ItemStyle Wrap="False" HorizontalAlign="Left"/>
                    <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#758DC7"/>
                    <Columns>
                        <asp:BoundColumn DataField="ID" Visible="false"/>
                        <asp:BoundColumn DataField="setupID" Visible="false"/>
                        <asp:BoundColumn DataField="name"/>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:Image ID="test" ImageUrl='<%# GetImageUrl(DataBinder.Eval(Container.DataItem, "decline").ToString()) %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="80px" />
                            <HeaderTemplate>
                                Start
                            </HeaderTemplate>
                            <ItemStyle Wrap="true" />
                            <ItemTemplate>
                                <asp:Label ID="arrivalDate" runat="server" Text='<%# SetDayAndPeriode(DataBinder.Eval(Container.DataItem, "arrivalDate").ToString(), DataBinder.Eval(Container.DataItem, "arrivalOnDay").ToString()) %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="messageAny" HeaderText="Beskeder">
                            <HeaderStyle Width="250px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="messageBoat" HeaderText="Båd">
                            <HeaderStyle Width="125px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="messageBed" HeaderText="Overnatning">
                            <HeaderStyle Width="125px" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="80px" />
                            <HeaderTemplate>
                                Slut
                            </HeaderTemplate>
                            <ItemStyle Wrap="true" />
                            <ItemTemplate>
                                <asp:Label ID="departureDate" runat="server" Text='<%# SetDayAndPeriode(DataBinder.Eval(Container.DataItem, "departureDate").ToString(), DataBinder.Eval(Container.DataItem, "departureOnDay").ToString())  %>'/>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td colspan="3"><asp:Label ID="lblInfo" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td colspan="3"><asp:Label ID="lblMessage" runat="server" Width="600px" /></td>
        </tr>
    </table>
</asp:Content>
