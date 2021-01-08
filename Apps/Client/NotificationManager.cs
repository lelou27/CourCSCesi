using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
{
    public class NotificationManager
    {
        public Queue<string> _messages = new Queue<string>();

        public IReadOnlyList<string> Messages => _messages.ToArray();

        public event Action OnChange;

        public NotificationManager() 
        { 

        }

        public void Add(string message)
        {
            _messages.Enqueue(message);

            Task.Run(async () =>
            {
                await Task.Delay(5000);
                _messages.Dequeue();
                OnChange?.Invoke();
            });

            OnChange?.Invoke();
        }
    }
}
