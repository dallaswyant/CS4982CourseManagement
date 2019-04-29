<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CreateSemesters.aspx.cs" Inherits="Admin.Views.DepartmentAdmin.AddSemesters" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style7 {
            width: 296px;
        }
        .auto-style8 {
            width: 296px;
            height: 59px;
        }
        .auto-style9 {
            height: 59px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error" />
    <table class="tableInfo">
        <tr>
            <td class="auto-style7">Season:<br />
                <asp:DropDownList ID="semesterSeason" runat="server">
                </asp:DropDownList>
            </td>
            <td>Year:<br />
                <asp:DropDownList ID="semesterYear" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Start Date:<br />
                <asp:TextBox ID="startDate" runat="server" TextMode="Date"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="startDate" CssClass="error" Display="None" ErrorMessage="The start date is required"></asp:RequiredFieldValidator>
                <br />
                <asp:CompareValidator Display="None" ID="CompareValidator1" runat="server" ControlToCompare="endDate" ControlToValidate="startDate" CssClass="error" ErrorMessage="The start date must be before the end date." Operator="LessThan" Type="Date"></asp:CompareValidator>
            </td>
            <td class="auto-style9">End Date:<br />
                <asp:TextBox ID="endDate" runat="server" TextMode="Date"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ControlToValidate="EndDate" CssClass="error" ErrorMessage="The end date is required"></asp:RequiredFieldValidator>
                <br />
            </td>
        </tr>
        <tr>
            <td class="auto-style7">Final Grade Deadline:<br />
                <asp:TextBox ID="finalGradeDeadline" runat="server" TextMode="Date"></asp:TextBox>
                <br />
                <asp:CompareValidator Display="None" ID="CompareValidator2" runat="server" ControlToCompare="endDate" ControlToValidate="finalGradeDeadline" CssClass="error" ErrorMessage="The deadline must be after the semester ends." Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
                <br />
                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ControlToValidate="finalGradeDeadline" CssClass="error" ErrorMessage="The final grade deadline is required."></asp:RequiredFieldValidator>
                <br />
            </td>
            <td>Add/Drop Deadline:<br />
                <asp:TextBox ID="addDropDeadline" runat="server" TextMode="Date"></asp:TextBox>
                <br />
                <asp:CompareValidator Display="None" ID="CompareValidator3" runat="server" ControlToCompare="endDate" ControlToValidate="addDropDeadline" CssClass="error" ErrorMessage="Add/Drop must end before the semester ends" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
                <br />
                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" CssClass="error" ErrorMessage="Add/Drop deadline is required" ControlToValidate="addDropDeadline"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <asp:Button ID="Button1" runat="server" OnClick="createSemester_onClick" Text="Create Semester" />
    <br />
    <asp:Label ID="confirmation" runat="server" ForeColor="#009933"></asp:Label>
    <br />
    <asp:Label ID="error" runat="server" CssClass="error"></asp:Label>
    <br />
</asp:Content>
