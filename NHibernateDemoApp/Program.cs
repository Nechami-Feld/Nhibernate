using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

using System;
using System.Linq;
using System.Reflection;
using System.Data.SqlClient;
using NHibernate;

namespace NHibernateDemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            NHibernateProfilerBootstrapper.PreStart();

            var cfg = new Configuration();

            String DataSource = "asia13797\\sqlexpress";
            String InitialCatalog = "NHibernateDemoDB";
            String IntegratedSecurity = "True";
            String ConnectTimeout = "15";
            String Encrypt = "False";
            String TrustServerCertificate = "False";
            String ApplicationIntent = "ReadWrite";
            String MultiSubnetFailover = "False";

            cfg.DataBaseIntegration(x => {
                x.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = NhibernateDemoDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
   


            x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
                x.LogSqlInConsole = true;

            });

            cfg.AddAssembly(Assembly.GetExecutingAssembly());

            var sefact = cfg.BuildSessionFactory();
            
            ReadData(sefact);
            //AddData(sefact);
            //ReadData(sefact, 1);
            //UpdateData(sefact, 1);
            //DeleteData(sefact, 4);
        }


        public static void AddData(ISessionFactory sefact)
        {
            using (var session = sefact.OpenSession())
            {

                using (var tx = session.BeginTransaction())
                {
                    var student1 = new Student
                    {
                        ID = 3,
                        FirstName = "Allan",
                        LastName = "Bommer"
                    };

                    var student2 = new Student
                    {
                        ID = 4,
                        FirstName = "Jerry",
                        LastName = "Lewis"
                    };

                    session.Save(student1);
                    session.Save(student2);
                    tx.Commit();
                }    //perform database logic 

                Console.WriteLine("Added Data");
                Console.ReadLine();
            }
        }
        public static void ReadData(ISessionFactory sefact)
        {
            using (var session = sefact.OpenSession())
            {

                using (var tx = session.BeginTransaction())
                {
                    var students = session.CreateCriteria<Student>().List<Student>();

                    foreach (var student in students)
                    {
                        Console.WriteLine("{0} \t{1} \t{2}",
                           student.ID, student.FirstName, student.LastName);
                    }

                    tx.Commit();
                }

                Console.ReadLine();
            }
        }
        public static void ReadData(ISessionFactory sefact, int id)
        {
            using (var session = sefact.OpenSession())
            {

                using (var tx = session.BeginTransaction())
                {
                    var students = session.CreateCriteria<Student>().List<Student>();

                    foreach (var student in students)
                    {
                        Console.WriteLine("{0} \t{1} \t{2}", student.ID,
                           student.FirstName, student.LastName);
                    }

                    var stdnt = session.Get<Student>(id);
                    Console.WriteLine("Retrieved by ID");
                    Console.WriteLine("{0} \t{1} \t{2}", stdnt.ID,
                       stdnt.FirstName, stdnt.LastName);
                    tx.Commit();
                }

                Console.ReadLine();
            }
        }
        public static void UpdateData(ISessionFactory sefact, int id)
        {
            using (var session = sefact.OpenSession())
            {

                using (var tx = session.BeginTransaction())
                {
                    var students = session.CreateCriteria<Student>().List<Student>();

                    foreach (var student in students)
                    {
                        Console.WriteLine("{0} \t{1} \t{2}", student.ID,
                           student.FirstName, student.LastName);
                    }

                    var stdnt = session.Get<Student>(id);
                    Console.WriteLine("Retrieved by ID");
                    Console.WriteLine("{0} \t{1} \t{2}", stdnt.ID, stdnt.FirstName, stdnt.LastName);

                    Console.WriteLine("Update the last name of ID = {0}", stdnt.ID);
                    stdnt.LastName = "Donald";
                    session.Update(stdnt);
                    Console.WriteLine("\nFetch the complete list again\n");

                    foreach (var student in students)
                    {
                        Console.WriteLine("{0} \t{1} \t{2}", student.ID,
                           student.FirstName, student.LastName);
                    }

                    tx.Commit();
                }

                Console.ReadLine();
            }
        }
        public static void DeleteData(ISessionFactory sefact, int id)
        {
            using (var session = sefact.OpenSession())
            {

                using (var tx = session.BeginTransaction())
                {
                    var students = session.CreateCriteria<Student>().List<Student>();

                    foreach (var student in students)
                    {
                        Console.WriteLine("{0} \t{1} \t{2}", student.ID,
                           student.FirstName, student.LastName);
                    }

                    var stdnt = session.Get<Student>(id);
                    Console.WriteLine("Retrieved by ID");
                    Console.WriteLine("{0} \t{1} \t{2}", stdnt.ID, stdnt.FirstName, stdnt.LastName);

                    Console.WriteLine("Delete the record which has ID = {0}", stdnt.ID);
                    session.Delete(stdnt);
                    Console.WriteLine("\nFetch the complete list again\n");

                    foreach (var student in students)
                    {
                        Console.WriteLine("{0} \t{1} \t{2}", student.ID, student.FirstName,
                           student.LastName);
                    }

                    tx.Commit();
                }

                Console.ReadLine();
            }
        }
    }
}

