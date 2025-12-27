<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoUsuarios.aspx.cs" Inherits="Vistas.WebForm3" %>

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

 .usuario {
   text-align: right;
   margin-bottom: 10px;
   color: #f1f1f1;
   font-weight: bold;
 }
    
 h1{
      margin: 10px 0 8px 0;
      color: #fff;
 }

 #form1 {
     max-width: 90%;
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
          <div class="usuario">
            <asp:Label ID="lblUsuario" runat="server"></asp:Label>
            <asp:Label ID="lblFecha" runat="server"></asp:Label>
          </div>
          <a class="boton" href="HomeUsuarios.aspx">Volver al menú Usuarios</a>
          <h1>Listado de usuarios</h1>
           <div class="listado_Usuarios">
               <asp:GridView
                   ID="gvListaUsuarios"
                   runat="server"
                   AutoGenerateColumns="False"
                   AutoGenerateEditButton="True"
                   AllowPaging="True"
                   PageSize="10"
                   OnPageIndexChanging="gvListaUsuarios_PageIndexChanging"
                   OnRowEditing="gvListaUsuarios_RowEditing"
                   OnRowUpdating="gvListaUsuarios_RowUpdating"
                   OnRowCancelingEdit="gvListaUsuarios_RowCancelingEdit"
                   OnRowCommand="gvListaUsuarios_RowCommand"
                   DataKeyNames="id"
                   BackColor="White"
                   BorderColor="#336666"
                   BorderStyle="Double"
                   BorderWidth="3px"
                   CellPadding="4"
                   GridLines="Horizontal"
               >
                   <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID"  ReadOnly="True" />

                        <asp:TemplateField HeaderText="Usuario">
                            <ItemTemplate>
                                <%# Eval("usuario") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUsuario" runat="server" Text='<%# Bind("usuario") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>

                       <asp:TemplateField HeaderText="Médico asignado">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(Eval("tipoUsuario"))
                                    ? "-" 
                                    : (string.IsNullOrEmpty(Eval("apellido").ToString()) ? "-" : Eval("apellido")) %>
                            </ItemTemplate>
                        </asp:TemplateField>

                       <asp:TemplateField HeaderText="Legajo del médico">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(Eval("tipoUsuario"))
                                    ? "-"
                                    : (string.IsNullOrEmpty(Eval("legajo").ToString()) ? "-" : Eval("legajo")) %>
                            </ItemTemplate>
                        </asp:TemplateField>

                       <asp:TemplateField HeaderText="Rol">
                           <ItemTemplate>
                               <%# (Convert.ToBoolean(Eval("tipoUsuario")) ? "Administrador" : "Médico") %>
                           </ItemTemplate>
                       </asp:TemplateField>

                       <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button 
                                    ID="btnCambiarContrasenia" 
                                    runat="server" 
                                    Text="Cambiar contraseña"
                                    CommandName="CambiarContrasenia"
                                    CommandArgument='<%# Eval("id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>


                   </Columns>

                   <FooterStyle BackColor="White" ForeColor="#333333" />
                   <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                   <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                   <RowStyle ForeColor="#333333" BackColor="White" />
                   <SortedAscendingCellStyle BackColor="#F7F7F7" />
                   <SortedAscendingHeaderStyle BackColor="#487575" />
                   <SortedDescendingCellStyle BackColor="#E5E5E5" />
                   <SortedDescendingHeaderStyle BackColor="#275353" />
               </asp:GridView>
           </div>
      </div>
        <div>
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>

