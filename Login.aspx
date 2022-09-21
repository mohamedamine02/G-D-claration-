<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="creation.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <style type="text/css">
        element.style {
}
        .style{
             margin: 0px;
    padding: 0px;
    box-sizing: border-box;
        }
          .login100-form-btn {
              margin: 5px 20px 22px 56px;
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
    border-radius: 21px;
    background: #846add;
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
        
        .auto-style1 {
            font-family: Poppins-Medium;
    font-size: 15px;
    line-height: 1.5;
    color: #666666;
    display: block;
    background: #e6e6e6;
    border-radius: 25px;
    padding: 20px 15px 20px 15px;
            letter-spacing: 1px;
            justify-content:center;
            top: 30px;
            left: 34px;
            margin-left: 56px;
        }
        .auto-style2 {
            font-size: x-large;
        }
        .auto-style3 {
            height: 517px;
            background-color: #FFFFFF;
            margin-left: auto;
            width: 700px;
        }
        .auto-style7 {
           width: 100%;
    min-height: 100vh;
    display: -webkit-box;
    display: -webkit-flex;
    display: -moz-box;
    display: -ms-flexbox;
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    align-items: center;
    padding: 15px;
    background: rgba(132,106,221,0.8);
    position: relative;
    z-index: 10;
        }
        .auto-style8 {
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
    border-radius: 21px;
    background: #846add;
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
            margin-left: 56px;
            margin-top: 0px;
        }
        .auto-style9 {
            text-align: center;
            height: 157px;
        }
       
}
    </style>
</head>




<body class="auto-style7">
   
   









    <form id="form1" runat="server">
        
    
    
        <div class="auto-style1 auto-style3">
               
                   
               
            <div class="auto-style9">
            
            <br />
            <span class="auto-style2"><strong>page de connexion</strong></span><br />
            <br />
            <br />
            <br />
            <br />
            <br />
            </div>
                 <div style="width: 338px; margin-left: 199px">
           <asp:TextBox ID="TextBox1" placeholder="Login" runat="server" CssClass="auto-style1" Width="193px" Height="16px" ></asp:TextBox>
            <br />
          <asp:TextBox ID="TextBox2" TextMode="Password" placeholder = "MotDePasse" runat="server" CssClass="auto-style1" Width="196px" Height="16px"></asp:TextBox>
           
            
           <br />
           
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="Connexion" CssClass="login100-form-btn" Height="40px" Width="199px" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Inscription " CssClass="auto-style8" Height="40px" Width="199px" />
         
                     <br />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
         
            </div>
    </div>
    
       </form> 
</body>
</html>
