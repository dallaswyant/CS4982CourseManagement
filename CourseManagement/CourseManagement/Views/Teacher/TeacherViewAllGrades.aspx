<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="TeacherViewAllGrades.aspx.cs" Inherits="CourseManagement.Views.TeacherViewAllGrades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ObjectDataSource ID="odsCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseBulletinByTeacherID" TypeName="CourseManagement.DAL.CourseDAL">
        <SelectParameters>
            <asp:SessionParameter Name="teacherIDCheck" SessionField="UserID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table class="tableInfo">
        <tr>
            <td>

    <asp:DropDownList ID="ddlCourses" runat="server" DataSourceID="odsCourses" DataTextField="Name" DataValueField="CRN">
    </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlAssignments" runat="server" AutoPostBack="True" DataSourceID="odsAssignments" DataTextField="Value" DataValueField="Value" OnSelectedIndexChanged="ddlAssignments_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>

                &nbsp;</td>
            <td>
                <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit This Assignment" />
            </td>
        </tr>
</table>
    <asp:ObjectDataSource ID="odsAssignments" runat="server" SelectMethod="GetUniqueGradedItemsByCRN" TypeName="CourseManagement.DAL.GradedItemDAL" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:ScriptManager runat="server" ID="ScriptManager"/>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
    <asp:GridView ID="gvwStudents" runat="server" OnSelectedIndexChanging="gvwGrade_SelectedIndexChanging" AutoGenerateColumns="False" DataSourceID="odsStudents" AllowPaging="True" CssClass="table">
        <Columns>
            <asp:BoundField DataField="Student" HeaderText="Student" SortExpression="Student" />
            <asp:BoundField HeaderText="Grade" DataField="Grade" ReadOnly="True" SortExpression="Grade"/>
            <asp:BoundField HeaderText="PossiblePoints" DataField="PossiblePoints" ReadOnly="True" SortExpression="PossiblePoints"/>
            <asp:BoundField DataField="GradeType" HeaderText="GradeType" ReadOnly="True" SortExpression="GradeType" />
            <asp:BoundField HeaderText="Feedback" DataField="Feedback" SortExpression="Feedback"/>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsStudents" runat="server" SelectMethod="GetGradedItemsByCRNAndGradeNameForAllStudents" TypeName="CourseManagement.DAL.GradedItemDAL">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="ddlAssignments" Name="gradeName" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br/>

    </asp:Content>