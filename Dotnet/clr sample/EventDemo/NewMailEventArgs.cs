using System;
using System.Collections.Generic;
using System.Text;

namespace EventDemo
{
    //
    // EventArgs摘要:
    //     Represents the base class for classes that contain event data, and provides a
    //     value to use for events that do not include event data.
    public class NewMailEventArgs:EventArgs
    {
        private readonly string m_from, m_to, m_subject;

        public NewMailEventArgs(string from, string to, string subject)
        {
            m_from = from;
            m_to = to;
            m_subject = subject;
        }

        public string From => m_from;

        public string To => m_to;

        public string Subject => m_subject;
    }
}
