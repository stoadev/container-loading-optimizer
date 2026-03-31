# Faz 2 — Yerleşim Algoritması (C#)

## Görev
Konteynıra ürün yerleştiren algoritmayı yaz. Açıklama/yorum yazma, sadece kod.

## Klasör
```
Assets/Scripts/Algorithm/
├── ContainerPacker.cs
├── PackingResult.cs
```

## Mevcut Kod (referans, tekrar yazma)
- `ContainerData`, `ProductData`, `DeliveryOrder`, `PlacementResult` → `ContainerLoading.Models` namespace'inde zaten var.

## Namespace
`ContainerLoading.Algorithm`

## ContainerPacker.cs — Static class

### Public method:
```csharp
public static PackingResult Pack(ContainerData container, List<DeliveryOrder> orders)
```

### Algoritma 3 adımlı:

**Adım 1 — LIFO sıralama:**
- `orders` listesini `deliverySequence`'e göre AZALAN sırala (en son teslim = ilk yüklenir = en arkaya)
- Kapı tarafına göre "arka" yönü belirle:
  - `SingleFront` → derinlik ekseni pozitif yönde arkaya doğru doldur
  - `SingleBack` → derinlik ekseni negatif yönde öne doğru doldur
  - `Double` → ortadan başla, ilk teslimler her iki uca yakın

**Adım 2 — Ağırlık düzenleme:**
- Aynı derinlik katmanındaki ürünleri ağırlığa göre sırala
- Ağır olan tabana (y=0), hafif olan üste (stackable ise)
- Üste koyma kuralı: `canBeStackedOn == true` VE üstteki ürün ağırlığı ≤ `maxStackWeight`

**Adım 3 — Pozisyon hesaplama:**
- Konteynır origin: (0, 0, 0) = sol alt arka köşe (kapı önden ise arka duvar)
- Her ürün için `Vector3 position` hesapla (ürünün pivot'u alt merkez)
- Padding: her yönde `container.padding` kadar boşluk bırak (ürünler arası ve duvardan)
- Aynı derinlik katmanında yan yana sığıyorsa (`toplam genişlik + padding ≤ container.width`) yan yana koy
- Sığmıyorsa yeni derinlik katmanı aç (z ekseninde ilerle)
- Y ekseninde istifleme: alttaki ürünün `canBeStackedOn` ve `maxStackWeight` kontrolü
- Rotasyon: `RotationConstraint.YAxisOnly` olan ürünleri 0° ve 90° dene, daha iyi sığanı seç
- Sığmayan ürün varsa: `isValid = false`, `invalidReason = "Konteynıra sığmıyor"` yaz

## PackingResult.cs

```csharp
[System.Serializable]
public class PackingResult
{
    public List<PlacementResult> placements;
    public bool allItemsFit;
    public float usedVolumePercentage;
    public float totalWeight;
}
```

## Kurallar
- MonoBehaviour kullanma
- using UnityEngine; (Vector3, Quaternion için)
- Sadece bu 2 dosyayı yaz
- Double kapı tipi için basit yaklaşım yeterli (tam optimizasyon gerekmez)
- `RotationConstraint.None` olan ürün hiç döndürülmez, `Free` olan 4 yön denenebilir
