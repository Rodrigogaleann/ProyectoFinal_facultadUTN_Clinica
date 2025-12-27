<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeTurnos.aspx.cs" Inherits="Vistas.HomeTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Menu Turnos</title>

    <style>
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

    body {
        height: 100vh;
        background-image: url(./img_vistas/fachadaClinica.jpg);
        background-repeat: no-repeat;
        background-size: cover;
        margin: 0;
        font-family: Arial, sans-serif;
        color: #fff;
    }

    .contenido {
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

    .usuario {
        text-align: right;
        margin-bottom: 10px;
        color: #f1f1f1;
        font-weight: bold;
    }

    h1 {
        margin: 10px 0 8px 0;
        color: #fff;
    }

    .descripcion {
        color: #e6e6e6;
        line-height: 1.6;
    }

    .acciones {
        margin-top: 20px;
        display:flex;
        gap: 12px;
        flex-wrap:wrap;
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
             <!-- Nombre del usuario logueado (se completa desde code-behind) -->
             <div class="usuario">
                 <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                 <asp:Label ID="lblFecha" runat="server"></asp:Label>
             </div>
        
             <h1>Menu Turnos</h1>
        
             <div class="descripcion">
                 <p>Bienvenido al sistema de gestion de la clínica.En este menu podras:</p>
                 <ul>
                     <li>Agregar Turnos.</li>
                     <li>Modificar los datos del Turno.</li>
                     <li>Dar de baja Turnos.</li>
                     <li>listar todos los Turnos.</li>
                 </ul>
                 <p>Usá el menú superior para navegar a las distintas pantallas. Las búsquedas y listados estarán paginados y permiten filtros.</p>
             </div>
        
             <div class="acciones">
                 <a class="boton" href="AgregarTurnos.aspx">Ir a Agregar</a>
                 <a class="boton" href="ListadoTurnos.aspx">Ir a Listar</a>
             </div>
         </div>
     </form>
</body>
</html>
