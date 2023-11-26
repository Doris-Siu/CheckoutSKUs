using System;
using CheckoutSKUs.Interfaces;

namespace CheckoutSKUs.Implementations
{
	public class UnitPriceRule: IPriceRule
	{
		public char itemId { get; }
		private double _unitPrice { get; set; }
		public UnitPriceRule(char itemId, double unitPrice)
		{
			this.itemId = char.ToUpper(itemId);
			_unitPrice = unitPrice; 
		}
		public void SetUnitPrice(double unitPrice) {
			_unitPrice = unitPrice;
		}
		public double CalculatePrice(int quantity)
		{
			return _unitPrice * quantity;
		}
	}
}

