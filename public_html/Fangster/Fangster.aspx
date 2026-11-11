<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Fangster.aspx.cs" 
    Inherits="Fangster.Fangster" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Fangster.master" 
    Title="Fangster" %>

<asp:Content ID="ContentFangster" ContentPlaceHolderID="FangsterBody" runat="Server">
    <table>
        <tr>
            <td class="" style="vertical-align: top; width: 100%; text-align: left">
                <table id="Table3" style="padding: 1px; width: 100%; border-width: 0px; border-spacing: 1px">
                    <tr style="height: 20">
                        <td style="width: 100px; vertical-align: top; horiz-align: left">
                            <p><asp:LinkButton ID="LinkButton_Forrige_Top" runat="server" OnClick="LinkButtonForrigeTopClick">Forrige Side</asp:LinkButton></p>
                            <p>&nbsp;</p>
                        </td>
                        <td>
                            <h3 style="text-align: center">Fangster</h3>
                        </td>
                        <td style="width: 100px; height: 40px; vertical-align: top; horiz-align: right">
                            <asp:LinkButton ID="LinkButton_Naeste_Top" runat="server" OnClick="LinkButtonNaesteTopClick">Næste Side</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <p><asp:Label ID="Label_Output" runat="server" ForeColor="Red"/></p>
                <span id="fangster" runat="server"></span>
                <table id="Table4" style="padding: 1px; width: 100%; border-width: 0px; border-spacing: 1px">
                    <tr>
                        <td style="width: 50%; vertical-align: top; horiz-align: left">
                            <p><asp:LinkButton ID="LinkButton_Forrige_Bund" runat="server" OnClick="LinkButtonForrigeBundClick">Forrige Side</asp:LinkButton></p>
                        </td>
                        <td style="width: 50%; height: 40px; vertical-align: top; horiz-align: right">
                            <asp:LinkButton ID="LinkButton_Naeste_Bund" runat="server" OnClick="LinkButtonNaesteBundClick">Næste Side</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
