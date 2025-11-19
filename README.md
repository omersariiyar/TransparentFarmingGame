🚜 Unity 2D Basit Çiftçilik Oyunu (Tohum, Bekle, Biç)

Youtube => https://youtu.be/OXG1uBhsEW0

Bu proje, Unity 2D ortamında geliştirilmiş, oyuncunun tohum ekme, bitki büyümesini bekleme ve hasat etme gibi temel çiftçilik döngüsünü deneyimlediği minimalist bir oyundur.

🔑 Temel Mekanikler

Tohum Seçimi: Envanterden (Canvas üzerindeki butonlar aracılığıyla) Seed veya Carrot tohumları seçilebilir.

Ekim: Seçili tohum ile boş tarla alanına tıklanarak ekim yapılır. Tohum envanterden düşülür.

Büyüme: Ekilen bitki, belirlenen süre (timeToGrow) boyunca aşamalı olarak büyür ve görseli değişir.

Hasat: Bitki olgunlaştığında (Harvestable), tarlaya tıklandığında ürün toplanır. Hasat, envantere rastgele miktarda (1-2 adet) ek tohum düşürür.

Kayıt Sistemi: Envanter durumu ve tarlaların ekili/büyüme durumu (PlayerPrefs) kullanılarak kaydedilir ve yüklenir.

⚙️ Kurulum ve Çalıştırma

TransparentGame RedStains.unitypackage dosyasını yükleyip projeniz açıkken dosyayı çalışıtırıp import etmeniz yeterli olucaktır.

📌 Kritik Ayar: Tarla ID'leri

Her bir FarmPlot objesinin Inspector penceresindeki Plot ID değişkeni, verilerin doğru kaydedilmesi için benzersiz bir tamsayı (1, 2, 3, vb.) olarak ayarlanmalıdır.

📂 Script Yapısı

InventorySystem.cs	Envanter, tohum/ürün sayımı, büyüme takibi, UI güncellemeleri ve PlayerPrefs kaydı/yüklemesi.
FarmPlot.cs	Tarlanın durumu (Boş, Büyüyor, Hasat Edilebilir), büyüme zamanlayıcısı, ekim/hasat mantığı ve tarlanın durum kaydı/yüklemesi.
InputManager.cs	Kullanıcının fare tıklamalarını algılayıp, tıklanan tarla objesinin Interact() metodunu çağırma.
