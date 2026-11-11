<%@ Control 
    AutoEventWireup="true" 
    CodeFile="UcOpslag.ascx.cs" 
    Inherits="UserControls.UcOpslag" 
    Language="C#" %>

<div style="border-style: none; width: 600px; text-align:left">
    <p><a id="anchor" runat="server" /><b><asp:Label ID="lblOverskrift" runat="server" BorderStyle="None" /></b></p>
    <p><asp:Label ID="lblBeskrivelse" runat="server" BorderStyle="None" /></p>
    <p><a href="" id="refLink" runat="server" target="_blank"><asp:Label ID="lblLink" runat="server" /></a></p>
    <p><a href="" id="refBilled" runat="server" target="_blank"><asp:image ID="imgBilled" runat="server" /></a></p>
    <div style="text-align: right; font-size: xx-small; color: Gray"><asp:Label ID="lblDato" runat="server" Width="100px" BorderStyle="None" /></div>
    <br/>
</div>
<hr />
