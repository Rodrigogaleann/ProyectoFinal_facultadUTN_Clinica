<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarMedico.aspx.cs" Inherits="Vistas.AgregarMedico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<style type="text/css">

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

    #container_login{
        display: flex;
        flex-direction:column;
        gap:20px;
    }

    #button_ingreso{
        display: flex;
        justify-content: center;
    }
    .auto-style1 {
        width: 100%;
    }
    .auto-style2 {
        width: 150px;
        color: white;
        font-weight: bold;
        text-decoration: underline;
        font-size: 22px;
        width: 215px;
    }
    .auto-style3 {
        width: 195px;
        color: white;
        font-weight: bold;
        text-decoration: underline;
        font-size: 21px;
    }
    .auto-style4 {
        width: 204px;
    }
    .auto-style5 {
        width: 215px;
        height: 26px;
    }
    .auto-style6 {
        width: 195px;
        height: 26px;
    }
    .auto-style7 {
        width: 204px;
        height: 26px;
    }
    .auto-style8 {
        height: 26px;
    }
    .auto-style9 {
        color: black;
        font-weight: bold;
        text-decoration: underline;
        font-size: 22px;
        width: 215px;
        height: 29px;
    }
    .auto-style11 {
        width: 204px;
        height: 29px;
    }
    .auto-style12 {
        height: 29px;
    }


    .btn_agregar{
       background-color:#5a9;
       height:35px;
       width:50%;
       border-radius:5px;
      font-size:20px;
      color:#fff;
    }
     /* --- Navbar --- */
 .navbar {
     background-color: #333;
     overflow: hidden;
    
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
    .auto-style13 {
        width: 150px;
        color: white;
        font-weight: bold;
        text-decoration: underline;
        font-size: 22px;
        width: 215px;
        height: 54px;
    }
    .auto-style14 {
        width: 195px;
        color: white;
        font-weight: bold;
        text-decoration: underline;
        font-size: 21px;
        height: 54px;
    }
    .auto-style15 {
        width: 204px;
        height: 54px;
    }
    .auto-style16 {
        height: 54px;
    }

    .usuario {
        text-align: right;
        margin-bottom: 10px;
        color: #f1f1f1;
        font-weight: bold;
    }

</style>
    <title></title>
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
            
            <table class="auto-style1">
                <tr>
                    <td class="auto-style13">Agregar los datos
                        del Medico:</td>
                    <td class="auto-style14"></td>
                    <td class="auto-style15"></td>
                    <td class="auto-style16"></td>
                    <td class="auto-style16"></td>
                </tr>
                <tr>
                    <td class="auto-style9"></td>
                    <td class="auto-style3">Legajo:</td>
                    <td class="auto-style11">
                        <asp:TextBox ID="txtLegajo" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style12"></td>
                    <td class="auto-style12"></td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">DNI:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtDni" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Apellido:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Nombre:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Sexo:</td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="ddlSexo" runat="server">
                            <asp:ListItem Value="0">--Seleccionar--</asp:ListItem>
                            <asp:ListItem Value="Masculino">Masculino</asp:ListItem>
                            <asp:ListItem Value="Femenino">Femenino</asp:ListItem>
                            <asp:ListItem Value="Otro">Otro</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Nacionalidad:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtNacionalidad" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Fecha de
                        <br />
                        Nacimiento:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Provincia:</td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="ddlProvincia" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged">
                            <asp:ListItem Value="0">-Seleccionar-</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Localidad:</td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="ddlLocalidad" runat="server">
                            <asp:ListItem Value="0">-- Seleccione Localidad --</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Dirección:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Correo Electronico:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtCorreoElectronico" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Telefono:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Especialidad:</td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="ddlEspecialidad" runat="server">
                            <asp:ListItem Value="0">-Seleccionar-</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>

                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Usuario:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Contraseña:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtContraseña" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">Repetir Contraseña:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtRepetirContraseña" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style6"></td>
                    <td class="auto-style7">
                    </td>
                    <td class="auto-style8"></td>
                    <td class="auto-style8"></td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3"></td>
                    <td class="auto-style4">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMensajeusuario" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3"></td>
                    <td class="auto-style4">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn_agregar" OnClick="btnAgregar_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
