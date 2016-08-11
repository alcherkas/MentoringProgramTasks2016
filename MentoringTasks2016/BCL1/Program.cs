using System;

namespace BCL1
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class CodeAuthorAttribute : Attribute
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    [CodeAuthor(Email = "email@site.com", Name = "Mor Qs")]

    class Program
    {

        private static TValue GetAttributeValue<TValue>(Type objectType, Type attributeType, string fieldName)
        {
            var attribute = Attribute.GetCustomAttribute(objectType, attributeType);
            return (TValue)GetPropertyValue(attribute, fieldName);
        }

        private static object GetPropertyValue(object source, string propertyName)
        {
            return source.GetType().GetProperty(propertyName).GetValue(source);
        }

        static void Main(string[] args)
        {
            Console.WriteLine(GetAttributeValue<string>(typeof (Program), typeof (CodeAuthorAttribute), nameof(CodeAuthorAttribute.Email)));
            Console.WriteLine(GetAttributeValue<string>(typeof (Program), typeof (CodeAuthorAttribute), nameof(CodeAuthorAttribute.Name)));
            Console.ReadLine();
        }
    }
}
