<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="StudentViewAllGrades.aspx.cs" Inherits="CourseManagement.Views.StudentViewAllGrades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 819px;
        }
        .auto-style5 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style3">
                <asp:DropDownList ID="ddlStudentCourses" runat="server" DataSourceID="odsCourses" DataTextField="Name" DataValueField="CRN" AutoPostBack="True">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseBulletinByStudentID" TypeName="CourseManagement.DAL.CourseDAL">
                    <SelectParameters>
                        <asp:SessionParameter Name="studentID" SessionField="UserID" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Prerequisites:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
                    <Columns>

                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
                        <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
                        <asp:BoundField DataField="CreditHours" HeaderText="CreditHours" ReadOnly="True" SortExpression="CreditHours" />
                        <asp:BoundField DataField="CRN" HeaderText="CRN" ReadOnly="True" SortExpression="CRN" />
                        <asp:BoundField DataField="SectionNumber" HeaderText="SectionNumber" ReadOnly="True" SortExpression="SectionNumber" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCoursesByCRN" TypeName="CourseManagement.DAL.CourseDAL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlStudentCourses" Name="CRN" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>
                <asp:ListView ID="lvwPrerequisites" runat="server">
                </asp:ListView>
            </td>
        </tr>
        <tr>
            <td class="auto-style5" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5" colspan="2">
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" AutoGenerateColumns="False" DataSourceID="odsGrades">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name" />
            <asp:BoundField DataField="Grade" HeaderText="Grade" ReadOnly="True" SortExpression="Grade" />
            <asp:BoundField DataField="Feedback" HeaderText="Feedback" SortExpression="Feedback" />
            <asp:BoundField DataField="PossiblePoints" HeaderText="PossiblePoints" ReadOnly="True" SortExpression="PossiblePoints" />
            <asp:BoundField DataField="GradeType" HeaderText="GradeType" ReadOnly="True" SortExpression="GradeType" />
            <asp:BoundField DataField="GradeId" HeaderText="GradeId" ReadOnly="True" SortExpression="GradeId" />
        </Columns>
    </asp:GridView>
<asp:ObjectDataSource ID="odsGrades" runat="server" SelectMethod="GetGradedItemsByStudentId" TypeName="CourseManagement.DAL.GradedItemDAL">
    <SelectParameters>
        <asp:SessionParameter Name="studentId" SessionField="UserID" Type="String" />
        <asp:ControlParameter ControlID="ddlStudentCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
</asp:Content>