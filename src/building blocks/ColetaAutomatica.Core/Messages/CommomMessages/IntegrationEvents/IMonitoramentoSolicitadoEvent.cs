namespace ColetaAutomatica.Core.Messages.CommomMessages.IntegrationEvents
{
    public interface IMonitoramentoSolicitadoEvent 
    {
        public Guid NumeroProcesso { get; set; }        
    }
}
