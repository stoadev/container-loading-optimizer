using UnityEngine;

namespace ContainerLoading.Models
{
    public static class ProductDatabase
    {
        public static ProductData Buzdolabi()
        {
            return new ProductData
            {
                productName = "Buzdolabi",
                width = 0.70f,
                height = 1.85f,
                depth = 0.70f,
                weight = 75f,
                rotationConstraint = RotationConstraint.YAxisOnly,
                canBeStackedOn = false,
                maxStackWeight = 0f,
                gizmoColor = Color.blue
            };
        }

        public static ProductData CamasirMakinesi()
        {
            return new ProductData
            {
                productName = "Camasir Makinesi",
                width = 0.60f,
                height = 0.85f,
                depth = 0.60f,
                weight = 70f,
                rotationConstraint = RotationConstraint.YAxisOnly,
                canBeStackedOn = true,
                maxStackWeight = 80f,
                gizmoColor = Color.green
            };
        }

        public static ProductData BulasikMakinesi()
        {
            return new ProductData
            {
                productName = "Bulasik Makinesi",
                width = 0.60f,
                height = 0.85f,
                depth = 0.60f,
                weight = 50f,
                rotationConstraint = RotationConstraint.YAxisOnly,
                canBeStackedOn = true,
                maxStackWeight = 60f,
                gizmoColor = new Color(0.5f, 0f, 0.5f, 1f) // Mor
            };
        }

        public static ProductData Firin()
        {
            return new ProductData
            {
                productName = "Firin",
                width = 0.60f,
                height = 0.90f,
                depth = 0.60f,
                weight = 45f,
                rotationConstraint = RotationConstraint.YAxisOnly,
                canBeStackedOn = true,
                maxStackWeight = 50f,
                gizmoColor = new Color(1f, 0.5f, 0f, 1f) // Turuncu
            };
        }

        public static ProductData[] GetAllProducts()
        {
            return new ProductData[]
            {
                Buzdolabi(),
                CamasirMakinesi(),
                BulasikMakinesi(),
                Firin()
            };
        }
    }
}
