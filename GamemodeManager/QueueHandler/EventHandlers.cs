using Exiled.API.Features;

namespace GamemodeManager.QueueHandler
{
    public class EventHandlers
    {
        private readonly QueueHandler queueHandler;
        public EventHandlers(QueueHandler handler) => queueHandler = handler;

        public void OnWaitingForPlayers()
        {
            Log.Debug($"Queue has {queueHandler.Queue.Count} items.", Plugin.Singleton.Config.Debug);
            if (queueHandler.Queue.Count > 0)
            {
                Log.Debug($"Enabling {queueHandler.Queue[0].Item1.Name}.", Plugin.Singleton.Config.Debug);
                Plugin.Singleton.Methods.EnableGamemode(queueHandler.Queue[0].Item1, out string _, false, null, queueHandler.Queue[0].Item2);
                queueHandler.RemoveQueueItem(0);
            }
        }
    }
}