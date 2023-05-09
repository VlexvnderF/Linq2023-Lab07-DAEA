using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2023
{
    internal class Program
    {

        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            //DataSource();
            IntroToLINQ();
            DataSource();
            Filtering();
            Ordering();
            Grouping();
            Grouping2();
            Joining();
            IntroToLINQLMB();
            DataSourceLMB();
            FilteringLMB();
            OrderingLMB();
            GroupingLMB();
            Grouping2LMB();
            JoiningLMB();
            Console.Read();

        }

        static void IntroToLINQ()
        {


            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6 };


            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            foreach(int num in numQuery) {
            
            Console.Write("{0,1}", num);
            }
        }

        static void DataSource()
        {

            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach(var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach(var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void Ordering()
        {
            var queryLondonCustomers3 =
                from cust in context.clientes
                where cust.Ciudad == "London"
                orderby cust.NombreCompañia ascending
                select cust;
            foreach(var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Grouping()
        {
            var queryCustomersByCity =
                from cust in context.clientes
                group cust by cust.Ciudad;

        foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach(clientes customer in customerGroup)
                {
                    Console.WriteLine("     {0}", customer.NombreCompañia);
                }
            }
        }

        static void Grouping2()
        {
            var custQuery =
                from cust in context.clientes
                group cust by cust.Ciudad into custGroup
                where custGroup.Count() > 2
                orderby custGroup.Key 
                select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Joining()
        {
            var innerJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };
            foreach ( var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }



        //Metodos con expresiones lambda

        static void IntroToLINQLMB()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6 };

            var evenNums = numbers.Where(num => num % 2 == 0);

            Console.WriteLine(" ");
            Console.WriteLine("Métodos utilizando LAMBDA");

            foreach (int num in evenNums)
            {
                Console.Write("{0,1}", num);
            }
        }

        static void DataSourceLMB()
        {
            var allCustomers = context.clientes.ToList();

            foreach (var item in allCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void FilteringLMB()
        {
            var londonCustomers = context.clientes.Where(cust => cust.Ciudad == "Londres").ToList();

            foreach (var item in londonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }


        static void OrderingLMB()
        {
            var londonCustomers = context.clientes.Where(cust => cust.Ciudad == "Londres").OrderBy(cust => cust.NombreCompañia);

            foreach (var item in londonCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void GroupingLMB()
        {
            var customersByCity = context.clientes.GroupBy(cust => cust.Ciudad);

            foreach (var customerGroup in customersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("     {0}", customer.NombreCompañia);
                }
            }
        }

        static void Grouping2LMB()
        {
            var customersByCity = context.clientes.GroupBy(cust => cust.Ciudad).Where(custGroup => custGroup.Count() > 2).OrderBy(custGroup => custGroup.Key);

            foreach (var item in customersByCity)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void JoiningLMB()
        {
            var innerJoinQuery = context.clientes.Join(context.Pedidos, cust => cust.idCliente, dist => dist.IdCliente, (cust, dist) => new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario });

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }




    }
}
