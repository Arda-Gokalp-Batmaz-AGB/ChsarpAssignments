using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;

namespace Assignment_4 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        [System.AttributeUsage(System.AttributeTargets.Class)]
        public class TestContainerAttribute : System.Attribute
        {
            public TestContainerAttribute()
            {
            }
        }
        [System.AttributeUsage(System.AttributeTargets.Method)]

        public class TestMethodAttribute : System.Attribute
        {
            private string description;
            public string Description
            {
                get
                {
                    if (description == null || description == "")
                    {
                        return "Description is empty.";
                    }
                    return description;
                }
                set { description = value; }
            }
            public TestMethodAttribute()
            {
            }
        }
        [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method)]

        public class OrderAttribute : System.Attribute
        {
            public int Order;
            public OrderAttribute(int Order)
            {
                this.Order = Order;
            }
        }

        [TestContainer]
        public class TestClass1
        {
            [TestMethod(Description = "TestClass1: Test Method 1."), Order(3)]
            public void Test1() { }

            [TestMethod(Description = "TestClass1: Test Method 2."), Order(2)]
            public void Test2() { }

            [TestMethod(Description = "TestClass1: Test Method 3."), Order(1)]
            public void Test3() { }

        }

        [TestContainer, Order(1)]
        public class TestClass2
        {
            [TestMethod(Description = "TestClass2: Test Method 1.")]
            public void Test1() { }

            [TestMethod, Order(1)]
            public void Test2() { }
        }

        public static IEnumerable<MethodInfo> createMethodPrintOrder(Type currentClass)
        {
            var methods = currentClass.GetMethods()
                .Where(x => x.IsDefined(typeof(TestMethodAttribute)));
            var methodsWithOrder = methods
                .Where(x => x.IsDefined(typeof(OrderAttribute)));
            var methodsWithoutOrder = methods
                .Where(x => !x.IsDefined(typeof(OrderAttribute)));
            var ordersSorted = methodsWithOrder
                .OrderBy(x => x.GetCustomAttribute<OrderAttribute>().Order);
            var printOrder = ordersSorted.Concat(methodsWithoutOrder);
            return printOrder;
        }
        public static IEnumerable<Type> createClassPrintOrder(Type[] allTypes)
        {
            var classesWithTestContainer = allTypes
                    .Where(x => x.IsDefined(typeof(TestContainerAttribute)));
            var classesWithOrder = classesWithTestContainer
                .Where(x => x.IsDefined(typeof(OrderAttribute)));
            var classesWithoutOrder = classesWithTestContainer
                .Where(x => !x.IsDefined(typeof(OrderAttribute)));
            var ordersSorted = classesWithOrder
                .OrderBy(x => x.GetCustomAttribute<OrderAttribute>().Order);
            var printOrder = ordersSorted.Concat(classesWithoutOrder);
            return printOrder;
        }
        public static void printController(Type[] allTypes)
        {
            var classesWithTestContainer = createClassPrintOrder(allTypes);
            foreach (var item in classesWithTestContainer)
            {
                var methodPrintOrder = createMethodPrintOrder(item);
                Array.ForEach(methodPrintOrder.ToArray(), x =>
                Console.WriteLine(item.Name + "." + x.Name + " => \"" + x.GetCustomAttribute<TestMethodAttribute>().Description+ "\""));

            }

        }

        static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var allTypes = assembly.GetTypes();
            printController(allTypes);
        }
    }
}