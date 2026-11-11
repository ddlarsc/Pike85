<%@ Page 
    AutoEventWireup="true" 
    CodeFile="FindFangster.aspx.cs" 
    Inherits="Fangster.FindFangster" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Fangster.master" 
    Title="Find Fangster" %>

<asp:Content ID="Content" ContentPlaceHolderID="FangsterBody" Runat="Server">
    <table style="text-align: center; width: 600px">
        <tr>
            <td style="width: 150px"/>
            <td style="text-align: center; width: 300px"><h4>Find Fangster</h4></td>
            <td style="width: 150px"/>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td/>
            <td><asp:DropDownList AutoPostBack="False" ID="drpPerson" runat="server" TabIndex="50" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td/>
            <td><asp:DropDownList AutoPostBack="False" ID="drpArt" runat="server" TabIndex="51" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td/>
            <td><asp:DropDownList AutoPostBack="False" ID="drpAntal" runat="server" TabIndex="52" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td/>
            <td><asp:Button ID="btnGet" runat="server" Width="300px" onclick="BtnGetClick" TabIndex="61" Text="Hent" /></td>
            <td/>
        </tr>
        <tr>
            <td colspan="3"><br/><hr/><br/></td>
        </tr>
        <tr>
            <td/>
            <td style="text-align: center; width: 300px"><h4>Danmark</h4></td>
            <td/>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <asp:DataGrid AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ID="dgDanmark" runat="server">
                    <FooterStyle BackColor="#CCCCCC"/>
                    <SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" VerticalAlign="Top" Wrap="False" />
                    <EditItemStyle VerticalAlign="Top" Wrap="False" />
                    <AlternatingItemStyle BackColor="#CCCCCC" VerticalAlign="Top" Wrap="False" />
                    <ItemStyle VerticalAlign="Top" Wrap="False" />
                    <HeaderStyle BackColor="#758DC7" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundColumn DataField="NR" HeaderText="Nr">
                            <HeaderStyle Width="40px"/>
                            <ItemStyle Wrap="False"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Dato" DataFormatString="{0:dd-MM-yy}" HeaderText="Dato">
                            <HeaderStyle Width="40px"/>
                            <ItemStyle Wrap="False"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Fanger" HeaderText="Fanger">
                            <HeaderStyle Width="40px"/>
                            <ItemStyle Wrap="False"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Vaegt" HeaderText="Vægt" DataFormatString="{0:N2}">
                            <HeaderStyle Width="40px"/>
                            <ItemStyle Wrap="False"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Fiskevand" HeaderText="Fiskevand">
                            <HeaderStyle Width="40px"/>
                            <ItemStyle Wrap="False"/>
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Billed">
                            <HeaderStyle Width="80px"/>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_Billed" runat="server" ImageUrl='<%# Pike85.ExternalHelper.DataItemIsEmpty(DataBinder.Eval(Container, "DataItem.Billed1").ToString(), DataBinder.Eval(Container, "DataItem.BilledMappe") + DataBinder.Eval(Container, "DataItem.Billed1").ToString() + "_thumb.jpg") %>' NavigateUrl='<%# Pike85.ExternalHelper.BuildPictureUrl(DataBinder.Eval(Container, "DataItem.BilledMappe").ToString() + DataBinder.Eval(Container, "DataItem.Billed1") ) %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999"/>
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td/>
            <td style="text-align: center; width: 300px"><h4>Udlandet</h4></td>
            <td/>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <asp:DataGrid AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ID="dgUdland" runat="server">
                    <FooterStyle BackColor="#CCCCCC"/>
                    <SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" VerticalAlign="Top" Wrap="False" />
                    <EditItemStyle VerticalAlign="Top" Wrap="False" />
                    <AlternatingItemStyle BackColor="#CCCCCC" VerticalAlign="Top" Wrap="False" />
                    <ItemStyle VerticalAlign="Top" Wrap="False" />
                    <HeaderStyle BackColor="#758DC7" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundColumn DataField="NR" HeaderText="Nr">
                            <HeaderStyle Width="40px"/>
                            <ItemStyle Wrap="False"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Dato" DataFormatString="{0:dd-MM-yy}" HeaderText="Dato">
                            <HeaderStyle Width="40px"/>
                            <ItemStyle Wrap="False"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Fanger" HeaderText="Fanger">
                            <HeaderStyle Width="40px"/>
                            <ItemStyle Wrap="False"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Vaegt" HeaderText="Vægt" DataFormatString="{0:N2}">
                            <HeaderStyle Width="40px"/>
                            <ItemStyle Wrap="False"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Fiskevand" HeaderText="Fiskevand">
                            <HeaderStyle Width="40px"/>
                            <ItemStyle Wrap="False"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Land" HeaderText="Land">
                            <HeaderStyle Width="40px"/>
                            <ItemStyle Wrap="False"/>
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Billed">
                            <HeaderStyle Width="80px"/>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_Billed" runat="server" ImageUrl='<%# Pike85.ExternalHelper.DataItemIsEmpty(DataBinder.Eval(Container, "DataItem.Billed1").ToString(), DataBinder.Eval(Container, "DataItem.BilledMappe") + DataBinder.Eval(Container, "DataItem.Billed1").ToString() + "_thumb.jpg") %>' NavigateUrl='<%# Pike85.ExternalHelper.BuildPictureUrl(DataBinder.Eval(Container, "DataItem.BilledMappe").ToString() + DataBinder.Eval(Container, "DataItem.Billed1") ) %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999"/>
                </asp:DataGrid>
            </td>
        </tr>
    </table>    
</asp:Content>

