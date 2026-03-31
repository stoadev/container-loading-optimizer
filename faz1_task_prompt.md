# Faz 1 — Veri Modeli (C# Sınıfları)

## Proje Bağlamı
Unity 3D ile konteynır yükleme optimizasyonu yapıyoruz. Beyaz eşya ürünleri (buzdolabı, çamaşır makinesi, bulaşık makinesi, fırın) bir konteynıra teslimat sırasına göre LIFO mantığıyla yerleştirilecek. Bu görevde sadece veri modelini oluşturuyoruz — henüz algoritma veya Unity sahnesi yok.

## Klasör Yapısı
```
Assets/Scripts/Models/
├── ContainerData.cs
├── ProductData.cs
├── DeliveryOrder.cs
├── PlacementResult.cs
└── Enums.cs
```

## Sınıf Tanımları

### Enums.cs
```csharp
// DoorType: SingleFront, SingleBack, Double
// RotationConstraint: None, YAxisOnly, Free
```

### ContainerData.cs
- Düz C# class (MonoBehaviour DEĞİL)
- `[System.Serializable]` olsun (Inspector'da görünsün diye)
- Alanlar:
  - `string containerName`
  - `float width` (metre cinsinden)
  - `float height` (metre cinsinden)  
  - `float depth` (metre cinsinden, kapıdan arka duvara)
  - `DoorType doorType`
  - `float padding = 0.05f` (ürünler arası minimum boşluk, metre)

### ProductData.cs
- Düz C# class, `[System.Serializable]`
- Alanlar:
  - `string productName`
  - `float width, height, depth` (metre)
  - `float weight` (kg)
  - `RotationConstraint rotationConstraint`
  - `bool canBeStackedOn` (bu ürünün ÜSTÜNE başka ürün konabilir mi)
  - `float maxStackWeight` (üstüne konabilecek max ağırlık, kg)
  - `Color gizmoColor` (Unity'de 3D görselleştirmede ayırt etmek için)

### DeliveryOrder.cs
- Düz C# class, `[System.Serializable]`
- Alanlar:
  - `ProductData product`
  - `int deliverySequence` (1 = ilk teslim edilecek)
  - `string routeId`
  - `string destination`

### PlacementResult.cs
- Düz C# class
- Alanlar:
  - `ProductData product`
  - `Vector3 position` (konteynır içindeki konum)
  - `Quaternion rotation`
  - `int loadOrder` (konteynıra yüklenme sırası, 1 = ilk yüklenen = en arkadaki)
  - `bool isValid` (yerleşim kurallarına uygun mu)
  - `string invalidReason` (uygun değilse neden)

## Ek: Sabit Ürün Verileri
Bir static helper class yaz: `ProductDatabase.cs`
4 ürünün varsayılan değerlerini dönsün:

| Ürün | W | H | D | Ağırlık | Rotation | CanStack | MaxStack | Renk |
|------|---|---|---|---------|----------|----------|----------|------|
| Buzdolabı | 0.70 | 1.85 | 0.70 | 75 | YAxisOnly | false | 0 | Mavi |
| Çamaşır M. | 0.60 | 0.85 | 0.60 | 70 | YAxisOnly | true | 80 | Yeşil |
| Bulaşık M. | 0.60 | 0.85 | 0.60 | 50 | YAxisOnly | true | 60 | Mor |
| Fırın | 0.60 | 0.90 | 0.60 | 45 | YAxisOnly | true | 50 | Turuncu |

## Kurallar
- Namespace: `ContainerLoading.Models`
- using UnityEngine; (Vector3, Quaternion, Color için)
- Hiçbir sınıf MonoBehaviour'dan türemesin
- Hepsinde `[System.Serializable]` olsun
- Public alanlar kullan (Inspector uyumu için)
- Henüz hiçbir iş mantığı (algoritma) yazma — sadece veri yapıları
