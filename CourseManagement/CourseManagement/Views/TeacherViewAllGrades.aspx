<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="TeacherViewAllGrades.aspx.cs" Inherits="CourseManagement.Views.TeacherViewAllGrades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:DropDownList ID="ddlCourses" runat="server" DataSourceID="odsCourses" DataTextField="Name" DataValueField="CRN">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="odsCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseBulletinByTeacherID" TypeName="CourseManagement.DAL.CourseDAL">
        <SelectParameters>
            <asp:SessionParameter Name="teacherIDCheck" SessionField="UserID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:Label ID="lblAssignmentSelected" runat="server"></asp:Label>

    <asp:GridView ID="gvwGradeItems" runat="server" OnSelectedIndexChanging="gvwGrade_SelectedIndexChanging" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="odsAssignments">
        <Columns>
            <asp:BoundField HeaderText="Name" DataField="Name" ReadOnly="True" SortExpression="Name"/>
            <asp:BoundField HeaderText="PossiblePoints" DataField="PossiblePoints" ReadOnly="True" SortExpression="PossiblePoints"/>
            <asp:BoundField HeaderText="GradeType" DataField="GradeType" ReadOnly="True" SortExpression="GradeType"/>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsAssignments" runat="server" SelectMethod="GetGradedItemsByCRN" TypeName="CourseManagement.DAL.GradedItemDAL">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:GridView ID="gvwStudents" runat="server" OnSelectedIndexChanging="gvwGrade_SelectedIndexChanging" AutoGenerateColumns="False" DataSourceID="odsStudents">
        <Columns>
            <asp:BoundField HeaderText="name" DataField="name" ReadOnly="True" SortExpression="name"/>
            <asp:BoundField HeaderText="StudentUID" DataField="StudentUID" ReadOnly="True" SortExpression="StudentUID"/>
            <asp:BoundField HeaderText="Email" DataField="Email" ReadOnly="True" SortExpression="Email"/>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsStudents" runat="server" SelectMethod="GetStudentsByCRN" TypeName="CourseManagement.DAL.StudentDAL">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br/>

    </asp:Content>