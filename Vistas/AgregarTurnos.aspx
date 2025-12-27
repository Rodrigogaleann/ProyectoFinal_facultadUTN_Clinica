<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarTurnos.aspx.cs" Inherits="Vistas.Turnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <style>

         body{
           height: 100vh;
           background-image: url(./img_vistas/fachadaClinica.jpg);
           background-repeat:no-repeat;
           margin:0;
           background-size: cover;
    
         }

         table{
            display:flex;
            flex-direction:column;
            justify-content: center;
            align-items: center;
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

         .container {
           display: flex;
           flex-direction: column;
           justify-content: center;
           align-items: center;
         }

        
        /* --- Navbar --- */
        .navbar {
            background-color: #333;
            overflow: hidden;
            width:100%;
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
            padding: 20px;
            font-family: Arial, sans-serif;
        }
        .auto-style1 {
            width: 100%;
            color: white;
        }
        .auto-style2 {
            width: 213px;
            color: white;
        }
        .auto-style3 {
            width: 127px;
            color: white;
        }
        .auto-style4 {
            width: 190px;
        }
        .usuario {
            text-align: right;
            margin-bottom: 10px;
            color: #f1f1f1;
            font-weight: bold;
        }
        #calTurno {
            margin: 0px;
            background-color: white;
        }

        #calTurno * {
            margin: 0px;
        }

        #calTurno th {
            color: black;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
             <!-- navbar -->
 <div class="navbar">
    <a href="Home.aspx">Inicio</a>
    <a href="HomeMedicos.aspx">Médico</a>
    <a href="HomeTurnos.aspx">Turnos</a>
    <a href="HomePacientes.aspx">Pacientes</a>
    <a href="HomeUsuarios.aspx">Usuarios</a>
    <a href="Informes.aspx">Informes</a>
    <a href="InterfazLoguin.aspx">Login</a>
 </div>
            <div class="usuario">
                <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                <asp:Label ID="lblFecha" runat="server"></asp:Label>
            </div>
          
            <br />
            <br />
         
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
          
            <br />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<table class="auto-style1">
                 <tr>
                     <td class="auto-style2">
        
          
            <asp:Label ID="lblturno" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Asigne un turno:" ValidateRequestMode="Disabled"></asp:Label>
                     </td>
                     <td class="auto-style3">
                         &nbsp;</td>
                     <td class="auto-style4">&nbsp;</td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="auto-style2">&nbsp;</td>
                     <td class="auto-style3">Especialidad:</td>
                     <td class="auto-style4">
                         <asp:DropDownList
                             ID="ddlEspecialidad"
                             runat="server"
                             AutoPostBack="true"
                             OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
                        </asp:DropDownList>
          
                     </td>
                     <td>
                         <asp:RequiredFieldValidator ID="rfvEspecialidad" runat="server" ControlToValidate="ddlEspecialidad" ErrorMessage="RequiredFieldValidator" InitialValue="0" ValidationGroup="grupo1">Elija una especialidad</asp:RequiredFieldValidator>
                     </td>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="auto-style2">&nbsp;</td>
                     <td class="auto-style3">&nbsp;Medico:</td>
                     <td class="auto-style4">
                         <asp:DropDownList
                             ID="ddlMedicos"
                             runat="server"
                             OnSelectedIndexChanged="ddlMedicos_SelectedIndexChanged"
                             AutoPostBack="true"
                             ></asp:DropDownList>
                     </td>
                     <td>
                         <asp:RequiredFieldValidator ID="rfvMedico" runat="server" ControlToValidate="ddlMedicos" ErrorMessage="RequiredFieldValidator" InitialValue="0" ValidationGroup="grupo1">elija un medico</asp:RequiredFieldValidator>
                     </td>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="auto-style2">&nbsp;</td>
                     <td class="auto-style3">&nbsp;Día:&nbsp;&nbsp;</td>
                     <td class="auto-style4">
                         <asp:Calendar ID="calTurno" runat="server" 
                            OnDayRender="calTurno_DayRender" 
                            OnSelectionChanged="calTurno_SelectionChanged">
                        </asp:Calendar>

                     <td>
                         
                     </td>
                     <td>
                         
                     </td>
                 </tr>
                 <tr>
                     <td class="auto-style2">&nbsp;</td>
                     <td class="auto-style3">&nbsp;Horario:</td>
                     <td class="auto-style4">
                    <asp:DropDownList ID="ddlHorarios" runat="server">
                        <asp:ListItem Value="0">-- Seleccionar --</asp:ListItem>
                        <asp:ListItem Value="14">14:00</asp:ListItem>
                        <asp:ListItem Value="15">15:00</asp:ListItem>
                        <asp:ListItem Value="16">16:00</asp:ListItem>
                        <asp:ListItem Value="17">17:00</asp:ListItem>
                        <asp:ListItem Value="18">18:00</asp:ListItem>
                        <asp:ListItem Value="19">19:00</asp:ListItem>
                        <asp:ListItem Value="20">20:00</asp:ListItem>
                        <asp:ListItem Value="21">21:00</asp:ListItem>
                        <asp:ListItem Value="22">22:00</asp:ListItem>
                    </asp:DropDownList>

                     </td>
                     <td>
                         <asp:RequiredFieldValidator
                             ID="rfvHorario"
                             runat="server"
                             ControlToValidate="ddlHorarios"
                             ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                     </td>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="auto-style2">&nbsp;</td>
                     <td class="auto-style3">Paciente:</td>
                     <td class="auto-style4">
                        <asp:DropDownList ID="ddlPacientes" runat="server"></asp:DropDownList>
                     </td>
                     <td>
                         
                     </td>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="auto-style2">&nbsp;</td>
                     <td class="auto-style3">&nbsp;</td>
                     <td class="auto-style4">&nbsp;</td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="auto-style2">&nbsp;</td>
                     <td class="auto-style3">&nbsp;</td>
                     <td class="auto-style4">
                         <asp:Button ID="btnTurno" runat="server" Text="Agregar turno" ValidationGroup="grupo1" OnClick="btnTurno_Click" />
                         <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                     </td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                 </tr>
             </table>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
        </div>
     
    </form>
</body>
</html>
