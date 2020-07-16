using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Lab1_class
{

    class Person
    {
        public string Name { get; set; }
        public byte Age { get; set; }
        public decimal Salary { get; set; }

        public string Address { get; set; }

        public bool Valid()
        {
            if (Name.Length == 0)
            {
                return false; //code below it won't execute inside method
            }

            if (Age < 0 || Age >= 150)
            {
                return false;
            }

            return true;
        }
    }

    public class Product
    {
        private int _qty;
        private double _cost;
        private double _discount;
        private double _sell;
        public int qty
        {
            get { return _qty; }

            set 
            {
                if (! isValidQty(value))
                {
                    throw new Exception("Invalid QTY!");
                }

                _qty = value;
            }
        }
        public double cost
        {
            get
            {
                return _cost;
            }
            set
            {
                if (!isValidPrice(value))
                {
                    throw new Exception("Invalid Price!");
                }

                _cost = value;
            }
        }

        public double discount
        {
            get
            {
                return _discount;
            }
            set
            {
                if (!isValidPrice(value))
                {
                    throw new Exception("Invalid Price!");
                }

                _discount = value;
            }
        }

        public double sell
        {
            get
            {
                return _sell;
            }
            set
            {
                if (!isValidPrice(value))
                {
                    throw new Exception("Invalid Price!");
                }

                _sell = value;
            }
        }

        private bool isValidQty(int qty) //encapsulation: function is private as unnecessary validations are hidden from users
        {
            return qty <= 0 ? false : true;
        }

        private bool isValidPrice(double cost) //encapsulation: function is private as unnecessary validations are hidden from users
        {
            return cost <= 0 ? false : true;
        }
        //overloading : same func name and different func signature/ parameters(diff datatype, diff number of arguments. diff sequence); return type does not matter
        // Overloading - Static Polymorphism
        public double CalculateCost(int qty, double price) //overload1
        {
            return qty * price;
        }


        public double CalculateCost(double price, int qty) //overload2
        {
            return qty * price;
        }

        public double CalculateCost(int qty, double price, double discount) //overload3
        {
            return (CalculateCost(qty, price) + discount); //extendability
        }

        //overrriding: min 2 classes is required (Inheritance) and func definition is same 
        public virtual double CalculateProfit(int qty, double cost, double discount, double sell)
        {
            return (qty * sell) - CalculateCost(qty, cost, discount);
        }


    }

    public class ProductwithSnS : Product
    {
   
        private double SnSAmt = 0;

       
        public ProductwithSnS(double snsAmt)
        {
            SnSAmt = snsAmt;
        }
        public override double CalculateProfit(int qty, double cost, double discount, double sell)
        {
            return (qty * sell) - (base.CalculateCost(qty, cost, discount) + SnSAmt);
        }
    }


    public abstract class DBconn //abstract class
    {
        //always parent and base class (Half made classes)
        //class object can NOT be created
        //class contain astract and non abtract methods
        //abstract class has to be inherited by child class to implement abstract methods
        //abstract is by deafault public
        //abstract class can NOT be sealed or static
        //abstract class can NOT private, protected, Internal
        //abstract method can not be virtual as by deafult they are virtual
        public abstract void connection(); //abstract func

        public string GetIpAddress() //non abstract func
        {
            return "10.0.0.1";
        }
    }

    public class OracleDB:DBconn
    {
        public override void connection()
        {
            Console.WriteLine("Connected to Oracle");
        }
    }

    public class SQLDB : DBconn
    {
        public override void connection()
        {
            Console.WriteLine("Connected to SQL");
        }
    }


    // Inteface : force developer to follow standards - enforce standardization of vocabulary

    interface IDatabase
    {
        void Insert();
    }

    //developer 1
    class DB1 : IDatabase
    {
        // void DBInsert() { }; //without interface they could have named functions the way they wanted
        public void Insert() { Console.WriteLine("Inserted records to DB1"); }
    }

    //developer 2
    class DB2 :IDatabase
    {
        // void AddDB() { };  //without interface they could have named functions the way they wanted

        public void Insert() { Console.WriteLine("Inserted Records to DB2"); }
    }


    //Interface - decoupling 

    public interface IDb
    {
        void Update();
    }

    public class clsOracle:IDb //concrete class
    {
        public void Update()
        {
            Console.WriteLine("Updated records to Oracle");
        }
    }

    public class clsSQL : IDb //concrete class
    {
        
        public void Update()
        {
            Console.WriteLine("Updated records to SQL");
        }
        
    }

    public class clsCustomer //concrete class will talk only to inteface and not directly to diff database objects
    {
        // we can add another concerete class: MySQL and we need not change clsCustomer class
        public void Update(IDb obj)
        {
            obj.Update();
        }
    }

    //Inteface can not have field variables
    //inteface cannot have implemented funtions and methods
    //all methods in inteface needs to be implemented
    //interface can be used to implemenet run time interface
    //child class can implement multiple inteface
    //all interface methods are public 


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("No of records to be entered:");
            byte records = Convert.ToByte(Console.ReadLine());

            for (byte i = 0; i < records; i++)
            {
                try
                {
                    Person pobj = new Person();
                    pobj.Name = Console.ReadLine();
                    pobj.Age = Convert.ToByte(Console.ReadLine());

                    if (pobj.Valid())
                    {
                        Console.WriteLine(string.Format("Name:{0} and Age:{1}", pobj.Name, pobj.Age));

                    }
                    else
                    {
                        throw new Exception("Invalid Data!!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("*******************************");


            Product probj;

       

            bool SnS = true;

            if (SnS)
            {
                probj = new ProductwithSnS(5);
                probj.qty = 10;
                probj.cost = 10;
                probj.sell = 15;
                probj.discount = 5;
                double profit = probj.CalculateProfit(probj.qty, probj.cost, probj.discount, probj.sell);
                Console.WriteLine(profit);
            }

            else
            {
                probj = new Product();
                probj.qty = 10;
                probj.cost = 10;
                probj.sell = 15;
                probj.discount = 5;
                Console.WriteLine(probj.CalculateProfit(probj.qty, probj.cost, probj.discount, probj.sell));
            }

        }
    }
}

