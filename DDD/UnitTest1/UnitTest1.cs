using System;
using Xunit;
using Domain;

namespace UnitTest1
{
    public class UnitTest1
    {

        private IOrderRepository _repository;


        public UnitTest1()
        {
            _repository = new OrderRepositoryFake();
        }

        [Fact]
        public void OrderHasCustomer()
        {
            Order o = new Order(new Customer());

            Assert.NotNull(o.Customer);
        }

        [Fact]
        public void OrderDateIsCurrentAfterCreation()
        {
            var theTimeBefore = DateTime.Now.AddMilliseconds(-1);
            Order o = new Order(new Customer());

            Assert.True(o.OrderDate > theTimeBefore);
            Assert.True(o.OrderDate < DateTime.Now.AddMilliseconds(1));
        }

        [Fact]
        public void OrderNumberIsZeroAfterCreation()
        {
            Order o = new Order(new Customer());

            Assert.Equal(0, o.OrderNumber);
        }


        //[Ignore]
        [Fact]
        public void OrderNumberCantBeZeroAfterReconstitution()
        {
            int theOrderNumber = 42;
            _FakeAnOrder(theOrderNumber, new Customer(), _repository);

            Order o = _repository.GetOrder(theOrderNumber);

            Assert.Equal(theOrderNumber, o.OrderNumber);
        }

        private void _FakeAnOrder(int orderNumber, Customer c, IOrderRepository repository)
        {
            var o = new Order(c);
            RepositoryHelper.SetFieldWhenReconstitutingFromPersistence(o, "_orderNumber", orderNumber);
            repository.AddOrder(o);
        }

        [Fact]
        public void CanAddOrder()
        {
            _repository.AddOrder(new Order(new Customer()));
        }

        [Fact]
        public void CanFindOrdersViaCustomer()
        {
            var c = _FakeACustomer(1);
            _FakeAnOrder(42, c, _repository);
            _FakeAnOrder(12, _FakeACustomer(2), _repository);
            _FakeAnOrder(3, _FakeACustomer(1), _repository);

            Assert.Equal(2, _repository.GetOrders(c).Count);

        }

        private Customer _FakeACustomer(int customerNumber)
        {
            var c = new Customer();
            RepositoryHelper.SetFieldWhenReconstitutingFromPersistence(c, "_customerNumber", customerNumber);

            return c;
        }

        [Fact]
        public void EmptyOrderHasZeroForTotalAmount()
        {
            var o = new Order(new Customer());
            Assert.Equal(0, o.TotalAmount);
        }

        [Fact(Skip = "reason")]
        public void OrderWithLinesHasTotalAmount()
        {
            var o = new Order(new Customer());

            var ol = new OrderLine(new Product("Chair", 52.00));
            ol.NumberOfUnits = 2;
            o.AddOrderLine(ol);

            var ol2 = new OrderLine(new Product("Desk", 115.00));
            ol2.NumberOfUnits = 3;
            o.AddOrderLine(ol2);

            Assert.Equal(104.00 + 345.00, o.TotalAmount);

        }

        [Fact]
        public void OrderLineGetsDefaultPrice()
        {
            var p = new Product("Chair", 52.00);
            var ol = new OrderLine(p);

            Assert.Equal(52.00, ol.Price);
        }

        [Fact]
        public void OrderLineHasTotalAmount()
        {
            var ol = new OrderLine(new Product("Chair", 52.00));
            ol.NumberOfUnits = 2;

            Assert.Equal(104.00, ol.TotalAmout);
        }

        [Fact]
        public void CanAddOrderLine()
        {
            var o = new Order(new Customer());


        }

    }
}

