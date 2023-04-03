using System.Collections.Generic;

namespace Testing
{
    public class Inventory
    {
        public List<ShopItem> Items => _items;
        
        private List<ShopItem> _items;
        private int _maxItemCapacity;

        public Inventory(int maxItemCapacity)
        {
            _maxItemCapacity = maxItemCapacity;
        }
        
        public Inventory(List<ShopItem> startingItems, int maxItemCapacity)
        {
            _items = startingItems;
            _maxItemCapacity = maxItemCapacity;
        }

        public bool AddItem(ShopItem shopItem)
        {
            if (_items.Count < _maxItemCapacity)
            {
                _items.Add(shopItem);
                return true;
            }
            return false;
        }

        public bool RemoveItem(ShopItem item)
        {
            return _items.Remove(item);
        }

        public bool HasItem(ShopItem item)
        {
            return _items.Contains(item);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}