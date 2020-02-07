using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;
using System;


namespace RunSQLJob
{
    class Program
    {
        static void Main(string[] args)
        {

            var userInput = Menu();

            if (userInput > 0)
            {
                Run(userInput);
            }
            else
            {
                userInput = Menu();
            }



        }

        static void Run(int databaseOption)
        {
            Console.WriteLine("Running Overnight Job on Demand");
            Server server = new Server(@"SERVERNAME");
            try
            {
                var database = "";

                switch (databaseOption)
                {
                    case 1:
                        database = "SETUP";
                        break;
                    case 2:
                        database = "QA";
                        break;
                    case 3:
                        database = "TRAIN";
                        break;
                    case 4:
                        database = "TEST";
                        break;
                    default:
                        database = "SETUP";
                        break;
                }

                server.ConnectionContext.LoginSecure = false;
                server.ConnectionContext.Login = "USERNAME";
                server.ConnectionContext.Password = "PASSWORD";
                server.ConnectionContext.Connect();
                Job job = server.JobServer.Jobs["BMSSmartcare" + database + " - Nightly Billing Processes"];
                job.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            finally
            {
                if (server.ConnectionContext.IsOpen)
                {
                    server.ConnectionContext.Disconnect();
                }

            }


        }

        static public int Menu()
        {
            Console.WriteLine("Nightly Process on Demand");
            Console.WriteLine("Please select the number of the database:");
            Console.WriteLine("1)  SETUP");
            Console.WriteLine("2)  QA");
            Console.WriteLine("3)  Train");
            Console.WriteLine("4)  Test");

            var result = Console.ReadLine();

            return Convert.ToInt32(result);




        }
    }
}
