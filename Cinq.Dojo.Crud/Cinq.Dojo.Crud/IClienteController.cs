namespace Cinq.Dojo.Crud
{
    public interface IClienteController
    {
        void CadastraCliente(Cliente cliente);
        Cliente PesquisaCliente(string cpf);
        void DeletaCliente(string cpf);
        Cliente[] RetornaTodosClientes();
    }
}
