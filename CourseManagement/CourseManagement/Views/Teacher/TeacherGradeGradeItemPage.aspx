<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="TeacherGradeGradeItemPage.aspx.cs" Inherits="CourseManagement.TeacherGradeGradeItemPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .tableInfo {
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
        .auto-style23 {
            text-align: center;
            width: 147px;
            height: 51px;
        }
        .auto-style25 {
            height: 87px;
        }
        .auto-style26 {
            text-align: center;
            width: 147px;
            height: 33px;
        }
    </style>
   
    <link href="../../Styles/DialogueStyleSheet.css" rel="stylesheet" type="text/css" />

    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    

    <ajaxtoolkit:modalpopupextender id="unsavedChangesModal" TargetControlID="btnDummy" PopupControlID="PnlModal" runat="server"   backgroundcssclass="modalBackground">
    </ajaxtoolkit:modalpopupextender>
	<asp:Button ID="btnDummy" runat="server" Text="Edit" Style="display: none;" />
    <asp:Panel ID="PnlModal"   runat="server"  CssClass="modalPopup">
        <div  style="text-align: center; font-family:'Roboto',sans-serif;  " >
        There are unsaved Changes<br/>
        </div>
        <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="buttons"  OnClick="savebtn_OnClick"  />
        <asp:Button ID="continuebtn" runat="server" Text="Continue" CssClass="buttons" OnClick="continuebtn_OnClick"  />
    </asp:Panel>

	
	<ajaxtoolkit:modalpopupextender id="gradedModal" TargetControlID="btnDummy2" PopupControlID="PnlModal2" runat="server"   backgroundcssclass="modalBackground">
	</ajaxtoolkit:modalpopupextender>
	<asp:Button ID="btnDummy2" runat="server" Text="Edit" Style="display: none;" />
	<asp:Panel ID="PnlModal2"  runat="server"  CssClass="oneBtnModal">
	    <div  style="text-align: center; font-family:'Roboto',sans-serif; " >
		Grade Updated<br />
	    </div>
		<asp:Button ID="okayBtn" CssClass="buttons" runat="server" Text="Okay"   />
		
	</asp:Panel>
    
    
   
    &nbsp;<br/>
    <table class="tableInfo">
        <tr>
            <td class="auto-style3" rowspan="3">
                <asp:Image ID="profilePicture" runat="server" Height="90px" Width="136px" ImageUrl="~/Images/No image.jpg"/>

            </td>
            <td class="auto-style26">
                <asp:Label ID="lblTeacher" runat="server" Text="Teacher Name"></asp:Label>
            </td>
            <td rowspan="3" class="auto-style9">&nbsp;</td>
            <td class="auto-style25" rowspan="2">
                &nbsp;
                <asp:DropDownList ID="ddlStudentNames" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStudentNames_OnSelectedIndexChanged"/>
            </td>
            <td class="auto-style25" rowspan="2">
                    <asp:DropDownList ID="ddlAssignmentNames" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAssignmentNames_SelectedIndexChanged" >
                        <asp:ListItem >Assignment Name</asp:ListItem>
                    
                    </asp:DropDownList>
                <asp:ScriptManager ID="ScriptManager1" runat="server"/>
                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="auto-style23">
                <asp:Label ID="lblCourse" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:Label ID="lblEmail" runat="server" Font-Italic="True" Text="Email"></asp:Label>
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
    <table class="tableInfo">
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
                <asp:Button ID="Button1" runat="server" Text="View File" TabIndex="1"/>
            </td>
        </tr>
        <tr>
            <td class="auto-style19">Last Modified</td>
            <td class="auto-style15">
                <asp:Label ID="Label2" runat="server" Text="Monday, January 1, 2019 11:59 AM"></asp:Label>
            </td>
            <td class="auto-style10">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Height="16px" Width="45px" OnTextChanged="TextBox2_TextChanged" TabIndex="2"></asp:TextBox>
                
                        <asp:Label ID="grade" runat="server" Text="/100" Font-Size="Large"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <br/>
    <table class="tableInfo">
        <tr>
            <td colspan="3" class="feedback">Feedback Comments</td>
        </tr>
        <tr>
            <td colspan="3" class="feedback">
                <asp:TextBox ID="tbxDescription" runat="server" Height="149px" Width="702px" TextMode="MultiLine" TabIndex="3" OnTextChanged="tbxDescription_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style22">
                <asp:Button ID="Button2" runat="server" Text="Save"/>
            </td>
            <td class="auto-style21">
                <asp:Button ID="Button3" runat="server" Text="Grade" OnClick="Button3_Click"/>
            </td>
            <td class="auto-style18">
                <asp:Button ID="Button4" runat="server"  OnClick="Button4_Click"  Text="Next" TabIndex="4" />
            </td>
        </tr>
    </table>
    <br/>
    <br/>
</asp:Content>