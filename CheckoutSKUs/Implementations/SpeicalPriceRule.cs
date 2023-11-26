using System;
using CheckoutSKUs.Interfaces;

namespace CheckoutSKUs.Implementations
{
	public class SpeicalPriceRule: IPriceRule
	{
        public char itemId { get; }
        private int _specialQuantity { get; set; }
        private double _specialPrice { get; set; }
		private double _unitPrice { get; set; }

		public SpeicalPriceRule(char itemId, double unitPrice, int specialQuantity, double specialPrice)
		{
			this.itemId = char.ToUpper(itemId);
			_specialQuantity = specialQuantity;
			_specialPrice = specialPrice;
			_unitPrice = unitPrice;
		}
		public void SetUnitPrice(double unitPrice) {
			_unitPrice = unitPrice;
		}
		/// <summary>
		/// special price for special quantity
		/// </summary>
		/// <param name="specialQuantity"></param>
		/// <param name="specialPrice"></param>
		public void SetSpecialPrice(int specialQuantity, double specialPrice) {
            _specialQuantity = specialQuantity;
            _specialPrice = specialPrice;
        }
        public double CalculatePrice(int quantity)
		{
			return _specialPrice * (quantity / _specialQuantity) + _unitPrice * (quantity % _specialQuantity);
		}
	}
}

