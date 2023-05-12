namespace SharedKernel.Messages
{
    using System;
    using System.Text;

    public interface IIUpdateStatus
    {
        public int VendingMachineId { get; set; }

        public string Status { get; set; }
    }

    public class IUpdateStatus : IIUpdateStatus
    {
        public int VendingMachineId { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}