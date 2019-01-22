<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="RubricPage.aspx.cs" Inherits="CourseManagement.Views.RubricPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="ddlCourse" runat="server" DataSourceID="odsCourses" DataTextField="Name" DataValueField="CRN">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="odsCourses" runat="server" SelectMethod="GetCourseBulletinByTeacherID" TypeName="CourseManagement.DAL.CourseDAL" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter Name="teacherIDCheck" SessionField="UserID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="gvwWeights" runat="server" AutoGenerateColumns="False" DataSourceID="odsWeights" OnRowUpdated="GridView1_RowUpdated">
        <Columns>
            <asp:TemplateField HeaderText="Assignment Type" SortExpression="Key">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Key") %>'></asp:TextBox><asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ValidationGroup="gridview" Text="*" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Key") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Weight" SortExpression="Value">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Value") %>'></asp:Label><asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ValidationGroup="gridview" Text="*" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsWeights" runat="server" SelectMethod="GetCourseRubricByCRN" TypeName="CourseManagement.DAL.CourseRubricDAL" UpdateMethod="UpdateCourseRubric" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCourse" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="CRN" Type="Int32" />
            <asp:Parameter Name="rubric" Type="Object" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="333px">
        <Fields>
            <asp:BoundField HeaderText="Assignment Type"/>
            <asp:BoundField HeaderText="Weight"/>
            <asp:CommandField ShowInsertButton="True"/>
        </Fields>
    </asp:DetailsView>
</asp:Content>