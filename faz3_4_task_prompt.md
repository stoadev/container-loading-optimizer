# Faz 3+4 — Unity Sahne + Çalışan Demo

## Görev
Unity sahnesinde konteynır ve ürünleri 3D göster, butona basınca algoritma çalışıp ürünleri yerleştirsin. Açıklama/yorum yazma, sadece kod.

## Oluşturulacak dosyalar
```
Assets/Scripts/
├── Core/ContainerManager.cs      (MonoBehaviour — sahne yöneticisi)
├── Core/ProductSpawner.cs         (MonoBehaviour — ürün objeleri oluşturur)
```

## ContainerManager.cs — MonoBehaviour, sahneye boş GameObject'e ekle

### Inspector'da görünecek alanlar:
- `ContainerData containerData` (Inspector'dan boyut ve kapı tipi ayarlanacak)
- `List<DeliveryOrder> deliveryOrders` (Inspector'dan sıra atanacak)
- `Button packButton` (UI butonu referansı)

### Davranış:
- Start()'ta konteynır kutusunu oluştur:
  - `GameObject.CreatePrimitive(PrimitiveType.Cube)` ile
  - Scale = containerData boyutları
  - Yarı saydam materyal (alpha 0.2, beyaz renk)
  - Position = (width/2, height/2, depth/2) — origin sol alt arka köşe olsun
  - Collider kaldır (sadece görsel)
- packButton.onClick'e Pack metodunu bağla
- Pack() çağırıldığında:
  - `ContainerPacker.Pack()` çağır
  - Sonucu `ProductSpawner.SpawnProducts()` ile göster

## ProductSpawner.cs — MonoBehaviour

### Public method:
```csharp
public void SpawnProducts(PackingResult result)
```

### Davranış:
- Önceki spawn edilmiş objeleri temizle
- Her PlacementResult için:
  - `isValid == false` ise atla
  - `GameObject.CreatePrimitive(PrimitiveType.Cube)` oluştur
  - Scale = ürünün width, height, depth
  - Position = placement.position
  - Rotation = placement.rotation
  - Materyal rengi = product.gizmoColor
  - Obje ismi = product.productName

## Sahne Kurulumu (elle yapılacak, kod değil — Salih'e not)
1. Yeni sahne: "ContainerScene"
2. Boş GameObject "GameManager" — ContainerManager + ProductSpawner ekle
3. UI Canvas → Button "Pack" — ContainerManager'ın packButton'una sürükle
4. Inspector'dan ContainerData doldur (örnek: 6.1, 2.6, 2.4 — 20ft konteynır)
5. Inspector'dan 4 DeliveryOrder ekle, her birine ProductDatabase'den ürün ata, deliverySequence ver
6. Kamera: position (3, 3, -5), rotation (30, -20, 0) — konteynırı görsün

## Kurallar
- using ContainerLoading.Models;
- using ContainerLoading.Algorithm;
- Sadece bu 2 dosyayı yaz
- Basit tutun: shader/materyal dosyası oluşturma, runtime'da `new Material(Shader.Find("Standard"))` kullan
- Alpha için: `material.SetFloat("_Mode", 3); material.color = new Color(1,1,1,0.2f);` + renderQueue ayarla
