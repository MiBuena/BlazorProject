using System.Reflection;

namespace ListGenerator.Client.ViewModels
{
    public class Heading
    {
        public int Id { get; set; }

        public string ThTitle { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public TableHeading HeadingRule { get; set; }
    }
}
