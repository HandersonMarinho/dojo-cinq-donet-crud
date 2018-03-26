namespace Cinq.Dojo.Crud
{
    public interface IBancoDeDados
    {
        void Salvar(Cliente cliente);
        Cliente Pesquisa(string cpf);
        void Deleta(string cpf);
        Cliente[] RetornaTodos();
        void DeletaTodos();
    }
}
