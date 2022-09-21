<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Autorisationsoc.aspx.cs" Inherits="creation.Autorisationsoc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta name="viewport" content="width=device-width" />
    <title></title>
    <style></style>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
          <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"/>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>


</head>
<body>
     <nav class="navbar navbar-inverse">
  <div class="container-fluid">
    <div class="navbar-header">
      <a class="navbar-brand" href="#">GDL</a>
    </div>
    <ul class="nav navbar-nav">
      <li class="active"><a href="utilisateur.aspx">Fichier</a></li>
        <li class="active"><a href="Role.aspx">Exercice</a></li>
      <li class="dropdown">
        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Structure
        <span class="caret"></span></a>
        <ul class="dropdown-menu">
          <li><a href="Soc.aspx">Parametre des société</a></li>
          <li><a href="utilisateur.aspx">Parametre des Utilisateur</a></li>
          <li><a href="#">Parametre des Totaux</a></li>
            <li><a href="Exerciceaspx.aspx">Parametre des Exercice</a></li>
            <li><a href="Autorisation.aspx">Autorisation</a></li>
        </ul>
      </li>
         <li class="dropdown">
        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Traitement
        <span class="caret"></span></a>
        <ul class="dropdown-menu">
          <li><a href="ANXBEN01.aspx">Annexe 1</a></li>
          <li><a href="ANXBEN02.aspx">Annexe 2</a></li>
          <li><a href="ANXBEN03.aspx">Annexe 3</a></li>
          <li><a href="ANXBEN04.aspx">Annexe 4</a></li>
          <li><a href="ANXBEN05.aspx">Annexe 5</a></li>
            <li><a href="ANXBEN06.aspx">Annexe 6</a></li>
            <li><a href="ANXBEN07.aspx">Annexe 7</a></li>
            <li><a href="#">Tableau Récap</a></li>
        </ul>
      </li>
    </ul>
  </div>
</nav>

     <nav class="navbar navbar-inverse">
  <div class="container-fluid">
    <div class="navbar-header">
      <<a class="navbar-brand" href="#">GDL</a>-
    </div>--
   <ul class="nav navbar-nav">
      <li class="active"><a href="utilisateur.aspx">Parametre</a></li>
        <li class="active"><a href="Exerciceaspx.aspx">Nouvel Exercice</a></li>
        <li class="active"><a href="ANXBEN01.aspx">annexe 1</a></li>
        <li class="active"><a href="ANXBEN02.aspx">annexe 2</a></li>
        <li class="active"><a href="ANXBEN03.aspx">annexe 3</a></li>
        <li class="active"><a href="ANXBEN04.aspx">annexe 4</a></li>
        <li class="active"><a href="ANXBEN05.aspx">annexe 5</a></li>
        <li class="active"><a href="ANXBEN06.aspx">annexe 6</a></li>
        <li class="active"><a href="ANXBEN07.aspx">annexe 7</a></li>
        <li class="active"><a href="Role.aspx">Tableau Récap</a></li>
        </ul>
        </div>
         </nav>






    <form id="form1" runat="server">
        <div>
            <br />
            <asp:Label ID="Label1" runat="server" Text="idSoc"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="idUser"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Flag"></asp:Label>
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <br />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Autorisesoc" />
            <br />
            <br />
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="autoID" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Width="380px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="autoID" HeaderText="autoID" InsertVisible="False" ReadOnly="True" SortExpression="autoID" />
                    <asp:BoundField DataField="idSoc" HeaderText="idSoc" SortExpression="idSoc" />
                    <asp:BoundField DataField="idUser" HeaderText="idUser" SortExpression="idUser" />
                    <asp:CheckBoxField DataField="Flag" HeaderText="Flag" SortExpression="Flag" />
                    <asp:CommandField ButtonType="Button" HeaderText="Supprimer" ShowDeleteButton="True" ShowHeader="True" />
                    <asp:CommandField ButtonType="Button" HeaderText="Modifier" ShowEditButton="True" ShowHeader="True" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GDLConnectionString2 %>" SelectCommand="SELECT * FROM [T_Autorisation_Soc]">
            </asp:SqlDataSource>
            <br />
        </div>
    </form>
</body>
</html>
