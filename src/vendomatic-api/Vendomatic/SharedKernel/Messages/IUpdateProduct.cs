namespace SharedKernel.Messages
{
    using System;
    using System.Text;

    public interface IIUpdateProduct
    {
        public int VendingMachineId { get; set; }

public int ProductId { get; set; }

public int TotalCount { get; set; }
    }

    public class IUpdateProduct : IIUpdateProduct
    {
        public int VendingMachineId { get; set; }

public int ProductId { get; set; }

public int TotalCount { get; set; }
    }
}