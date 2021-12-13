using ColetaAutomatica.Core.Data.EventSourcing;
using ColetaAutomatica.Core.Messages;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace ColetaAutomatica.TratamentoEventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreService _eventStoreService;

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            var conn = await _eventStoreService.GetConnection().AppendToStreamAsync(
                evento.AggregateId.ToString(),
                ExpectedVersion.Any,
                FormatarEvento(evento));
        }

        public async Task<IEnumerable<StoredEvent>> ObterEventos(Guid id)
        {
            var eventos = await _eventStoreService.GetConnection()
                .ReadStreamEventsForwardAsync(id.ToString(), 0, 500, false);

            var listaEventos = new List<StoredEvent>();

            foreach (var resolvedEvent in eventos.Events)
            {
                var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
                var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);

                var evento = new StoredEvent(
                    resolvedEvent.Event.EventId,
                    resolvedEvent.Event.EventType,
                    jsonData.Timestamp,
                    dataEncoded);

                listaEventos.Add(evento);
            }

            return listaEventos.OrderBy(e => e.DataOcorrencia);
        }

        private static IEnumerable<EventData> FormatarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            yield return new EventData(
                Guid.NewGuid(),
                evento.MessageType,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evento)),
                null);
        }

        private static string ComputeHash(byte[] objectAsBytes)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            try
            {
                byte[] result = md5.ComputeHash(objectAsBytes);

                // Build the final string by converting each byte
                // into hex and appending it to a StringBuilder
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("X2"));
                }

                // And return it
                return sb.ToString();
            }
            catch (ArgumentNullException ane)
            {
                //If something occurred during serialization, 
                //this method is called with a null argument. 
                Console.WriteLine("Hash has not been generated.");
                return null;
            }
        }

        public static byte[] ConvertNumToByte(int Number)
        {
            byte[] ByteArray = new byte[32];
            string BinString = Convert.ToString(Number, 2);
            char[] BinCharArray = BinString.ToCharArray();
            try
            {
                System.Array.Reverse(BinCharArray);
                if (BinCharArray != null && BinCharArray.Length > 0)
                {
                    for (int index = 0; index < BinCharArray.Length; ++index)
                    {
                        ByteArray[index] = Convert.ToByte(Convert.ToString(BinCharArray[index]));
                    }
                }
            }
            catch
            {
            }
            return ByteArray;
        }

        internal class BaseEvent
        {
            public DateTime Timestamp { get; set; }
        }
    }
}