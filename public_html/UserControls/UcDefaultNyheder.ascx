<%@ Control 
    AutoEventWireup="true" 
    CodeFile="UcDefaultNyheder.ascx.cs" 
    Inherits="UserControls.UcDefaultNyheder" 
    Language="C#" %>
    
<div style="width: 95%; text-align:center">
    <h3>Seneste Nyheder</h3>
    <div style="text-align: left">
        <span id="spanNyheder" runat="server"></span>
    </div>
    &gt;&gt; <a href="/Nyheder/VisNyheder.aspx">Se flere nyheder </a>&lt;&lt;
    <br/>
</div>
