using System;
using System.Linq.Expressions;

namespace Specifications
{

    public class Description<T> : IDisposable where T:new() {
        public Description() {



        }
        public void It(string message, Action should){

        }        
        
        #region IDisposable Members

        public void Dispose() {
            throw new NotImplementedException();
        }

        #endregion
    }


    public static class SpecificationExtensions
    {
        public static void Context(this string message, Action arrange)
        {

            Console.WriteLine(message);
            SpecificationContext.Context(message, arrange);
        }
        public static void Given(this string message, Action arrange) {

            Console.WriteLine(message);
            SpecificationContext.Context(message, arrange);
        }

        public static void Do(this string message, Action act)
        {
            Console.Write(">>> "+message);
            SpecificationContext.Do(message, act);
        }
        public static void When(this string message, Action act) {
            Console.Write(">>> " + message);
            SpecificationContext.Do(message, act);
        }

        public static void Assert(this string message, Action assert)
        {
            Console.WriteLine(": " + message);
            SpecificationContext.Assert(message, assert);
        }
        public static void Then(this string message, Action assert) {
            Console.WriteLine(": " + message);
            SpecificationContext.Assert(message, assert);
        }

    }
}