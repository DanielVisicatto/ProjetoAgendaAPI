using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Entities;

namespace PrimeiraAPI.Context
{
    public class AgendaContext : DbContext
    {
        //esse construtor verboso fica vazio mesmo
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {
        }

        //precisa colocar uma propriedade que se refere a nossa entidade, no caso contato
        //dentro do DbSet pq é uma classe do nosso programa que vai representar uma tabela no nosso Banco de Dados
        public DbSet<Contato> Contatos { get; set; }

        // nossa conexão ao banco de dados tem q ser cadastrada nos arquivos Json>>>>>>>>>>>>>>>>>>>>>>>>>


    }
}
