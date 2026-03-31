using System.Collections.Generic;
using UnityEngine;
using ContainerLoading.Models;

namespace ContainerLoading.Algorithm
{
    public static class ContainerPacker
    {
        public static PackingResult Pack(ContainerData container, List<DeliveryOrder> orders)
        {
            var result = new PackingResult
            {
                placements = new List<PlacementResult>(),
                allItemsFit = true,
                totalWeight = 0f
            };

            var sorted = new List<DeliveryOrder>(orders);
            sorted.Sort((a, b) =>
            {
                int cmp = b.deliverySequence.CompareTo(a.deliverySequence);
                return cmp != 0 ? cmp : b.product.weight.CompareTo(a.product.weight);
            });

            float containerVolume = container.width * container.height * container.depth;
            float usedVolume = 0f;

            if (container.doorType == DoorType.Double)
            {
                int mid = sorted.Count / 2;
                var frontHalf = sorted.GetRange(0, mid);
                var backHalf = sorted.GetRange(mid, sorted.Count - mid);
                PlaceItems(container, frontHalf, true, result, ref usedVolume);
                PlaceItems(container, backHalf, false, result, ref usedVolume);
            }
            else
            {
                bool frontToBack = container.doorType == DoorType.SingleFront;
                PlaceItems(container, sorted, frontToBack, result, ref usedVolume);
            }

            result.allItemsFit = result.placements.TrueForAll(pr => pr.isValid);
            result.usedVolumePercentage = containerVolume > 0f ? (usedVolume / containerVolume) * 100f : 0f;
            return result;
        }

        private static void PlaceItems(ContainerData container, List<DeliveryOrder> items, bool frontToBack, PackingResult result, ref float usedVolume)
        {
            float p = container.padding;
            float currentZ = frontToBack ? p : container.depth - p;
            float layerDepth = 0f;
            float currentX = p;

            PlacementResult slotBottomItem = null;
            float slotBottomTopY = 0f;
            float slotPosX = 0f;
            int currentLayerSequence = int.MinValue;

            for (int i = 0; i < items.Count; i++)
            {
                var product = items[i].product;
                int seq = items[i].deliverySequence;

                float bestW = product.width;
                float bestD = product.depth;
                Quaternion bestRot = Quaternion.identity;

                if (product.rotationConstraint == RotationConstraint.YAxisOnly)
                {
                    float w90 = product.depth, d90 = product.width;
                    bool fits0 = (currentX + product.width + p <= container.width);
                    bool fits90 = (currentX + w90 + p <= container.width);
                    if (!fits0 && fits90)
                    {
                        bestW = w90;
                        bestD = d90;
                        bestRot = Quaternion.Euler(0, 90, 0);
                    }
                }
                else if (product.rotationConstraint == RotationConstraint.Free)
                {
                    float[][] orientations = { new[] { product.width, product.depth }, new[] { product.depth, product.width } };
                    for (int o = 0; o < orientations.Length; o++)
                    {
                        if (currentX + orientations[o][0] + p <= container.width)
                        {
                            bestW = orientations[o][0];
                            bestD = orientations[o][1];
                            bestRot = Quaternion.Euler(0, o * 90f, 0);
                            break;
                        }
                    }
                }

                bool seqChanged = seq != currentLayerSequence && currentLayerSequence != int.MinValue;

                bool stacked = false;
                float posY = p;
                float posX;

                if (!seqChanged &&
                    slotBottomItem != null &&
                    slotBottomItem.product.canBeStackedOn &&
                    product.weight <= slotBottomItem.product.maxStackWeight &&
                    slotBottomTopY + product.height + p <= container.height)
                {
                    posY = slotBottomTopY + p;
                    posX = slotPosX;
                    slotBottomItem = null;
                    stacked = true;
                }
                else
                {
                    if (seqChanged || currentX + bestW + p > container.width)
                    {
                        currentX = p;
                        if (frontToBack)
                            currentZ += layerDepth + p;
                        else
                            currentZ -= layerDepth + p;
                        layerDepth = 0f;
                        slotBottomItem = null;
                    }
                    currentLayerSequence = seq;
                    posX = currentX + bestW * 0.5f;
                }

                bool depthOk = frontToBack
                    ? (currentZ + bestD + p <= container.depth)
                    : (currentZ - bestD - p >= 0f);
                float posZ = frontToBack ? currentZ : currentZ - bestD;
                bool heightOk = (posY + product.height + p <= container.height);

                if (!depthOk || !heightOk)
                {
                    result.placements.Add(new PlacementResult
                    {
                        product = product,
                        position = Vector3.zero,
                        rotation = Quaternion.identity,
                        loadOrder = result.placements.Count,
                        isValid = false,
                        invalidReason = "Konteynıra sığmıyor"
                    });
                    result.allItemsFit = false;
                    slotBottomItem = null;
                    continue;
                }

                var placement = new PlacementResult
                {
                    product = product,
                    position = new Vector3(posX, posY, posZ + bestD * 0.5f),
                    rotation = bestRot,
                    loadOrder = result.placements.Count,
                    isValid = true,
                    invalidReason = string.Empty
                };

                result.placements.Add(placement);
                result.totalWeight += product.weight;
                usedVolume += product.width * product.height * product.depth;

                if (bestD > layerDepth) layerDepth = bestD;

                if (!stacked)
                {
                    slotPosX = posX;
                    slotBottomTopY = p + product.height;
                    slotBottomItem = placement;
                    currentX += bestW + p;
                }
            }
        }
    }
}
