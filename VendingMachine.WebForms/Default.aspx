<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VendingMachine.WebForms.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Scripts/bootstrap.js"></script>
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-4">
                <h3>Пользователь:</h3>
                <div class="row">
                    <p class="col-lg-4">Кошелек:</p>
                </div>
                <asp:ListView runat="server" ID="UserPurseListView">
                    <ItemTemplate>
                        <div class="row">
                            <p class="col-lg-1"><%# Eval("Nominal") %>р.</p>
                            <p class="col-lg-1"><%# Eval("Count") %></p>
                            <p class="col-lg-4">
                                <asp:LinkButton Text="Добавить в кофемашину" runat="server"
                                    CommandArgument='<%# Eval("Nominal") %>'
                                    OnClick="AddCoint_Click"></asp:LinkButton>
                            </p>
                        </div>

                    </ItemTemplate>
                </asp:ListView>
                <div class="row"><p class="col-lg-4">Купленные товары:</p></div>
                <asp:ListView runat="server" ID="UserProductsListView">
                    <ItemTemplate>
                        <div class="row">
                            <p class="col-lg-2"><%# Eval("Name") %></p>
                            <p class="col-lg-1"><%# Eval("Price") %>р.</p>
                            <p class="col-lg-4"><%# Eval("Count") %></p>
                        </div>

                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div class="col-lg-4">
                <h3>Кофемашина:</h3>
                <div class="row">
                    <p class="col-lg-4">
                        Внесенная сумма:
                        <asp:Label runat="server" ID="UserSummLabel"></asp:Label>р.
                    </p>
                </div>
                <div class="row"><p class="col-lg-4">Товары в кофемашине:</p></div>
                <asp:ListView runat="server" ID="VendingProductsListView">
                    <ItemTemplate>
                        <div class="row">
                            <p class="col-lg-2"><%# Eval("Name") %></p>
                            <p class="col-lg-1"><%# Eval("Price") %>р.</p>
                            <p class="col-lg-2"><%# Eval("Count") %></p>
                            <asp:LinkButton Text="Купить" runat="server"
                                CommandArgument='<%# Eval("Id") %>'
                                OnClick="BuyProduct_Click"></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
                <div class="row"><p class="col-lg-4">Монеты в кофемашине:</p></div>
                <asp:ListView runat="server" ID="VendingConsListView">
                    <ItemTemplate>
                        <div class="row">
                            <p class="col-lg-4"><%# Eval("Nominal") %>р.</p>
                            <p class="col-lg-4"><%# Eval("Count") %></p>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
                <div class="row">
                    <div class="col-lg-8">
                        <asp:LinkButton Text="Получить сдачу" runat="server"
                                    
                                OnClick="TakeChange_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
