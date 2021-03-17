using System;

namespace TryDecorator
{
    public class Notifier
    {
        public virtual void Send(string message)
        {
            Console.WriteLine($"Email sends {message}.");
        }
    }

    public class BaseDecorator : Notifier
    {
        private readonly Notifier _wrappee;

        public BaseDecorator(Notifier notifier)
        {
            _wrappee = notifier;
        }

        public override void Send(string message)
        {
            _wrappee.Send(message);
        }
    }

    public class SMSDecorator : BaseDecorator
    {
        public SMSDecorator(Notifier notifier) : base(notifier)
        {
        }

        public override void Send(string message)
        {
            base.Send(message);
            Console.WriteLine($"SMS sends {message}.");
        }
    }

    public class FacebookDecorator : BaseDecorator
    {
        public FacebookDecorator(Notifier notifier) : base(notifier)
        {
        }

        public override void Send(string message)
        {
            base.Send(message);
            Console.WriteLine($"Facebook sends {message}.");
        }
    }

    public class SlackDecorator : BaseDecorator
    {
        public SlackDecorator(Notifier notifier) : base(notifier)
        {
        }

        public override void Send(string message)
        {
            base.Send(message);
            Console.WriteLine($"Slack sends {message}.");
        }
    }

    public class MyApplication
    {
        private Notifier _notifier;

        public void SetNotifier(Notifier notifier)
        {
            _notifier = notifier;
        }

        public void DoSomething()
        {
            _notifier.Send("Alert!");
        }
    }
}