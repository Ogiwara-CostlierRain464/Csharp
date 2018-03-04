using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDD;

namespace UnitTestProject1 {
    [TestClass]
    public class UnitTest1 {

        private IOrderRepository _repository;

        [TestInitialize]
        public void SetUp() {
            _repository = new OrderRepositoryFake();
        }

        [TestMethod]
        public void OrderHasCustomer() {
            Order o = new Order(new Customer());

            Assert.IsNotNull(o.Customer);
        }

        [TestMethod]
        public void OrderDateIsCurrentAfterCreation() {
            var theTimeBefore = DateTime.Now.AddMilliseconds(-1);
            Order o = new Order(new Customer());

            Assert.IsTrue(o.OrderDate > theTimeBefore);
            Assert.IsTrue(o.OrderDate < DateTime.Now.AddMilliseconds(1));
        }

        [TestMethod]
        public void OrderNumberIsZeroAfterCreation() {
            Order o = new Order(new Customer());

            Assert.AreEqual(0, o.OrderNumber);
        }


        //[Ignore]
        [TestMethod]
        public void OrderNumberCantBeZeroAfterReconstitution() {
            int theOrderNumber = 42;
            _FakeAnOrder(theOrderNumber, new Customer(), _repository);

            Order o = _repository.GetOrder(theOrderNumber);

            Assert.AreEqual(theOrderNumber, o.OrderNumber);
        }

        private void _FakeAnOrder(int orderNumber, Customer c, IOrderRepository repository) {
            var o = new Order(c);
            RepositoryHelper.SetFieldWhenReconstitutingFromPersistence(o, "_orderNumber", orderNumber);
            repository.AddOrder(o);
        }

        [TestMethod]
        public void CanAddOrder() {
            _repository.AddOrder(new Order(new Customer()));
        }

        [TestMethod]
        public void CanFindOrdersViaCustomer() {
            var c = _FakeACustomer(1);
            _FakeAnOrder(42, c, _repository);
            _FakeAnOrder(12, _FakeACustomer(2), _repository);
            _FakeAnOrder(3, _FakeACustomer(1), _repository);

            Assert.AreEqual(2, _repository.GetOrders(c).Count);

        }

        private Customer _FakeACustomer(int customerNumber) {
            var c = new Customer();
            RepositoryHelper.SetFieldWhenReconstitutingFromPersistence(c, "_customerNumber", customerNumber);

            return c;
        }

        [TestMethod]
        public void EmptyOrderHasZeroForTotalAmount() {
            var o = new Order(new Customer());
            Assert.AreEqual(0, o.TotalAmount);
        }

        [Ignore]
        [TestMethod]
        public void OrderWithLinesHasTotalAmount() {
            var o = new Order(new Customer());

            var ol = new OrderLine(new Product("Chair", 52.00));
            ol.NumberOfUnits = 2;
            o.AddOrderLine(ol);

            var ol2 = new OrderLine(new Product("Desk", 115.00));
            ol2.NumberOfUnits = 3;
            o.AddOrderLine(ol2);

            Assert.AreEqual(104.00 + 345.00, o.TotalAmount);
        
        }

        [TestMethod]
        public void OrderLineGetsDefaultPrice() {
            var p = new Product("Chair", 52.00);
            var ol = new OrderLine(p);

            Assert.AreEqual(52.00, ol.Price);
        }

        [TestMethod]
        public void OrderLineHasTotalAmount() {
            var ol = new OrderLine(new Product("Chair", 52.00));
            ol.NumberOfUnits = 2;

            Assert.AreEqual(104.00, ol.TotalAmout);
        }

        [TestMethod]
        public void CanAddOrderLine() {
            var o = new Order(new Customer());
            

        }

    }
}
