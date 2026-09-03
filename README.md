
# .NET CORE MVC BLOG SİTESİ

Bu proje .Net core mvc ,next.js,tailwind,html,css,EF Core,MSSQL kullanılarak yazılmıştır.
Çok katmanlı mimari ve generic repository,unit of work design patternları clean code prensipleri kullanarak ölçeklenebilir,yönetilebilir kodlar yazılmıştır.


Canlı link : https://blog.ahmetkar.com


## Kullanılan Teknolojiler

- .NET CORE MVC ve WEB API
- AutoMapper
- Identity Service
- Entity Framework CORE
- MSSQL - Microsoft Azure SQL
- Fluent Validation
- Next.js
- HTML-CSS
- Tailwind
- Memcached
- Caddy (Httpsden backend api containerına Reverse proxy için,https/tls yapılandırmaları için)
- Docker Swarm
- Google Cloud Storage
- Google Cloud Compute Engine
- Vercel

## Kullanılan Mimari ve Design Pattern'lar

- Çok Katmanlı Mimari
- Unit Of Work Design Pattern
- Dependency Injection
- Generic Repository Design Pattern
- Clean Architecture
- Memcached ile Cache Aside/Lazy Loading Design Pattern
- Memcached ile Cache Invalidation Pattern

## Özellikler

- Admin paneli ile makale,yorum,kategori yönetimi
- Admin paneli ile resimlerin yönetimi
- Admin paneli ile kullanıcıların yönetimi
- Admin,editör,kullanıcı rolleriyle farklı yetkilerle sitenin yönetimi
- Admin paneli aracılığıyla anasayfa,hakkımızda,iletişim sayfalarının dinamik düzenlenmesi
- Her eklenen makalenin önceki makaleler ile makine öğrenmesi aracılığıyla benzerliğinin hesaplanıp makale görüntüleme sayfasında benzer makalelerin tavsiye edilmesi
- Yorum yapılması
- Arama özelliği
- Admin panelinden silinen makale,medya,kategori gibi verilerin silinmeyip çöp kutusuna gönderilmesi ve istenirse tamamen silinmesi
- Tailwind ve nextjs kullanan responsive ve modern,mobil uyumlu tasarım
- E-mail abonelik sistemi,Abonelerin yönetimi
- Memcache ile caching
- Google storage ile resimlerin depolanması
- Google Compute Engine ile .net backendin,azure sql ile sql veritabanının ve vercel ile next.js frontendin deploy edilmesi
- İleri Seviye SEO Desteği

# Memcached Altyapısı Açıklanması

- Memcached ile blogların veritabanından çekilirken eğer memcached de varsa oradan alınıp gönderilmesi yoksa veritabanından çekilip memcached e kaydedilip öyle gönderilmesi sağlanmıştır. Bu Lazy Loading pattern olarak bilinmektedir.
- Memcached ile bağlantılı cachelerin aynı etikete sahip cache listesinde tutulması sağlanmış. Eğer örneğin article silinirse,güncellenirse,yenisi eklenirse bu cache listesindeki tüm cache başlıklarına karşılık gelen verilerin temizlenmesi sağlanmıştır.
- Eğer cache listesindeki sadece berirli cache başlıkları etkileniyorsa onların özellikle verilerek o cache listesinden silinebilmesi sağlanmıştır.
- Normalde rediste cache tag lar aracılığıyla bu özellik gömülü olarak desteklenir ama ben bunu memcached de cache listesi ni json olarak ayrı bir cache başlığı ile tutarak yaptım.


## Siteden Ekran Görüntüleri

![Site Resim 1](ekrangoruntuleri/Screenshot%202026-08-19%20010607.png)

![Site Resim 2](ekrangoruntuleri/Screenshot%202026-08-19%20010620.png)

![Site Resim 3](ekrangoruntuleri/Screenshot%202026-08-19%20010644.png)

![Site Resim 4](ekrangoruntuleri/Screenshot%202026-08-19%20010706.png)

![Site Resim 5](ekrangoruntuleri/Screenshot%202026-08-19%20010738.png)

![Site Resim 6](ekrangoruntuleri/Screenshot%202026-08-19%20010752.png)

![Site Resim 7](ekrangoruntuleri/Screenshot%202026-08-19%20010810.png)



## Admin Panelinden Ekran Görüntüleri

![Site Resim 1](ekrangoruntuleri/Screenshot%202026-08-19%20010836.png)

![Site Resim 2](ekrangoruntuleri/Screenshot%202026-08-19%20010852.png)

![Site Resim 3](ekrangoruntuleri/Screenshot%202026-08-19%20010906.png)

![Site Resim 4](ekrangoruntuleri/Screenshot%202026-08-19%20010924.png)






