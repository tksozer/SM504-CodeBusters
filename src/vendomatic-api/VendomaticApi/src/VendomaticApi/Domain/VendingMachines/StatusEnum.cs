namespace VendomaticApi.Domain.VendingMachines;

using Ardalis.SmartEnum;

public abstract class StatusEnum : SmartEnum<StatusEnum>
{
    public static readonly StatusEnum Healthy = new HealthyType();
    public static readonly StatusEnum Problematic = new ProblematicType();
    public static readonly StatusEnum Down = new DownType();

    protected StatusEnum(string name, int value) : base(name, value)
    {
    }

    private class HealthyType : StatusEnum
    {
        public HealthyType() : base("Healthy", 0)
        {
        }
    }

    private class ProblematicType : StatusEnum
    {
        public ProblematicType() : base("Problematic", 1)
        {
        }
    }

    private class DownType : StatusEnum
    {
        public DownType() : base("Down", 2)
        {
        }
    }
}