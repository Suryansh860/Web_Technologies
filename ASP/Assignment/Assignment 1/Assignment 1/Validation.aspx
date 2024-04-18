<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validation.aspx.cs" Inherits="Assignment_1.Validation" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 2px">
            <h2>Insert Your Details:</h2>
            Name:              <asp:TextBox ID="TextBox1" runat="server" BackColor="#FFFF99"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Name cannot be blank" ForeColor="Red">*</asp:RequiredFieldValidator>
<br />
<br />
            Family Name:&nbsp; <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged" BorderColor="#000000" BackColor="#FFFF99"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox2" Display="Dynamic" ErrorMessage="Family Name cannot be blank" ForeColor="Red">*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox1" ControlToValidate="TextBox2" ErrorMessage="Differs from Name" ForeColor="Red" Operator="NotEqual">*</asp:CompareValidator>
<br />
<br />
            Address:           <asp:TextBox ID="TextBox3" runat="server" BackColor="#FFFF99"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox3" Display="Dynamic" ErrorMessage="Address cannot be blank" ForeColor="Red">*</asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TextBox3" Display="Dynamic" ErrorMessage="Address must be atleast two characters" ForeColor="Red" ValidationExpression=".{2,}">*</asp:RegularExpressionValidator>
<br />
<br />
            City:                 <asp:TextBox ID="TextBox4" runat="server" BackColor="#FFFF99"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox4" ErrorMessage="City cannot be blank" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox4" Display="Dynamic" ErrorMessage="City must be atleast two characters" ForeColor="Red" ValidationExpression=".{2,}">*</asp:RegularExpressionValidator>
<br />
<br />
            Zip Code:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBox5" runat="server" BackColor="#FFFF99"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox5" ErrorMessage="Zip code cannot be blank" ForeColor="Red">*</asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox5" ErrorMessage="Zip Code must be in 5 digits" ForeColor="Red" ValidationExpression="\d{5}">*</asp:RegularExpressionValidator>
<br />
<br />
            Phone:              <asp:TextBox ID="TextBox6" runat="server" BackColor="#FFFF99"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox6" ErrorMessage="Phone Number cannot be blank" ForeColor="Red">*</asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox6" ErrorMessage="(XX-XXXXXXXX / XXX-XXXXXXX)" ForeColor="Red" ValidationExpression="^\d{2,3}-\d{7,7}$">*</asp:RegularExpressionValidator>
<br />
<br />
            E-Mail:              <asp:TextBox ID="TextBox7" runat="server" BackColor="#FFFF99"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox7" ErrorMessage="Email cannot be blank" ForeColor="Red">*</asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox7" ErrorMessage="example@gmail.com" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
<br />
<br />
<asp:Button ID="btn" runat="server" Text="Submit" OnClick="btn_Click" BackColor="#009900" />
        <div style="margin-left: 2px">&nbsp;</div>
<br />
            <asp:Label ID="Label1" runat="server" Text="Status:"></asp:Label>
<br />
<asp:Label ID="lbl1" runat="server" Text="Validation ERROR" Visible="False"></asp:Label>
<br />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
<br />
<br />
</div>
        <div style="margin-left: 2px">&nbsp;</div>
    </form>
</body>
</html>