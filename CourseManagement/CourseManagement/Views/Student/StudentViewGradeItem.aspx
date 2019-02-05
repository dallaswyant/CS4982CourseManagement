<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="StudentViewGradeItem.aspx.cs" Inherits="CourseManagement.StudentViewGradeItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/StudentViewAllGrades.css" rel="stylesheet" type="text/css"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    &nbsp;<br/>
    <table class="tableInfo">
        <tr>
            <td class="auto-style3" rowspan="2">
                <asp:Image ID="profilePicture" runat="server" Height="90px" Width="136px" ImageUrl="~/Images/No image.jpg"/>

            </td>
            <td class="auto-style4">
                <asp:Label ID="Label3" runat="server" Text="Student name"></asp:Label>
            </td>
            <td rowspan="2" class="auto-style9">&nbsp;</td>
            <td>
                <asp:DropDownList ID="ddlAssignmentNames" runat="server" TabIndex="1">
                    <asp:ListItem>Assignment name</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:Label ID="Label4" runat="server" Font-Italic="True" Text="Email"></asp:Label>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <br/>
    <table class="tableInfo">
        <tr>
            <td colspan="3">
                Description:
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblDescription" runat="server" Font-Italic="True" Text="Assignment Description"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label runat="server" Font-Names="Californian FB" Font-Size="Larger" Text="Submission" Font-Bold="True"></asp:Label>
            </td>
            <td class="auto-style10">
                <asp:Label ID="Label6" runat="server" Font-Names="Californian FB" Font-Size="Larger" Text="Uploaded File" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style12">Submission Status</td>
            <td class="auto-style13">
                <asp:Label ID="lblSubmissionStatus" runat="server" Text="Submission Status"></asp:Label>
            </td>
            <td class="auto-style10" rowspan="3">
                <asp:Image ID="thumbnail" runat="server" Height="92px" Width="139px" ImageUrl="~/Images/EmptyFile.png"/>
                <br/>
                <asp:Label ID="Label5" runat="server" Text="..."></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style19">Grading Status</td>
            <td class="auto-style15">
                <asp:Label ID="lblGradeStatus" runat="server" Text="This Assignment Has Not Been Graded"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style19">Due Date</td>
            <td class="auto-style15">
                <asp:Label ID="Label1" runat="server" Text="Monday, January 1, 2019 , 11:59 PM"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style19">Time Remaining</td>
            <td class="auto-style15">
                <asp:Label ID="lblTimeRemaining" runat="server" Text="12 Hours"></asp:Label>
            </td>
            <td class="auto-style10">
                <asp:Button ID="btnChooseFile" runat="server" Text="Choose File" TabIndex="2"/>
            </td>
        </tr>
        <tr>
            <td class="auto-style19">Last Modified</td>
            <td class="auto-style15">
                <asp:Label ID="Label2" runat="server" Text="Monday, January 1, 2019 11:59 AM"></asp:Label>
            </td>
            <td class="auto-style10">
                <asp:Label ID="Label7" runat="server" Text="___" TabIndex="-1"></asp:Label>
                <asp:Label ID="grade" runat="server" Text="/100" Font-Size="Large"></asp:Label>
            </td>
        </tr>
    </table>
    <br/>
    <table class="tableInfo">
        <tr>
            <td colspan="3" class="feedback">Feedback Comments:</td>
        </tr>
        <tr>
            <td colspan="3" class="auto-style23">
                <asp:TextBox ID="TextBox1" runat="server" Height="149px" Width="702px" Enabled="False" BackColor="#CCCCCC"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style22"></td>
            <td class="auto-style21">
            </td>
            <td class="auto-style18">
            </td>
        </tr>
    </table>
    <br/>

</asp:Content>