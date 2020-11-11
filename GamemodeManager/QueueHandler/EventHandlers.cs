using Exiled.API.Features;

namespace GamemodeManager.QueueHandler
{
    public class EventHandlers
    {
        private readonly QueueHandler _queueHandler;
        public EventHandlers(QueueHandler handler) => _queueHandler = handler;

        public void OnWaitingForPlayers()
        {
            Log.Debug($"Queue has {_queueHandler.Queue.Count} items.", Plugin.Singleton.Config.Debug);
            if (_queueHandler.Queue.Count > 0)
            {
                Log.Debug($"Enabling {_queueHandler.Queue[0].Item1.Name}.", Plugin.Singleton.Config.Debug);
                Plugin.Singleton.Methods.EnableGamemode(_queueHandler.Queue[0].Item1, out string _, false, null, _queueHandler.Queue[0].Item2);
                _queueHandler.RemoveQueueItem(0);
            }
        }
    }
}