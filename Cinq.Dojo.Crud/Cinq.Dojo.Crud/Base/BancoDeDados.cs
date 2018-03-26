using LiteDB;
using System;
using System.Linq;

namespace Cinq.Dojo.Crud
{
    public class BancoDeDados : IBancoDeDados
    {
        private const string DatabaseFile = @"my-database.db";

        public void Deleta(string cpf)
        {
            using (var db = new LiteDatabase(DatabaseFile))
            {
                try
                {
                    var collection = db.GetCollection<Cliente>("cliente");
                    collection.Delete(x => x.CPF == cpf);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void DeletaTodos()
        {
            using (var db = new LiteDatabase(DatabaseFile))
            {
                try
                {
                    db.DropCollection("cliente");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public Cliente Pesquisa(string cpf)
        {
            using (var db = new LiteDatabase(DatabaseFile))
            {
                try
                {
                    var collection = db.GetCollection<Cliente>("cliente");
                    return collection.FindOne(x => x.CPF == cpf);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public Cliente[] RetornaTodos()
        {
            using (var db = new LiteDatabase(DatabaseFile))
            {
                try
                {
                    var collection = db.GetCollection<Cliente>("cliente");
                    return collection.FindAll().ToArray();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void Salvar(Cliente cliente)
        {
            using (var db = new LiteDatabase(DatabaseFile))
            {
                try
                {
                    var dbClientes = db.GetCollection<Cliente>("cliente");
                    cliente.Id = RetornaTodos().Count() + 1;
                    cliente.Nome = cliente.Nome.ToUpper();
                    cliente.Endereco = cliente.Endereco.ToUpper();
                    dbClientes.Insert(cliente);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
