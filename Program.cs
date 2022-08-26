using PrimeiraAPI.Context;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Falando pra nossa Agenda usar o SqlServer, e pegando a configuração da connectionString e o nome q demos pra nossa colação.
builder.Services.AddDbContext<AgendaContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//Então, criamos o nosso DbContext, criamos nossa Entidade chamada Contato que vai representra uma tabela do nosso
//banco de dados, criamos o nosso Context que vai representar o nosso banco, o nosso DbSet para representar uma 
//tabela, cadastramos a nossa conexão para se conectar ao banco de dados e por ultimo passamos nossa configuração
// aqui no program , la em cima pra ele usar nossa ConexãoPadrão lá do Json


#region "Recapitulando passo a passo:"
/*
 * instalar o Entity Framework a nivel de maquina (Uma unica vez)
 * instalamos o pacote Microsoft.EntityFrameworkCore.SqlServer (cada projeto) caso SqlServer
 * instalamos o pacote Microsoft.EntityFrameworkCore.Design (cada projeto)

1 - no nosso appsettings.Development.json colocamos numa variável nossa string de conexão.
tem q ter "ConnectionStrings": { "variavel": Server=localhost\\sqlexpress; Initial Catalog= NomeDoBanco;
Integrated Security=True" 
}

2 - Criamos nossa Entidade contato, é uma classe da nossa API que a mesmo tempo vai ser uma tabela no nosso
banco de dados.
Temos o nosso Context, que é nossa classe que acessa o banco de dados no caso AgendaContext que herda de
DbContext, e tem aquele construtor super verboso junto com a propriedade DbSet<Contato>{g;s;} com a
propriedade Contatos que definimos aqui que chamamos para acessar os registros da nossa tabela de contatos
assim vinculamos nossa Entidade no DbSet

3 - adicionamos ao Program.cs, logo abaixo do add services to container o comando:
builder.Services.AddDbContext<AgendaContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("variavel")));
que faz a coexão efetiva com nosso banco de dados. (para o SQLServer nesse caso).

4 - Criamos nossas Migrations, que é o código que espelha nossas alterações feitas pela API no nosso banco
de dados (transforma na linguagem de Sql) É ideal deixar que o Entity Framework crie a nossa tabela, sendo
assim ele fica com maior controle sobre os dados dela.
Nossa Migration gera 2 métodos o Up() que gera mudanças e o Down() que reverte essas mudanças.
(não é só com o fato de criar a migration q ela vai passar a existir no nosso banco de dados, temos que
aplicar ela, executando o comando update Database no terminal. dotnet-ef database-update

5 - Fizemos nossas controllers, que são o ponto de entrada onde vamos disponibilizar os nossos métodos. Elas
ficam dentro da pasta controllers e tem o nome XxxxController.cs
Vao ter os atributos [ApiController] e o [Route("[Controller]")]
e na nossa controller, vamos receber via construtor o nosso AgendaContext, que é uma classe privada 
com nossa propriedade _context que nos permite acessar o banco de dados, que é uma injeção de dependências.

*/
#endregion