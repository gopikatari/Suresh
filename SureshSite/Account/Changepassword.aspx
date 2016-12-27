<%@ Page Title="" Language="C#" MasterPageFile="~/Account/Site.Master" AutoEventWireup="true" CodeBehind="Changepassword.aspx.cs" Inherits="SureshSite.Account.Changepassword" EnableEventValidation="false" %>
<%@ Register src="ctrlChangePassword.ascx" tagname="ctrlChangePassword" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <uc1:ctrlChangePassword ID="ctrlChangePassword1" runat="server" />
&nbsp;
    
</asp:Content>
