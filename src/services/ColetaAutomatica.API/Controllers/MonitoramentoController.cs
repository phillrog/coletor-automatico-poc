using ColetaAutomatica.API.Application.Commands;
using ColetaAutomatica.Core.Data.EventSourcing;
using ColetaAutomatica.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ColetaAutomatica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoramentoController : Controller
    {
        private readonly IMediatorHandler _mediator;
        private readonly IEventSourcingRepository _eventSourcingRepository;
        private readonly ILogger<MonitoramentoController> _logger;

        public MonitoramentoController(IMediatorHandler mediator, 
            IEventSourcingRepository eventSourcingRepository,
            ILogger<MonitoramentoController> logger)
        {
            _mediator = mediator;
            _eventSourcingRepository = eventSourcingRepository;
            _logger = logger;
        }

        [HttpPost("solicitar-monitoramento-processo")]
        public async Task<IActionResult> SolicitarMonitoramento(Guid numeroProcesso)
        {
            _logger.LogInformation("Solicitação de MOnitoramento da pasta: " + numeroProcesso.ToString(), DateTime.UtcNow);
            var comando = new SolicitarMonitoramentoProcessoCommand(numeroProcesso);
            await _mediator.EnviarComando<SolicitarMonitoramentoProcessoCommand>(comando);

            return Ok();
        }

        [HttpGet("eventos/{id:guid}")]
        public async Task<IActionResult> Eventos(Guid id)
        {
            
            var eventos = await _eventSourcingRepository.ObterEventos(id);
            return Ok(eventos);
        }
    }
}
