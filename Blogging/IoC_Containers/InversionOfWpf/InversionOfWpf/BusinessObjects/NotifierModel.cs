using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using StructureMap;

namespace InversionOfWpf.BusinessObjects
{
    public class NotifierModel
    {
        public NotifierModel()
        {
            _Notifier = ObjectFactory.GetInstance<INotificationService>();
        }

        private INotificationService _Notifier;
        public void Notify(string message)
        {
            _Notifier.Notify(message);
        }
    }

    public class DialogNotifier : INotificationService
    {
        public void Notify(string message)
        {
            MessageBox.Show(message);
        }
    }

    public class TestableNotifier : INotificationService
    {
        public string Message;
        public void Notify(string message)
        {
            Message = message;
        }
    }

    public interface INotificationService
    {
        void Notify(string message);
    }
}
