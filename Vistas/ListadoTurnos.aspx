<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoTurnos.aspx.cs" Inherits="Vistas.ListarTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Turnos - Médico</title>

    <style>
        body {
            height: 100vh;
            background-image: url(./img_vistas/fachadaClinica.jpg);
            background-repeat: no-repeat;
            background-size: cover;
            margin: 0;
            font-family: Arial, sans-serif;
            color: #fff;
        }

        .navbar {
            background-color: #333;
            overflow: hidden;
            width: 100%;
            position: fixed;
            top: 0;
            left: 0;
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
            background-color: rgba(0, 0, 0, 0.55);
            padding: 30px;
            border-radius: 12px;
            max-width: 980px;
            margin-left: auto;
            margin-right: auto;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.4);
        }

         .usuario {
             text-align: right;
             margin-bottom: 10px;
             color: #f1f1f1;
             font-weight: bold;
         }

        h1 {
            color: #fff;
            text-align: left;
            margin-bottom: 18px;
        }

        

        .filtros {
            display: flex;
            gap: 12px;
            flex-wrap: wrap;
            align-items: center;
            margin-bottom: 18px;
            justify-content: flex-start;
        }

        .filtros input[type="text"], .filtros select {
            padding: 8px 10px;
            border-radius: 6px;
            border: none;
            font-size: 14px;
        }

        .filtros .btn {
            padding: 8px 12px;
            border-radius: 6px;
            border: none;
            background-color: #5a9;
            color: #fff;
            cursor: pointer;
        }

        .grid-wrap {
            background-color: rgba(255,255,255,0.03);
            padding: 8px;
            border-radius: 8px;
            margin-bottom: 20px;
        }

        table.gv {
            width: 100%;
            border-collapse: collapse;
            background-color: #fff;
            color: #222;
        }
        table.gv th, table.gv td {
            padding: 10px;
            border: 1px solid #ddd;
            text-align: left;
        }
        table.gv th {
            background-color: #333;
            color: #fff;
        }
        table.gv tr:hover {
            background-color: #f1f1f1;
        }

        .observacion {
            margin-top: 20px;
            background-color: rgba(255, 255, 255, 0.08);
            padding: 18px;
            border-radius: 10px;
            max-width: 700px;
            margin-left: auto;
            margin-right: auto;
        }

        .observacion h2 {
            color: #fff;
            margin: 0 0 8px 0;
        }

        .observacion p { color: #e6e6e6; margin: 0 0 12px 0; }

        .asistencia {
            margin-bottom: 10px;
        }

        .observacion textarea {
            width: 100%;
            min-height: 110px;
            padding: 10px;
            border-radius: 8px;
            border: none;
            font-size: 14px;
            resize: vertical;
        }

        .observacion .btn-guardar {
            margin-top: 12px;
            background-color: #5a9;
            color: #fff;
            border: none;
            padding: 10px 18px;
            border-radius: 6px;
            cursor: pointer;
        }
        .observacion .btn-guardar:hover { opacity: 0.95; }
    </style>

</head>

<body>
   <form id="form1" runat="server">
    <!-- Navbar -->
    <div class="navbar">
        <a href="Home.aspx">Inicio</a>
        <a href="HomeMedicos.aspx">Médicos</a>
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

            <h1>Turnos</h1>

            <div class="filtros">
                <asp:TextBox ID="txtBuscar" runat="server" TextMode="Number" placeholder="Buscar paciente por DNI..." />
                <asp:DropDownList ID="ddlFiltroFecha" runat="server">
                    <asp:ListItem>Todos</asp:ListItem>
                    <asp:ListItem>Hoy</asp:ListItem>
                    <asp:ListItem>Mañana</asp:ListItem>
                    <asp:ListItem>Próxima semana</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn" OnClick="btnFiltrar_Click" />
            </div>

            <div class="grid-wrap">
                <asp:GridView
                    ID="gvTurnos"
                    runat="server"
                    AutoGenerateDeleteButton="True"
                    AutoGenerateEditButton="True"
                    OnRowDeleting="gvTurnos_RowDeleting"
                    OnRowUpdating="gvTurnos_RowUpdating"
                    OnRowEditing="gvTurnos_RowEditing" 
                    OnRowCancelingEdit="gvTurnos_RowCancelingEdit"
                    OnRowDataBound="gvTurnos_RowDataBound"
                    AllowPaging="True"
                    PageSize="10"
                    OnPageIndexChanging="gvTurnos_PageIndexChanging"
                    DataKeyNames="numTurno"
                    AutoGenerateColumns="False"
                    CellPadding="4"
                    ForeColor="#333333"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="numTurno" HeaderText="Turno N°" ReadOnly="True" />
                        <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" ReadOnly="True" />
                        <asp:BoundField DataField="dia" HeaderText="Dia" ReadOnly="True" />
                        <asp:BoundField DataField="horario" HeaderText="Hora" ReadOnly="True" />

                        <asp:TemplateField HeaderText="Asistencia">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(Eval("asistencia")) ? "Presente" : "Ausente" %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Nombre Paciente" HeaderText="Paciente" ReadOnly="True" />
                        <asp:BoundField DataField="Nombre Medico" HeaderText="Médico" ReadOnly="True" />

                        <asp:TemplateField HeaderText="Observaciones">
                            <ItemTemplate>
                                <asp:Label ID="lblObs" runat="server" Text='<%# Eval("observaciones") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtObsEdit" runat="server" Text='<%# Bind("observaciones") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
                <br />
                <br />
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>

        </div>
    </form>
</body>
</html>
