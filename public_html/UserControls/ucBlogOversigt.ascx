<%@ Control 
    AutoEventWireup="true" 
    CodeFile="UcBlogOversigt.ascx.cs" 
    EnableViewState="true"
    Inherits="UserControls.UcBlogOversigt"
    Language="C#" 
    ViewStateMode="Enabled" %>
    
<div style="width: 95%; text-align: center; background: white; padding: 4px; margin-bottom: 2px; -webkit-border-radius: 3px 3px 3px 3px; -khtml-border-radius: 10px; -moz-border-radius: 10px;">    
    <h3>Seneste fra Bloggene</h3>
    <div style="text-align: left">
        <span id="bloggerText" runat="server" />
    </div>
</div>
