using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Context;
using PrimeiraAPI.Entities;

namespace PrimeiraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        // nosso objetivo aqui é implementar um CRUD na nossa tabela de contato

        //vamos criar um atributo privado somente leitura do tipo AgendaContext
        private readonly AgendaContext _context;

        //nossa tabela iniciou vazia então vamos começar com o método de criação
        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new {id = contato.Id}, contato);//apos criar, para 
            //obter o registro recém criado, vc chama ObterPorId, e retorno o Id recem criado e a propria
            //informação do contato.
        }

        //vamos precisar de um construtor para receber o nosso Context
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        //agora um EndPoint com a ação de obter o contato do banco de dados
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato == null)
                return NotFound();
            return Ok(contato);
        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));
            return Ok(contatos);
        }

        //agora um EndPoint com a ação de atualizar um contato do banco de dados
        [HttpPut("{id}")]
        public IActionResult AtualizarContato(int id, Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(id);

            //verifica nulo
            if (contatoBanco == null)
                return NotFound();

            //se não for nulo
            contatoBanco.Nome = contato.Nome;//pega o que tá no banco e substitui pelo da requisição
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            //atualiza salva e retorna o contato atualizado
            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return Ok(contatoBanco);
        }

        //por fim o EndPoint de deletar um registro do Banco de dados
        [HttpDelete("{id}")]
        public IActionResult DeletarContato(int id)
        {
            // tenho que verificar se o que quero deletar existe...
            var contatoBanco = _context.Contatos.Find(id);

            //verifica nulo
            if (contatoBanco == null)
                return NotFound();

            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
