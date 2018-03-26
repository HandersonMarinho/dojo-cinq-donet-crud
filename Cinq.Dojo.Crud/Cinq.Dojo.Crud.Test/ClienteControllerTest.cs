using System;
using Xunit;
using Moq;
using Cinq.Dojo.Crud;
using System.Linq;

public class Test
{
    public Mock<IBancoDeDados> Banco { get; set; } = new Mock<IBancoDeDados>();

    public Cliente ClienteData { get; set; } = new Cliente
    {
        CPF = "000.000.000-00",
        Email = "cliente@gmail.com",
        Endereco = "Rua Gran Nicco 113",
        Idade = 33,
        Nome = "Handerson Marinho",
        Telefone = "(41) 99653-4797"
    };

    [Fact]
    public void _001_Construtor_Se_BancoDeDadosNulo_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            new ClienteController(null);
        });
    }

    [Fact]
    public void _002_CadastraCliente_Se_ClienteNulo_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            obj.CadastraCliente(null);
        });
    }

    [Fact]
    public void _003_CadastraCliente_Se_CpfNulo_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            ClienteData.CPF = string.Empty;
            obj.CadastraCliente(ClienteData);
        });
    }

    [Fact]
    public void _004_CadastraCliente_Se_EmailNulo_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            ClienteData.Email = string.Empty;
            obj.CadastraCliente(ClienteData);
        });
    }

    [Fact]
    public void _005_CadastraCliente_Se_EnderecoNulo_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            ClienteData.Endereco = string.Empty;
            obj.CadastraCliente(ClienteData);
        });
    }

    [Fact]
    public void _006_CadastraCliente_Se_IdadeMenorQue18_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            ClienteData.Idade = 17;
            obj.CadastraCliente(ClienteData);
        });
    }

    [Fact]
    public void _007_CadastraCliente_Se_TelefoneNulo_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            ClienteData.Telefone = string.Empty;
            obj.CadastraCliente(ClienteData);
        });
    }

    [Fact]
    public void _008_CadastraCliente_Se_CpfInvalido_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            ClienteData.CPF = "000.000.000-000";
            obj.CadastraCliente(ClienteData);
        });
    }

    [Fact]
    public void _009_CadastraCliente_Se_EmailInvalido_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            ClienteData.Email = "@@@.@@@";
            obj.CadastraCliente(ClienteData);
        });
    }

    [Fact]
    public void _010_CadastraCliente_Se_NomeMaiorQue50Caracteres_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            ClienteData.Nome = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            obj.CadastraCliente(ClienteData);
        });
    }

    [Fact]
    public void _011_CadastraCliente_Se_ClienteValido_Entao_SalvaBanco()
    {
        Banco = new Mock<IBancoDeDados>();
        Banco.Setup(x => x.Salvar(It.IsAny<Cliente>()));

        var obj = new ClienteController(Banco.Object);
        obj.CadastraCliente(ClienteData);

        Banco.Verify(x => x.Salvar(It.IsAny<Cliente>()));
    }

    [Fact]
    public void _012_PesquisaCliente_Se_CpfNulo_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            var cliente = obj.PesquisaCliente(string.Empty);
        });
    }

    [Fact]
    public void _013_PesquisaCliente_Se_CpfInvalido_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            var cliente = obj.PesquisaCliente("000.000.000-000");
        });
    }

    [Fact]
    public void _014_PesquisaCliente_Se_ClienteNaoEncontrado_Entao_RetornaNulo()
    {
        Banco = new Mock<IBancoDeDados>();
        Banco.Setup(x => x.Pesquisa(It.IsAny<string>())).Returns<Cliente>(null);

        var obj = new ClienteController(Banco.Object);
        var cliente = obj.PesquisaCliente("111.111.111-11");

        Banco.Verify(x => x.Pesquisa(It.IsAny<string>()));
        Assert.Null(cliente);
    }

    [Fact]
    public void _015_PesquisaCliente_Se_ClienteEncontrado_Entao_RetornaCliente()
    {
        Banco = new Mock<IBancoDeDados>();
        Banco.Setup(x => x.Pesquisa(It.IsAny<string>())).Returns(ClienteData);

        var obj = new ClienteController(Banco.Object);
        var cliente = obj.PesquisaCliente("000.000.000-00");

        Banco.Verify(x => x.Pesquisa(It.IsAny<string>()));
        Assert.NotNull(cliente);
    }

    [Fact]
    public void _016_RetornaTodosClientes_Se_DadosEncontrados_Entao_RetornaClientes()
    {
        Banco = new Mock<IBancoDeDados>();
        Banco.Setup(x => x.RetornaTodos()).Returns(new[] { ClienteData, ClienteData });

        var obj = new ClienteController(Banco.Object);
        var clientes = obj.RetornaTodosClientes();

        Banco.Verify(x => x.RetornaTodos());
        Assert.NotNull(clientes);
        Assert.NotEmpty(clientes);
    }

    [Fact]
    public void _017_DeletaCliente_Se_CpfNulo_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            obj.DeletaCliente(string.Empty);
        });
    }

    [Fact]
    public void _018_DeletaCliente_Se_CpfInvalido_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            var obj = new ClienteController(Banco.Object);
            obj.DeletaCliente("000.000.000.000");
        });
    }

    [Fact]
    public void _019_DeletaCliente_Se_ClienteNaoEncontrado_Entao_RetornaException()
    {
        Assert.Throws<Exception>(() =>
        {
            Banco = new Mock<IBancoDeDados>();
            Banco.Setup(x => x.Pesquisa(It.IsAny<string>())).Returns<Cliente>(null);
            Banco.Setup(x => x.Deleta(It.IsAny<string>()));

            var obj = new ClienteController(Banco.Object);
            obj.DeletaCliente("111.111.111-11");

            Banco.Verify(x => x.Pesquisa(It.IsAny<string>()), Times.Once);
            Banco.Verify(x => x.Deleta(It.IsAny<string>()), Times.Never);
        });
    }

    [Fact]
    public void _020_DeletaCliente_Se_ClienteEncontrado_Entao_Deleta()
    {
        Banco = new Mock<IBancoDeDados>();
        Banco.Setup(x => x.Pesquisa(It.IsAny<string>())).Returns(ClienteData);
        Banco.Setup(x => x.Deleta(It.IsAny<string>()));

        var obj = new ClienteController(Banco.Object);
        obj.DeletaCliente("000.000.000-00");

        Banco.Verify(x => x.Deleta(It.IsAny<string>()), Times.Once);
        Banco.Verify(x => x.Pesquisa(It.IsAny<string>()), Times.Once);
    }
}