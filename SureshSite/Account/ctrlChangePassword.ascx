<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlChangePassword.ascx.cs" Inherits="SureshSite.Account.ctrlChangePassword" %>
<%--<div class="container">
  <h3>Change Password</h3>
<table style="border: 1px solid black" class="table table-hover">
    <tr>
        
    </tr>
    <tr>
        <td><label for="pwd">New Password:</label> </td>
        <td>:<asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" class="form-control" placeholder="Enter Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewPassword" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="New Password required" ForeColor="Red" Text="*">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td><label for="pwd">Confirm Password:</label> </td>
        <td>:<asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" class="form-control" placeholder="Re Enter Password">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfirmNewPassword" runat="server" ControlToValidate="txtConfirmNewPassword" Display="Dynamic" ErrorMessage="Confirm New Password required" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword" Display="Dynamic" ErrorMessage="New Password and Confirm New Password must match" ForeColor="Red" Operator="Equal" Text="*" Type="String">
            </asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>&nbsp;<asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" Width="70px" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMessage" runat="server">
            </asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
        </td>
    </tr>
</table>
    </div>--%>


<div class="container">
    <div class="form-group">
    <div class=""><label><b>New Password</b></label></div>
    <div class="col-xs-4">
    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" class="form-control" placeholder="Enter Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewPassword" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="New Password required" ForeColor="Red" Text="*">
            </asp:RequiredFieldValidator>
        </div><div></div>
     <div class=""><label><b>Confirm Password</b></label></div>
    <div class="col-xs-4">
   <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" class="form-control" placeholder="Re Enter Password">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfirmNewPassword" runat="server" ControlToValidate="txtConfirmNewPassword" Display="Dynamic" ErrorMessage="Confirm New Password required" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword" Display="Dynamic" ErrorMessage="New Password and Confirm New Password must match" ForeColor="Red" Operator="Equal" Text="*" Type="String">
            </asp:CompareValidator></div>
     <asp:Label ID="lblMessage" runat="server">
            </asp:Label>
    <asp:Button ID="btnSave" runat="server" class="form-control" onclick="btnSave_Click" Text="Save" Width="70px" />
    </div>
  </div>
