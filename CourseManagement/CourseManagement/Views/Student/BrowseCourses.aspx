<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="BrowseCourses.aspx.cs" Inherits="CourseManagement.Views.Student.BrowseCourses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
<asp:Label ID="Label3" runat="server" Text="Choose Department:"></asp:Label>
<br />
<asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="odsDepartment" DataTextField="DeptName" DataValueField="DeptCourses">
</asp:DropDownList>
<asp:ObjectDataSource ID="odsDepartment" runat="server" SelectMethod="GetAllDepartments" TypeName="CourseManagement.DAL.DepartmentDAL"></asp:ObjectDataSource>
<br />
<asp:Label ID="Label1" runat="server" Text="Your Courses"></asp:Label>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="odsUserCourses">
    <Columns>
        <asp:BoundField DataField="CRN" HeaderText="CRN" ReadOnly="True" SortExpression="CRN" />
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
        <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
        <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
        <asp:BoundField DataField="CreditHours" HeaderText="CreditHours" ReadOnly="True" SortExpression="CreditHours" />
        <asp:BoundField DataField="SectionNumber" HeaderText="SectionNumber" ReadOnly="True" SortExpression="SectionNumber" />
    </Columns>
</asp:GridView>
<asp:ObjectDataSource ID="odsUserCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseBulletinByStudentID" TypeName="CourseManagement.DAL.CourseDAL">
    <SelectParameters>
        <asp:SessionParameter Name="studentID" SessionField="UserID" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<asp:Label ID="Label2" runat="server" Text="Department Courses"></asp:Label>
<br />
<asp:GridView ID="GridView2" runat="server">
</asp:GridView>
<br />
<br />
</asp:Content>
