<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CheckBoxListTestDemo.UI.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td><b>Create user access</b></td>
            <td>
                <asp:TextBox ID="userAccessTextBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="createButton" runat="server" Text="Create" Width="66px" OnClick="createButton_Click" />

            </td>
             <td>
                 <asp:DropDownList ID="userAccessDropDownlist" Width="200px" Height="25px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="userAccessDropDownlist_SelectedIndexChanged">
                     
                 </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="updateButton" runat="server" Text="Update" OnClick="updateButton_Click" />
            </td>
            <td>
                <asp:Button ID="deleteButton" runat="server" Text="Delete" OnClick="deleteButton_Click" />
            </td>


             <td>
                 <asp:Label ID="statusLabel" runat="server" ForeColor="red" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    </div>
        <div>
            <p><b>Per dealership override</b></p>
            <asp:CheckBoxList ID="perDealershipOverridesCheckBoxList" RepeatColumns="4" RepeatDirection="Horizontal" runat="server" TextAlign="Left">
                <asp:ListItem Text="New Sales Message Alert" Value="1"></asp:ListItem>
                 <asp:ListItem Text="New Service Message Alert" Value="2"></asp:ListItem>
                 <asp:ListItem Text="New CCC Message Alert" Value="3"></asp:ListItem>
                 <asp:ListItem Text="New Advisors Message Alert" Value="4"></asp:ListItem>
                 <asp:ListItem Text="New Finance Message Alert" Value="5"></asp:ListItem>
                 

            </asp:CheckBoxList>
            <p><b>Global rules</b></p>
             <asp:CheckBoxList ID="globalRulesCheckBoxList" RepeatColumns="4" RepeatDirection="Horizontal"  runat="server" TextAlign="Left">
                <asp:ListItem Text="Showroom" Value="6"></asp:ListItem>
                 <asp:ListItem Text="New Advisors Message Alert" Value="7"></asp:ListItem>
                 <asp:ListItem Text="Review Inbound Calls" Value="8"></asp:ListItem>
                 <asp:ListItem Text="Campaigns" Value="9"></asp:ListItem>
                 <asp:ListItem Text="New Entry Wizard" Value="10"></asp:ListItem>
                 <asp:ListItem Text="New CCC Message Alert" Value="11"></asp:ListItem>
                 <asp:ListItem Text="Phone Search" Value="12"></asp:ListItem>
                 <asp:ListItem Text="Email Scripts" Value="13"></asp:ListItem>
                 <asp:ListItem Text="Customer Search" Value="14"></asp:ListItem>
                 <asp:ListItem Text="New Sales MailBox" Value="15"></asp:ListItem>
                 <asp:ListItem Text="Phone System Calls" Value="16"></asp:ListItem>
                 <asp:ListItem Text="Text Message Scripts" Value="17"></asp:ListItem>
                 <asp:ListItem Text="Appoinment Search" Value="18"></asp:ListItem>
                 <asp:ListItem Text="New Finance MailBox" Value="19"></asp:ListItem>
                 <asp:ListItem Text="Phone Optout Page" Value="20"></asp:ListItem>
                 <asp:ListItem Text="Outbound Recordings" Value="21"></asp:ListItem>
                 <asp:ListItem Text="Visit Search" Value="22"></asp:ListItem>
                 <asp:ListItem Text="New Service MailBox" Value="23"></asp:ListItem>
                 <asp:ListItem Text="Service" Value="24"></asp:ListItem>
                 <asp:ListItem Text="Campaign History" Value="25"></asp:ListItem>
                 <asp:ListItem Text="Vehicle Search" Value="26"></asp:ListItem>
                 <asp:ListItem Text="New Advisor MailBox" Value="27"></asp:ListItem>
                 <asp:ListItem Text="Service Appt Search" Value="28"></asp:ListItem>
                 <asp:ListItem Text="Campaign Setup" Value="29"></asp:ListItem>
                 <asp:ListItem Text="Deal Search" Value="30"></asp:ListItem>
                 <asp:ListItem Text="New CCC MailBox" Value="31"></asp:ListItem>
                 <asp:ListItem Text="RO Search" Value="32"></asp:ListItem>
                 <asp:ListItem Text="Service Reminders" Value="33"></asp:ListItem>
                 <asp:ListItem Text="Trade Search" Value="34"></asp:ListItem>
                 <asp:ListItem Text="Email/Text Search" Value="35"></asp:ListItem>
                 <asp:ListItem Text="Booking Search" Value="36"></asp:ListItem>
                 <asp:ListItem Text="Lead Queue Reports" Value="37"></asp:ListItem>
                 <asp:ListItem Text="Rapid Reports" Value="38"></asp:ListItem>
                 <asp:ListItem Text="Leads Not Picked Up" Value="39"></asp:ListItem>
                 <asp:ListItem Text="Mail Reminders" Value="40"></asp:ListItem>
                 <asp:ListItem Text="Admin" Value="41"></asp:ListItem>
                 <asp:ListItem Text="Email/Text" Value="42"></asp:ListItem>
                 <asp:ListItem Text="Unique Lead Reports" Value="43"></asp:ListItem>
                 <asp:ListItem Text="Booking Log" Value="44"></asp:ListItem>
                 <asp:ListItem Text="Contact Search" Value="45"></asp:ListItem>
                 <asp:ListItem Text="New Private Message Alert" Value="46"></asp:ListItem>
                 <asp:ListItem Text="Email Optout Page" Value="47"></asp:ListItem>
                 <asp:ListItem Text="Services Due" Value="48"></asp:ListItem>
                 <asp:ListItem Text="Online Inventory" Value="49"></asp:ListItem>
                 <asp:ListItem Text="New Sales Message Alert" Value="50"></asp:ListItem>
                 <asp:ListItem Text="Phone" Value="51"></asp:ListItem>
                 <asp:ListItem Text="Edit Suggestions" Value="52"></asp:ListItem>
                 <asp:ListItem Text="Updates" Value="53"></asp:ListItem>
                 <asp:ListItem Text="New Finance Message Alert" Value="54"></asp:ListItem>
                 
                  <asp:ListItem Text="Text Optout Page" Value="55"></asp:ListItem>
                 <asp:ListItem Text="Performance List" Value="56"></asp:ListItem>
                 <asp:ListItem Text="Users" Value="57"></asp:ListItem>
                 <asp:ListItem Text="New Service Message Alert" Value="58"></asp:ListItem>
                  <asp:ListItem Text="New Phone Calls" Value="59"></asp:ListItem>
                 

            </asp:CheckBoxList>
        </div>
    </form>
</body>
</html>
