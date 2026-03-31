# 📦 Container Loading Optimizer — Unity 3D

Beyaz eşya lojistiği için akıllı konteynır yükleme sistemi. Teslimat rotasına göre ürünleri otomatik yerleştirir, 3D olarak görselleştirir.

---

## Ne Yapıyor?

Bir konteynıra yüklenecek ürünleri **teslimat sırasına göre optimize ederek** yerleştirir. İlk teslim edilecek ürün kapıya en yakın durur — böylece teslimat noktasında diğer ürünleri sökmeden direkt erişirsiniz.

Gerçek lojistik dünyasındaki LIFO (Last In, First Out) prensibini uygular.

---

## Özellikler

### Akıllı Yerleşim Algoritması
- **LIFO optimizasyonu** — teslimat sırası ters yükleme sırasını belirler, her teslimat grubu kendi derinlik katmanında durur
- **3D bin packing** — ürünleri konteynır içine sığdırırken alan kullanımını optimize eder
- **Ağırlık dengesi** — ağır ürünler tabanda, hafif ürünler üstte konumlanır
- **İstifleme kontrolü** — her ürünün üstüne yük binip binemeyeceği ve maksimum taşıma kapasitesi kontrol edilir
- **Rotasyon denemesi** — ürünler 0° ve 90° döndürülerek en iyi sığma pozisyonu bulunur
- **Çift kapı desteği** — tek ön, tek arka veya çift kapılı konteynırlar için ayrı stratejiler

### Endüstri Standartları
- Buzdolabı yan yatırılamaz (kompresör koruması)
- Ürünler arası minimum 5cm güvenlik boşluğu
- Ağır eşya her zaman tabanda
- Taşıma kapasitesi aşılamaz

### Esnek Konteynır Sistemi
- Farklı konteynır boyutları desteklenir (20ft, 40ft veya özel ölçü)
- Kapı tipi seçilebilir: ön, arka veya çift kapı
- Tüm parametreler runtime'da değiştirilebilir

### 3D Görselleştirme
- Yarı saydam konteynır içinde renkli ürün kutuları
- Her ürün kendi rengiyle ayırt edilir
- Tek butonla yerleşimi çalıştır, anında sonucu gör

### Kamera Kontrolleri
- **Orbit** — sağ tık ile konteynır etrafında serbestçe dön
- **Zoom** — scroll ile yakınlaş / uzaklaş
- **Pan** — orta tık ile kaydır
- **Dikey görünüm** — tek tıkla üstten kuş bakışı
- **Yatay görünüm** — tek tıkla yandan kesit görünümü

### Sabit Ürün Kütüphanesi
| Ürün | Boyut | Ağırlık | İstiflenebilir |
|------|-------|---------|----------------|
| Buzdolabı | 70×185×70 cm | 75 kg | Üstüne yük konamaz |
| Çamaşır Makinesi | 60×85×60 cm | 70 kg | Max 80 kg |
| Bulaşık Makinesi | 60×85×60 cm | 50 kg | Max 60 kg |
| Fırın | 60×90×60 cm | 45 kg | Max 50 kg |

---

## Teknik Yapı

```
Assets/Scripts/
├── Models/         → Veri katmanı (6 sınıf)
├── Algorithm/      → Yerleşim motoru (bin packing + LIFO)
└── Core/           → Unity entegrasyonu (sahne, spawn, kamera)
```

- **Temiz mimari** — veri, algoritma ve görselleştirme katmanları birbirinden bağımsız
- **Saf C# algoritma** — MonoBehaviour'a bağımlı değil, unit test edilebilir
- **Serializable veri modeli** — Unity Inspector'dan doğrudan düzenlenebilir

---

## Kullanım Alanları

- Beyaz eşya ve mobilya lojistiği
- Kargo ve nakliye firmaları
- Depo yönetimi ve sevkiyat planlama
- Lojistik eğitim simülasyonları

---

## Geliştirme Yol Haritası

- [ ] Runtime UI ile sipariş girişi (Inspector'a gerek kalmadan)
- [ ] Yükleme animasyonu (ürünler tek tek yerleşsin)
- [ ] Ürün isimlerinin 3D olarak gösterilmesi
- [ ] Farklı ürün tipleri ekleme (koltuk, masa, TV vb.)
- [ ] JSON/CSV'den sipariş yükleme
- [ ] Raporlama (alan kullanımı, ağırlık dağılımı)

---

*Unity 2022+ | C# | 3D Bin Packing | LIFO Optimization*
