using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODotNetFeatures
{
    internal class Customer
    {
        private readonly string connectionString;

        public int CustomerId { get; set; }
        public string Name { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public Customer()
        {
            this.connectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["localDb"]);
        }

        public Customer(string name, string city, string state)
            : this()
        {
            this.Name = name;
            this.City = city;
            this.State = state;
        }


        private SqlConnection connection = null;
        private SqlCommand command = null;
        private SqlDataReader reader = null;
        private SqlDataAdapter adapter = null;

        public int Add()
        {
            int isDataAddedSuccesfully = 0;
            try
            {
                using (this.connection = new SqlConnection(this.connectionString))
                {
                    using (this.command = new SqlCommand())
                    {

                        this.command.Connection = this.connection;
                        this.command.CommandText = "INSERT INTO Customer(Name,State,City) VALUES (@name, @state,@city)";
                        this.command.Parameters.AddWithValue("@name", this.Name);
                        this.command.Parameters.AddWithValue("@state", this.State);
                        this.command.Parameters.AddWithValue("@city", this.City);

                        this.connection.Open();

                        isDataAddedSuccesfully = this.command.ExecuteNonQuery();

                        this.connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            finally
            {
                if (this.connection != null && this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }

            return isDataAddedSuccesfully;
        }

        public int AddWithStoredProcedure()
        {
            try
            {
                using (this.connection = new SqlConnection(this.connectionString))
                {
                    using (this.command = new SqlCommand("AddNewCustomer", this.connection))
                    {

                        this.command.CommandType = CommandType.StoredProcedure;
                        this.command.Parameters.AddWithValue("@name", this.Name);
                        this.command.Parameters.AddWithValue("@state", this.State);
                        this.command.Parameters.AddWithValue("@city", this.City);
                        this.command.Parameters.Add("@returValue", SqlDbType.Int)
                            .Direction = ParameterDirection.ReturnValue;

                        connection.Open();

                        this.command.ExecuteNonQuery();
                        this.CustomerId = Convert.ToInt32(this.command.Parameters["@returValue"].Value);

                        connection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            finally
            {
                if (this.connection != null && this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }
            return this.CustomerId;
        }

        public int AddWithStoredProcedureWithOutParameter()
        {
            try
            {
                using (this.connection = new SqlConnection(this.connectionString))
                {
                    using (this.command = new SqlCommand("AddCustomer", this.connection))
                    {

                        this.command.CommandType = CommandType.StoredProcedure;
                        this.command.Parameters.AddWithValue("@name", this.Name);
                        this.command.Parameters.AddWithValue("@state", this.State);
                        this.command.Parameters.AddWithValue("@city", this.City);
                        this.command.Parameters.Add("@CustomerId", SqlDbType.Int)
                            .Direction = ParameterDirection.Output;

                        connection.Open();

                        this.command.ExecuteNonQuery();
                        this.CustomerId = Convert.ToInt32(this.command.Parameters["@CustomerId"].Value);

                        connection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            finally
            {
                if (this.connection != null && this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }
            return this.CustomerId;
        }


        public List<Customer> GetAll()
        {
            List<Customer> customerCollection = null;

            try
            {
                using (this.connection = new SqlConnection(this.connectionString))
                {
                    using (this.command = new SqlCommand("GetAllCustomer", this.connection))
                    {
                        this.connection.Open();

                        using (this.reader = this.command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (this.reader.HasRows)
                            {
                                customerCollection = new List<Customer>();

                                while (this.reader.Read())
                                {
                                    customerCollection.Add(new Customer
                                    {
                                        CustomerId = Convert.ToInt32(reader["Id"])
                                        ,
                                        Name = Convert.ToString(reader["Name"])
                                        ,
                                        State = Convert.ToString(reader["State"])
                                        ,
                                        City = Convert.ToString(reader["City"])
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            finally
            {
                if (this.connection != null && this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }

            return customerCollection;
        }

        public void GetAllCustomerAndOrder(out List<Customer> customerCollection, out List<Order> orderCollection)
        {
            customerCollection = new List<Customer>();
            orderCollection = new List<Order>();

            try
            {
                using (this.connection = new SqlConnection(this.connectionString))
                {
                    using (this.command = new SqlCommand("GetAllCustomerAndOrder", this.connection))
                    {
                        this.connection.Open();

                        using (this.reader = this.command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (this.reader.HasRows)
                            {
                                while (this.reader.Read())
                                {
                                    customerCollection.Add(new Customer
                                    {
                                        CustomerId = Convert.ToInt32(reader["Id"])
                                        ,
                                        Name = Convert.ToString(reader["Name"])
                                        ,
                                        State = Convert.ToString(reader["State"])
                                        ,
                                        City = Convert.ToString(reader["City"])
                                    });
                                }
                            }

                            if (this.reader.NextResult())
                            {
                                if (reader.HasRows)
                                {
                                    while (this.reader.Read())
                                    {
                                        orderCollection.Add(new Order
                                        {
                                            OrderId = Convert.ToInt32(reader["OrderId"])
                                            ,
                                            OrderDate = Convert.ToDateTime(reader["OrderDate"])
                                            ,
                                            CustomerId = Convert.ToInt32(reader["CustomerId"])
                                        });
                                    }
                                }

                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            finally
            {
                if (this.connection != null && this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }

        }

        public void GetAllCustomerAndOrderUsingAdapter(out List<Customer> customerCollection, out List<Order> orderCollection)
        {
            customerCollection = new List<Customer>();
            orderCollection = new List<Order>();

            try
            {
                using (this.connection = new SqlConnection(this.connectionString))
                {
                    using (this.adapter = new SqlDataAdapter("GetAllCustomerAndOrder", this.connection))
                    {
                        using (DataSet set = new DataSet())
                        {
                            this.adapter.Fill(set);

                            if (set.Tables[0] != null && set.Tables[0].Rows.Count != 0)
                            {
                                foreach (DataRow row in set.Tables[0].Rows)
                                {
                                    customerCollection.Add(new Customer
                                    {
                                        CustomerId = Convert.ToInt32(row["Id"])
                                        ,
                                        Name = Convert.ToString(row["Name"])
                                        ,
                                        State = Convert.ToString(row["State"])
                                        ,
                                        City = Convert.ToString(row["City"])
                                    });
                                }
                            }

                            if (set.Tables[1] != null && set.Tables[1].Rows.Count != 0)
                            {
                                foreach (DataRow row in set.Tables[1].Rows)
                                {
                                    orderCollection.Add(new Order
                                    {
                                        OrderId = Convert.ToInt32(row["OrderId"])
                                        ,
                                        OrderDate = Convert.ToDateTime(row["OrderDate"])
                                        ,
                                        CustomerId = Convert.ToInt32(row["CustomerId"])
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            finally
            {
                if (this.connection != null && this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }

        }


    }

}
