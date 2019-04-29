<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CreateTeachers.aspx.cs" Inherits="Admin.Views.DepartmentAdmin.CreateTeachers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style15 {
            width: 100%;
            height: 190px;
        }
        .auto-style11 {
            width: 160px;
            height: 45px;
        }
        .auto-style13 {
            width: 172px;
            height: 45px;
        }
        .auto-style14 {
            height: 45px;
        }
        .auto-style10 {
            width: 160px;
        }
        .auto-style9 {
            width: 172px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="errors" runat="server" CssClass="error" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="fname" Display="None" ErrorMessage="First name required"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="middleInital" Display="None" ErrorMessage="Middle initial required"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="lastname" Display="None" ErrorMessage="Last name required"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="email" Display="None" ErrorMessage="Email required"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="phone" Display="None" ErrorMessage="Phone number required"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ssn" Display="None" ErrorMessage="Social Security number required"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="teacherID" Display="None" ErrorMessage="Student ID required"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="PhoneRegularExpressionValidator2" runat="server" ControlToValidate="phone" Display="None" ErrorMessage="Phone number must be 10 digits in the following format: xxxxxxxxxx" ValidationExpression="^\d{10}"></asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="ZipRegularExpressionValidator3" runat="server" ControlToValidate="zip" Display="None" ErrorMessage="Zip must be 5 digits in the following format: xxxxx" ValidationExpression="^\d{5}"></asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="SSNRegularExpressionValidator4" runat="server" ControlToValidate="ssn" Display="None" ErrorMessage="Social security number must be 9  digits in the following format: xxxxxxxxx" ValidationExpression="^\d{9}"></asp:RegularExpressionValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="birthday" ErrorMessage="Please enter your birthday"></asp:RequiredFieldValidator>
    <table class="auto-style15">
        <tr>
            <td class="auto-style11">First Name:<br />
                <asp:TextBox ID="fname" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style13">Middle Initial:<br />
                <asp:TextBox ID="middleInital" runat="server" Width="84px"></asp:TextBox>
            </td>
            <td class="auto-style13">&nbsp; Last Name:<br />&nbsp;<asp:TextBox ID="lastname" runat="server" Width="108px"></asp:TextBox>
            </td>
            <td class="auto-style14">Email (Check for public):<br />
                <asp:TextBox ID="email" runat="server" TextMode="Email" Width="141px"></asp:TextBox>
            &nbsp;<asp:CheckBox ID="isEmailPublic" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="auto-style10">Phone:<br />
                <asp:TextBox ID="phone" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style9">Race:<br />
                <asp:DropDownList ID="race" runat="server">
                </asp:DropDownList>
            </td>
            <td class="auto-style9">Sex:<br />
                <asp:DropDownList ID="sex" runat="server">
                </asp:DropDownList>
            </td>
            <td>SSN:<br />
                <asp:TextBox ID="ssn" runat="server" Width="107px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style10">Birthday:<br />
                <asp:TextBox ID="birthday" runat="server" TextMode="Date"></asp:TextBox>
                <br />
                <br />
                <br />
            </td>
            <td class="auto-style9">Department:<br />
                <asp:DropDownList ID="department" runat="server">
                </asp:DropDownList>
                <br />
                <br />
                <br />
                <br />
            </td>
            <td class="auto-style9">Teacher ID:<br />
                <asp:TextBox ID="teacherID" runat="server"></asp:TextBox>
                <br />
                <br />
                <br />
                <br />
            </td>
            <td>&nbsp; Office Location:<br />
&nbsp;<asp:TextBox ID="officeLocation" runat="server" Width="93px"></asp:TextBox>
                <br />
&nbsp;Office Hours:<br />
&nbsp;<asp:TextBox ID="officeHours" runat="server" Width="93px"></asp:TextBox>
                <br />
                <br />
            </td>
        </tr>
    </table>
    <br />
    Address Line 1:<br />
    <asp:TextBox ID="addressLine1" runat="server"></asp:TextBox>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="addressLine1" CssClass="error" ErrorMessage="Address required"></asp:RequiredFieldValidator>
    <br />Address Line 2:<br />
    <asp:TextBox ID="addressLine2" runat="server"></asp:TextBox>
    <br />Zip:<br />
    <asp:TextBox ID="zip" runat="server"></asp:TextBox>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="zip" CssClass="error" ErrorMessage="Zip required"></asp:RequiredFieldValidator>
    <br />City:<br />
    <asp:TextBox ID="city" runat="server"></asp:TextBox>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="city" CssClass="error" ErrorMessage="City required"></asp:RequiredFieldValidator>
    <br />State:<br />
    <asp:DropDownList ID="states" runat="server" >
    </asp:DropDownList>
    <br />Country:<br />
    <asp:DropDownList ID="countries" runat="server">
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <asp:Button ID="createTeacher" runat="server" OnClick="createTeacher_Click" Text="Create Teacher" />
    <br />
    <asp:Label ID="confirmation" runat="server" ForeColor="#009933"></asp:Label>
</asp:Content>
