using System;
using System.Collections.Generic;
using System.Linq;

namespace ADODotNetFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            AddCustomer();

            AddNewCustomer();

            GetAllCustomer();

            GetAllCustomerAndOrder();

            Console.ReadLine();

        }

        private static void GetAllCustomerAndOrder()
        {
            List<Customer> customerCollection;
            List<Order> orderCollection;

            new Customer().GetAllCustomerAndOrder(out customerCollection, out orderCollection);

            customerCollection.ForEach(
                    c =>
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("CustomerId: {0} ; Name : {1} ; State: {2} ; City: {3}", c.CustomerId, c.Name, c.State, c.City);
                        orderCollection
                            .Where(o => o.CustomerId == c.CustomerId).ToList()
                                .ForEach(oc =>
                                {
                                    Console.WriteLine("OrderId: {0} ; Order Date: {1}", oc.OrderId, oc.OrderDate);
                                });
                    });

        }

        private static void GetAllCustomer()
        {
            List<Customer> customerCollection = (new Customer()).GetAll();

            if (customerCollection != null && customerCollection.Any())
            {
                foreach (var item in customerCollection)
                {
                    Console.WriteLine("Id:{0};Name:{1};State:{2};City:{3}",
                        item.CustomerId, item.Name, item.State, item.City);

                }
            }
        }

        static void AddCustomer()
        {
            Console.WriteLine("Provide Customer Name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Provide Customer City: ");
            var city = Console.ReadLine();

            Console.WriteLine("Provide Customer State: ");
            var state = Console.ReadLine();

            var customerObj = new Customer(name, city, state);
            if (customerObj.AddWithStoredProcedure() > 0)
            {
                Console.WriteLine("Customer added successfully.Do you want to add more? ");

                if (Console.ReadLine().ToString().ToUpper() == "Y")
                {
                    AddCustomer();
                }
            }
        }

        static void AddNewCustomer()
        {
            Console.WriteLine("Provide Customer Name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Provide Customer City: ");
            var city = Console.ReadLine();

            Console.WriteLine("Provide Customer State: ");
            var state = Console.ReadLine();

            var customerObj = new Customer(name, city, state);
            if (customerObj.AddWithStoredProcedureWithOutParameter() > 0)
            {
                Console.WriteLine("Customer added successfully.Do you want to add more? ");

                if (Console.ReadLine().ToString().ToUpper() == "Y")
                {
                    AddNewCustomer();
                }
            }
        }
    }
}
