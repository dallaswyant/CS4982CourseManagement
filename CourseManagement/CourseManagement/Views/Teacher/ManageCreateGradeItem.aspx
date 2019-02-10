<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="ManageCreateGradeItem.aspx.cs" Inherits="CourseManagement.ManageCreateGradeItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tableInfo { width: 100%; }

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

        .auto-style10 {
            height: 26px;
            width: 75px;
        }

        .auto-style11 {
            height: 18px;
            width: 661px;
        }

        .auto-style12 { height: 18px; }
        .auto-style14 {
            height: 23px;
        }
        .auto-style15 {
            height: 26px;
        }
        </style>
    
    <link href="../../Styles/DialogueStyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
    
    <ajaxtoolkit:modalpopupextender id="createdModal" TargetControlID="btnDummy1" PopupControlID="PnlModal1" runat="server"   backgroundcssclass="modalBackground">
    </ajaxtoolkit:modalpopupextender>
    <asp:Button ID="btnDummy1" runat="server" Text="Edit" Style="display: none;" />
    <asp:Panel ID="PnlModal1"  runat="server"  CssClass="oneBtnModal">
        <div  style="text-align: center; font-family:'Roboto',sans-serif;  " >
        Item Created<br />
        </div>
        <asp:Button ID="okayBtn1" CssClass="buttons" runat="server" Text="OK" OnClick="okayBtn1_Click"   />
		
    </asp:Panel>
    
    <ajaxtoolkit:modalpopupextender id="deleteModal" TargetControlID="btnDummy2" PopupControlID="PnlModal2" runat="server"   backgroundcssclass="modalBackground">
    </ajaxtoolkit:modalpopupextender>
    <asp:Button ID="btnDummy2" runat="server" Text="Edit" Style="display: none;" />
    <asp:Panel ID="PnlModal2"  runat="server"  CssClass="oneBtnModal">
        <div  style="text-align: center; font-family:'Roboto',sans-serif;  " >
        Item Deleted<br />
        </div>
        <asp:Button ID="okayBtn2" CssClass="buttons" runat="server" Text="OK" OnClick="okayBtn2_Click"   />
		
    </asp:Panel>

    <table class="tableInfo">
        <tr>
            <td class="auto-style7" colspan="5">
    <asp:ObjectDataSource ID="odsCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseBulletinByTeacherID" TypeName="CourseManagement.DAL.CourseDAL">
        <SelectParameters>
            <asp:SessionParameter Name="teacherIDCheck" SessionField="UserID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7" colspan="5">
                Course:</td>
            <td>

                Assignment name:
                <br />
            </td>
        </tr>
        <tr>
            <td class="auto-style7" colspan="5">

    <asp:DropDownList ID="ddlCourses" runat="server" DataSourceID="odsCourses" DataTextField="Name" DataValueField="CRN">
    </asp:DropDownList>
                </td>
            <td>
                <asp:TextBox ID="tbxAssignmentName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style6" colspan="5">
                &nbsp;
            </td>
            <td class="auto-style15">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5" colspan="5">
                &nbsp;
            Due Date:</td>
            <td class="auto-style3">
                Assignment Type:
            </td>
        </tr>
        <tr>
            <td class="auto-style7" colspan="5" rowspan="8">
                &nbsp;
                <asp:Calendar ID="tbxCalendar" runat="server"></asp:Calendar>
                &nbsp;
            </td>
            <td class="auto-style2">
                &nbsp;
                <asp:DropDownList ID="ddlAssignmentType" runat="server" DataSourceID="odsGradeType" DataTextField="AssignmentType" DataValueField="AssignmentType">
                    <asp:ListItem>Assignment Type</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:ObjectDataSource ID="odsGradeType" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseRubricByCRN" TypeName="CourseManagement.DAL.CourseRubricDAL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td class="auto-style15">Possible Points:</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="tbxPossiblePoints" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>Time Due:</td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:TextBox ID="tbxDueDate" runat="server" TextMode="DateTime"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style7" colspan="5">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style11" colspan="5"></td>
            <td class="auto-style12"></td>
        </tr>
        <tr>
            <td class="auto-style12" colspan="3">Description:</td>
            <td class="auto-style12" colspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style12" colspan="6">
                <asp:TextBox ID="tbxDescription" runat="server" Height="211px" Width="1000px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style15">
                &nbsp;</td>
            <td class="auto-style10">
                &nbsp;</td>
            <td class="auto-style10" colspan="2">
                <asp:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" Text="Create"/>
            </td>
            <td class="auto-style10">
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"/>
            </td>
            <td class="auto-style15">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style6" colspan="5"></td>
            <td class="auto-style4"></td>
        </tr>
        <tr>
            <td class="auto-style7" colspan="5">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>