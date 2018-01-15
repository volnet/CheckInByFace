using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckInbyFaceTests.PythonTest
{
    [TestClass()]
    public class Test
    {
        [TestMethod()]
        public void TestTests()
        {
            ScriptEngine pyEngine = Python.CreateEngine();//创建Python解释器对象
            var scope = pyEngine.CreateScope();
            var source = pyEngine.CreateScriptSourceFromFile("PythonTest\\test.py");
            source.Execute(scope);

            var say_hello = scope.GetVariable<Func<object>>("say_hello");
            say_hello();

            var get_text = scope.GetVariable<Func<object>>("get_text");
            var text = get_text().ToString();
            Assert.AreEqual("text from test.py", text);

            var add = scope.GetVariable<Func<object, object, object>>("add");
            var result1 = add(1, 2);
            Assert.AreEqual(3, result1);

            var result2 = add("hello ", "world");
            Assert.AreEqual("hello world", result2);
        }
    }
}
