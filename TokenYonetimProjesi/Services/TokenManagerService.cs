using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TokenYonetimProjesi.Models;

namespace TokenYonetimProjesi.Services
{
    public class TokenManagerService : ITokenManagerService
    {
        private readonly HttpClient _httpClient;
        private string? _cachedToken;
        private DateTime _tokenExpirationTime;
        private static readonly object _tokenLock = new object();

        public TokenManagerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _tokenExpirationTime = DateTime.MinValue; 
        }

        public async Task<string> GetValidTokenAsync()
        {
            lock (_tokenLock)
            {
                if (!string.IsNullOrEmpty(_cachedToken) && _tokenExpirationTime > DateTime.UtcNow)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[{DateTime.Now:T}] Önbellekteki token kullanılıyor. Kalan Süre: {(_tokenExpirationTime - DateTime.UtcNow).TotalSeconds:N0} saniye.");
                    Console.ResetColor();
                    return _cachedToken;
                }
            }
            return await RefreshTokenAsync();
        }
        private async Task<string> RefreshTokenAsync()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{DateTime.Now:T}] Token süresi doldu veya yok. Yeni token isteniyor...");
            Console.ResetColor();

            var fakeApiResponse = new TokenResponse
            {
                AccessToken = Guid.NewGuid().ToString(), 
                ExpiresIn = 3600, 
                TokenType = "Bearer"
            };

            await Task.Delay(500);

            lock (_tokenLock)
            {
                _cachedToken = fakeApiResponse.AccessToken;
                _tokenExpirationTime = DateTime.UtcNow.AddSeconds(fakeApiResponse.ExpiresIn - 60);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[{DateTime.Now:T}] Yeni token alındı. Son Kullanma Zamanı: {_tokenExpirationTime:T}");
            Console.ResetColor();

            return _cachedToken;
        }
    }
}
