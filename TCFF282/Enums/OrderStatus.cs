using System.ComponentModel;

namespace Domain.Enums
{
    public enum OrderStatus
    {
        [Description("Recebido")]
        Received = 1,

        [Description("Em preparação")]
        InPreparation = 2,

        [Description("Pronto")]
        Ready = 3,

        [Description("Finalizado")]
        Finished = 4
    }
}
