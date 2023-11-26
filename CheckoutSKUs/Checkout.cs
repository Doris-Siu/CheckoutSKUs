using System;
using CheckoutSKUs.Interfaces;

namespace CheckoutSKUs
{
	public class Checkout
	{
		private Dictionary<char,List<Item>> _itemDict = new Dictionary<char, List<Item>>();
		private List<IPriceRule> _priceRules = new List<IPriceRule>();

        public Checkout(List<IPriceRule> priceRules)
        {
            _priceRules = priceRules;
        }
        /// <summary>
        /// Add item to dict
        /// </summary>
        /// <param name="item"></param>
        public void Scan(Item item)
        {
            item.Id = char.ToUpper(item.Id);

            if (_itemDict.ContainsKey(item.Id) is false)
            {
                _itemDict.Add(item.Id, new List<Item>());
            }
            _itemDict[item.Id].Add(item);
        }
        /// <summary>
        /// add item to dict in char form
        /// </summary>
        /// <param name="itemId"></param>
        public void Scan(char itemId) {
            Scan(new Item { Id = itemId });
        }
        /// <summary>
        /// add every item in char form from a item string
        /// </summary>
        /// <param name="itemStr"></param>
        public void Scan(string itemStr)
        {
            if (string.IsNullOrEmpty(itemStr))
                return;

            itemStr = itemStr.ToUpper();
            foreach(char itemId in itemStr)
            { 
                Scan(itemId);
            }
        }
        
        
        public double Total()
        {
            double total = 0;

            foreach(var itemEntry in _itemDict)
            {
              
                if (itemEntry.Value == null)
                    continue;

                int itemQuantity = itemEntry.Value.Sum(x => x.Quantity);

                if (itemQuantity == 0)
                    continue;

                var itemPriceRule = _priceRules.FirstOrDefault(x => x.itemId == itemEntry.Key);
                if (itemPriceRule == null)
                    continue;

                total += itemPriceRule.CalculatePrice(itemQuantity);
            }

            return total;
        }
    }
}

