using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListGenerator.Web.UnitTests
{
    public class AssertAllTest
    {
        protected void AssertAll(params Action[] rules)
        {
            var messages = new StringBuilder();

            messages.AppendLine();

            var exceptions = new List<Exception>();

            foreach (var rule in rules)
            {
                try
                {
                    rule();
                }
                catch (Exception exception)
                {
                    messages.AppendLine("----Assert Failed----");

                    messages.AppendLine(exception.Message);

                    exceptions.Add(exception);
                }
            }

            if (exceptions.Any())
            {
                messages.AppendLine();

                throw new AggregateException(messages.ToString());
            }
        }
    }
}
