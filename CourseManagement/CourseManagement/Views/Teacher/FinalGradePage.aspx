﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="FinalGradePage.aspx.cs" Inherits="CourseManagement.Views.Teacher.FinalGradePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style7 {
            width: 410px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tableInfo">
        <tr>
            <td>
                <asp:DropDownList ID="ddlSemesters" runat="server" DataSourceID="odsSemesters" DataTextField="SemesterID" DataValueField="SemesterID">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsSemesters" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetTermsInProgress" TypeName="CourseManagement.DAL.SemesterDAL"></asp:ObjectDataSource>
            </td>
            <td>
                <asp:DropDownList ID="ddlCourses" runat="server" DataSourceID="odsCourses" DataTextField="Name" DataValueField="CRN">
                </asp:DropDownList>                    
                <asp:ObjectDataSource ID="odsCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCoursesByTeacherAndSemester" TypeName="CourseManagement.DAL.CourseDAL">
                        <SelectParameters>
                            <asp:SessionParameter Name="teacherIDCheck" SessionField="UserID" Type="String" />
                            <asp:ControlParameter ControlID="ddlSemesters" Name="semesterID" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
            </td>
            <td>
                <asp:DropDownList ID="ddlStudents" runat="server" DataSourceID="odsStudents">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsStudents" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetStudentsByCRN" TypeName="CourseManagement.DAL.StudentDAL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="odsCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <table class="tableInfo">
        <tr>
            <td class="auto-style7">
                <asp:GridView ID="gvwGrades" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="odsGrades" OnDataBound="gvwGrades_DataBound" PageSize="3">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="Grade" />
                        <asp:BoundField DataField="PossiblePoints" HeaderText="PossiblePoints" ReadOnly="True" SortExpression="PossiblePoints" />
                        <asp:BoundField DataField="GradeType" HeaderText="GradeType" ReadOnly="True" SortExpression="GradeType" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsGrades" runat="server" SelectMethod="GetGradedItemsByStudentId" TypeName="CourseManagement.DAL.GradeItemDAL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlStudents" Name="studentUIDCheck" PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="ddlCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Final Grade:"></asp:Label>
                <br />
                <asp:TextBox ID="finalGrade" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Save" Width="129px" />
            </td>
        </tr>
    </table>
    <asp:Label ID="Label2" runat="server" Text="Points earned:"></asp:Label>
    &nbsp;<asp:Label ID="pointsEarned" runat="server" Text="points/total"></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Percent:"></asp:Label>
    &nbsp;<asp:Label ID="gradePercentage" runat="server" Text="0.00%"></asp:Label>
</asp:Content>
