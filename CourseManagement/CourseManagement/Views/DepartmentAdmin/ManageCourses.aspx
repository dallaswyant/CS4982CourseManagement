﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" Inherits="CourseManagement.Views.DepartmentAdmin.ManageCourses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style7 {
            width: 260px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvwDepartmentCourses" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="odsDeptCourses" OldValuesParameterFormatString="original{0}" PageSize="5" OnSelectedIndexChanged="gvwDepartmentCourses_SelectedIndexChanged" DataKeyNames="CRN,Name,Description,Location,CreditHours,SectionNumber,DepartmentName,MaxSeats,SemesterID" OnRowUpdating="gvwDepartmentCourses_RowUpdating" >
              <Columns>
            <asp:TemplateField HeaderText="CRN" SortExpression="CRN">
                <EditItemTemplate>
                    <asp:Label ID="crn" runat="server" Text='<%# Eval("CRN") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("CRN") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <EditItemTemplate>
                    <asp:Textbox ID="name" runat="server" Text='<%# Eval("Name") %>'></asp:Textbox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                <EditItemTemplate>
                    <asp:Textbox ID="description" runat="server" Text='<%# Eval("Description") %>'></asp:Textbox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location" SortExpression="Location">
                <EditItemTemplate>
                    <asp:Textbox ID="location" runat="server" Text='<%# Eval("Location") %>'></asp:Textbox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Credit Hours" SortExpression="CreditHours">
                <EditItemTemplate>
                    <asp:Textbox ID="creditHours" runat="server" Text='<%# Eval("CreditHours") %>'></asp:Textbox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("CreditHours") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Section" SortExpression="SectionNumber">
                <EditItemTemplate>
                    <asp:Textbox ID="section" runat="server" Text='<%# Eval("SectionNumber") %>'></asp:Textbox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("SectionNumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Department" SortExpression="DepartmentName">
                <EditItemTemplate>
                    <asp:Textbox ID="department" runat="server" Text='<%# Eval("DepartmentName") %>'></asp:Textbox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("DepartmentName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Max Seats" SortExpression="MaxSeats">
                <EditItemTemplate>
                    <asp:Textbox ID="seats" runat="server" Text='<%# Eval("MaxSeats") %>'></asp:Textbox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("MaxSeats") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Semester ID" SortExpression="SemesterID">
                <EditItemTemplate>
                    <asp:Label ID="semesterID" runat="server" Text='<%# Eval("SemesterID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("SemesterID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Select"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblGVWCoursesResults" runat="server" CssClass="error"></asp:Label>
    <br />
    <asp:ObjectDataSource ID="odsDeptCourses" runat="server" DataObjectTypeName="CourseManagement.Models.Course" DeleteMethod="DeleteCourseByDepartmentAndCRN" InsertMethod="InsertNewCourse" SelectMethod="GetDepartmentCoursesByUserID" TypeName="CourseManagement.DAL.DepartmentAdminDAL" UpdateMethod="UpdateNeedsFixingCourse" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter Name="userID" SessionField="UserID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
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
                <asp:Button ID="btnAddTeachers" runat="server" Text="Assign Teacher" OnClick="btnAddTeachers_Click" />
            &nbsp;<asp:Button ID="Button1" runat="server" OnClick="btnViewTeacherCourses_Click" Text="View Teacher Courses" />
            </td>
        </tr>
        <tr>
            <td class="auto-style7">
                <asp:DetailsView ID="dvwTeacherCourse" runat="server" Height="62px" Width="233px" AutoGenerateRows="False" DataSourceID="odsTeacherCourse">
                    <Fields>
                        <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" ReadOnly="True" SortExpression="DepartmentName" />
                        <asp:BoundField DataField="CRN" HeaderText="CRN" ReadOnly="True" SortExpression="CRN" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ReadOnly="True" />
                        <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
                        <asp:BoundField DataField="CreditHours" HeaderText="CreditHours" ReadOnly="True" SortExpression="CreditHours" />
                        <asp:BoundField DataField="MaxSeats" HeaderText="MaxSeats" ReadOnly="True" SortExpression="MaxSeats" />
                        <asp:BoundField DataField="SectionNumber" HeaderText="SectionNumber" ReadOnly="True" SortExpression="SectionNumber" />
                        <asp:BoundField DataField="SemesterID" HeaderText="SemesterID" ReadOnly="True" SortExpression="SemesterID" />
                    </Fields>
                </asp:DetailsView>
                <asp:ObjectDataSource ID="odsTeacherCourse" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseByCRN" TypeName="CourseManagement.DAL.CourseDAL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="gvwDepartmentCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
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
