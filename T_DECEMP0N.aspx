<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="T_DECEMP0N.aspx.cs" Inherits="creation.T_DECEMP0N" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"/>
</head>
<body>
    <nav class="navbar navbar-inverse">
  <div class="container-fluid">
    <div class="navbar-header">
      <a class="navbar-brand" href="Acceuil.aspx">GDL</a>
    </div>
    <ul class="nav navbar-nav">
      <li class="active"><a href="utilisateur.aspx">Fichier</a></li>
        <li class="active"><a href="Exerciceaspx.aspx">Exercice</a></li>
      <li class="dropdown">
        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Structure
        <span class="caret"></span></a>
        <ul class="dropdown-menu">
          <li><a href="Soc.aspx">Parametre des société</a></li>
          <li><a href="utilisateur.aspx">Parametre des Utilisateur</a></li>
          <li><a href="T_Requettes.aspx">Parametre des Totaux</a></li>
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
            <li><a href="Tableau Recap.aspx">Tableau Récap</a></li>
        </ul>
      </li>
    </ul>
  </div>
</nav>

     <nav class="navbar navbar-inverse">
  <div class="container-fluid">
    <div class="navbar-header">
      <a class="navbar-brand" href="Acceuil.aspx">GDL</a>
    </div>--
   <ul class="nav navbar-nav">
      <li class="active"><a href="#">Parametre</a></li>
        <li class="active"><a href="Exerciceaspx.aspx">Nouvel Exercice</a></li>
        <li class="active"><a href="ANXBEN01.aspx">annexe 1</a></li>
        <li class="active"><a href="ANXBEN02.aspx">annexe 2</a></li>
        <li class="active"><a href="ANXBEN03.aspx">annexe 3</a></li>
        <li class="active"><a href="ANXBEN04.aspx">annexe 4</a></li>
        <li class="active"><a href="ANXBEN05.aspx">annexe 5</a></li>
        <li class="active"><a href="ANXBEN06.aspx">annexe 6</a></li>
        <li class="active"><a href="ANXBEN07.aspx">annexe 7</a></li>
        <li class="active"><a href="Tableau Recap.aspx">Tableau Récap</a></li>
        </ul>
        </div>
         </nav>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Sauvegarder" />

            <asp:Button ID="Button2" runat="server" Text="Exporter" />

            <asp:Button ID="Button3" runat="server" Text="Imprimer" />

        </div>
        <div>

            

            <asp:CheckBox ID="ANXBEN01" Text="ANXBEN01" runat="server" />
&nbsp;<asp:CheckBox ID="ANXBEN02" Text="ANXBEN02" runat="server" />
&nbsp;<asp:CheckBox ID="ANXBEN03" Text="ANXBEN03" runat="server" />
&nbsp;<asp:CheckBox ID="ANXBEN04" Text="ANXBEN04" runat="server" />
&nbsp;<asp:CheckBox ID="ANXBEN05" Text="ANXBEN05" runat="server" />
&nbsp;<asp:CheckBox ID="ANXBEN06" Text="ANXBEN06" runat="server" />
&nbsp;<asp:CheckBox ID="ANXBEN07" Text="ANXBEN07" runat="server" />

        </div>
        <div>

            <br />
            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
            </asp:CheckBoxList>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="DECEMP01_ID">
                <Columns>
                    <asp:BoundField DataField="DECEMP01_ID" HeaderText="DECEMP01_ID" InsertVisible="False" ReadOnly="True" SortExpression="DECEMP01_ID" />
                    <asp:BoundField DataField="Tot_Ass" HeaderText="Tot_Ass" SortExpression="Tot_Ass" />
                    <asp:BoundField DataField="Tau_Ret" HeaderText="Tau_Ret" SortExpression="Tau_Ret" />
                    <asp:BoundField DataField="Tot_Ret" HeaderText="Tot_Ret" SortExpression="Tot_Ret" />
                    <asp:BoundField DataField="Exercice" HeaderText="Exercice" SortExpression="Exercice" />
                    <asp:BoundField DataField="idUser" HeaderText="idUser" SortExpression="idUser" />
                    <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                    <asp:BoundField DataField="Libele" HeaderText="Libele" SortExpression="Libele" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GDLConnectionString %>" SelectCommand="SELECT * FROM [T_DECEMP0N]"></asp:SqlDataSource>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
