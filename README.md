# PublicHolidayTracker

Bu proje, **Piri Reis Üniversitesi - Görsel Programlama** dersi vize ödevi kapsamýnda geliþtirilmiþ bir C# Konsol uygulamasýdýr.

Uygulama, Nager.Date API servisini kullanarak Türkiye'deki resmi tatil verilerini (2023, 2024 ve 2025 yýllarý için) çeker, iþler ve kullanýcýya sorgulama imkaný sunar.

## Özellikler

Uygulama baþlatýldýðýnda API üzerinden veriler otomatik olarak hafýzaya alýnýr ve aþaðýdaki iþlemleri gerçekleþtirebilir:

* **Yýla Göre Listeleme:** Kullanýcýnýn seçtiði yýla (2023-2025) ait tüm tatilleri listeler.
* **Tarihe Göre Arama:** Gün ve Ay (gg-aa) formatýnda girilen tarihte bir tatil olup olmadýðýný kontrol eder.
* **Ýsme Göre Arama:** Tatil ismine (Örn: "Cumhuriyet") göre arama yapar.
* **Tam Liste:** Desteklenen 3 yýlýn tüm tatillerini tek seferde ekrana döker.
* **Dinamik Veri Çekimi:** `HttpClient` kullanýlarak veriler JSON formatýnda canlý olarak çekilir.

## Teknik Detaylar

Proje **.NET** platformu üzerinde **C#** dili ile geliþtirilmiþtir.

* **Veri Kaynaðý:** [Nager.Date API](https://date.nager.at/Api)
* **JSON Ýþlemleri:** `System.Text.Json` kütüphanesi kullanýlarak API'den gelen veriler nesneye dönüþtürülmüþtür.
* **Veri Modeli:** API verileri aþaðýdaki sýnýf yapýsýna uygun olarak iþlenmektedir:

```csharp
class Holiday
{
    public string date { get; set; }
    public string localName { get; set; }
    public string name { get; set; }
    public string countryCode { get; set; }
    public bool @fixed { get; set; } // C# keyword çakýþmasý için @ kullanýldý
    public bool global { get; set; }
}

Menü Yapýsý

===== PublicHolidayTracker =====
1. Tatil listesini göster (yýl seçmeli)
2. Tarihe göre tatil ara (gg-aa formatý)
3. Ýsme göre tatil ara
4. Tüm tatilleri 3 yýl boyunca göster (2023–2025)
5. Çýkýþ
Seçiminiz:

Geliþtirici: Gökçen Çiftci Okul No: 20230108016 Ders: Görsel Programlama Teslim Tarihi: 30.11.2025