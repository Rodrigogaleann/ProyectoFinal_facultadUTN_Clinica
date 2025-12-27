<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarPaciente.aspx.cs" Inherits="Vistas.AgregarPaciente" %>

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

        .btn_agregar{
           background-color:#5a9;
           height:35px;
           width:80%;
           border-radius:5px;
           font-size:20px;
           color:#fff;
        }

        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 139px;
            color: white;
        }
        .auto-style3 {
            width: 170px;
            color: white;
        }
        .auto-style4 {
            width: 143px;
        }
        .auto-style5 {
            width: 170px;
            height: 23px;
        }
        .auto-style6 {
            width: 139px;
            height: 23px;
        }
        .auto-style7 {
            width: 143px;
            height: 23px;
        }
        .auto-style8 {
            height: 23px;
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

 .usuario {
     text-align: right;
     margin-bottom: 10px;
     color: #f1f1f1;
     font-weight: bold;
 }
        .auto-style9 {
            width: 170px;
            color: white;
            height: 23px;
        }
        .auto-style10 {
            width: 139px;
            color: white;
            height: 23px;
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
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">Agregue datos de paciente</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style4">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="#CC0000"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style6"></td>
                    <td class="auto-style7"></td>
                    <td class="auto-style8"></td>
                    <td class="auto-style8"></td>
                    <td class="auto-style8"></td>
                    <td class="auto-style8"></td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">DNI:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtDNIpaciente" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvDniPaciente" runat="server" ControlToValidate="txtDNIpaciente" ErrorMessage="Ingrese dni" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revDniPaciente" runat="server" ControlToValidate="txtDNIpaciente" ErrorMessage="valores invalidos" ValidationExpression="^[0-9]{7,8}$" ValidationGroup="grupo1"></asp:RegularExpressionValidator>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">Nombre:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtNombrePaciente" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvNombrePaciente" runat="server" ControlToValidate="txtNombrePaciente" ErrorMessage="Ingrese nombre del paciente" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator 
                            ID="recNombre" 
                            runat="server" 
                            ControlToValidate="txtNombrePaciente" 
                            ErrorMessage="Nombre inválido" 
                            ValidationExpression="^[A-Za-zÑñÁÉÍÓÚáéíóú ]+$"
                            ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>

                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">Apellido:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtApellidoPaciente" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvApellidoMedico" runat="server" ControlToValidate="txtApellidoPaciente" ErrorMessage="Ingrese apellido del paciente" ValidationGroup="grupo1">Ingrese apellido del paciente</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator 
                            ID="revApellidoPaciente" 
                            runat="server" 
                            ControlToValidate="txtApellidoPaciente" 
                            ErrorMessage="Apellido inválido" 
                            ValidationExpression="^[A-Za-zÑñÁÉÍÓÚáéíóú ]+$"
                            ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>

                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">Sexo:</td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="DropDownListSexo" runat="server">
                            <asp:ListItem Value="0">--seleccionar--</asp:ListItem>
                            <asp:ListItem Value="Masculino">Masculino</asp:ListItem>
                            <asp:ListItem Value="Femenino">Femenino</asp:ListItem>
                            <asp:ListItem Value="Otro">Otro</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvSexoPaciente" runat="server" ControlToValidate="DropDownListSexo" ErrorMessage="Ingrese sexo del paciente" InitialValue="0" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">Nacionalidad:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtNacionalidadPaciente" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvNacionalidadPaciente" runat="server" ControlToValidate="txtNacionalidadPaciente" ErrorMessage="Ingrese nacionalidad del paciente" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revNacionalidad" runat="server" ControlToValidate="txtNacionalidadPaciente" ErrorMessage="valores invalidos" ValidationExpression="^[A-Za-zÑñ]+$" ValidationGroup="grupo1"></asp:RegularExpressionValidator>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">Fecha de nacimiento:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtFNacimientoPaciente" runat="server" TextMode="Date"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvNacimiento" runat="server" ControlToValidate="txtFNacimientoPaciente" ErrorMessage="Ingrese fecha nacimiento" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revNacimientoPaciente" runat="server" ControlToValidate="txtFNacimientoPaciente" ErrorMessage="valores no validos" ValidationExpression="^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$" ValidationGroup="grupo1"></asp:RegularExpressionValidator>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">Direccion:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtDireccionPaciente" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvDireccionPaciente" runat="server" ControlToValidate="txtDireccionPaciente" ErrorMessage="Ingrese direccion paciente" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revDireccionPaciente" runat="server" ControlToValidate="txtDireccionPaciente" ErrorMessage="valores invalidos" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9\s.,°\-]+$" ValidationGroup="grupo1"></asp:RegularExpressionValidator>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">&nbsp;Provincia:</td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="ddlProvinciaPaciente" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged1">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvProvinciaPaciente" runat="server" ControlToValidate="ddlProvinciaPaciente" ErrorMessage="Ingrese alguna opcion" InitialValue="0" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">Localidad:</td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="ddlLocalidad" runat="server">
                            <asp:ListItem Value="0">-- Seleccione Localidad --</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvLocalidadPaciente" runat="server" ControlToValidate="ddlLocalidad" ErrorMessage="Seleccione una localidad" ValidationGroup="grupo1" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">Correo Electronico:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtCElectronicoPaciente" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvCorreoPaciente" runat="server" ControlToValidate="txtCElectronicoPaciente" ErrorMessage="Ingrese correo electronico" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revCorreoPaciente" runat="server" ControlToValidate="txtCElectronicoPaciente" ErrorMessage="mail invalido" ValidationExpression="^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$" ValidationGroup="grupo1"></asp:RegularExpressionValidator>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
    <td class="auto-style2">Teléfono:</td>
    <td class="auto-style4">
                    <asp:TextBox ID="txtTelefonoPaciente" runat="server"></asp:TextBox>
                    
</td>

<td>
    <asp:RequiredFieldValidator ID="rfvTelefonoPaciente" runat="server" ControlToValidate="txtTelefonoPaciente" ErrorMessage="Ingrese numero de telefono" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
    </td>
   <td>
    <asp:RegularExpressionValidator 
        ID="revTelefonoPaciente" 
        runat="server" 
        ControlToValidate="txtTelefonoPaciente"
        ErrorMessage="Ingrese solo números"
        ValidationExpression="^[0-9]+$"
        ValidationGroup="grupo1">
    </asp:RegularExpressionValidator>
</td>
    </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">
                        <asp:Button ID="btnAgregar" 
                             runat="server" 
                              Text="Agregar" 
                             CssClass="btn_agregar"
                             OnClick="btnAgregar_Click" ValidationGroup="grupo1" />
                    </td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style9"></td>
                    <td class="auto-style10"></td>
                    <td class="auto-style7"></td>
                    <td class="auto-style8"></td>
                    <td class="auto-style8"></td>
                    <td class="auto-style8"></td>
                    <td class="auto-style8"></td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">
                        <asp:GridView ID="GridView1" runat="server">
                        </asp:GridView>
                    </td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
