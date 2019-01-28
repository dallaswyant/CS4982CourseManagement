<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="ManageCreateGradeItem.aspx.cs" Inherits="CourseManagement.ManageCreateGradeItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 { width: 100%; }

        .auto-style2 { width: 218px; }

        .auto-style3 { height: 29px; }

        .auto-style4 { height: 26px; }

        .auto-style5 {
            height: 29px;
            width: 661px;
        }

        .auto-style6 {
            height: 26px;
            width: 661px;
        }

        .auto-style7 { width: 661px; }

        .auto-style9 {
            height: 26px;
            width: 88px;
        }

        .auto-style10 {
            height: 26px;
            width: 75px;
        }

        .auto-style11 {
            height: 18px;
            width: 661px;
        }

        .auto-style12 { height: 18px; }
        .auto-style13 {
            width: 661px;
            height: 23px;
        }
        .auto-style14 {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style7">
                &nbsp;</td>
            <td colspan="3">
                Course:</td>
        </tr>
        <tr>
            <td class="auto-style7">
                &nbsp;</td>
            <td colspan="3">

    <asp:DropDownList ID="ddlCourses" runat="server" DataSourceID="odsCourses" DataTextField="Name" DataValueField="CRN">
    </asp:DropDownList>
                <br />
    <asp:ObjectDataSource ID="odsCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseBulletinByTeacherID" TypeName="CourseManagement.DAL.CourseDAL">
        <SelectParameters>
            <asp:SessionParameter Name="teacherIDCheck" SessionField="UserID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td class="auto-style7">
                &nbsp;</td>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7">
                &nbsp;
            </td>
            <td colspan="3">
                Assignment name:
            </td>
        </tr>
        <tr>
            <td class="auto-style5">
                &nbsp;
            </td>
            <td colspan="3" class="auto-style3">
                <asp:TextBox ID="tbxAssignmentName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style7">
                &nbsp;
            </td>
            <td class="auto-style2" colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="auto-style13">
                &nbsp;
            </td>
            <td colspan="3" class="auto-style14">
                Assignment Type:
            </td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlAssignmentType" runat="server" DataSourceID="odsGradeType" DataTextField="AssignmentType" DataValueField="AssignmentType">
                    <asp:ListItem>Assignment Type</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:ObjectDataSource ID="odsGradeType" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseRubricByCRN" TypeName="CourseManagement.DAL.CourseRubricDAL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td colspan="3">Possible Points:</td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td colspan="3">
                <asp:TextBox ID="tbxPossiblePoints" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style6">&nbsp;</td>
            <td colspan="3" class="auto-style4">Weight:</td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td colspan="3">
                <asp:Label ID="lblWeight" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style11"></td>
            <td colspan="3" class="auto-style12"></td>
        </tr>
        <tr>
            <td class="auto-style11">&nbsp;</td>
            <td colspan="3" class="auto-style12">Due Date:</td>
        </tr>
        <tr>
            <td class="auto-style11">&nbsp;</td>
            <td colspan="3" class="auto-style12">
                <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
            </td>
        </tr>
        <tr>
            <td class="auto-style11">&nbsp;</td>
            <td colspan="3" class="auto-style12">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style11">&nbsp;</td>
            <td colspan="3" class="auto-style12">Time Due:</td>
        </tr>
        <tr>
            <td class="auto-style11"></td>
            <td colspan="3" class="auto-style12">
                <asp:TextBox ID="tbxDueDate" runat="server" TextMode="DateTime"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style11">&nbsp;</td>
            <td colspan="3" class="auto-style12">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style11">&nbsp;</td>
            <td colspan="3" class="auto-style12">Description:</td>
        </tr>
        <tr>
            <td class="auto-style11">&nbsp;</td>
            <td colspan="3" rowspan="2">
                <asp:TextBox ID="TextBox1" runat="server" Height="211px" Width="445px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style11">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style11">&nbsp;</td>
            <td colspan="3" class="auto-style12">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style6"></td>
            <td class="auto-style10">
                <asp:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" Text="Create"/>
            </td>
            <td class="auto-style9">
                &nbsp;<asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"/>
            </td>
            <td class="auto-style4">
                <asp:Button ID="btnDelete0" runat="server" OnClick="btnDelete_Click" Text="Back"/>
            </td>
        </tr>
        <tr>
            <td class="auto-style6"></td>
            <td colspan="3" class="auto-style4"></td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td colspan="3">&nbsp;</td>
        </tr>
    </table>
</asp:Content>