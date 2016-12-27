<%@ Page Title="" Language="C#" MasterPageFile="~/SecureSite.Master" AutoEventWireup="true" CodeBehind="LockedAccounts.aspx.cs" Inherits="SureshSite.LockedAccounts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="font-family: Arial">
<asp:GridView ID="gvLockedAccounts" runat="server" AutoGenerateColumns="False" OnRowCommand="gvLockedAccounts_RowCommand" CssClass="table table-hover">
    <Columns>
        <asp:BoundField DataField="UserName" HeaderText="User Name" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="LockedDateTime" HeaderText="Locked Date &amp; Time" />
        <asp:BoundField DataField="HoursElapsed" HeaderText="Hours Elapsed">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="Enable">
            <ItemTemplate>
                <asp:Button ID="btnEnable" CommandArgument='<%# Eval("UserName") %>' runat="server"
                    Text="Enable" Enabled='<%#Convert.ToInt32(Eval("HoursElapsed")) > 24%>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</div>
</asp:Content>
