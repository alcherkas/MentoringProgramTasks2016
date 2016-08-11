using System;

namespace BCL4
{

    /// <summary>
    /// Allows to indicate that value wasn't set
    /// </summary>
    internal sealed class SurveyResult
    {
        public int? Age;
        public bool? IsStudent;

        public override string ToString()
        {
            return string.Format("Age: {0} \nStudent: {1}", Age?.ToString() ?? "Unknown",
                IsStudent.HasValue ? IsStudent.Value ? "Y" : "N" : "Unknown");
        }
    }

    internal sealed class PaymentDetails
    {
        public int? Amount;
    }

    class Program
    {
        struct IpAddressv4
        {
            public byte First;
            public byte Second;
            public byte Third;
            public byte Fourth;

            public IpAddressv4(byte first, byte second, byte third, byte fourth)
            {
                First = first;
                Second = second;
                Third = third;
                Fourth = fourth;
            }

            public override string ToString() => string.Format("{0}:{1}:{2}:{3}", First, Second, Third, Fourth);
        }
        private static IpAddressv4? FindUser()
        {
            if (DateTime.Now.Hour < 18)
            {
                return null;
            }

            return new IpAddressv4(12, 31, 120, 7);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Please, enter your age");
            var ageStr = Console.ReadLine()?.Trim();
            SurveyResult surveyResult = new SurveyResult();
           
            if (!string.IsNullOrEmpty(ageStr))
            {
                int age;
                if (int.TryParse(ageStr, out age))
                {
                    surveyResult.Age = age;
                }
            }

            Console.WriteLine("Are you student? (Y/N)");
            var answer = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(answer))
            {
                switch (answer)
                {
                    case "Y":
                        surveyResult.IsStudent = true;
                        break;
                    case "N":
                        surveyResult.IsStudent = false;
                        break;
                }
            }


            Console.WriteLine("RESULTS:");
            Console.WriteLine(surveyResult);


            Console.WriteLine(FindUser());
            Console.ReadLine();
        }
    }
}
