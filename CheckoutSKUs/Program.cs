using CheckoutSKUs;
using CheckoutSKUs.Implementations;
using CheckoutSKUs.Interfaces;

var currentPriceRules = new List<IPriceRule>
{
    new SpeicalPriceRule('A', 60, 3, 150),
    new SpeicalPriceRule('B', 30, 2, 45),
    new UnitPriceRule('C', 30),
    new UnitPriceRule('D', 25)
};

var checkout = new Checkout(currentPriceRules);
checkout.Scan("DABABA");
Console.WriteLine(checkout.Total());