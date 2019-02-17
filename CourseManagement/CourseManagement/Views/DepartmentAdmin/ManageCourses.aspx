<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" Inherits="CourseManagement.Views.DepartmentAdmin.ManageCourses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style7 {
            width: 260px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DetailsView ID="dtvDepartmentCourses" runat="server" Height="50px" Width="125px">
    </asp:DetailsView>
    <asp:ObjectDataSource ID="odsDeptCourses" runat="server"></asp:ObjectDataSource>
    <br />
    <table class="tableInfo">
        <tr>
            <td class="auto-style7">
                <asp:DropDownList ID="ddlTeachers" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnAddTeachers" runat="server" Text="Add Teachers" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:GridView ID="gvwTeacherCourses" runat="server">
    </asp:GridView>
    <br />
    <asp:ObjectDataSource ID="odsTeacherCourses" runat="server"></asp:ObjectDataSource>
    <br />
    </asp:Content>
