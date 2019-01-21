<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="TeacherGradeGradeItemPage.aspx.cs" Inherits="CourseManagement.TeacherGradeGradeItemPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            align-content: center;
            width: 1263px;
        }

        .auto-style3 { width: 157px; }

        .auto-style4 {
            text-align: center;
            width: 147px;
        }

        .auto-style9 { width: 554px; }

        .auto-style10 {
            text-align: center;
            width: 186px;
        }

        .auto-style12 {
            height: 48px;
            width: 147px;
        }

        .auto-style13 {
            height: 48px;
            width: 314px;
        }

        .auto-style15 { width: 314px; }

        .auto-style18 { width: 866px; }

        .feedback { text-align: center; }

        .auto-style19 { width: 147px; }

        .auto-style21 { width: 638px; }

        .auto-style22 { width: 792px; }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    &nbsp;<br/>
    <table class="auto-style1">
        <tr>
            <td class="auto-style3" rowspan="2">
                <asp:Image ID="profilePicture" runat="server" Height="90px" Width="136px" ImageUrl="~/Images/No image.jpg"/>

            </td>
            <td class="auto-style4">
                <asp:Label ID="Label3" runat="server" Text="Teacher Name"></asp:Label>
            </td>
            <td rowspan="2" class="auto-style9">&nbsp;</td>
            <td>
                &nbsp;
                <asp:DropDownList ID="ddlStudentNames" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStudentNames_OnSelectedIndexChanged">
                    <asp:ListItem>Student Name</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server"/>
                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <asp:DropDownList ID="ddlAssignmentNames" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAssignmentNames_SelectedIndexChanged" >
                        <asp:ListItem >Assignment Name</asp:ListItem>
                    
                    </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:Label ID="Label4" runat="server" Font-Italic="True" Text="Email"></asp:Label>
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
    <br/>
    <br/>

    <br/>
    <br/>
    <table class="auto-style1">
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
                <asp:Button ID="Button1" runat="server" Text="View File"/>
            </td>
        </tr>
        <tr>
            <td class="auto-style19">Last Modified</td>
            <td class="auto-style15">
                <asp:Label ID="Label2" runat="server" Text="Monday, January 1, 2019 11:59 AM"></asp:Label>
            </td>
            <td class="auto-style10">
                
                <asp:TextBox ID="TextBox2" runat="server" Height="16px" Width="45px"></asp:TextBox>
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server"><ContentTemplate>
                <asp:Label ID="grade" runat="server" Text="/100" Font-Size="Large"></asp:Label>
                </ContentTemplate></asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <br/>
    <table class="auto-style1">
        <tr>
            <td colspan="3" class="feedback">Feedback Comments</td>
        </tr>
        <tr>
            <td colspan="3" class="feedback">
                <asp:TextBox ID="TextBox1" runat="server" Height="149px" Width="702px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style22">&nbsp;</td>
            <td class="auto-style21">
                <asp:Button ID="Button2" runat="server" Text="Save"/>
            </td>
            <td class="auto-style18">
                <asp:Button ID="Button3" runat="server" Text="Grade"/>
            </td>
        </tr>
    </table>
    <br/>
    <br/>
</asp:Content>