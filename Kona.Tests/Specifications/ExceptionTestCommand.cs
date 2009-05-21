using System;
using System.Reflection;
using Xunit.Sdk;
using System.Xml;

namespace Specifications
{
    public class ExceptionTestCommand : ITestCommand
    {
        readonly Exception exception;
        readonly MethodInfo method;

        public ExceptionTestCommand(MethodInfo method, Exception exception)
        {
            this.method = method;
            this.exception = exception;
        }

        public string Name
        {
            get { return null; }
        }

        public bool ShouldCreateInstance
        {
            get { return false; }
        }

        public MethodResult Execute(object testClass)
        {
            return new FailedResult(method, exception, Name);
        }

        public string DisplayName
        {
            get { throw new NotImplementedException(); }
        }

        public virtual XmlNode ToStartXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<dummy/>");
            XmlNode testNode = XmlUtility.AddElement(doc.ChildNodes[0], "start");

            string typeName = method.ReflectedType.FullName;
            string methodName = method.Name;

            XmlUtility.AddAttribute(testNode, "name", typeName + "." + methodName);
            XmlUtility.AddAttribute(testNode, "type", typeName);
            XmlUtility.AddAttribute(testNode, "method", methodName);

            return testNode;
        }
    }
}