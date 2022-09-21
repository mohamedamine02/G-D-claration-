<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inscription.aspx.cs" Inherits="creation.Inscription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css"> 
        element.style {
}

    .btn-info{
            font-family: Montserrat-Bold;
    font-size: 12px;
    color: #fff;
    line-height: 1.2;
    text-transform: uppercase;
    display: -webkit-box;
    display: -webkit-flex;
    display: -moz-box;
    display: -ms-flexbox;
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 0 20px;
    min-width: 160px;
    height: 42px;
    border-radius: 21px;
    background: rgba(142, 29, 50, 0.95);
    box-shadow: 0 10px 30px 0px rgb(132 106 221 / 50%);
    -moz-box-shadow: 0 10px 30px 0px rgba(132, 106, 221, 0.5);
    -webkit-box-shadow: 0 10px 30px 0px rgb(132 106 221 / 50%);
    -o-box-shadow: 0 10px 30px 0px rgba(132, 106, 221, 0.5);
    -ms-box-shadow: 0 10px 30px 0px rgba(132, 106, 221, 0.5);
    -webkit-transition: all 0.4s;
    -o-transition: all 0.4s;
    -moz-transition: all 0.4s;
    transition: all 0.4s;
            text-align: center;
        }
        </style>
</head>
<body>
    
    <form id="form1" runat="server">
        
         <div style="height: 605px; background-color: #FFFFFF;">
            <table>
            <tr>
            <td> <asp:Button ID="Button3" runat="server" Text="Retourne" CssClass="btn-info" OnClick="Button3_Click" />
            </td>
             
            <td> <asp:Button ID="Button4" runat="server" OnClick="Button1_Click" Text="Ajouter" CssClass="btn-info" /></td>
            </tr>
            <tr>
            <td> <asp:Label ID="Label2" runat="server" Text="Nom"></asp:Label></td>
             
             <td><asp:TextBox ID="TextBox2" runat="server" CssClass="tab-pane" Height="30px" Width="160px"></asp:TextBox></td>
             </tr>
                <tr>
             <td><asp:Label ID="Label3" runat="server" Text="Prenom"></asp:Label></td>
             
             <td><asp:TextBox ID="TextBox3" runat="server" CssClass="tab-pane" Height="30px" Width="160px"></asp:TextBox></td>
              
              </tr>
                <tr>
            <td>  <asp:Label ID="Label4" runat="server" Text="Login"></asp:Label></td>
             
           <td>  <asp:TextBox ID="TextBox4" runat="server" CssClass="tab-pane" Height="30px" Width="160px"></asp:TextBox></td>
             </tr>
            <tr>
           <td> <asp:Label ID="Label5" runat="server" Text="MotDePasse"></asp:Label></td>
            <td><asp:TextBox ID="TextBox5" TextMode="Password" runat="server" CssClass="tab-pane" Height="30px" Width="160px"></asp:TextBox></td>
           
            </tr>
            
                    </table>
             <br />
             <br />
             
             <br />
             <br />
             <br />
             <br />

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="idUser" DataSourceID="SqlDataSource1" Width="672px" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" Visible="false">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:BoundField DataField="idUser" HeaderText="idUser" InsertVisible="False" ReadOnly="True" SortExpression="idUser" />
                    <asp:BoundField DataField="Nom" HeaderText="Nom" SortExpression="Nom" />
                    <asp:BoundField DataField="Prenom" HeaderText="Prenom" SortExpression="Prenom" />
                    <asp:BoundField DataField="Login" HeaderText="Login" SortExpression="Login" />
                    <asp:BoundField DataField="MotDePasse" HeaderText="MotDePasse" SortExpression="MotDePasse"  />
                    <asp:CommandField ButtonType="Button" HeaderText="Supprimer" ShowDeleteButton="True" ShowHeader="True" />
                    <asp:CommandField ButtonType="Button" HeaderText="Modifier" ShowEditButton="True" ShowHeader="True" />
                </Columns>
                <EditRowStyle BackColor="#d1d1d1" />
                <FooterStyle BackColor="#d1d1d1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#d1d1d1" Font-Bold="True" ForeColor="#8d2e82" Height="50px" />
              <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#E3EAEB" />
              <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
              <SortedAscendingCellStyle BackColor="#F8FAFA" />
              <SortedAscendingHeaderStyle BackColor="#246B61" />
              <SortedDescendingCellStyle BackColor="#D4DFE1" />
              <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GDLConnectionString2 %>" SelectCommand="SELECT * FROM [T_Utilisateur]" DeleteCommand="DELETE FROM [T_Utilisateur] WHERE [idUser] = @idUser" InsertCommand="INSERT INTO [T_Utilisateur] ([Nom], [Prenom], [Login], [MotDePasse]) VALUES (@Nom, @Prenom, @Login, @MotDePasse)" UpdateCommand="UPDATE [T_Utilisateur] SET [Nom] = @Nom, [Prenom] = @Prenom, [Login] = @Login, [MotDePasse] = @MotDePasse WHERE [idUser] = @idUser">
                <DeleteParameters>
                    <asp:Parameter Name="idUser" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Nom" Type="String" />
                    <asp:Parameter Name="Prenom" Type="String" />
                    <asp:Parameter Name="Login" Type="String" />
                    <asp:Parameter Name="MotDePasse" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Nom" Type="String" />
                    <asp:Parameter Name="Prenom" Type="String" />
                    <asp:Parameter Name="Login" Type="String" />
                    <asp:Parameter Name="MotDePasse" Type="String" />
                    <asp:Parameter Name="idUser" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
             </div>
    </form>
</body>
</html>
