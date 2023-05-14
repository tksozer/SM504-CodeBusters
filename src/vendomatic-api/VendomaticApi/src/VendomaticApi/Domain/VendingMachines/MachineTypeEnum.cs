namespace VendomaticApi.Domain.VendingMachines;

using Ardalis.SmartEnum;

public abstract class MachineTypeEnum : SmartEnum<MachineTypeEnum>
{
    public static readonly MachineTypeEnum Snack = new SnackType();
    public static readonly MachineTypeEnum Beverage = new BeverageType();
    public static readonly MachineTypeEnum Fresh = new FreshType();
    public static readonly MachineTypeEnum Cooked = new CookedType();

    protected MachineTypeEnum(string name, int value) : base(name, value)
    {
    }

    private class SnackType : MachineTypeEnum
    {
        public SnackType() : base("Snack", 0)
        {
        }
    }

    private class BeverageType : MachineTypeEnum
    {
        public BeverageType() : base("Beverage", 1)
        {
        }
    }

    private class FreshType : MachineTypeEnum
    {
        public FreshType() : base("Fresh", 2)
        {
        }
    }

    private class CookedType : MachineTypeEnum
    {
        public CookedType() : base("Cooked", 3)
        {
        }
    }
}