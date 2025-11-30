using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PublicHolidayTracker
{
    class Holiday
    {
        public string date { get; set; }
        public string localName { get; set; }
        public string name { get; set; }
        public string countryCode { get; set; }
        public bool @fixed { get; set; }
        public bool global { get; set; }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static List<Holiday> holidayCache = new List<Holiday>();

        static async Task Main(string[] args)
        {
            await LoadHolidaysAsync();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== PublicHolidayTracker =====");
                Console.WriteLine("1. Tatil listesini göster (yıl seçmeli)");
                Console.WriteLine("2. Tarihe göre tatil ara (gg-aa formatı)");
                Console.WriteLine("3. İsme göre tatil ara");
                Console.WriteLine("4. Tüm tatilleri 3 yıl boyunca göster (2023–2025)");
                Console.WriteLine("5. Çıkış");
                Console.Write("Seçiminiz: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowHolidaysByYear();
                        break;
                    case "2":
                        SearchByDate();
                        break;
                    case "3":
                        SearchByName();
                        break;
                    case "4":
                        ShowAllHolidays();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Devam etmek için bir tuşa basın.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static async Task LoadHolidaysAsync()
        {
            Console.WriteLine("Veriler API üzerinden çekiliyor, lütfen bekleyiniz...");
            int[] years = { 2023, 2024, 2025 };

            foreach (var year in years)
            {
                try
                {
                    string url = $"https://date.nager.at/api/v3/PublicHolidays/{year}/TR";
                    string jsonString = await client.GetStringAsync(url);
                    var holidays = JsonSerializer.Deserialize<List<Holiday>>(jsonString);

                    if (holidays != null)
                    {
                        holidayCache.AddRange(holidays);
                    }
                }
                catch
                {
                    Console.WriteLine($"{year} yılı verileri alınırken hata oluştu.");
                }
            }
        }

        static void ShowHolidaysByYear()
        {
            Console.Write("Listelenecek yılı giriniz (2023, 2024, 2025): ");
            string inputYear = Console.ReadLine();

            var filtered = holidayCache.Where(h => h.date.StartsWith(inputYear)).ToList();

            if (filtered.Count > 0)
            {
                PrintTable(filtered);
            }
            else
            {
                Console.WriteLine("Bu yıla ait veri bulunamadı.");
            }
            Wait();
        }

        static void SearchByDate()
        {
            Console.Write("Tarih giriniz (gg-aa): ");
            string inputDate = Console.ReadLine();

            var filtered = holidayCache.Where(h =>
            {
                DateTime dt;
                if (DateTime.TryParse(h.date, out dt))
                {
                    return dt.ToString("dd-MM") == inputDate;
                }
                return false;
            }).ToList();

            if (filtered.Count > 0)
            {
                PrintTable(filtered);
            }
            else
            {
                Console.WriteLine("Belirtilen tarihte bir tatil bulunamadı.");
            }
            Wait();
        }

        static void SearchByName()
        {
            Console.Write("Tatil adı giriniz: ");
            string keyword = Console.ReadLine().ToLower();

            var filtered = holidayCache.Where(h =>
                h.localName.ToLower().Contains(keyword) ||
                h.name.ToLower().Contains(keyword)
            ).ToList();

            if (filtered.Count > 0)
            {
                PrintTable(filtered);
            }
            else
            {
                Console.WriteLine("Bu isme uygun tatil bulunamadı.");
            }
            Wait();
        }

        static void ShowAllHolidays()
        {
            if (holidayCache.Count > 0)
            {
                PrintTable(holidayCache);
            }
            else
            {
                Console.WriteLine("Veri bulunamadı.");
            }
            Wait();
        }

        static void PrintTable(List<Holiday> holidays)
        {
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("{0,-12} {1,-35} {2,-30}", "Tarih", "Yerel Ad", "Uluslararası Ad");
            Console.WriteLine("--------------------------------------------------------------------------------");

            foreach (var h in holidays)
            {
                Console.WriteLine("{0,-12} {1,-35} {2,-30}", h.date, h.localName, h.name);
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        static void Wait()
        {
            Console.WriteLine("\nAna menüye dönmek için bir tuşa basınız...");
            Console.ReadKey();
        }
    }
}