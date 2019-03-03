<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="BrowseCourses.aspx.cs" Inherits="CourseManagement.Views.Student.BrowseCourses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
<asp:Label ID="Label3" runat="server" Text="Choose Department:"></asp:Label>
<br />
<asp:DropDownList ID="ddlDepartments" AutoPostBack="True" runat="server"  DataTextField="Name" DataValueField="Name" TabIndex="5">
    <asp:ListItem>All Departments</asp:ListItem>
    </asp:DropDownList>
    <br />
    Choose Semester:<br />
    <asp:DropDownList ID="ddlSemester" runat="server" DataSourceID="odsSemesters" DataTextField="SemesterID" DataValueField="SemesterID" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="True">
    </asp:DropDownList>
    <br />
    <asp:ObjectDataSource ID="odsSemesters" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCurrentAndFutureSemesters" TypeName="CourseManagement.DAL.SemesterDAL"></asp:ObjectDataSource>
<br />

	<asp:ScriptManager ID="berowseCourseScriptManager" runat="server"></asp:ScriptManager>
	<asp:UpdatePanel ID="browseCoursUpdatePanel" UpdateMode="Conditional" runat="server">
		<ContentTemplate>
		    <asp:Label ID="Label1" runat="server" Text="Your Courses"></asp:Label>
		    <asp:GridView ID="UserCourseGrid" runat="server" AutoGenerateColumns="False" DataSourceID="odsStudentCourses">
		        <Columns>
		            <asp:BoundField DataField="CRN" HeaderText="CRN" ReadOnly="True" SortExpression="CRN" />
		            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name" />
		            <asp:BoundField DataField="CreditHours" HeaderText="CreditHours" ReadOnly="True" SortExpression="CreditHours" />
                    <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
                    <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
                    <asp:BoundField DataField="SectionNumber" HeaderText="SectionNumber" ReadOnly="True" SortExpression="SectionNumber" />
                    <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" ReadOnly="True" SortExpression="DepartmentName" />
                    <asp:BoundField DataField="MaxSeats" HeaderText="MaxSeats" ReadOnly="True" SortExpression="MaxSeats" />
                    <asp:BoundField DataField="SemesterID" HeaderText="SemesterID" ReadOnly="True" SortExpression="SemesterID" />
		        </Columns>
		    </asp:GridView>
		    <asp:ObjectDataSource ID="odsStudentCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCoursesByStudentIDAndSemester" TypeName="CourseManagement.DAL.CourseDAL">
                <SelectParameters>
                    <asp:SessionParameter Name="studentUIDCheck" SessionField="UserID" Type="String" />
                    <asp:ControlParameter ControlID="ddlSemester" Name="semesterName" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
		    <br />
		    <asp:Label ID="Label2" runat="server" Text="Department Courses"></asp:Label>
		    <br />
		    <asp:GridView ID="AvailableCoursesGrid" runat="server" AutoGenerateColumns="False" Width="482px" OnSelectedIndexChanged="AvailableCourses_SelectedIndexChanged" DataKeyNames="CRN,Name,Description,Location,LectureNotes,CreditHours,SectionNumber,DepartmentName,MaxSeats,SemesterID" DataSourceID="odsDepartmentCourses">
        <Columns>
            <asp:BoundField DataField="CRN" HeaderText="CRN" ReadOnly="True" SortExpression="CRN" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ReadOnly="True" />
            <asp:BoundField DataField="CreditHours" HeaderText="CreditHours" ReadOnly="True" SortExpression="CreditHours" />
            <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
            <asp:BoundField DataField="SectionNumber" HeaderText="SectionNumber" ReadOnly="True" SortExpression="SectionNumber" />
            <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" ReadOnly="True" SortExpression="DepartmentName" />
            <asp:BoundField DataField="MaxSeats" HeaderText="MaxSeats" ReadOnly="True" SortExpression="MaxSeats" />
            <asp:BoundField DataField="SemesterID" HeaderText="SemesterID" ReadOnly="True" SortExpression="SemesterID" />
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        </asp:GridView>
            <asp:ObjectDataSource ID="odsDepartmentCourses" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCoursesByDepartmentNameAndSemester" TypeName="CourseManagement.DAL.CourseDAL">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlDepartments" Name="departmentCheck" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddlSemester" Name="semesterName" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br/>
            <br/>
		<asp:Label runat="server" ID ="lblCourseToAdd"></asp:Label>
		    <br />
            <asp:Button ID="btnAddCourse" runat="server" OnClick="btnAddCourse_Click" Text="Add Course" TabIndex="6" />
		</ContentTemplate>
	</asp:UpdatePanel>
	
	<br/>
<br/>
</asp:Content>
