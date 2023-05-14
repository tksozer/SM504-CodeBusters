namespace VendomaticApi.Domain.Products;

using Ardalis.SmartEnum;

public abstract class TypeEnum : SmartEnum<TypeEnum>
{
    public static readonly TypeEnum Snack = new SnackType();
    public static readonly TypeEnum Beverage = new BeverageType();
    public static readonly TypeEnum Fresh = new FreshType();
    public static readonly TypeEnum Cooked = new CookedType();

    protected TypeEnum(string name, int value) : base(name, value)
    {
    }

    private class SnackType : TypeEnum
    {
        public SnackType() : base("Snack", 0)
        {
        }
    }

    private class BeverageType : TypeEnum
    {
        public BeverageType() : base("Beverage", 1)
        {
        }
    }

    private class FreshType : TypeEnum
    {
        public FreshType() : base("Fresh", 2)
        {
        }
    }

    private class CookedType : TypeEnum
    {
        public CookedType() : base("Cooked", 3)
        {
        }
    }
}