<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="TeacherSummaryView.aspx.cs" Inherits="CourseManagement.Views.Teacher.TeacherSummaryView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="ddlSemesters" runat="server" AutoPostBack="True" DataSourceID="odsSemesters" DataTextField="SemesterID" DataValueField="SemesterID"></asp:DropDownList>
    <asp:ObjectDataSource ID="odsSemesters" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllSemesters" TypeName="CourseManagement.DAL.SemesterDAL"></asp:ObjectDataSource>
    <asp:DropDownList ID="ddlCourses" runat="server" AutoPostBack="True" DataSourceID="odsCourses" DataTextField="Name" DataValueField="CRN" EnableViewState="False"></asp:DropDownList>
    <asp:ObjectDataSource ID="odsCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCoursesByTeacherAndSemester" TypeName="CourseManagement.DAL.CourseDAL">
        <SelectParameters>
            <asp:SessionParameter Name="teacherIDCheck" SessionField="UserID" Type="String" />
            <asp:ControlParameter ControlID="ddlSemesters" Name="semesterID" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="gvwSummary" runat="server" EnableViewState="False">
    </asp:GridView>

    <br />
    <asp:Label ID="lblError" runat="server"></asp:Label>

</asp:Content>
