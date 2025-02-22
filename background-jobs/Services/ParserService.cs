using System.Text;
using background_jobs.Models;
using HtmlAgilityPack;

namespace background_jobs.Services
{
    public class ParserService : IParserService
    {
        private readonly string _url = "https://ulyanovsk.drom.ru/auto/all/?distance=500";
        private readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;

        public ParserService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _httpClient = new HttpClient();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public async Task ParseWebsite()
        {
            try
            {
                var htmlContent = await GetPageContentAsync();
                if (string.IsNullOrEmpty(htmlContent))
                {
                    Console.WriteLine("Не удалось получить HTML-код страницы.");
                    return;
                }

                var carList = ExtractCarsFromHtml(htmlContent);
                if (carList.Count == 0)
                {
                    Console.WriteLine("Авто не найдены!");
                    return;
                }

                Console.WriteLine($"Машины с сайта!, {carList.Count}");
                Console.WriteLine("--------------------");
                using var scope = _serviceProvider.CreateScope();
                var carService = scope.ServiceProvider.GetService<ICarService>();
                var filteredCars = await carService.CheckCarAsync(carList);
                if (filteredCars.Count != 0)
                {
                    carList = await carService.RemoveDuplicateCarsAsync(carList, filteredCars);
                }
                Console.WriteLine($"Совпадения из БД!, {filteredCars.Count}");
                Console.WriteLine("--------------------");
                await carService.SaveCarsAsync(carList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task<string> GetPageContentAsync()
        {
            var response = await _httpClient.GetAsync(_url);
            if (!response.IsSuccessStatusCode) return string.Empty;

            var byteArray = await response.Content.ReadAsByteArrayAsync();
            var encoding = Encoding.GetEncoding("windows-1251");
            return encoding.GetString(byteArray);
        }


        public List<Car> ExtractCarsFromHtml(string htmlContent)
        {
            var carList = new List<Car>();
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);
            var carNodes = doc.DocumentNode.SelectNodes("//div[@class='css-1f68fiz ea1vuk60']");
            if (carNodes == null) return carList;
            foreach (var node in carNodes)
            {
                var title = node.SelectSingleNode(".//h3")?.InnerText.Trim() ?? "Название не найдено";
                var link = node.SelectSingleNode(".//a")?.GetAttributeValue("href", "") ?? "Ссылка не найдена";
                var price = node.SelectSingleNode(".//span[@data-ftid='bull_price']")?.InnerText.Trim()
                    .Replace("\u00A0", " ") ?? "Цена не найдена";

                carList.Add(new Car { Name = title, Link = link, Price = price });
            }

            return carList;
        }
    }
}