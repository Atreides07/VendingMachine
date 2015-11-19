using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendingMachine.LogicLayer;

namespace VendingMachine.WebForms
{
    public partial class Default : System.Web.UI.Page
    {
        private VendingMachineCore vendingMachineCore;
        private User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();

                UpdateState();
            }
            else
            {
                vendingMachineCore = (VendingMachineCore)Session["vm"];
                user = (User)Session["user"];
            }
        }

        private void UpdateState()
        {
            UserPurseListView.DataSource = user.Purse.GetAllCoins();
            UserPurseListView.DataBind();
            UserProductsListView.DataSource = user.GetProducts();
            UserProductsListView.DataBind();
            VendingProductsListView.DataSource = vendingMachineCore.GetProducts();
            VendingProductsListView.DataBind();
            VendingConsListView.DataSource = vendingMachineCore.Purse.GetAllCoins();
            VendingConsListView.DataBind();


            UserSummLabel.Text = vendingMachineCore.UserMoneySumm.ToString();
        }

        private void InitData()
        {
            var vendingInitState = new VendingInitState();
            vendingInitState.SetCoins(1, 100);
            vendingInitState.SetCoins(2, 100);
            vendingInitState.SetCoins(5, 100);
            vendingInitState.SetCoins(10, 100);
            vendingInitState.AddProduct("Чай", 13, 10);
            vendingInitState.AddProduct("Кофе", 18, 20);
            vendingInitState.AddProduct("Кофе с молоком", 21, 20);
            vendingInitState.AddProduct("Сок", 35, 15);
            vendingMachineCore = new VendingMachineCore(vendingInitState);

            user = new User();
            user.Purse.SetCoins(1, 10);
            user.Purse.SetCoins(2, 30);
            user.Purse.SetCoins(5, 20);
            user.Purse.SetCoins(10, 15);

            Session["vm"] = vendingMachineCore;
            Session["user"] = user;
        }

        protected void AddCoint_Click(object sender, EventArgs e)
        {
            Handle(() =>
            {
                var nominal = int.Parse(((LinkButton)sender).CommandArgument);
                vendingMachineCore.AddCoinByUser(user.Purse, nominal);
                UpdateState();
            });
        }

        protected void BuyProduct_Click(object sender, EventArgs e)
        {
            Handle(() =>
            {
                var productId = new Guid(((LinkButton)sender).CommandArgument);
                var product = vendingMachineCore.BuyProduct(productId);
                user.AddProduct(product);
                UpdateState();
            });


        }

        private void Handle(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exp)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + exp.Message + "');", true);
            }
        }

        protected void TakeChange_Click(object sender, EventArgs e)
        {
            Handle(() =>
            {
                vendingMachineCore.TakeChange(user.Purse);
                UpdateState();
            });
        }
    }
}