using System;

namespace Cinq.Dojo.Crud
{
    public class ClienteController : IClienteController
    {
        public ClienteController(IBancoDeDados banco)
        {
            if (banco == null)
            {
                throw new Exception("O banco é obrigatorio!");
            }
            this.Banco = banco;
        }

        public IBancoDeDados Banco { get; set; }


        public void CadastraCliente(Cliente cliente)
        {
            if(cliente == null)
            {
                throw new Exception("O Cliente nao pode ser nulo");
            }

            if (cliente.CPF.Equals("")) {
                throw new Exception("CPF do Cliente obrigatorio");
            }

            if (cliente.Email.Equals(""))
            {
                throw new Exception("EMAIL do Cliente obrigatorio");
            }

            if (cliente.Endereco.Equals(""))
            {
                throw new Exception("Endereço do Cliente obrigatorio");
            }

            if (cliente.Idade < 18)
            {
                throw new Exception("Cliente deve ser maior de idade");
            }

            if (cliente.Telefone.Equals(""))
            {
                throw new Exception("Telefone do Cliente obrigatorio");
            }

            if (!Valida(cliente.CPF))
            {
                throw new Exception("CPF inválido");
            } 

            if (!ValidarEmail(cliente.Email))
            {
                throw new Exception("Email inválido");
            }

            if(cliente.Nome.Length > 50)
            {
                throw new Exception("Nome deve ser menor que 50 caracteres");
            }

            Banco.Salvar(cliente);
            
        }

        public Cliente PesquisaCliente(string cpf)
        {

            

            if (!Valida(cpf))
            {
                throw new Exception("CPF inválido");
            }

            Cliente cliente = Banco.Pesquisa(cpf);

            if (cliente == null)
            {
                return null;
            }

            return cliente;
            
        }

        public Cliente[] RetornaTodosClientes()
        {
            Cliente[] cliente = Banco.RetornaTodos();

            if (cliente != null)
            {
                return cliente;
            }
            return null;
            
        }

        public void DeletaCliente(string cpf)
        {
            throw new NotImplementedException();
        }

        public bool Valida(string cpf)

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

        public static bool ValidarEmail(string email)
        {
            bool validEmail = false;
            int indexArr = email.IndexOf('@');
            if (indexArr > 0)
            {
                int indexDot = email.IndexOf('.', indexArr);
                if (indexDot - 1 > indexArr)
                {
                    if (indexDot + 1 < email.Length)
                    {
                        string indexDot2 = email.Substring(indexDot + 1, 1);
                        if (indexDot2 != ".")
                        {
                            validEmail = true;
                        }
                    }
                }
            }
            return validEmail;
        }
    }
}
