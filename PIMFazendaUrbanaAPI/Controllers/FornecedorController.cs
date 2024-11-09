using Microsoft.AspNetCore.Mvc;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaAPI.DTOs;
using AutoMapper;

namespace PIMFazendaUrbanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper; // Adiciona o AutoMapper

        // O controlador utiliza a interface IFornecedorService para acessar as operações de fornecedor
        public FornecedorController(IFornecedorService fornecedorService, IMapper mapper)
        {
            _fornecedorService = fornecedorService;
            _mapper = mapper; // Inicializa o AutoMapper
        }

        [HttpGet("filtrados-fornecedor")]
        public ActionResult<List<FornecedorDTO>> ListarFornecedoresFiltrados(string search)
        {
            try
            {
                var fornecedores = _fornecedorService.ListarFornecedoresComFiltros(search);
                var fornecedoresDto = _mapper.Map<List<FornecedorDTO>>(fornecedores); // Mapeia Fornecedor para FornecedorDTO
                return Ok(fornecedoresDto); // Retorna a lista de fornecedores filtrados como resposta
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar fornecedores: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }


        // Método para listar fornecedores ativos
        [HttpGet("ativos-fornecedor")]
        public IActionResult ListarFornecedorAtivos()
        {
            try
            {
                var fornecedores = _fornecedorService.ListarFornecedoresAtivos();
                var fornecedoresDto = _mapper.Map<List<FornecedorDTO>>(fornecedores); // Mapeia Fornecedor para FornecedorDTO
                return Ok(fornecedoresDto);
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar fornecedores: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }


        // Método para cadastrar um fornecedor
        [HttpPost("cadastrar-fornecedor")]
        public IActionResult CadastrarFornecedor([FromBody] FornecedorDTO fornecedorDto)
        {
            try
            {
                var fornecedor = _mapper.Map<Fornecedor>(fornecedorDto); // Mapeia Fornecedor para FornecedorDTO
                _fornecedorService.CadastrarFornecedor(fornecedor); // Chama o serviço para cadastrar o fornecedor
                return Ok(new { message = "Fornecedor cadastrado com sucesso." });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors }); // Retorna erros de validação
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para alterar um fornecedor
        [HttpPut("alterar-fornecedor")]
        public IActionResult AlterarFornecedor([FromBody] FornecedorDTO fornecedorDto)
        {
            try
            {
                var fornecedor = _mapper.Map<Fornecedor>(fornecedorDto); // Mapeia FornecedorDTO para Fornecedor
                _fornecedorService.AlterarFornecedor(fornecedor); // Chama o serviço para alterar o fornecedor
                return Ok(new { message = "Fornecedor alterado com sucesso." });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors }); // Retorna erros de validação
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para excluir um fornecedor (exclusão lógica)
        [HttpDelete("excluir-fornecedor/{id}")]
        public IActionResult ExcluirFornecedor(int id)
        {
            try
            {
                _fornecedorService.ExcluirFornecedor(id); // Chama o serviço para excluir o fornecedor
                return Ok(new { message = "Fornecedor excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para listar fornecedores inativos
        [HttpGet("inativos-fornecedor")]
        public IActionResult ListarForncedoresInativos()
        {
            try
            {
                var fornecedores = _fornecedorService.ListarFornecedoresInativos();
                var fornecedoresDto = _mapper.Map<List<FornecedorDTO>>(fornecedores); // Mapeia Fornecedor para FornecedorDTO
                return Ok(fornecedoresDto); // Retorna a lista de fornecedores inativos
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para consultar um fornecedor pelo ID
        [HttpGet("{id}")]
        public IActionResult ConsultarFornecedorPorID(int id)
        {
            try
            {
                var fornecedores = _fornecedorService.ConsultarFornecedorPorID(id);
                var fornecedoresDto = _mapper.Map<FornecedorDTO>(fornecedores); // Mapeia Fornecedor para FornecedorDTO
                return Ok(fornecedoresDto); // Retorna o fornecedor encontrado
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para consultar um fornecedor pelo nome
        [HttpGet("nome-fornecedor/{nome}")]
        public IActionResult ConsultarFornecedorPorNome(string nome)
        {
            try
            {
                var fornecedores = _fornecedorService.ConsultarFornecedorPorNome(nome);
                var fornecedoresDto = _mapper.Map<FornecedorDTO>(fornecedores); // Mapeia Fornecedor para FornecedorDTO
                return Ok(fornecedoresDto); // Retorna o fornecedor encontrado
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para consultar um fornecedor pelo CNPJ
        [HttpGet("cnpj-fornecedor/{cnpj}")]
        public IActionResult ConsultarFornecedorPorCNPJ(string cnpj)
        {
            try
            {
                var fornecedores = _fornecedorService.ConsultarFornecedorPorCNPJ(cnpj);
                var fornecedoresDto = _mapper.Map<FornecedorDTO>(fornecedores); // Mapeia Fornecedor para FornecedorDTO
                return Ok(fornecedoresDto); // Retorna o fornecedor encontrado
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para filtrar fornecedores pelo nome
        [HttpGet("filtrar-fornecedor/{nome}")]
        public IActionResult FiltrarFornecedorPorNome(string nome)
        {
            try
            {
                var fornecedores = _fornecedorService.FiltrarFornecedoresPorNome(nome);
                var fornecedoresDto = _mapper.Map<List<FornecedorDTO>>(fornecedores); // Mapeia Fornecedor para FornecedorDTO
                return Ok(fornecedoresDto); // Retorna a lista de fornecedores filtrados
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }
    }
}
