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
                TestApi();
            }
            catch (Exception ex) { Console.WriteLine("Api test error :" + ex.ToString());}
            Console.WriteLine("Complete.");
            Console.ReadKey();
            BlobCache.Shutdown().Wait();
        }
        private static void TestApi()
        {
            var service = RestService.For<ICivitaiService>("https://civitai.com");
            var cashedandpoly = new PoliCivitaiService(service, null);
            var request = new BaseQueryParameters() { Limit = 10, Page = 1 };
            var modelsrequest = new ModelsRequstParameters() { Limit = 6, Page = 1, Types = new List<Types>() { Types.Checkpoint, Types.TextualInversion }, Favorites=true };
            Console.WriteLine("Service.");
            TestCreators(service, request);
            TestTags(service, request);
            TestGetModelById(service, 5285);
            TestModelVersionById(service, 5285);
            TestModelVersionByHash(service, "471F486B92700C8DA88DD7CD8F1A9B41691F728A9BFC83A91A041F0FB56BD6C4");
            TestGetModels(service, modelsrequest);
            Console.WriteLine("Cashed and poly.");
            TestCreators(cashedandpoly, request);
            TestTags(cashedandpoly, request);
            TestGetModelById(cashedandpoly, 5285);
            TestModelVersionById(cashedandpoly, 5285);
            TestModelVersionByHash(cashedandpoly, "471F486B92700C8DA88DD7CD8F1A9B41691F728A9BFC83A91A041F0FB56BD6C4");
            TestGetModels(cashedandpoly, modelsrequest);
        }
        private static void TestCreators(ICivitaiService service, BaseQueryParameters request)
        {
            Console.WriteLine("TestCreators.");
            var creatorsresp = service.GetCreators(request).Result;
            foreach (var creator in creatorsresp.Items)
            {
                Console.WriteLine($"Creator: {creator.Username} ({creator.ModelCount} models) - {creator.Link}");
            }
            Console.WriteLine($"Meta: Page= {creatorsresp.Metadata.CurrentPage}, MaxPage = {creatorsresp.Metadata.TotalPages}");
        }
        private static void TestTags(ICivitaiService service, BaseQueryParameters request)
        {
            Console.WriteLine("TestTags.");
           
            var responce = service.GetTags(request).Result;
            foreach (var tag in responce.Items)
            {
                Console.WriteLine($"Tag: {tag.Name} ({tag.ModelCount} models) - {tag.Link}");
            }
            Console.WriteLine($"Meta: Page= {responce.Metadata.CurrentPage}, MaxPage = {responce.Metadata.TotalPages}");
        }
        private static void TestModelVersionById(ICivitaiService service, int id)
        {
            Console.WriteLine("TestModelVersionById.");

            var responce = service.GetModelVersion(id).Result;
            Console.WriteLine($"{id}  Responce: {JsonSerializer.Serialize(responce, new JsonSerializerOptions() { WriteIndented = true})}");
        }
        private static void TestModelVersionByHash(ICivitaiService service, string hash)
        {
            Console.WriteLine("TestModelVersionById.");

            var responce = service.GetModelVersion(hash).Result;
            Console.WriteLine($"{hash} Responce: {JsonSerializer.Serialize(responce, new JsonSerializerOptions() { WriteIndented = true})}");
        }
        private static void TestGetModels(ICivitaiService service, ModelsRequstParameters modelsRequstParameters)
        {
            Console.WriteLine("TestModelVersionById.");

            var responce = service.GetModels(modelsRequstParameters).Result;
            Console.WriteLine($"Request : {JsonSerializer.Serialize(modelsRequstParameters, new JsonSerializerOptions() { WriteIndented = true })}" +
                $" Responce: {JsonSerializer.Serialize(responce, new JsonSerializerOptions() { WriteIndented = true})}");
        }
        private static void TestGetModelById(ICivitaiService service, int id)
        {
            Console.WriteLine("TestModelVersionById.");

            var responce = service.GetModel(id).Result;
            Console.WriteLine($"{id} {JsonSerializer.Serialize(responce, new JsonSerializerOptions() { WriteIndented = true})}");
        }
    }
}