<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoAutorizado.aspx.cs" Inherits="Vistas.NoAutorizado" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>401 - No Autorizado</title>
    <style>
        body {
            font-family: Arial;
            background-color: #f5f5f5;
            text-align: center;
            padding-top: 80px;
        }
        .box {
            background: white;
            display: inline-block;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0,0,0,0.2);
        }
        h1 {
            color: #d9534f;
        }
        button {
            margin-top: 20px;
            padding: 10px 20px;
            border: none;
            background-color: #0275d8;
            color: white;
            border-radius: 4px;
            cursor: pointer;
        }
        button:hover {
            background-color: #025aa5;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <h1>401 - No Autorizado</h1>
            <p>No tiene permisos para acceder a este recurso.</p>

            <button type="button" onclick="history.back();">Volver atrás</button>
        </div>
    </form>
</body>
</html>
