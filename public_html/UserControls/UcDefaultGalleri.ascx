<%@ Control 
    ClassName="UcDefaultGalleri" 
    CodeFile="UcDefaultGalleri.ascx.cs" 
    Inherits="UserControls.UcDefaultGalleri" 
    Language="C#" %>

<h3>Galleri</h3>
<script type="text/javascript">
    function galleriPopup(url) {
        var w = window.open("", "galleriPopup", "resizable=1,scrollbars=1,width=800,height=600");
        if (!w) { return true; }
        w.document.write("<html><head><title>Billede</title></head><body style='margin:0;background:#000;text-align:center'>" +
            "<img src='" + url + "' style='max-width:100%;max-height:100vh' onclick='window.close()' /></body></html>");
        w.document.close();
        w.focus();
        return false;
    }
</script>
<asp:Literal ID="LiteralGalleri" runat="server" />
