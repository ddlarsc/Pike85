<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcGeneralforsamling.ascx.cs" Inherits="UserControls.UcGeneralforsamling" %>
<div style="border: lightgray; border-style:solid; border-width: thin; width: 300px; padding:10px">
    <div style="text-align: center">
        <h4>
            <asp:Label ID="lblHeader" runat="server" BorderStyle="None"></asp:Label>
        </h4>
    </div>
    <div style="text-align: left">
        <asp:Label ID="lblBody" runat="server" BorderStyle="None"></asp:Label>
    </div>
    <div style="text-align: center">
        <p>
            <asp:HyperLink ID="hlLink" runat="server" Target="_blank" ></asp:HyperLink>
        </p>
    </div>
    <div style="text-align: left">
        <asp:Label ID="lblFooter" runat="server" ></asp:Label>
    </div>
    <div style="text-align: right; font-size: xx-small; color: Gray">
        <asp:Label ID="lblTimestamp" runat="server" Width="100px" BorderStyle="None"></asp:Label>
    </div>
</div>
<br/>
