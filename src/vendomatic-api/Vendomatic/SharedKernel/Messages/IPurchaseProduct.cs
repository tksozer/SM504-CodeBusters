namespace SharedKernel.Messages
{
    using System;
    using System.Text;

    public interface IIPurchaseProduct
    {
        public int VendingMachineId { get; set; }

public int ProductId { get; set; }

public int PurchasedAmount { get; set; }
    }

    public class IPurchaseProduct : IIPurchaseProduct
    {
        public int VendingMachineId { get; set; }

public int ProductId { get; set; }

public int PurchasedAmount { get; set; }
    }
}