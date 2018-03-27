# Resumo

Projeto criado para sessão de Dojô da CINQ com o objetivo de apresentar conceitos de CRUD utilizando o framework .NET

# Stack

A lista abaixo mostra todos os componentes e técnicas utilizadas para implementar o desafio do dojô.

|Componente         |Proposta                                 |
|------------------|----------------------------------------|
|`.Net`          |Plataforma de desenvolvimento Microsoft. |
|`xUnit`           |Framework de test unitário. |
|`LiteDB` |Base de dados NoSQL para aplicações .NET |
|`Moq` |Biblioteca para _mocar_ objetos para a plataforma .NET |
|`Dependecy Injection`       |Padrão de desenvolvimento de programas de computadores utilizado quando é necessário manter baixo o nível de acoplamento entre diferentes módulos de um sistema. |

# Módulos

Lista dos módulos (camadas) utilizados no desafio.

|Component               |Purpose                        |
|------------------------|-------------------------------|
|`IBancoDeDados`       |Mantem operações de persistência provendo métodos de interação com a base de dados.|
|`IClienteController`          |Módulo core ou negocial, onde todas as operações de validação e decisão são orquestradas e executadas.|
|`Cliente`          |Domínio da aplicação contendo todos atributos que refletem os dados de um cliente.|
|`Program` |Módulo de apresentação onde pode ser encontrado estruturas de menu e feedback visual para o usuário.|

