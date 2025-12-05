# PublicHolidayTracker

Bu proje, **Piri Reis Üniversitesi - Görsel Programlama** dersi vize ödevi kapsamında geliştirilmiş bir C# Konsol uygulamasıdır.

Uygulama, Nager.Date API servisini kullanarak Türkiye'deki resmi tatil verilerini (2023, 2024 ve 2025 yılları için) çeker, işler ve kullanıcıya sorgulama imkanı sunar.

## Özellikler

Uygulama başlatıldığında API üzerinden veriler otomatik olarak hafızaya alınır ve aşağıdaki işlemleri gerçekleştirebilir:

* **Yıla Göre Listeleme:** Kullanıcının seçtiği yıla (2023-2025) ait tüm tatilleri listeler.
* **Tarihe Göre Arama:** Gün ve Ay (gg-aa) formatında girilen tarihte bir tatil olup olmadığını kontrol eder.
* **İsme Göre Arama:** Tatil ismine (Örn: "Cumhuriyet") göre arama yapar.
* **Tam Liste:** Desteklenen 3 yılın tüm tatillerini tek seferde ekrana döker.
* **Dinamik Veri Çekimi:** `HttpClient` kullanılarak veriler JSON formatında canlı olarak çekilir.

## Teknik Detaylar

Proje **.NET** platformu üzerinde **C#** dili ile geliştirilmiştir.

* **Veri Kaynağı:** [Nager.Date API](https://date.nager.at/Api)
* **JSON İşlemleri:** `System.Text.Json` kütüphanesi kullanılarak API'den gelen veriler nesneye dönüştürülmüştür.
