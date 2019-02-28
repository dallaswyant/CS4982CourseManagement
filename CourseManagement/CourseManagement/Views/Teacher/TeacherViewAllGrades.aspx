<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="TeacherViewAllGrades.aspx.cs" Inherits="CourseManagement.Views.TeacherViewAllGrades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style7 {
            height: 67px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <asp:ObjectDataSource ID="odsSemesters" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllSemesters" TypeName="CourseManagement.DAL.SemesterDAL"></asp:ObjectDataSource>
    <asp:DropDownList ID="ddlSemesters" runat="server" AutoPostBack="True" DataSourceID="odsSemesters" DataTextField="SemesterID" DataValueField="SemesterID" OnSelectedIndexChanged="ddlSemesters_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <table class="tableInfo">
        <tr>
            <td class="auto-style7">

    <asp:DropDownList ID="ddlCourses" runat="server" DataTextField="Name" DataValueField="CRN" AutoPostBack="True" OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged">
    </asp:DropDownList>
            </td>
            <td class="auto-style7">
                <asp:DropDownList ID="ddlAssignments" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="ddlAssignments_SelectedIndexChanged" DataSourceID="odsAssignments" DataTextField="Value" DataValueField="Value" >
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsAssignments" runat="server" SelectMethod="GetUniqueGradedItemsByCRN" TypeName="CourseManagement.DAL.GradeItemDAL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>

                &nbsp;</td>
            <td>
                <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit This Assignment" />
    <asp:ScriptManager runat="server" ID="ScriptManager"/>
            </td>
        </tr>
        <tr>
            <td>

                Assignment visible:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:CheckBox ID="cbxVisibility" runat="server" AutoPostBack="True" OnCheckedChanged="cbxVisibility_CheckedChanged" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
</table>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
    <asp:GridView ID="gvwStudents" runat="server" OnSelectedIndexChanging="gvwGrade_SelectedIndexChanging" AutoGenerateColumns="False" DataSourceID="odsStudents" AllowPaging="True" CssClass="table">
        <Columns>
            <asp:TemplateField HeaderText="Student" SortExpression="Student">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Student") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Student.Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Grade" DataField="Grade" ReadOnly="True" SortExpression="Grade"/>
            <asp:BoundField HeaderText="PossiblePoints" DataField="PossiblePoints" ReadOnly="True" SortExpression="PossiblePoints"/>
            <asp:BoundField DataField="GradeType" HeaderText="GradeType" ReadOnly="True" SortExpression="GradeType" />
            <asp:BoundField HeaderText="Feedback" DataField="Feedback" SortExpression="Feedback"/>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <asp:ObjectDataSource ID="odsStudents" runat="server" SelectMethod="GetGradedItemsByCRNAndGradeNameForAllStudents" TypeName="CourseManagement.DAL.GradeItemDAL" >
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="ddlAssignments" Name="gradeName" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br/>

    </asp:Content>