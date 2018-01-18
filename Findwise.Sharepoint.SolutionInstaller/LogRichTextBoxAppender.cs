using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Util;

namespace Findwise.Sharepoint.SolutionInstaller
{
    /// <summary>
    /// Appender logging events in <see cref="System.Windows.Forms.RichTextBox"/> control.
    /// </summary>
    public class LogRichTextBoxAppender : IAppender
    {
        /// <summary>
        /// A <see cref="System.Windows.Forms.RichTextBox"/> control in which events should be written.
        /// </summary>
        public RichTextBox RichTextBox { get; set; } = null;

        /// <summary>
        /// Gets or sets the name of this appender.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Configures appender with the name specified to log events in specified <see cref="System.Windows.Forms.RichTextBox"/> control.
        /// </summary>
        /// <param name="appenderName">A name of the appender to configure.</param>
        /// <param name="richTextBox">A <see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        public static void Configure(string appenderName, RichTextBox richTextBox)
        {
            var appender = LogManager.GetAllRepositories().SelectMany(r => r.GetAppenders()).OfType<LogRichTextBoxAppender>().FirstOrDefault(a => a.Name == appenderName);
            if (appender != null) appender.RichTextBox = richTextBox;
        }

        public void Close()
        {
        }

        public void DoAppend(LoggingEvent loggingEvent)
        {
            string text;
            if (loggingEvent.ExceptionObject != null)
                text = $"{loggingEvent.RenderedMessage} - [{loggingEvent.ExceptionObject?.Message}]";
            else
                text = loggingEvent.RenderedMessage;

            if (RichTextBox != null)
            {
                UpdateText(text);
            }
        }
        private void UpdateText(string text)
        {
            if (RichTextBox.InvokeRequired)
            {
                RichTextBox.Invoke(new MethodInvoker(() => UpdateText(text)));
            }
            else
            {
                if (RichTextBox.TextLength > 0) RichTextBox?.AppendText(Environment.NewLine);
                RichTextBox.AppendText(text);
                RichTextBox.ScrollToCaret();
            }
        }


        //protected override void Append(LoggingEvent loggingEvent)
        //{
        //    var levelMapping = (LevelMapping)typeof(ColoredConsoleAppender).GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(f => f.GetType() == typeof(LevelMapping)).FirstOrDefault()?.GetValue(this);
        //    //if (levelMapping?.Lookup(loggingEvent.Level) is LevelColors levelColors)
        //    //{
        //    //}
        //    string text = base.RenderLoggingEvent(loggingEvent);
        //    RichTextBox?.AppendText(text);
        //}

    }
}
