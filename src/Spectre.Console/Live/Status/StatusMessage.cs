using System;

namespace Spectre.Console
{
    /// <summary>
    /// Deterministic lifetime for a specific the <see cref="StatusContext.Status"/> message.
    /// Sets it to a new value in the constructor and sets it back in <see cref="Dispose"/>.
    /// </summary>
    public sealed class StatusMessage : IDisposable
    {
        private readonly StatusContext _ctx;
        private readonly string _originalMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusMessage"/> class.
        /// Sets the <see cref="StatusContext.Status"/> to a new value.
        /// </summary>
        /// <param name="ctx">The StatusContext instance.</param>
        /// <param name="msg">The new message text.</param>
        public StatusMessage(StatusContext ctx, string msg)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            if (string.IsNullOrEmpty(msg))
            {
                throw new ArgumentException($"{nameof(msg)} cannot be null or empty");
            }

            _originalMessage = _ctx.Status;
            _ctx.Status = msg;
        }

        /// <summary>
        /// Sets <see cref="StatusContext.Status"/> back to its startign value.
        /// </summary>
        public void Dispose()
        {
            _ctx.Status = _originalMessage;

            GC.SuppressFinalize(this);
        }
    }
}
