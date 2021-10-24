using Kastra.Admin.Core.Enums;
using System;

namespace Kastra.Core.Components
{
    public class ToastDefinition
    {
        /// <summary>
        /// Toast Id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Toast message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Time (in milliseconds) to hide the toast.
        /// </summary>
        public int? Timer { get; set; }

        /// <summary>
        /// If the toast is displayed.
        /// </summary>
        public bool IsDisplayed { get; set; }

        /// <summary>
        /// Toast type.
        /// </summary>
        public ToastEnum Type { get; set; }

        public ToastDefinition(string message, ToastEnum type, int? timer = 10000)
        {
            Id = Guid.NewGuid();
            IsDisplayed = true;
            Type = type;
            Message = message;
            Timer = timer;
        }
    }
}
