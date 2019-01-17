<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="StudentGradeItemPage.aspx.cs" Inherits="CourseManagement.StudentGradeItemPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            width: 1263px;
        }
        .auto-style2 {
            width: 512px;
        }
        .auto-style3 {
            width: 157px;
        }
        .auto-style4 {
            width: 147px;
            text-align: center
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



&nbsp;<br />
    <table class="auto-style1">
        <tr>
            <td class="auto-style3" rowspan="2">
    <asp:Image ID="profilePicture" runat="server" Height="90px" Width="136px" />


            </td>
            <td class="auto-style4">
                <asp:Label ID="Label3" runat="server" Text="Student Name"></asp:Label>
            </td>
            <td rowspan="2">&nbsp;</td>
            <td>&nbsp;<asp:DropDownList ID="ddlStudentNames" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:Label ID="Label4" runat="server" Font-Italic="True" Text="Email"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAssignmentNames" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <br />

    <br />
    <br />
    <table class="auto-style1">
        <tr>
            <td colspan="2">
    <asp:Label runat="server" Font-Names="Californian FB" Font-Size="Larger" Text="Submission"></asp:Label>
            </td>
            <td class="auto-style4" al>
                Uploaded File</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">Submission Status</td>
            <td class="auto-style7">
                <asp:Label ID="lblSubmissionStatus" runat="server" Text="Submission Status"></asp:Label>
            </td>
            <td class="auto-style4" rowspan="3">
                <asp:Image ID="thumbnail" runat="server" Height="69px" Width="112px" ImageUrl="~/Images/EmptyFile.png" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">Grading Status</td>
            <td class="auto-style7">
                <asp:Label ID="lblGradeStatus" runat="server" Text="This Assignment Has Not Been Graded"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style6">Due Date</td>
            <td class="auto-style8">
                <asp:Label ID="Label1" runat="server" Text="Monday, January 1, 2019 , 11:59 PM"></asp:Label>
                </td>
            <td class="auto-style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">Time Remaining</td>
            <td class="auto-style7">
                <asp:Label ID="lblTimeRemaining" runat="server" Text="12 Hours"></asp:Label>
            </td>
            <td class="auto-style4">
                <asp:Label ID="Label5" runat="server" Text="..."></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">Last Modified</td>
            <td class="auto-style7">
                <asp:Label ID="Label2" runat="server" Text="Monday, January 1, 2019 11:59 AM"></asp:Label>
                </td>
            <td class="auto-style4">
                <asp:Label ID="grade" runat="server" Text="___/100" ></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <br />




</asp:Content>
