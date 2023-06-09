using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Cat.Models;
using Cat.Data;


namespace Cat.Controllers
{

    [ApiController]
    [Route("categorias")]

    public class CategoriaController : Controller
    {

        [HttpPost]
        [Route("")]

        public async Task<ActionResult<Categoria>> Post([FromServices] DataContext context, [FromBody] Categoria body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = new Categoria()
            {
                Nome = body.Nome,
                Descricao = body.Descricao,
                Quantidade = body.Quantidade,
                Preco = body.Preco,
                DataCadastro = body.DataCadastro
                
            };

            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            return body;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Listas([FromServices] DataContext context)
        {
            var categorias = await context.Categorias.ToListAsync();

            return categorias;
        }


        

        [HttpPut("{id}")]
        public IActionResult AtualizarCategoria(int id, [FromBody] Categoria categoria, [FromServices] DataContext context)
        {
           

           
            var categoriaExistente = context.Categorias.Find(id);

            if (categoriaExistente == null)
            {
                return NotFound(); // Retorna uma resposta de erro (404 Not Found) se a categoria não for encontrada
            }

            categoriaExistente.Nome = categoria.Nome;
            categoriaExistente.Descricao = categoria.Descricao;
            categoriaExistente.Preco = categoria.Preco;
            categoriaExistente.Quantidade = categoria.Quantidade;
            categoriaExistente.DataCadastro = categoria.DataCadastro;

            context.SaveChangesAsync(); // Salva as alterações no banco de dados

            return Ok(); // Retorna uma resposta de sucesso (200 OK) ao front-end
        }

        

       



        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromServices] DataContext context, int id)
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            context.Categorias.Remove(categoria);
            await context.SaveChangesAsync();
            return Ok(categoria);
        }
    }
}