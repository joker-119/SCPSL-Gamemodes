using System;
using System.Collections.Generic;
using Exiled.API.Interfaces;

namespace GamemodeManager.QueueHandler
{
    public class QueueHandler
    {
        private readonly Plugin plugin;

        public EventHandlers EventHandlers;
        public QueueHandler(Plugin plugin)
        {
            this.plugin = plugin;
            EventHandlers = new EventHandlers(this);
            RegisterEventHandlers();
        }
        
        public List<Tuple<IPlugin<IConfig>, string>> Queue = new List<Tuple<IPlugin<IConfig>, string>>();

        public bool IsAnyGamemodeActive
        {
            get
            {
                foreach (IPlugin<IConfig> plugin in plugin.LoadedGamemodes.Keys)
                    if ((bool) plugin.GetInstanceField("IsEnabled"))
                        return true;
                return false;
            }
        }

        public void AddToQueue(IPlugin<IConfig> plugin, int roundCount = 1, string extraArgs = "")
        {
            for (int i = 0; i < roundCount; i++)
                Queue.Add(new Tuple<IPlugin<IConfig>, string>(plugin, extraArgs));
        }

        public void RemoveQueueItem(IPlugin<IConfig> plugin) => Queue.RemoveAll(p => p.Item1 == plugin);

        public void RemoveQueueItem(int index) => Queue.RemoveAt(index);

        public void Destroy()
        {
            Queue.Clear();
            RegisterEventHandlers(true);
            EventHandlers = null;
        }

        void RegisterEventHandlers(bool disable = false)
        {
            switch (disable)
            {
                case true:
                    Exiled.Events.Handlers.Server.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
                    break;
                case false:
                    Exiled.Events.Handlers.Server.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
                    break;
            }
        }
    }
}