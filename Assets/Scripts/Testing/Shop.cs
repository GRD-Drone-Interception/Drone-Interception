using UnityEngine;

namespace DroneLoadout
{
    public class Shop
    {
        private Inventory _inventory;
        private BuildBudget _playerBudget;

        public Shop(Inventory inventory, BuildBudget playerBudget)
        {
            _inventory = inventory;
            _playerBudget = playerBudget;
        }
        
        public bool Buy(ShopItem item)
        {
            if (_playerBudget.CanAfford(item.Cost))
            {
                _playerBudget.Spend(item.Cost);
                Debug.Log("Item purchased!");
                return true;
            }
            return false;
        }

        public bool Sell(ShopItem item)
        {
            if (_inventory.RemoveItem(item))
            {
                
            }
            
            _playerBudget.Sell(item.Cost);
            Debug.Log("Item sold!");
            return true;
        }
    }
}