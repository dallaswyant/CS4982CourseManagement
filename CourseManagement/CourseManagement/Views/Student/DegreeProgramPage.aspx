<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="DegreeProgramPage.aspx.cs" Inherits="CourseManagement.Views.Student.DegreeProgramPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style7 {
            width: 69px;
        }
        .auto-style8 {
            width: 342px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Degree Program:<br />
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" ToolTip="Your degree program.">
    </asp:DropDownList>
    <br />
    <table class="tableInfo">
        <tr>
            <td class="auto-style8">Courses Required:<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False">
                </asp:GridView>
            </td>
            <td class="auto-style7">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br />
    Courses Taken:<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
            <asp:BoundField DataField="CRN" HeaderText="CRN" SortExpression="CRN" />
            <asp:BoundField DataField="SectionNumber" HeaderText="SectionNumber" SortExpression="SectionNumber" />
            <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" SortExpression="DepartmentName" />
            <asp:BoundField DataField="MaxSeats" HeaderText="MaxSeats" SortExpression="MaxSeats" />
            <asp:BoundField DataField="SemesterID" HeaderText="SemesterID" SortExpression="SemesterID" />
            <asp:BoundField DataField="CourseTimeID" HeaderText="CourseTimeID" SortExpression="CourseTimeID" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCoursesByStudentID" TypeName="CourseManagement.DAL.CourseDAL">
        <SelectParameters>
            <asp:SessionParameter Name="studentUIDCheck" SessionField="UserID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    
    <br />
</asp:Content>
