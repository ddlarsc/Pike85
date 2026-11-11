<%@ Page Language="C#" MasterPageFile="~/Skabelon/Pike85.master" AutoEventWireup="true"
    CodeFile="Liste_Opslag.aspx.cs" Inherits="Pike85.Medlemmer.Liste_Opslag" Title="Medlem Liste Opslag side."
    CodeFileBaseClass="SkabelonPage" RuntimeMasterPageFile="~/Skabelon/Medlem.master" %>

<asp:Content ID="ContentMedlemListeOpslag" ContentPlaceHolderID="MedlemBody" runat="Server">
    <h3 style="text-align: center">
        Liste over Opslag</h3>
    <div style="text-align: left">
        <p>
            <asp:Label ID="Label_Output" runat="server" ForeColor="Blue" Font-Bold="True"></asp:Label></p>
        <p>
            År :&nbsp;
            <asp:DropDownList ID="DropDownList_Aar" TabIndex="1" runat="server" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownList_Aar_SelectedIndexChanged">
                <asp:ListItem Value="2005" Selected="True">2005</asp:ListItem>
            </asp:DropDownList></p>
        <p>
            <asp:DataGrid ID="DataGrid_Opslag" runat="server" ForeColor="Black" AllowPaging="True"
                BackColor="White" BorderWidth="1px" BorderStyle="Solid" BorderColor="#999999"
                AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" HorizontalAlign="Center">
                <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                <SelectedItemStyle Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#000099">
                </SelectedItemStyle>
                <EditItemStyle Wrap="False"></EditItemStyle>
                <AlternatingItemStyle Wrap="False" BackColor="#CCCCCC"></AlternatingItemStyle>
                <ItemStyle Wrap="False"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#758DC7"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="OpslagId"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Dato" HeaderText="Dato" DataFormatString="{0:d}">
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Overskrift" HeaderText="Overskrift">
                        <HeaderStyle Width="300px"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:EditCommandColumn ButtonType="PushButton" UpdateText="" HeaderText="Rediger"
                        CancelText="" EditText="Rediger"></asp:EditCommandColumn>
                    <asp:ButtonColumn Text="Slet" ButtonType="PushButton" HeaderText="Slet" CommandName="Delete">
                    </asp:ButtonColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Center" ForeColor="#CC0000" Position="TopAndBottom"
                    BackColor="Silver" PageButtonCount="5" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid></p>
    </div>
</asp:Content>
