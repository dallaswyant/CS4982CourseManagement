<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="CourseManagement.Views.Homepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .tableInfo {
        width: 100%;
    }
    .auto-style2 {
        width: 398px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tableInfo">
    <tr>
        <td class="auto-style2">
    <asp:TreeView ID="tvwSite" runat="server" DataSourceID="SiteMapDataSource1" TabIndex="-1">
</asp:TreeView>
            <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" SiteMapProvider="Student" />
        </td>
    </tr>
</table>
</asp:Content>
