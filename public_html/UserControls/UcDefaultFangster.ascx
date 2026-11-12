<%@ Control 
    AutoEventWireup="true" 
    CodeFile="UcDefaultFangster.ascx.cs" 
    Inherits="UserControls.UcDefaultFangster" 
    Language="C#" %>

<div style="text-align: center">
    <h3>Seneste Fangst Rapporter</h3>
    <style type="text/css">.fangstBilled { min-width: 130px; }</style>
    <p>
        <asp:DataGrid ID="DataGrid_Fangster" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" BackColor="White" ForeColor="Black">
            <FooterStyle BackColor="#CCCCCC"></FooterStyle>
            <SelectedItemStyle Font-Bold="True" Wrap="False" ForeColor="White" VerticalAlign="Top" BackColor="#000099"></SelectedItemStyle>
            <EditItemStyle Wrap="False" VerticalAlign="Top"></EditItemStyle>
            <AlternatingItemStyle Wrap="False" VerticalAlign="Top" BackColor="#CCCCCC"></AlternatingItemStyle>
            <ItemStyle Wrap="False" VerticalAlign="Top"></ItemStyle>
            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#758DC7"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="Dato" HeaderText="Dato" DataFormatString="{0:dd-MM-yy}">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Art" HeaderText="Fiskeart">
                    <HeaderStyle Width="100%"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Vaegt" HeaderText="V&#230;gt" DataFormatString="{0:N2}">
                    <HeaderStyle Width="25px"></HeaderStyle>
                    <ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Billed">
                    <HeaderStyle Width="130px" CssClass="fangstBilled"></HeaderStyle>
                    <ItemStyle CssClass="fangstBilled"></ItemStyle>
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink_Billed" runat="server" ImageUrl='<%# Pike85.ExternalHelper.DataItemIsEmpty(DataBinder.Eval(Container, "DataItem.Billed1").ToString(), DataBinder.Eval(Container, "DataItem.BilledMappe") + DataBinder.Eval(Container, "DataItem.Billed1").ToString() + "_thumb.jpg") %>' NavigateUrl='<%# Pike85.ExternalHelper.BuildPictureUrl(DataBinder.Eval(Container, "DataItem.BilledMappe").ToString() + DataBinder.Eval(Container, "DataItem.Billed1") ) %>'>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:HyperLinkColumn Text="l&#230;s" DataNavigateUrlField="FangstId" DataNavigateUrlFormatString="/Fangster/Fangster.aspx#{0}">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Wrap="False"></ItemStyle>
                </asp:HyperLinkColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999"></PagerStyle>
        </asp:DataGrid>
    </p>
    &gt;&gt; <a href="/Fangster/Fangster.aspx">Se flere&nbsp;fangst rapporter</a> &lt;&lt;
</div>