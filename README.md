# .NET ile Verimli Token Yönetimi Projesi

Bu proje, saatlik istek sınırı (rate limit) bulunan bir API için token yönetiminin nasıl verimli bir şekilde yapılacağını gösteren bir .NET Console uygulamasıdır.

## Senaryo

Bir servis, her 5 dakikada bir REST API üzerinden sipariş listesi çekmektedir. Ancak bu API'ye istek atmadan önce bir `access_token` alınması gerekmektedir. Token veren endpoint'in ise saatte 5 istek gibi katı bir limiti bulunmaktadır.

Bu proje, bu kısıtlamayı aşmak için token'ı akıllıca önbelleğe alan (caching) ve sadece gerektiğinde yenileyen bir strateji uygular.

## Kullanılan Strateji

- **Önbelleğe Alma (Caching):** Geçerli bir token alındığında, token bilgisi ve son kullanma tarihi (expires_in) bellekte saklanır.
- **Geçerlilik Kontrolü:** Her API isteğinden önce, saklanan token'ın süresinin dolup dolmadığı kontrol edilir.
- **Koşullu Yenileme:** Token'ın süresi hala geçerliyse, önbellekteki token kullanılır. Eğer süresi dolmuşsa veya dolmaya yakınsa, **sadece o zaman** yeni bir token için API'ye istek atılır. Bu sayede API'ye yapılan çağrılar minimumda tutulur.

## Projeyi Çalıştırma

### Ön Gereksinimler

* [.NET 8 SDK]

### Kurulum Adımları

1.  **Projeyi Klonlayın:**
    ```bash
    git clone <BU_PROJENIN_GITHUB_URL_ADRESI>
    ```

2.  **Proje Dizinine Gidin:**
    ```bash
    cd TokenYonetimProjesi
    ```

3.  **Uygulamayı Çalıştırın:**
    Projeyi başlatmak için aşağıdaki komutu kullanın.
    ```bash
    dotnet run
    ```

Uygulama çalıştığında, konsol ekranında her 10 saniyede bir siparişlerin çekildiğini ve token'ın yalnızca gerektiğinde (ilk çalıştırmada ve süresi dolduğunda) yenilendiğini göreceksiniz.
