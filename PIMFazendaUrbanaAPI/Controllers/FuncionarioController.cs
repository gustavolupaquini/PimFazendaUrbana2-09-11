using Microsoft.AspNetCore.Mvc;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaAPI.DTOs;
using AutoMapper;

namespace PIMFazendaUrbanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly IMapper _mapper; // Adiciona o AutoMapper

        // O controlador utiliza a interface IFuncionarioService para acessar as operações de cliente
        public FuncionarioController(IFuncionarioService funcionarioService, IMapper mapper)
        {
            _funcionarioService = funcionarioService;
            _mapper = mapper; // Inicializa o AutoMapper
        }

        [HttpGet("filtrados-funcionario")]
        public ActionResult<List<FuncionarioDTO>> ListarFuncionariosFiltrados(string search)
        {
            try
            {
                var funcionarios = _funcionarioService.ListarFuncionariosComFiltros(search);
                var funcionariosDto = _mapper.Map<List<FuncionarioDTO>>(funcionarios); // Mapeia Funcionario para FuncionarioDTO
                return Ok(funcionariosDto); // Retorna a lista de funcionarios filtrados como resposta
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar funcionarios: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }


        // Método para listar funcionarios ativos
        [HttpGet("ativos-funcionario")]
        public IActionResult ListarFuncionariosAtivos()
        {
            try
            {
                var funcionarios = _funcionarioService.ListarFuncionariosAtivos();
                var funcionariosDto = _mapper.Map<List<FuncionarioDTO>>(funcionarios); // Mapeia Funcionario para FuncionarioDTO
                return Ok(funcionariosDto);
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar funcionarios: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }


        // Método para cadastrar um funcionario
        [HttpPost("cadastrar-funcionario")]
        public IActionResult CadastrarFuncionario([FromBody] FuncionarioDTO funcionarioDto)
        {
            try
            {
                var funcionario = _mapper.Map<Funcionario>(funcionarioDto); // Mapeia FuncionarioDTO para Funcionario
                _funcionarioService.CadastrarFuncionario(funcionario); // Chama o serviço para cadastrar o funcionario
                return Ok(new { message = "Funcionario cadastrado com sucesso." });
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

        // Método para alterar um funcionario
        [HttpPut("alterar-funcionario")]
        public IActionResult AlterarFuncionario([FromBody] FuncionarioDTO funcionarioDTO)
        {
            try
            {
                var funcionario = _mapper.Map<Funcionario>(funcionarioDTO); // Mapeia FuncionarioDTO para Funcionario
                _funcionarioService.AlterarFuncionario(funcionario); // Chama o serviço para alterar o funcionario
                return Ok(new { message = "Funcionario alterado com sucesso." });
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

        // Método para excluir um funcionario (exclusão lógica)
        [HttpDelete("excluir-funcionario/{id}")]
        public IActionResult ExcluirFuncionario(int id)
        {
            try
            {
                _funcionarioService.ExcluirFuncionario(id); // Chama o serviço para excluir o funcionario
                return Ok(new { message = "Funcionario excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para listar funcionarios inativos
        [HttpGet("inativos-funcionario")]
        public IActionResult ListarFuncionariosInativos()
        {
            try
            {
                var funcionarios = _funcionarioService.ListarFuncionariosInativos();
                var funcionariosDto = _mapper.Map<List<FuncionarioDTO>>(funcionarios); // Mapeia Funcionario para FuncionarioDTO
                return Ok(funcionariosDto); // Retorna a lista de funcionarios inativos
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para consultar um funcionario pelo ID
        [HttpGet("{id}")]
        public IActionResult ConsultarFuncionarioPorID(int id)
        {
            try
            {
                var funcionario = _funcionarioService.ConsultarFuncionarioID(id);
                var funcionarioDto = _mapper.Map<FuncionarioDTO>(funcionario); // Mapeia Funcionario para FuncionarioDTO
                return Ok(funcionarioDto); // Retorna o funcionario encontrado
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para consultar um funcionario pelo nome
        [HttpGet("nome-funcionario/{nome}")]
        public IActionResult ConsultarFuncionarioPorNome(string nome)
        {
            try
            {
                var funcionario = _funcionarioService.ConsultarFuncionarioNome(nome);
                var funcionariosDto = _mapper.Map<FuncionarioDTO>(funcionario); // Mapeia Funcionario para FuncionarioDTO
                return Ok(funcionariosDto); // Retorna o funcionario encontrado
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para consultar um funcionario pelo CPF
        [HttpGet("cpf-funcionario/{cpf}")]
        public IActionResult ConsultarFuncionarioPorCPF(string cpf)
        {
            try
            {
                var funcionario = _funcionarioService.ConsultarFuncionarioCPF(cpf);
                var funcionarioDto = _mapper.Map<FuncionarioDTO>(funcionario); // Mapeia Funcionario para FuncionarioDTO
                return Ok(funcionarioDto); // Retorna o funcionario encontrado
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para filtrar funcionarios pelo nome
        [HttpGet("filtrar-funcionario/{nome}")]
        public IActionResult FiltrarFuncionariosPorNome(string nome)
        {
            try
            {
                var funcionarios = _funcionarioService.FiltrarFuncionariosNome(nome);
                var funcionariosDto = _mapper.Map<List<FuncionarioDTO>>(funcionarios); // Mapeia Funcionario para FuncionarioDTO
                return Ok(funcionariosDto); // Retorna a lista de funcionarios filtrados
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }
    }
}
