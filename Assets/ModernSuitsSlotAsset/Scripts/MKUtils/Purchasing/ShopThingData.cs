using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mkey
{
    public enum RealShopType { None, Coins, DealCoins, VideoAdsRewards };

    [System.Serializable]
    public class ShopThingData
    {
        public string name;
        public ShopThingHelper prefab;
        public bool isActive = false;

        [HideInInspector]
        public Button.ButtonClickedEvent clickEvent;

        public ShopThingData(ShopThingData prod)
        {
            if (prod == null) return;
            name = prod.name;
            prefab = prod.prefab;
            isActive = prod.isActive;
            clickEvent = prod.clickEvent;
        }
    }

    [System.Serializable]
    public class ShopThingDataReal : ShopThingData
    {
        public string kProductID;
        public RealShopType shopType = RealShopType.Coins;
        [Space(8, order = 0)]
        [Header("Purchase Event: ", order = 1)]
        public UnityEvent PurchaseEvent;

        public ShopThingDataReal(ShopThingDataReal prod) : base(prod)
        {
            kProductID = prod.kProductID;
            shopType = prod.shopType;
            PurchaseEvent = prod.PurchaseEvent;
        }
    }
}
