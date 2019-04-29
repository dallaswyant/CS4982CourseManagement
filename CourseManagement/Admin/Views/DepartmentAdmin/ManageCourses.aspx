<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" Inherits="Admin.Views.DepartmentAdmin.ManageCourses" %>
<%@ Import Namespace="CourseManagement.DAL" %>
<%@ Import Namespace="Admin.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style7 {
            width: 260px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="ddlDepartments" runat="server" DataSourceID="odsDepartments" DataTextField="Name" DataValueField="Name" AutoPostBack="True">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="odsDepartments" runat="server" SelectMethod="GetAllDepartments" TypeName="Admin.DAL.DepartmentDAL"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsDeptCourses" runat="server" DataObjectTypeName="Admin.Models.Course" DeleteMethod="DeleteCourseByDepartmentAndCRN" InsertMethod="InsertNewCourse" SelectMethod="GetCoursesByDepartment" TypeName="Admin.DAL.DepartmentAdminDAL" OldValuesParameterFormatString="original_{0}" UpdateMethod="UpdateCourse">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlDepartments" Name="department" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCourseTime" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllCourseTimes" TypeName="CourseManagement.DAL.CourseTimeDAL"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsSemesters" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllSemesters" TypeName="Admin.DAL.SemesterDAL"></asp:ObjectDataSource>
    <table class="tableInfo">
        <tr>
            <td class="auto-style8">
    <asp:GridView ID="gvwDepartmentCourses" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="odsDeptCourses" OldValuesParameterFormatString="original{0}" PageSize="3" OnSelectedIndexChanged="gvwDepartmentCourses_SelectedIndexChanged" DataKeyNames="CRN,Name,Description,Location,SectionNumber,DepartmentName,MaxSeats,SemesterID" OnRowUpdating="gvwDepartmentCourses_RowUpdating" >
              <Columns>
            <asp:TemplateField HeaderText="CRN" SortExpression="CRN">
                <EditItemTemplate>
                    <asp:Label ID="crn" runat="server" Text='<%# Eval("CRN") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("CRN") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <EditItemTemplate>
                    <asp:Textbox ID="name" runat="server" Text='<%# Eval("Name") %>'></asp:Textbox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                <EditItemTemplate>
                    <asp:Textbox ID="description" runat="server" Text='<%# Eval("Description") %>'></asp:Textbox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location" SortExpression="Location">
                <EditItemTemplate>
                    <asp:Textbox ID="location" runat="server" Text='<%# Eval("Location") %>'></asp:Textbox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Section" SortExpression="SectionNumber">
                <EditItemTemplate>
                    <asp:Textbox ID="section" runat="server" Text='<%# Eval("SectionNumber") %>'></asp:Textbox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("SectionNumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Department" SortExpression="DepartmentName">
                <EditItemTemplate>
                    <asp:DropDownList ID="department" runat="server" DataSourceID="odsDepartments" DataTextField="Name" DataValueField="Name" SelectedValue='<%# Bind("DepartmentName") %>'/>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("DepartmentName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Max Seats" SortExpression="MaxSeats">
                <EditItemTemplate>
                    <asp:Textbox ID="seats" runat="server" Text='<%# Eval("MaxSeats") %>'></asp:Textbox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("MaxSeats") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Semester ID" SortExpression="SemesterID">
                <ItemTemplate>
                    <asp:Label ID="semesterID" runat="server" Text='<%# Eval("SemesterID") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="semesterID" runat="server" DataSourceID="odsSemesters" DataTextField="SemesterID" DataValueField="SemesterID" SelectedValue='<%# Bind("SemesterID") %>'/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Course Time">
              <ItemTemplate>
                  <asp:Label ID="Label9" runat="server" Text='<%# CourseTimeDALHelper.GetCourseTimes().Find(time => time.CourseTimeID == int.Parse(Eval("CourseTimeID").ToString())).TimeSlot  %>' Font-Size="Smaller"></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:DropDownList ID="time" runat="server" DataSourceID="odsCourseTime" DataTextField="TimeSlot" DataValueField="CourseTimeID" SelectedValue='<%# Bind("CourseTimeID") %>'/>
              </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Select"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblGVWCoursesResults" runat="server" CssClass="error"></asp:Label>
            </td>
            <td>

                &nbsp;</td>
        </tr>
    </table>

    <asp:DetailsView ID="dvwDepartmentCourses" runat="server" DefaultMode="Insert" Height="50px" Width="125px" DataSourceID="odsDeptCourses" AutoGenerateRows="False">
        <Fields>
            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
            <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
            <asp:BoundField DataField="SectionNumber" HeaderText="SectionNumber" ReadOnly="True" SortExpression="SectionNumber" />
            <asp:TemplateField HeaderText="DepartmentName" SortExpression="DepartmentName">
                <InsertItemTemplate>
                    <asp:DropDownList ID="ddlDeptNames" runat="server" DataSourceID="odsDepartments" DataTextField="Name" DataValueField="Name" SelectedValue='<%# Bind("DepartmentName") %>'/>
                </InsertItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="MaxSeats" HeaderText="MaxSeats" ReadOnly="True" SortExpression="MaxSeats" />
            <asp:TemplateField HeaderText="SemesterID"  SortExpression="SemesterID">  
                <InsertItemTemplate>
                    <asp:DropDownList ID="ddlSemester" runat="server" DataSourceID="odsSemesters" DataTextField="SemesterID" DataValueField="SemesterID" SelectedValue='<%# Bind("SemesterID") %>'/>
                </InsertItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Course Time">
                <InsertItemTemplate>
                    <asp:DropDownList ID="time" runat="server" DataSourceID="odsCourseTime" DataTextField="TimeSlot" DataValueField="CourseTimeID" SelectedValue='<%# Bind("CourseTimeID") %>'/>
                </InsertItemTemplate>
            </asp:TemplateField>

            <asp:CommandField ShowCancelButton="False" ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>
    <br />
    <table class="tableInfo">
        <tr>
            <td class="auto-style7">
                <asp:DropDownList ID="ddlTeachers" runat="server" DataSourceID="odsDeptTeachers" DataTextField="Name" DataValueField="TeacherUID" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnAddTeachers" runat="server" Text="Assign Teacher" OnClick="btnAddTeachers_Click" />
            &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7">
                <asp:DetailsView ID="dvwTeacherCourse" runat="server" Height="62px" Width="233px" AutoGenerateRows="False" DataSourceID="odsTeacherCourse">
                    <Fields>
                        <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" ReadOnly="True" SortExpression="DepartmentName" />
                        <asp:BoundField DataField="CRN" HeaderText="CRN" ReadOnly="True" SortExpression="CRN" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ReadOnly="True" />
                        <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
                        <asp:BoundField DataField="MaxSeats" HeaderText="MaxSeats" ReadOnly="True" SortExpression="MaxSeats" />
                        <asp:BoundField DataField="SectionNumber" HeaderText="SectionNumber" ReadOnly="True" SortExpression="SectionNumber" />
                        <asp:BoundField DataField="SemesterID" HeaderText="SemesterID" ReadOnly="True" SortExpression="SemesterID" />
                    </Fields>
                </asp:DetailsView>
                <asp:ObjectDataSource ID="odsTeacherCourse" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseByCRN" TypeName="Admin.DAL.CourseDAL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="gvwDepartmentCourses" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" DefaultValue="" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>
                <asp:GridView ID="gvwTeacherCourses" runat="server" AutoGenerateColumns="False" DataSourceID="odsTeacherCourses" AllowPaging="True" PageSize="3">
                    <Columns>
                        <asp:BoundField DataField="CRN" HeaderText="CRN" ReadOnly="True" SortExpression="CRN" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ReadOnly="True" />
                        <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
                        <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
                        <asp:BoundField DataField="SectionNumber" HeaderText="Section" ReadOnly="True" SortExpression="SectionNumber" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" ReadOnly="True" SortExpression="DepartmentName" />
                        <asp:BoundField DataField="MaxSeats" HeaderText="Max Seats" ReadOnly="True" SortExpression="MaxSeats" />
                        <asp:BoundField DataField="SemesterID" HeaderText="Semester ID" ReadOnly="True" SortExpression="SemesterID" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsTeacherCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCoursesByTeacherID" TypeName="Admin.DAL.CourseDAL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlTeachers" Name="teacherIDCheck" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="odsDeptTeachers" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetTeachersByDepartment" TypeName="Admin.DAL.DepartmentDAL">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlDepartments" Name="department" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </asp:Content>
