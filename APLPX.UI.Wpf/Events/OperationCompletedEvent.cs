using System;

namespace APLPX.UI.WPF.Events
{
    /// <summary>
    /// Notifies subscribers that an operation has completed.
    /// </summary>
    public class OperationCompletedEvent
    {
        public string Message { get; private set; }

        public OperationCompletedEvent(string message)
        {
            Message = message;
        }
        
    }
}
