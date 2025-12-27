<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Informes.aspx.cs" Inherits="Vistas.Informes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Informes de Turnos</title>

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
            max-width: 900px;
            margin-left: auto;
            margin-right: auto;
            box-shadow: 0 4px 12px rgba(0,0,0,0.4);
        }

        h1 {
            color: #ffff;
            text-align: center;
            margin-bottom: 25px;
        }

        .filtros {
            display: flex;
            justify-content: center;
            gap: 20px;
            margin-bottom: 25px;
            flex-wrap: wrap;
        }

        .filtros label {
            font-weight: bold;
            color: #fff;
        }

        .resumen {
            text-align: center;
            background-color: rgba(255,255,255,0.1);
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 20px;
            font-weight: bold;
        }

        .grid-container {
            display: flex;
            gap: 20px;
            flex-wrap: wrap;
            justify-content: center;
        }

        .grid-item {
            flex: 1 1 45%;
            background-color: rgba(255,255,255,0.1);
            padding: 15px;
            border-radius: 8px;
        }

        .titulo-grid {
            text-align: center;
            font-weight: bold;
            margin-bottom: 10px;
            color: #fff;
        }

        .boton {
            background-color: #5a9;
            color: #fff;
            padding: 10px 20px;
            border-radius: 6px;
            text-decoration: none;
            display: inline-block;
            border: none;
            cursor: pointer;
        }

        .boton:hover {
            opacity: 0.9;
        }

        .chart-container {
            text-align: center;
            margin: 25px 0;
        }

        .descripcion{
            color:#ffff;
        }

        .acciones{
            color:#ffff;
        }


        .usuario {
          text-align: right;
          margin-bottom: 10px;
          color: #f1f1f1;
          font-weight: bold;
        }


        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 22px;
        }
        .auto-style3 {
            width: 182px;
        }
        .auto-style4 {
            height: 22px;
            width: 182px;
        }
        .auto-style5 {
            width: 69px;
        }
        .auto-style6 {
            height: 22px;
            width: 69px;
        }


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <!-- Navbar -->
        <div class="navbar">
            <a href="Home.aspx">Inicio</a>
            <a href="HomeMedicos.aspx">Médico</a>
            <a href="HomeTurnos.aspx">Turnos</a>
            <a href="HomePacientes.aspx">Pacientes</a>
            <a href="HomeUsuarios.aspx">Usuarios</a>
            <a href="Informes.aspx">Informes</a>
            <a href="InterfazLoguin.aspx">Login</a>
            <br />
        </div>
         <div class="contenido">
              <div class="usuario">
                   <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                   <asp:Label ID="lblFecha" runat="server"></asp:Label>
               </div>
     <h1>Informes</h1>

     <div class="descripcion">
         <p>Bienvenido al sistema de informes de la clínica, aca podras ver los informes que estan disponibles:</p>
             </div>
             <div class="acciones">
                 <table class="auto-style1">
                     <tr>
                         <td class="auto-style3">Elija tema del informe:</td>
                         <td class="auto-style5">&nbsp;</td>
                         <td><asp:DropDownList ID="ddlSeleccionarInforme" runat="server">
                     <asp:ListItem Value="0">--Seleccionar--</asp:ListItem>
                     <asp:ListItem Value="1">Asistencia de pacientes</asp:ListItem>
                     <asp:ListItem Value="2">Medicos activos</asp:ListItem>
                 </asp:DropDownList>
                         </td>
                         <td>
                             <asp:RequiredFieldValidator ID="rfvSeleccioneInforme" runat="server" ControlToValidate="ddlSeleccionarInforme" ErrorMessage="seleccione un informe" InitialValue="0" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                         </td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                     </tr>
                     <tr>
                         <td class="auto-style3">&nbsp;</td>
                         <td class="auto-style5">&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                     </tr>
                     <tr>
                         <td class="auto-style3">Ingrese rango del informe</td>
                         <td class="auto-style5">&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                     </tr>
                     <tr>
                         <td class="auto-style3">Fecha de comienzo:</td>
                         <td class="auto-style5">
                             <asp:TextBox ID="txtFechaInicioInforme" runat="server" TextMode="Date"></asp:TextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <asp:RequiredFieldValidator ID="rfvFechaInicio" runat="server" ControlToValidate="txtFechaInicioInforme" ErrorMessage="Ingrese fecha de inicio" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                         </td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                     </tr>
                     <tr>
                         <td class="auto-style3">Fecha de fin:</td>
                         <td class="auto-style5">
                             <asp:TextBox ID="txtFechaFinInforme" runat="server" TextMode="Date"></asp:TextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <asp:RequiredFieldValidator ID="rfvFechaFin" runat="server" ControlToValidate="txtFechaFinInforme" ErrorMessage="Ingrese fecha final" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                         </td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                     </tr>
                     <tr>
                         <td class="auto-style3">&nbsp;</td>
                         <td class="auto-style5">&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                     </tr>
                     <tr>
                         <td class="auto-style3">&nbsp;</td>
                         <td class="auto-style5">
                 <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" ValidationGroup="grupo1" />
                         </td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                     </tr>
                     <tr>
                         <td class="auto-style3">&nbsp;</td>
                         <td class="auto-style5">&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                     </tr>
                     <tr>
                         <td class="auto-style4">Informe:</td>
                         <td class="auto-style6">
                             <asp:Label ID="lblInforme"
                               runat="server"
                               Width="350px"
                               style="display:block; margin-top:10px; font-size:16px;">
                             </asp:Label>
                         </td>
                         <td class="auto-style2"></td>
                         <td class="auto-style2"></td>
                         <td class="auto-style2"></td>
                         <td class="auto-style2"></td>
                     </tr>
                 </table>
                 <br />
                 <br />
</div>
 </div>
    </form>
</body>
</html>

