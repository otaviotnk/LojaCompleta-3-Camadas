namespace AppMVC.Business.Models
{
    public enum StatusVenda
    {
        //Será alterado por algum usuário que possua Claims de Vendedor(para criar ainda)
        Criada = 1,
        Faturada,
        Enviada,
        Entregue,
        Cancelada
    }
}
