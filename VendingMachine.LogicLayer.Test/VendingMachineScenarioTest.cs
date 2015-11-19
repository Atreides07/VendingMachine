using System;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace VendingMachine.LogicLayer.Test
{
    /// <summary>
    /// Тест исходного сценария задания
    /// </summary>
    [TestFixture]
    public class VendingMachineScenarioTest
    {
        private Product tea;
        private Product coffe;
        private Product coffeMilk;
        private Product juice;
        IFixture fixture;

        private User user;
        VendingInitState vendingInitState;

        [SetUp]
        public void Init()
        {
            fixture=new Fixture().Customize(new AutoMoqCustomization());
            vendingInitState = new VendingInitState();
            vendingInitState.SetCoins(1, 100);
            vendingInitState.SetCoins(2, 100);
            vendingInitState.SetCoins(5, 100);
            vendingInitState.SetCoins(10, 100);
            tea=vendingInitState.AddProduct("Чай", 13, 10);
            coffe=vendingInitState.AddProduct("Кофе", 18, 20);
            coffeMilk=vendingInitState.AddProduct("Кофе с молоком", 21, 20);
            juice=vendingInitState.AddProduct("Сок", 35, 15);
            fixture.Register(()=>vendingInitState);

            user =new User();
            user.Purse.SetCoins(1,10);
            user.Purse.SetCoins(2,30);
            user.Purse.SetCoins(5,20);
            user.Purse.SetCoins(10,15);
            fixture.Register(()=>user);
        }
        [Test]
        public void InitializatTest()
        {
            var vendingMachine = fixture.Create<VendingMachineCore>();
            var user = fixture.Create<User>();
            Assert.AreEqual(10,user.Purse.GetCoinCount(1));
            Assert.AreEqual(4,vendingMachine.GetProducts().Count());
            Assert.AreEqual(4,vendingMachine.Purse.GetAllCoins().Count());


        }

        [Test]
        public void AddMoneyToVendingMachineTest()
        {
            var vendingMachine = fixture.Create<VendingMachineCore>();
            var user = fixture.Create<User>();
            vendingMachine.AddCoinByUser(user.Purse, 1);
            vendingMachine.AddCoinByUser(user.Purse, 5);
            Assert.AreEqual(101,vendingMachine.Purse.GetAllCoins().First(i=>i.Nominal==1).Count);
            Assert.AreEqual(101,vendingMachine.Purse.GetAllCoins().First(i=>i.Nominal==5).Count);
            Assert.AreEqual(6,vendingMachine.UserMoneySumm);

            Assert.AreEqual(9, user.Purse.GetAllCoins().First(i => i.Nominal == 1).Count);
            Assert.AreEqual(19, user.Purse.GetAllCoins().First(i => i.Nominal == 5).Count);
        }

        [Test]
        public void ByProductTest()
        {
            vendingInitState.StartUserSumm = 250;
            var vendingMachine = fixture.Create<VendingMachineCore>();
            
            Assert.AreEqual(250,vendingMachine.UserMoneySumm);
            var product=vendingMachine.BuyProduct(tea.Id);
            

            Assert.AreEqual(9,vendingMachine.GetProducts().First(i=>i.Id==tea.Id).Count);
            Assert.AreEqual(237,vendingMachine.UserMoneySumm);
            Assert.AreEqual(0,user.GetProducts().Count());
            user.AddProduct(product);
            Assert.AreEqual(1,user.GetProducts().Count());
        }

        [Test]
        public void TakeChangeTest()
        {
            vendingInitState.StartUserSumm = 28;
            user.Purse.SetCoins(1, 0);
            user.Purse.SetCoins(2, 0);
            user.Purse.SetCoins(5, 0);
            user.Purse.SetCoins(10, 0);

            var vendingMachine = fixture.Create<VendingMachineCore>();
            vendingMachine.TakeChange(user.Purse);
            Assert.AreEqual(98,vendingMachine.Purse.GetAllCoins().First(i=>i.Nominal==10).Count);
            Assert.AreEqual(99,vendingMachine.Purse.GetAllCoins().First(i=>i.Nominal==5).Count);
            Assert.AreEqual(99,vendingMachine.Purse.GetAllCoins().First(i=>i.Nominal==2).Count);
            Assert.AreEqual(99,vendingMachine.Purse.GetAllCoins().First(i=>i.Nominal==1).Count);

            
            Assert.AreEqual(2, user.Purse.GetAllCoins().First(i => i.Nominal == 10).Count);
            Assert.AreEqual(1, user.Purse.GetAllCoins().First(i => i.Nominal == 5).Count);
            Assert.AreEqual(1, user.Purse.GetAllCoins().First(i => i.Nominal == 2).Count);
            Assert.AreEqual(1, user.Purse.GetAllCoins().First(i => i.Nominal == 1).Count);


        }
    }
}
