<%@ Control 
    AutoEventWireup="true" 
    CodeFile="UcDefaultPokaler.ascx.cs" 
    Inherits="UserControls.UcDefaultPokaler" 
    Language="C#" %>

<div style="text-align: center">
    <h3>Pokal oversigt</h3>
    <p>
        <asp:DataGrid ID="DataGrid_Pokaler" runat="server" AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" BackColor="White" ForeColor="Black">
            <FooterStyle BackColor="#CCCCCC"/>
            <SelectedItemStyle Font-Bold="True" Wrap="False" ForeColor="White" VerticalAlign="Top" BackColor="#000099"/>
            <EditItemStyle Wrap="False" VerticalAlign="Top"/>
            <AlternatingItemStyle Wrap="False" VerticalAlign="Top" BackColor="#CCCCCC"/>
            <ItemStyle Wrap="False" VerticalAlign="Top"/>
            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#758DC7"/>
            <Columns>
                <asp:BoundColumn DataField="Dato" HeaderText="Dato" DataFormatString="{0:dd-MM-yy}">
                    <HeaderStyle Width="40px"/>
                    <ItemStyle Wrap="False"/>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Art" HeaderText="Fiskeart">
                    <HeaderStyle Width="40px"/>
                    <ItemStyle Wrap="False"/>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Vaegt" HeaderText="V&#230;gt" DataFormatString="{0:N2}">
                    <HeaderStyle Width="25px"/>
                    <ItemStyle Wrap="False"/>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Billed">
                    <HeaderStyle Width="80px"/>
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink_Billed" runat="server" ImageUrl='<%# Pike85.ExternalHelper.DataItemIsEmpty(DataBinder.Eval(Container, "DataItem.Billed1").ToString(), DataBinder.Eval(Container, "DataItem.BilledMappe") + DataBinder.Eval(Container, "DataItem.Billed1").ToString() + "_thumb.jpg") %>' NavigateUrl='<%# Pike85.ExternalHelper.BuildPictureUrl(DataBinder.Eval(Container, "DataItem.BilledMappe").ToString() + DataBinder.Eval(Container, "DataItem.Billed1") ) %>'/>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:HyperLinkColumn Text="l&#230;s" DataNavigateUrlField="FangstId" DataNavigateUrlFormatString="~/Fangster/Fangster.aspx#{0}">
                    <ItemStyle Wrap="False"/>
                </asp:HyperLinkColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999"/>
        </asp:DataGrid>
    </p>    
</div>