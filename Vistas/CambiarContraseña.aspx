<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarContraseña.aspx.cs" Inherits="Vistas.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
    color: white;
  }

 .navbar {
     background-color: #333;
     overflow: hidden;
     position: fixed; 
     top: 0;
     left: 0;
     width: 100%;
     z-index: 1000;
 }

 .navbar a {
     float: left;
     color: white;
     text-align: center;
     padding: 14px 20px;
     text-decoration: none;
 }

 .navbar a:hover {
     background-color: #ddd;
     color: black;
 }

 .contenido {
      margin-top: 90px;
      background-color: rgba(0,0,0,0.55);
      padding: 30px;
      border-radius: 12px;
      max-width: 90%;
      width: 100%;
      text-align: left;
      margin-left: auto;
      margin-right: auto;
      box-shadow: 0 4px 12px rgba(0,0,0,0.4);
      overflow-x: scroll;
 }
    
 h1{
      margin: 10px 0 8px 0;
      color: #fff;
 }

 #form1 {
     max-width: 90%;
 }

 #lblUsuario {
     font-size: 32px;
 }

 .boton {
    background-color: #5a9;
    color: #fff;
    padding: 10px 16px;
    border-radius: 6px;
    text-decoration: none;
    display:inline-block;
}

.boton:hover {
    opacity: 0.9;
}

.usuario {
    text-align: right;
    margin-bottom: 10px;
    color: #f1f1f1;
    font-weight: bold;
}

</style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- navbar-->
        <div class="navbar">
            <a href="Home.aspx">Inicio</a>
            <a href="HomeMedicos.aspx">Médico</a>
            <a href="HomeTurnos.aspx">Turnos</a>
            <a href="HomePacientes.aspx">Pacientes</a>
            <a href="HomeUsuarios.aspx">Usuarios</a>
            <a href="Informes.aspx">Informes</a>
            <a href="InterfazLoguin.aspx">Login</a>
        </div>

        <div class="contenido">
             <div class="usuario">
                 <asp:Label ID="Label1" runat="server"></asp:Label>
                 <asp:Label ID="lblFecha" runat="server"></asp:Label>
             </div>
            <a class="boton" href="ListadoUsuarios.aspx">Volver al listado</a>
            <h1>Cambiar contraseña</h1>

            <div>
                <p>Usuario: </p>
                <asp:Label ID="lblUsuario" runat="server"></asp:Label>

                <br />

                Nueva contraseña:
                <asp:TextBox ID="txtNuevaContrasenia" runat="server" TextMode="Password"></asp:TextBox>

                <br />

                Repetir contraseña:
                <asp:TextBox ID="txtRepetirContrasenia" runat="server" TextMode="Password"></asp:TextBox>

                <br />

                <asp:Button ID="btnCambiar" runat="server" Text="Cambiar" OnClick="btnCambiar_Click" />
            </div>
        </div>
        <div>
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>


