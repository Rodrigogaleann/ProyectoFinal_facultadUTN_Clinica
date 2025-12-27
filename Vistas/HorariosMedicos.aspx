<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HorariosMedicos.aspx.cs" Inherits="Vistas.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Menu Medicos</title>

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
        
             <h1>Administrar horarios médicos</h1>
             <h2>Seleccione un médico:</h2>
             <asp:DropDownList
                 ID="ddlMedicos"
                 runat="server" 
                 AutoPostBack="true"
                 OnSelectedIndexChanged="ddlMedicos_SelectedIndexChanged"
                 OnRowDataBound="gvHorarios_RowDataBound">
             </asp:DropDownList>


             <table class="table">
                <tr>
                    <th>Día</th>
                    <th>Activo</th>
                    <th>Hora Inicio</th>
                    <th>Hora Fin</th>
                </tr>

                <!-- Lunes -->
                <tr>
                    <td>Lunes</td>
                    <td><asp:CheckBox ID="chkLunes" runat="server" AutoPostBack="true" OnCheckedChanged="chkDia_CheckedChanged" /></td>
                    <td>
                        <asp:DropDownList ID="ddlInicioLunes" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFinLunes" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                </tr>

                <!-- Martes -->
                <tr>
                    <td>Martes</td>
                    <td><asp:CheckBox ID="chkMartes" runat="server" AutoPostBack="true" OnCheckedChanged="chkDia_CheckedChanged" /></td>
                    <td>
                        <asp:DropDownList ID="ddlInicioMartes" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFinMartes" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                </tr>

                <!-- Miércoles -->
                <tr>
                    <td>Miércoles</td>
                    <td><asp:CheckBox ID="chkMiercoles" runat="server" AutoPostBack="true" OnCheckedChanged="chkDia_CheckedChanged" /></td>
                    <td>
                        <asp:DropDownList ID="ddlInicioMiercoles" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFinMiercoles" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                </tr>

                <!-- Jueves -->
                <tr>
                    <td>Jueves</td>
                    <td><asp:CheckBox ID="chkJueves" runat="server" AutoPostBack="true" OnCheckedChanged="chkDia_CheckedChanged" /></td>
                    <td>
                        <asp:DropDownList ID="ddlInicioJueves" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFinJueves" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                </tr>

                <!-- Viernes -->
                <tr>
                    <td>Viernes</td>
                    <td><asp:CheckBox ID="chkViernes" runat="server" AutoPostBack="true" OnCheckedChanged="chkDia_CheckedChanged" /></td>
                    <td>
                        <asp:DropDownList ID="ddlInicioViernes" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFinViernes" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                </tr>

                <!-- Sábado -->
                <tr>
                    <td>Sábado</td>
                    <td><asp:CheckBox ID="chkSabado" runat="server" AutoPostBack="true" OnCheckedChanged="chkDia_CheckedChanged" /></td>
                    <td>
                        <asp:DropDownList ID="ddlInicioSabado" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFinSabado" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                </tr>

                <!-- Domingo -->
                <tr>
                    <td>Domingo</td>
                    <td><asp:CheckBox ID="chkDomingo" runat="server" AutoPostBack="true" OnCheckedChanged="chkDia_CheckedChanged" /></td>
                    <td>
                        <asp:DropDownList ID="ddlInicioDomingo" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFinDomingo" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                </tr>
            </table>
             <br />
             <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios" OnClick="btnGuardar_OnClick" />
             <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            <br />
         </div>
     </form>
</body>
</html>
