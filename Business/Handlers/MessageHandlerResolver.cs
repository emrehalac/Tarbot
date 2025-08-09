using Business.Abstract;
using Entities.Enums;

namespace Business.Handlers
{
    public class MessageHandlerResolver
    {
        private readonly IEnumerable<IMessageHandler> _handlers;

        public MessageHandlerResolver(IEnumerable<IMessageHandler> handlers)
        {
            _handlers = handlers;
        }

        public IMessageHandler Resolve(ConversationState state)
        {
            return _handlers.FirstOrDefault(h => h.State == state) 
                   ?? throw new NotSupportedException($"No handler found for state: {state}");
        }
    }
}
