using System;
namespace CheckoutSKUs.Interfaces
{
	public interface IPriceRule {
		char itemId { get; }
		double CalculatePrice(int quantity);
		void SetUnitPrice(double unitPrice);
	}

}

