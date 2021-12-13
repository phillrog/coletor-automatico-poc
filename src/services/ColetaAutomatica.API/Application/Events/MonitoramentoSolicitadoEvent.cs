using ColetaAutomatica.Core.Messages;

namespace ColetaAutomatica.API.Application.Events
{
    public class MonitoramentoSolicitadoEvent : Event
    {
        public Guid NumeroProcesso { get; set; }

        public MonitoramentoSolicitadoEvent(Guid numeroProcesso, Type paraOTipo = null)
        {
            AggregateId = numeroProcesso;
            NumeroProcesso = numeroProcesso;
            Tipo = paraOTipo;
        }
    }
}
