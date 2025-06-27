using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenYonetimProjesi.Services
{
    public class SiparisServisi
    {
        private readonly ITokenManagerService _tokenManager;
        private readonly HttpClient _httpClient;

        public SiparisServisi(ITokenManagerService tokenManager, HttpClient httpClient)
        {
            _tokenManager = tokenManager;
            _httpClient = httpClient;
        }

        public async Task SiparisleriGetirAsync()
        {
            Console.WriteLine($"\n[{DateTime.Now:T}] Sipariş listesi çekme işlemi başlatıldı...");

            //  Geçerli bir token al
            var token = await _tokenManager.GetValidTokenAsync();

            // Alınan token ile asıl isteği yap
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //var response = await httpClient.GetAsync("https://api.example.com/orders");
            // ...

            Console.WriteLine($"[{DateTime.Now:T}] Siparişler başarıyla çekildi. Kullanılan Token: ...{token.Substring(token.Length - 5)}");
        }
    }
}
