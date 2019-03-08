using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EventDemo
{
    public class MailManage
    {
        public event EventHandler<NewMailEventArgs> NewMail;

        protected void OnNewMail(NewMailEventArgs e)
        {
            Volatile.Read(ref NewMail)?.Invoke(this, e);
        }

        public void SimulateNewMail(string from,string to, string subject)
        {
            NewMailEventArgs e = new NewMailEventArgs(from, to, subject);
            OnNewMail(e);
        }
    }
}
