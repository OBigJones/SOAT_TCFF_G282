using System.ComponentModel;

namespace Domain.Enums
{
    public enum OrderStatus
    {
        [Description("Criado")]
        Created = 1,
        
        [Description("Recebido")]
        Received = 2,

        [Description("Em preparação")]
        InPreparation = 3,

        [Description("Pronto")]
        Ready = 4,

        [Description("Finalizado")]
        Finished = 5
    }
}
