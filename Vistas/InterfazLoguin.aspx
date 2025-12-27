<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterfazLoguin.aspx.cs" Inherits="Vistas.InterfazLoguin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<style type="text/css">
    body{
        height: 100vh;
        background-image:url(./img_vistas/fachadaClinica.jpg);
        background-repeat:no-repeat;
        margin:0;
        background-size: cover;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .container_login{
        display: flex;
        flex-direction:column;
        gap:20px;
        margin-top: 90px; 
        background-color: rgba(0,0,0,0.55);
        padding: 30px;
        border-radius: 12px;
        max-width: 800px;
        text-align: left;
        margin-left: auto;
        margin-right: auto;
        box-shadow: 0 4px 12px rgba(0,0,0,0.4);
    }

    .lblInterfaz{
        font-size:20px;
        color:#fff;
    }

    .button_ingreso{
        display: flex;
        justify-content: center;
    }

    .contenido {
         padding: 20px;
         font-family: Arial, sans-serif;
    }

    .btn_ingresar{
        background-color:#5a9;
        height:35px;
        width:30%;
        border-radius:5px;
        font-size:20px;
        color:#fff;
    }

</style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container_login">
            <div class="container_usuario">
                <asp:Label ID="Label1" runat="server" Text="Ingrese su Usuario " CssClass="lblInterfaz"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtUsuario" runat="server" Width="318px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="revUsuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="RequiredFieldValidator" ValidationGroup="grupo1">Ingrese usuario, por favor</asp:RequiredFieldValidator>
            </div>
            <div class="container_contrasenia">
                <asp:Label ID="Label2" runat="server" Text="Ingrese su Contraseña" CssClass="lblInterfaz"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtContrasenia" runat="server" Width="316px" TextMode="Password"></asp:TextBox>

                
            &nbsp;

                
                <asp:RequiredFieldValidator ID="rfvContraseña" runat="server" ControlToValidate="txtContrasenia" ErrorMessage="RequiredFieldValidator" ValidationGroup="grupo1">Ingrese contraseña</asp:RequiredFieldValidator>

                
            </div>
             <div class="button_ingreso">
                 
                 <asp:Button ID="Button1" runat="server" Text="Ingresar" class="btn_ingresar" OnClick="Button1_Click" ValidationGroup="grupo1" />
                 
                 <asp:Label ID="lblIngresoLogin" runat="server"></asp:Label>
                 
             </div>
        </div>
    </form>
</body>
</html>