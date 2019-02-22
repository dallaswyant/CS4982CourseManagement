<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" Inherits="CourseManagement.Views.DepartmentAdmin.ManageCourses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style7 {
            width: 260px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvwDepartmentCourses" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="odsDeptCourses" PageSize="5" OnSelectedIndexChanged="gvwDepartmentCourses_SelectedIndexChanged" DataKeyNames="CRN">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ReadOnly="True" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
            <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
            <asp:BoundField DataField="CreditHours" HeaderText="CreditHours" ReadOnly="True" SortExpression="CreditHours" />
            <asp:BoundField DataField="CRN" HeaderText="CRN" ReadOnly="True" SortExpression="CRN" />
            <asp:BoundField DataField="SectionNumber" HeaderText="SectionNumber" ReadOnly="True" SortExpression="SectionNumber" />
            <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" ReadOnly="True" SortExpression="DepartmentName" />
            <asp:BoundField DataField="MaxSeats" HeaderText="MaxSeats" ReadOnly="True" SortExpression="MaxSeats" />
            <asp:BoundField DataField="SemesterID" HeaderText="SemesterID" ReadOnly="True" SortExpression="SemesterID" />
            <asp:CommandField ShowSelectButton="True" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsDeptCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDepartmentCoursesByUserID" TypeName="CourseManagement.DAL.DepartmentAdminDAL" DataObjectTypeName="CourseManagement.App_Code.Course" InsertMethod="InsertNewCourse" UpdateMethod="UpdateCourse">
        <SelectParameters>
            <asp:SessionParameter Name="userID" SessionField="UserID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:DetailsView ID="dvwDepartmentCourses" runat="server" DefaultMode="Insert" Height="50px" Width="125px" DataSourceID="odsDeptCourses" AutoGenerateRows="False">
        <Fields>
            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
            <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
            <asp:BoundField DataField="CreditHours" HeaderText="CreditHours" ReadOnly="True" SortExpression="CreditHours" />
            <asp:BoundField DataField="CRN" HeaderText="CRN" ReadOnly="True" SortExpression="CRN" />
            <asp:BoundField DataField="SectionNumber" HeaderText="SectionNumber" ReadOnly="True" SortExpression="SectionNumber" />
            <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" ReadOnly="True" SortExpression="DepartmentName" />
            <asp:BoundField DataField="MaxSeats" HeaderText="MaxSeats" ReadOnly="True" SortExpression="MaxSeats" />
            <asp:BoundField DataField="SemesterID" HeaderText="SemesterID" ReadOnly="True" SortExpression="SemesterID" />
            <asp:CommandField ShowCancelButton="False" ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>
    <br />
    <br />
    <table class="tableInfo">
        <tr>
            <td class="auto-style7">
                <asp:DropDownList ID="ddlTeachers" runat="server" DataSourceID="odsDeptTeachers" DataTextField="Name" DataValueField="TeacherUID">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnAddTeachers" runat="server" Text="Assign Teacher" />
            </td>
        </tr>
        <tr>
            <td class="auto-style7">
                <asp:DetailsView ID="dvwTeacherCourse" runat="server" Height="50px" Width="125px" AutoGenerateRows="False" DataSourceID="odsTeacherCourse">
                    <Fields>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ReadOnly="True" />
                        <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
                        <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
                        <asp:BoundField DataField="CreditHours" HeaderText="CreditHours" ReadOnly="True" SortExpression="CreditHours" />
                        <asp:BoundField DataField="CRN" HeaderText="CRN" ReadOnly="True" SortExpression="CRN" />
                        <asp:BoundField DataField="SectionNumber" HeaderText="SectionNumber" ReadOnly="True" SortExpression="SectionNumber" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" ReadOnly="True" SortExpression="DepartmentName" />
                        <asp:BoundField DataField="MaxSeats" HeaderText="MaxSeats" ReadOnly="True" SortExpression="MaxSeats" />
                        <asp:BoundField DataField="SemesterID" HeaderText="SemesterID" ReadOnly="True" SortExpression="SemesterID" />
                    </Fields>
                </asp:DetailsView>
                <asp:ObjectDataSource ID="odsTeacherCourse" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCoursesByCRN" TypeName="CourseManagement.DAL.CourseDAL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="dvwDepartmentCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>
                <asp:GridView ID="gvwTeacherCourses" runat="server" AutoGenerateColumns="False" DataSourceID="odsTeacherCourses">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ReadOnly="True" />
                        <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
                        <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
                        <asp:BoundField DataField="CreditHours" HeaderText="CreditHours" ReadOnly="True" SortExpression="CreditHours" />
                        <asp:BoundField DataField="CRN" HeaderText="CRN" ReadOnly="True" SortExpression="CRN" />
                        <asp:BoundField DataField="SectionNumber" HeaderText="SectionNumber" ReadOnly="True" SortExpression="SectionNumber" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" ReadOnly="True" SortExpression="DepartmentName" />
                        <asp:BoundField DataField="MaxSeats" HeaderText="MaxSeats" ReadOnly="True" SortExpression="MaxSeats" />
                        <asp:BoundField DataField="SemesterID" HeaderText="SemesterID" ReadOnly="True" SortExpression="SemesterID" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsTeacherCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCoursesByTeacherID" TypeName="CourseManagement.DAL.CourseDAL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlTeachers" Name="teacherIDCheck" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="odsDeptTeachers" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllTeachersByAdminDepartment" TypeName="CourseManagement.DAL.DepartmentAdminDAL">
        <SelectParameters>
            <asp:SessionParameter Name="user" SessionField="User" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <br />
    <br />
    </asp:Content>
