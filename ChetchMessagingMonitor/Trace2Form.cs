using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;

namespace ChetchMessagingMonitor
{
    class TraceOutput : Chetch.Utilities.DataSourceObject
    {
        public String ID { get { return Get<String>(); } set { Set(value); } }
        public String Date { get { return Get<String>(); } set { Set(value); } }
        public String Line { get { return Get<String>(); } set { Set(value); } }
    }

    class Trace2Form : TraceListener
    {
        const int MAX_LINES = 256;


        public long LineCount { get; internal set; } = 0;
        public BindingList<TraceOutput> Lines { get; } = new BindingList<TraceOutput>();

        public Trace2Form()
        {
            
        }

        public override void Write(string message)
        {
            LineCount++;

            TraceOutput traceOutput = new TraceOutput();
            traceOutput.ID = LineCount.ToString();
            traceOutput.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            traceOutput.Line = message;

            Lines.Add(traceOutput);
            if(Lines.Count > MAX_LINES)
            {
                Lines.RemoveAt(0);
            }
        }

        public override void WriteLine(string message)
        {
            Write(message);
        }
    }

}
