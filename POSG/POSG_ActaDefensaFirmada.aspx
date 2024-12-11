﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="POSG_ActaDefensaFirmada.aspx.cs" Inherits="POSG_ActaDefensaFirmada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div class="alert alert-info text-center"> <h4>FORMULARIO PARA SUBIR LA ACTA DE DEFENSA FIRMADA</h4> </div>
    <asp:HiddenField ID="hdnIdActa" runat="server" />
    <div class="form-group alert alert-secondary">
        <label  Class="text-dark" for="fileUpload">Seleccione el archivo de acta de defensa firmada : </label>
        <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group">
        <asp:Button ID="btnUpload" runat="server" Text="Subir" OnClick="btnUpload_Click" CssClass="btn btn-primary mx-2" />

        <asp:Button ID="btnCancelar" runat="server" Text="Atras" OnClientClick="window.location.href='POSG_ActasDefensa.aspx'; return false;" CssClass="btn btn-danger" />
    </div>
    <asp:Label ID="lblMessage" runat="server" CssClass="text-success"></asp:Label>
</asp:Content>