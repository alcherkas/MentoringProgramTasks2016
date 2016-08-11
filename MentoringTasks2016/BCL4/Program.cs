using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BCL4
{

    /// <summary>
    /// Allows to indicate that value wasn't set
    /// </summary>
    internal sealed class SurveyResult
    {
        public int? Age;
        public bool? IsStudent;
    }

    internal sealed class PaymentDetails
    {
        public int? Amount;
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<object> list = null;
            var listCount = list?.Count;
        }
    }
}
