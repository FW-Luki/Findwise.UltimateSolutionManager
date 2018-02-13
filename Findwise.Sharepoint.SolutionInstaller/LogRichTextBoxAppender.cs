using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    public class LogRichTextBoxAppender : AppenderSkeleton
    {
        private LevelMapping levelMapping = new LevelMapping();

        /// <summary>
        /// A <see cref="System.Windows.Forms.RichTextBox"/> control in which events should be written.
        /// </summary>
        public RichTextBox RichTextBox { get; set; } = null;

        /// <summary>
        /// Configures appender with the name specified to log events in specified <see cref="System.Windows.Forms.RichTextBox"/> control.
        /// </summary>
        /// <param name="appenderName">A name of the appender to configure.</param>
        /// <param name="richTextBox">A <see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="maxTextLength">Maximum text length. After exceeding this threshold the oldest entries will be removed from <see cref="System.Windows.Forms.RichTextBox"/>.</param>
        public static void Configure(string appenderName, RichTextBox richTextBox, int maxTextLength = 100000)
        {
            var appender = LogManager.GetAllRepositories().SelectMany(r => r.GetAppenders()).OfType<LogRichTextBoxAppender>().FirstOrDefault(a => a.Name == appenderName);
            if (appender != null) appender.RichTextBox = richTextBox;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (RichTextBox.InvokeRequired)
            {
                RichTextBox.Invoke(new MethodInvoker(() => UpdateText(loggingEvent)));
            }
            else
            {
                UpdateText(loggingEvent);
            }
        }
        private void UpdateText(LoggingEvent loggingEvent)
        {
            if (levelMapping.Lookup(loggingEvent.Level) is ColoredConsoleAppender.LevelColors selectedStyle)
            {
                RichTextBox.Select(RichTextBox.TextLength, 0);
                RichTextBox.SelectionColor = selectedStyle.ForeColor.ToColor();
            }
            RichTextBox.AppendText(RenderLoggingEvent(loggingEvent));

            //if (RichTextBox.TextLength > 0) RichTextBox?.AppendText(Environment.NewLine);
            //RichTextBox.AppendText(text);
            RichTextBox.ScrollToCaret();
        }
        //private string RenderLoggingEventInternal(LoggingEvent loggingEvent)
        //{
        //    var shb = new StringBuilder(loggingEvent.RenderedMessage);
        //    if (loggingEvent.ExceptionObject != null)
        //    {
        //        loggingEvent.GetExceptionString
        //        shb.AppendLine();
        //        shb.Append(loggingEvent.ExceptionObject.Message);
        //    }
        //    return shb.ToString();
        //}

        public void AddMapping(ColoredConsoleAppender.LevelColors mapping)
        {
            levelMapping.Add(mapping);
        }

        public override void ActivateOptions()
        {
            base.ActivateOptions();
            levelMapping.ActivateOptions();
        }

        protected override bool RequiresLayout { get { return true; } }

    }
    public static class LogRichTextBoxAppenderHelpers
    {
        public static Color ToColor(this ColoredConsoleAppender.Colors colors)
        {
            var anyComponent = (colors & ColoredConsoleAppender.Colors.White) != 0;
            var high = colors.HasFlag(ColoredConsoleAppender.Colors.HighIntensity);
            if (colors.HasFlag(ColoredConsoleAppender.Colors.White))
            {
                var whiteComponent = high ? 255 : 170;
                return Color.FromArgb(whiteComponent, whiteComponent, whiteComponent);
            }
            else
            {
                return Color.FromArgb(GetComponentValue(colors.HasFlag(ColoredConsoleAppender.Colors.Red), high, anyComponent),
                                      GetComponentValue(colors.HasFlag(ColoredConsoleAppender.Colors.Green), high, anyComponent),
                                      GetComponentValue(colors.HasFlag(ColoredConsoleAppender.Colors.Blue), high, anyComponent));
            }
        }
        private static int GetComponentValue(bool thisComponent, bool intensified, bool anyComponent)
        {
            return anyComponent ?
                (thisComponent ? (128 + (intensified ? 127 : 0)) : 0) :
                (intensified ? 85 : 0);
        }
    }

    //public class LevelTextStyle : LevelMappingEntry
    //{
    //    public Color ForeColor { get; set; }
    //    public Color BackColor { get; set; }
    //}


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