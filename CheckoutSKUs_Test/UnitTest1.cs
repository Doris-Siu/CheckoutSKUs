using CheckoutSKUs;
using CheckoutSKUs.Implementations;
using CheckoutSKUs.Interfaces;

namespace CheckoutSKUs_Test;

public class Tests
{
    List<IPriceRule> currentPriceRules;
    
    [SetUp]
    public void Setup()
    {
        currentPriceRules = new List<IPriceRule>
        {
            new SpeicalPriceRule('A', 60, 3, 150),
            new SpeicalPriceRule('B', 30, 2, 45),
            new UnitPriceRule('C', 30),
            new UnitPriceRule('D', 25)
        };
    }

    [Test]
    [TestCase("A",60)]
    [TestCase("AB",90)]
    [TestCase("CDBA",145)]
    [TestCase("AA",120)]
    [TestCase("AAA",150)]
    [TestCase("AAAA",210)]
    [TestCase("AAAAA",270)]
    [TestCase("AAAAAA",300)]
    [TestCase("AAAB",180)]
    [TestCase("AAABB",195)]
    [TestCase("AAABBD",220)]
    [TestCase("DABABA",220)]
    public void TestItemStr(string itemStr, double expectedTotal)
    {
        Checkout checkout = new Checkout(currentPriceRules);
        checkout.Scan(itemStr);
        Assert.That(expectedTotal, Is.EqualTo(checkout.Total()));
    }

    [Test]
    public void TestItem3A2B1D()
    {
        
        Checkout checkout = new Checkout(currentPriceRules);
        checkout.Scan(new Item { Id = 'A', Quantity = 3 });
        checkout.Scan(new Item { Id = 'B', Quantity = 2 });
        checkout.Scan(new Item { Id = 'D', Quantity = 1 });

        Assert.That(checkout.Total(), Is.EqualTo(220));
    }

    [Test]
    public void TestItemEmpty()
    {

        Checkout checkout = new Checkout(currentPriceRules);

        Assert.That(checkout.Total(), Is.EqualTo(0));
    }

    [Test]
    public void TestRuleEmpty()
    {

        Checkout checkout = new Checkout(new List<IPriceRule>());
        checkout.Scan(new Item { Id = 'A', Quantity = 3 });
        checkout.Scan(new Item { Id = 'B', Quantity = 2 });
        checkout.Scan(new Item { Id = 'D', Quantity = 1 });


        Assert.That(checkout.Total(), Is.EqualTo(0));
    }

    [Test]
    public void TestItemWithAnotherPriceRule3A2B1D()
    {

        Checkout checkout = new Checkout(new List<IPriceRule>
        {
            new SpeicalPriceRule('A', 60, 3, 120),
            new SpeicalPriceRule('B', 30, 2, 30),
            new UnitPriceRule('C', 30),
            new UnitPriceRule('D', 50)
        });
        checkout.Scan(new Item { Id = 'A', Quantity = 3 });
        checkout.Scan(new Item { Id = 'B', Quantity = 2 });
        checkout.Scan(new Item { Id = 'D', Quantity = 1 });
        //120+30+50=200
        Assert.That(checkout.Total(), Is.EqualTo(200));
    }
}
