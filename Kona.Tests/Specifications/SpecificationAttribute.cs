using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Specifications
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SpecificationAttribute : FactAttribute
    {
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(MethodInfo method)
        {
            try
            {
                object obj = Activator.CreateInstance(method.ReflectedType);
                method.Invoke(obj, null);
                return SpecificationContext.ToTestCommands(method);
            }
            catch (Exception ex)
            {
                return new ITestCommand[] { new ExceptionTestCommand(method, ex) };
            }
        }
    }
}