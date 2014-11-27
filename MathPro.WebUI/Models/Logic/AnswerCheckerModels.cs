using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathPro.WebUI.Models
{
    /// <summary>
    /// Simple answer checker interface
    /// </summary>
    public interface IAnswerChecker
    {
        bool CheckAnswer(string expected, string actual);
    }

    /// <summary>
    /// Very-very simple answer checker
    /// </summary>
    public class VerySimpleAnswerChecker : IAnswerChecker
    {
        public bool CheckAnswer(string expected, string actual)
        {
            return actual.Trim().Equals(expected.Trim());
        }
    }

    
}