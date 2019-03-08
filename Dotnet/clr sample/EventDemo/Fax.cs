using System;
using System.Collections.Generic;
using System.Text;

namespace EventDemo
{
    public class Fax
    {
        public Fax(MailManage mm)
        {
            mm.NewMail += FaxMsg;
        }

        /// <summary>
        /// 方法必须符合事件委托的规范
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FaxMsg(Object sender, EventArgs e)
        {
            Console.WriteLine($"Fax收到转发：{((NewMailEventArgs)e).From}+");
        }

        public void Unregister(MailManage mm)
        {
            mm.NewMail -= FaxMsg;
        }
    }
}
