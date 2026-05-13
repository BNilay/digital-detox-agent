# Digital Detox Agent - Kurulum Rehberi

Bu proje, kullanıcıların günlük ekran süresi bilgilerini analiz ederek dijital detoks önerileri sunan ASP.NET Core MVC tabanlı bir web uygulamasıdır.

## Gereksinimler

Projeyi çalıştırmak için bilgisayarda aşağıdakiler kurulu olmalıdır:

- .NET SDK
- Git
- Visual Studio veya Visual Studio Code

  Kurulum
 
-Projeyi GitHub üzerinden indirin:

git clone https://github.com/BNilay/digital-detox-agent.git

-Proje klasörüne girin:

cd digital-detox-agent

-Gerekli paketleri yükleyin:

dotnet restore

-Veritabanını oluşturun:

dotnet ef database update

-Projeyi çalıştırın:

dotnet run
