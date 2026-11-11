<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    CodeFileBaseClass="SkabelonPage" 
    Inherits="Medlemmer.Default" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Medlem.master" 
    RuntimeMasterPageFile="~/Skabelon/Medlem.master" 
    Title="Medlem start side." %>

<asp:Content ContentPlaceHolderID="MedlemBody" ID="ContentMedlemDefault" runat="Server">
    <p style="text-align: left">
        Dette er medlemmernes startside, hvor der er mulighed for at indtast nyheder og
        fangst.
    </p>
    <p style="text-align: left">
        Ligeledes vil der være link til statistikker og andet, som KUN kan ses af medlemmer
        som er logget ind på denne side.</p>
</asp:Content>
