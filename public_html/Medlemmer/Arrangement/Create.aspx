<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Create.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Medlemmer.Arrangement.Arrangement" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    Title="Opsætning af arrangementer" %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentOpretArrangement" runat="server">
    <script type="text/javascript">
        (function () {
            var map = { pickDate1: 'txtDeadlineDate', pickDate2: 'txtStartDate', pickDate3: 'txtEndDate' };
            function find(suffix) { return document.querySelector('[id$="' + suffix + '"]'); }
            function toIso(s) {
                var m = /^(\d{1,2})-(\d{1,2})-(\d{4})$/.exec((s || '').trim());
                return m ? m[3] + '-' + ('0' + m[2]).slice(-2) + '-' + ('0' + m[1]).slice(-2) : '';
            }
            function toDk(s) { var p = s.split('-'); return p[2] + '-' + p[1] + '-' + p[0]; }
            function open(img, box) {
                var d = document.createElement('input');
                d.type = 'date';
                d.value = toIso(box.value);
                var r = img.getBoundingClientRect();
                d.style.cssText = 'position:fixed;left:' + r.left + 'px;top:' + r.bottom + 'px;width:1px;height:1px;opacity:0;border:0;padding:0';
                document.body.appendChild(d);
                var done = function () { if (d.parentNode) d.parentNode.removeChild(d); };
                d.addEventListener('change', function () {
                    if (d.value) { box.value = toDk(d.value); if (box.onchange) box.onchange(); }
                    done();
                });
                d.addEventListener('blur', function () { setTimeout(done, 200); });
                d.focus();
                if (d.showPicker) { d.showPicker(); } else { d.click(); }
            }
            function init() {
                for (var k in map) {
                    var img = find(k);
                    if (img && !img.getAttribute('src')) img.src = '/images/icon_calendar_day.png';
                    if (img) img.style.cursor = 'pointer';
                }
            }
            document.addEventListener('click', function (e) {
                var img = e.target;
                if (!img || !img.id) return;
                for (var k in map) {
                    if (img.id.slice(-k.length) === k) {
                        e.preventDefault();
                        var box = find(map[k]);
                        if (box) open(img, box);
                        return;
                    }
                }
            });
            if (document.readyState === 'loading') document.addEventListener('DOMContentLoaded', init); else init();
        })();
    </script>    <table style="border-spacing: 2px; width: 600px">
        <tr style="text-align: center">
            <td style="width:150px"/>
            <td style="width:300px"><h4>Opret arrangement / Rediger arrangement</h4></td>
            <td style="width:150px"/>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td/>
            <td><asp:DropDownList AutoPostBack="True" ID="drpGetArrangement" onselectedindexchanged="DrpGetArrangementSelectedIndexChanged" runat="server" TabIndex="49" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td colspan="3"><br/><hr /><br /></td>
        </tr>
        <tr style="text-align:left">
            <td style="text-align: left">Id:</td>
            <td style="text-align: left">
                <asp:Textbox Enabled="false" ID="txtId" runat="server" Width="50px" TabIndex="48" />
                <asp:Textbox Enabled="False" ID="txtUser" runat="server" style="margin-bottom: 0px" TabIndex="49" Width="246px" />
            </td>
            <td></td>            
        </tr>
        <tr style="text-align:left">
            <td>Overskrift:</td>
            <td><asp:TextBox ID="txtOverskrift" MaxLength="255" runat="server" TabIndex="51" Width="300px" /></td>
            <td>
                <asp:RequiredFieldValidator ID="rfvHeader" runat="server" />
                <a id="anchorCal" runat="server"/>
            </td>
        </tr>
        <tr style="text-align:left">
            <td>Sted:</td>
            <td><asp:TextBox ID="txtSted" MaxLength="255" runat="server" TabIndex="52" Width="300px" /></td>
            <td><asp:RequiredFieldValidator ID="rfvLocation" runat="server" /></td>
        </tr>
        <tr style="text-align:left">
            <td>Bemærkninger:</td>
            <td><asp:TextBox ID="txtRemarks" runat="server" TabIndex="53" Width="300px" TextMode="MultiLine" Rows="5" MaxLength="2048" /></td>
            <td><asp:RequiredFieldValidator ID="rfvRemarks" runat="server" /></td>
        </tr>
        <tr style="text-align:left">
            <td>Deadline dato:</td>
            <td><asp:TextBox ID="txtDeadlineDate" runat="server" TabIndex="54" Width="300px"></asp:TextBox></td>
            <td>
                <img alt="Open Calendar" id="pickDate1" onmouseout="this.style.cursor='pointer'" onmouseover="this.style.cursor='hand'" runat="server" src="" />
                <asp:RequiredFieldValidator ID="rfvDeadlineDate" runat="server" />
                <asp:RegularExpressionValidator ID="revDeadlineDate" runat="server" />
            </td>
        </tr>
        <tr style="text-align:left">
            <td>Startdato:</td>
            <td><asp:TextBox ID="txtStartDate" runat="server" TabIndex="55" Width="300px" /></td>
            <td>
                <img alt="Open Calendar" id="pickDate2" onmouseout="this.style.cursor='pointer'" onmouseover="this.style.cursor='hand'" runat="server" src="" />
                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" />
                <asp:RegularExpressionValidator ID="revStartDate" runat="server" />
                <asp:CompareValidator ID="cvStartDate" runat="server"/>
            </td>
        </tr>
        <tr style="text-align:left">
            <td>Slutdato:</td>
            <td><asp:TextBox ID="txtEndDate" runat="server" TabIndex="56" Width="300px" /></td>
            <td>
                <img alt="Open Calendar" id="pickDate3" runat="server" src="" />
                <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" />
                <asp:RegularExpressionValidator ID="revEndDate" runat="server" />
                <asp:CompareValidator ID="cvEndDate" runat="server"/>
            </td>
        </tr>
        <tr style="text-align:left">
            <td>Send e-mail:</td>
            <td><asp:DropDownList ID="drpMailAddressTo" runat="server" TabIndex="57" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr>
            <td/>
            <td><asp:Button ID="btnCreate" onclick="BtnCreateClick" runat="server" TabIndex="58" Text="Opret" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td/>
            <td><asp:Button Enabled="False" ID="btnUpdate" onclick="BtnUpdateClick" runat="server" TabIndex="59" Text="Gem" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td/>
            <td><asp:Button Enabled="False" ID="btnDelete" onclick="BtnDeleteClick" runat="server" TabIndex="60" Text="Slet" Width="300px" /></td>
            <td/>
        </tr>
        <tr>
            <td colspan="3"><br /></td>
        </tr>
        <tr style="text-align:center">
           <td colspan="3">
            <asp:Label id="lblMessages" runat="server" />
            <br />
            <asp:ValidationSummary ID="vldSummery" runat="server" />
          </td> 
        </tr>
    </table>
    <script type="text/javascript">
    function getDate(field)
    {
        return field.defaultValue;
    }
    </script>
</asp:Content>