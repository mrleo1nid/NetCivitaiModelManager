using Akavache;
using CivitaiApiWrapper.DataContracts.Requsts;
using CivitaiApiWrapper.Enums;
using CivitaiApiWrapper.Services;
using Refit;
using System.Text.Json;

namespace TestsApp 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");
            try
            {
                TestApi.Start();
            }
            catch (Exception ex) { Console.WriteLine("Api test error :" + ex.ToString());}
            Console.WriteLine("Complete.");
            Console.ReadKey();
        }
       
    }
}