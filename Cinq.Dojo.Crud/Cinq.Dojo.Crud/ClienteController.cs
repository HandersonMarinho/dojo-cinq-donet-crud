using System;

namespace Cinq.Dojo.Crud
{
    public class ClienteController : IClienteController
    {
        public ClienteController(IBancoDeDados banco)
        {
            throw new NotImplementedException();
        }

        public IBancoDeDados Banco { get; set; }

        public void CadastraCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Cliente PesquisaCliente(string cpf)
        {
            throw new NotImplementedException();
        }

        public Cliente[] RetornaTodosClientes()
        {
            throw new NotImplementedException();
        }

        public void DeletaCliente(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
