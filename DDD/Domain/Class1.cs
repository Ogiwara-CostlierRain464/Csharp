using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Domain
{
    public class Customer{
        private int _customerNumber = 0;
        public string Name;

        public int CustomerNumber
        {
            get { return _customerNumber; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Customer))
            {
                return false;
            }
            return (obj as Customer).CustomerNumber == _customerNumber;
        }

        public CustomerSnapshot TakeSnapshot()
        {
            var snapshot = new CustomerSnapshot();
            snapshot.CustomerNumber = _customerNumber;
            snapshot.Name = Name;

            return snapshot;
        }
    }

    public class ReferencePerson{

        public readonly string FirstName;
        public readonly string LastName;

		public override bool Equals(object obj)
		{
            var other = obj as ReferencePerson;
            return other != null
                && GetType() == other.GetType()
                                     && FirstName == other.FirstName
                                     && LastName == other.LastName;


		}
	}

    public class CustomerSnapshot{
        public int CustomerNumber;
        public string Name;
    }

    public class Order
    {

        public readonly CustomerSnapshot Customer;
        public readonly OrderType OrderType;
        public readonly ReferencePerson ReferencePerson;

        private DateTime _orderDate;
        private int _orderNumber;
        private readonly List<OrderLine> _orderLines = new List<OrderLine>();

        public Order(Customer customer)
        {
            Customer = customer.TakeSnapshot();
            _orderDate = DateTime.Now;
            _orderNumber = 0;
        }

        public IReadOnlyList<OrderLine> OrderLines
        {
            get { return _orderLines.AsReadOnly(); }
        }

        public DateTime OrderDate
        {
            get { return _orderDate; }
        }

        public int OrderNumber
        {
            get { return _orderNumber; }
        }

        public double TotalAmount
        {
            get {
                double sum = 0;
                foreach (OrderLine ol in _orderLines)
                    sum += ol.TotalAmout;

                return sum;
            }
        }

        public void AddOrderLine(OrderLine ol)
        {
            _orderLines.Add(ol);
        }
    }

    public enum OrderType{
        
    }


    public class OrderRepositoryFake : IOrderRepository
    {

        private List<Order> _theOrders = new List<Order>();

        public void AddOrder(Order order)
        {
            int theNumberOfOrdersBefore = _theOrders.Count;

            _theOrders.Add(order);

            //MyTrace.Assert(theNumberOfOrdersBefore == _theOrders.Count);
        }

        public Order GetOrder(int orderNumber)
        {
            return _theOrders.FirstOrDefault(e => e.OrderNumber == orderNumber);

        }

        public List<Order> GetOrders(Customer c)
        {
            return _theOrders.Where(e => e.Customer.Equals(c)).ToList();
        }
    }

    public interface IOrderRepository
    {
        void AddOrder(Order order);
        Order GetOrder(int orderNumber);
        List<Order> GetOrders(Customer c);
    }

    public interface IWorkspace{
        object GetById(Type typeToGet, object idValue);
    }

    public class MyTrace
    {
        public static void Assert(bool assert)
        {
            if (assert == false)
            {
                throw new Exception("Assertation Failed");
            }
        }
    }

    public class RepositoryHelper
    {
        public static void SetFieldWhenReconstitutingFromPersistence(object instance, string fieldName, object newValue)
        {
            Type t = instance.GetType();
            FieldInfo f = t.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            f.SetValue(instance, newValue);
        }
    }

    public class Product
    {
        private string _name;
        private double _price;

        public Product(string name, double price)
        {
            _name = name;
            _price = price;
        }

        public double UnitPrice
        {
            get { return _price; }
        }

        public string Description
        {
            get { return _name; }
        }
    }

    public class OrderLine
    {
        public double Price = 0;
        public int NumberOfUnits;

        private Product _product;

        public OrderLine(Product product)
        {
            _product = product;
            Price = product.UnitPrice;
        }



        public Product Product
        {
            get { return _product; }
        }

        public double TotalAmout
        {
            get { return Price * NumberOfUnits; }
        }
    }

}
