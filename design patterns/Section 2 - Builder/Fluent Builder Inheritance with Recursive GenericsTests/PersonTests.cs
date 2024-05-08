using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fluent_Builder_Inheritance_with_Recursive_Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;
using System.Diagnostics.CodeAnalysis;

namespace Fluent_Builder_Inheritance_with_Recursive_Generics.Tests
{
    [TestClass()]
    public class PersonTests
    {
        [TestMethod()]
        public void fooTest()
        {
            var p = new Person();
            int result = p.Foo(1);
            // expect(result).toEqual(2);
        }
    }
}