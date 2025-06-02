using System.ComponentModel;

namespace Domain.Enums
{
    public enum ProductType
    {
        [Description("hamburger")]
        Burger = 1,
        [Description("Bebida")]
        Beverage = 2,
        [Description("Sobremesa")]
        Dessert = 3
    }
}
