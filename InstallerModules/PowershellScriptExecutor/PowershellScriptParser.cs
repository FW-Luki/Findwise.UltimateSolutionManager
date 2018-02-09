using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PowershellScriptExecutor
{
    /// <summary>
    /// Provides simple functionalities of getting parameters andfunctions from PowerShell script. See Remarks for limitations.
    /// </summary>
    /// <remarks>
    /// Limitations:
    ///  - Values cannot contain commas (',')
    ///  - Values cannot contain brackets ('[',']')
    ///  - Values cannot contain equality signs ('=')
    ///  - Values are trimmed (stripped from leading and trailing whitespaces)
    ///  - All parameter attributes are ignored (they will still be processed by PowerShell)
    ///  - Param definition must be first non-comment non-empty line
    /// </remarks>
    internal class PowershellScriptParser
    {
        private const string CommentToken = "#";
        private const char OpeningBracket = '(';
        private const char ClosingBracket = ')';
        private const char ValueAssignOperator = '=';
        private const char ParameterSeparator = ',';
        private const char VariableQualifier = '$';

        public static IEnumerable<Parameter> GetParameters(string script)
        {
            var text = Normalize(RemoveComments(script));
            var paramToken = @"param\s*\(";
            var paramsMatch = Regex.Match(text, paramToken, RegexOptions.IgnoreCase).Captures.Cast<Capture>().FirstOrDefault();
            if (paramsMatch != null)
            {
                var paramsPos = paramsMatch.Index + paramsMatch.Length;
                var paramsLength = -1;
                var parenthesisOpen = 1;
                for (int i = paramsPos; i < text.Length; i++)
                {
                    if (text[i] == OpeningBracket) parenthesisOpen++;
                    if (text[i] == ClosingBracket) parenthesisOpen--;
                    if (parenthesisOpen == 0)
                    {
                        paramsLength = i - paramsPos;
                        break;
                    }
                }
                if (paramsLength > 0)
                {
                    var paramsText = text.Substring(paramsPos, paramsLength).Replace(Environment.NewLine, string.Empty);
                    return paramsText.Split(new[] { ParameterSeparator }).Select(parameter =>
                    {
                        var attributelessParameter = Regex.Replace(parameter, @"\[.*\]", string.Empty);
                        var parameterTokens = attributelessParameter.Split(new[] { ValueAssignOperator }, 2).Select(s => s.Trim());
                        return new Parameter()
                        {
                            Name = parameterTokens.ElementAt(0).TrimStart(VariableQualifier),
                            Value = parameterTokens.Count() > 1 ? parameterTokens.ElementAt(1) : null
                        };
                    });
                }
            }
            return Enumerable.Empty<Parameter>();
        }

        private static string Normalize(string text)
        {
            return Regex.Replace(text, @"\r\n|\n|\r", Environment.NewLine);
            //return text.Replace("\r", "").Replace("\n", "");
        }

        private static string RemoveComments(string text, string commentToken = CommentToken, string commentTerminator = null)
        {
            return Regex.Replace(text, string.IsNullOrEmpty(commentTerminator) ? $@"{commentToken}.*" : $@"{commentToken}.*?{commentTerminator}", string.Empty);
        }
    }
}
