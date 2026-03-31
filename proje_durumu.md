# Konteynır Yükleme Optimizasyonu — Proje Durumu

## Proje Özeti
Unity 3D ile beyaz eşya ürünlerini konteynıra teslimat sırasına göre (LIFO) yerleştiren 3D görselleştirme sistemi.

## Ekip
- **Manager**: Claude Opus (claude.ai) — mimari, algoritma kararları, review
- **Coder**: Claude Sonnet 4.6 (VS Code) — implementasyon
- **Kurallar**: CLAUDE.md dosyasında tanımlı

## Tamamlanan Fazlar

### Faz 1 — Veri Modeli ✅
**Dosyalar:** `Assets/Scripts/Models/`
- `Enums.cs` — DoorType (SingleFront, SingleBack, Double), RotationConstraint (None, YAxisOnly, Free)
- `ContainerData.cs` — Konteynır boyutları, kapı tipi, padding
- `ProductData.cs` — Ürün boyutları, ağırlık, stacking kuralları, gizmo rengi
- `DeliveryOrder.cs` — Ürün + teslimat sırası + rota
- `PlacementResult.cs` — Pozisyon, rotasyon, loadOrder, isValid, invalidReason
- `ProductDatabase.cs` — 4 sabit ürün (buzdolabı, çamaşır, bulaşık, fırın)

**Sabit Ürün Verileri:**
| Ürün | Boyut (m) | Ağırlık | Döndürme | Üstüne Yük | Max Yük |
|------|-----------|---------|----------|------------|---------|
| Buzdolabı | 0.70×1.85×0.70 | 75kg | YAxisOnly | Hayır | 0 |
| Çamaşır M. | 0.60×0.85×0.60 | 70kg | YAxisOnly | Evet | 80kg |
| Bulaşık M. | 0.60×0.85×0.60 | 50kg | YAxisOnly | Evet | 60kg |
| Fırın | 0.60×0.90×0.60 | 45kg | YAxisOnly | Evet | 50kg |

### Faz 2 — Yerleşim Algoritması ✅
**Dosyalar:** `Assets/Scripts/Algorithm/`
- `ContainerPacker.cs` — Static class, 3 adımlı algoritma
- `PackingResult.cs` — Sonuç verisi (placements, allItemsFit, usedVolumePercentage, totalWeight)

**Algoritma mantığı:**
1. **LIFO sıralama**: deliverySequence azalan sırala (son teslim ilk yüklenir)
2. **Katman ayırma**: Farklı deliverySequence mutlaka farklı z katmanında (genişlikte yer olsa bile)
3. **Ağırlık**: Aynı katmanda ağır alta, hafif üste (canBeStackedOn + maxStackWeight kontrolü)
4. **Pozisyon hesaplama**: Padding dahil, rotasyon denemeleri (YAxisOnly: 0°/90°)
5. **Double kapı**: Listeyi ikiye böl, yarısı öne yarısı arkaya

**Öncelik sırası:** LIFO > Ağırlık > Boyut optimizasyonu

### Faz 3+4 — Unity Sahne + Demo ✅
**Dosyalar:** `Assets/Scripts/Core/`
- `ContainerManager.cs` — Konteynır görseli oluşturur, Pack butonunu yönetir
- `ProductSpawner.cs` — Ürün objelerini spawn/temizle
- `CameraController.cs` — Orbit, zoom, pan + dikey/yatay görünüm butonları

**Sahne:** `ContainerScene`
- GameManager objesi (ContainerManager + ProductSpawner)
- UI Canvas: Pack butonu, Dikey/Yatay kamera butonları
- Yarı saydam konteynır + renkli ürün kutuları
- Kamera: sağ tık orbit, scroll zoom, orta tık pan

## Kalan Fazlar

### Faz 5 — UI + Animasyon (yapılmadı)
- Inspector yerine runtime UI ile sipariş girişi
- Yükleme animasyonu (ürünler tek tek yerleşsin)
- Konteynır bilgileri ekranda gösterilsin

### Faz 6 — Test + Optimizasyon (yapılmadı)
- Farklı konteynır boyutları test
- Farklı sipariş kombinasyonları
- Edge case'ler (sığmayan ürün, boş sipariş)
- Performans

## Bilinen Sorunlar
- Ürün isimleri 3D'de görünmüyor (eklenmedi)
- Zemin/çevre görseli yok (kozmetik)
- Inspector'dan manuel veri girişi gerekiyor (runtime UI yok henüz)

## Dosya Yapısı
```
Assets/Scripts/
├── Models/
│   ├── Enums.cs
│   ├── ContainerData.cs
│   ├── ProductData.cs
│   ├── DeliveryOrder.cs
│   ├── PlacementResult.cs
│   └── ProductDatabase.cs
├── Algorithm/
│   ├── ContainerPacker.cs
│   └── PackingResult.cs
└── Core/
    ├── ContainerManager.cs
    ├── ProductSpawner.cs
    └── CameraController.cs
```
