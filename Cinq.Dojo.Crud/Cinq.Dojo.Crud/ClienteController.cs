using System;

namespace Cinq.Dojo.Crud
{
    public class ClienteController : IClienteController
    {
        public ClienteController(IBancoDeDados banco)
        {
            if (banco == null)
                throw new Exception("Banco não informado");

            Banco = banco;
        }

        public IBancoDeDados Banco { get; set; }

        public void CadastraCliente(Cliente cliente)
        {
            if (cliente == null)
                throw new Exception("Cliente não informado.");

            if (string.IsNullOrEmpty(cliente.CPF))
                throw new Exception("Cpf não informado");

            if (string.IsNullOrEmpty(cliente.Email))
                throw new Exception("Email não informado");

            if (string.IsNullOrEmpty(cliente.Endereco))
                throw new Exception("Endereco não informado");

            if (cliente.Idade < 18)
                throw new Exception("Idade menor que 18");

            if (string.IsNullOrEmpty(cliente.Telefone))
                throw new Exception("Telefone não informado");

            if (IsValidEmail(cliente.Email) == false)
                throw new Exception("Email inválido");

            if (cliente.Nome.Length > 50)
                throw new Exception("Nome inválido");

            if (ValidaCPF.IsCpf(cliente.CPF) == false)
                throw new Exception("Cpf inválido");

            Banco.Salvar(cliente);
        }

        public Cliente PesquisaCliente(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                throw new Exception("Cpf não informado");

            if (ValidaCPF.IsCpf(cpf) == false)
                throw new Exception("Cpf inválido");

            return Banco.Pesquisa(cpf);
        }

        public Cliente[] RetornaTodosClientes()
        {
            return Banco.RetornaTodos();
        }

        public void DeletaCliente(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                throw new Exception("Cpf não informado");

            if (ValidaCPF.IsCpf(cpf) == false)
                throw new Exception("Cpf inválido");

            var cliente = Banco.Pesquisa(cpf);
            if (cliente == null)
                throw new Exception("Cliente não encontrado");

            Banco.Deleta(cpf);
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static class ValidaCPF
        {
            public static bool IsCpf(string cpf)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);
            }
        }
    }
}
