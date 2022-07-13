<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="doc_ocr._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Carga tu documento</h2>
        <div class="form-group">
        <label for="oFile">Documento</label>
        <input id="oFile" type="file" runat="server" NAME="oFile" CssClass="form-control"> 
            </div>
        <div class="form-group">
        <asp:button id="btnUpload" type="submit" text="Subir Archivo" runat="server" CssClass="btn btn-primary" OnClick="btnUpload_Click"></asp:button>
            </div>
    </div>

    <div class="jumbotron">
        <h2>Datos</h2>
        <asp:Panel ID="frmConfirmation" Visible="False" Runat="server">
         <asp:Label id="lblUploadResult" Runat="server"></asp:Label>
        </asp:Panel>
    </div>

    

</asp:Content>
