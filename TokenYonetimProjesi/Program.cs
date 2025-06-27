using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TokenYonetimProjesi.Services;


public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var siparisServisi = host.Services.GetRequiredService<SiparisServisi>();

        Console.WriteLine("Token Yönetim Simülasyonu Başladı.");
        Console.WriteLine("Her 10 saniyede bir sipariş listesi çekilecek (Normalde 5 dakika).");
        Console.WriteLine("Token'ın ömrü 1 saattir, ancak saatte 5'ten fazla istek atılamaz.");
        Console.WriteLine("------------------------------------------------------------------");

        // Her 5 dakikada bir çalışan işi simüle etmek için sonsuz döngü.
        // Demo için süreyi 10 saniyeye düşürdük.
        while (true)
        {
            await siparisServisi.SiparisleriGetirAsync();
            await Task.Delay(TimeSpan.FromSeconds(10)); // Normalde: TimeSpan.FromMinutes(5)
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // HttpClient'ı kaydediyoruz.
                services.AddHttpClient();

                // TokenManagerService'i Singleton olarak kaydediyoruz.
                // Bu çok önemli! Çünkü uygulamanın yaşam döngüsü boyunca SADECE BİR tane
                // token yöneticisi olmalı ki token'ı ve son kullanma tarihini doğru yönetebilsin.
                services.AddSingleton<ITokenManagerService, TokenManagerService>();

                // Sipariş servisini de kaydediyoruz.
                services.AddTransient<SiparisServisi>();
            });
}
