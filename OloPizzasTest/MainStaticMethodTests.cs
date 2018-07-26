using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using OloPizzas;

namespace OloPizzasTest
{
    [TestFixture]
    public class MainStaticMethodTests
    {
        public class ConsoleOutput : IDisposable
        {
            #region Fields

            private readonly TextWriter _originalOutput;
            private readonly StringWriter _stringWriter;

            #endregion

            #region Constructors

            public ConsoleOutput()
            {
                _stringWriter = new StringWriter();
                _originalOutput = Console.Out;
                Console.SetOut(_stringWriter);
            }

            #endregion

            #region Public Methods

            public void Dispose()
            {
                Console.SetOut(_originalOutput);
                _stringWriter.Dispose();
            }

            public string GetOuput() => _stringWriter.ToString();

            #endregion
        }

        public class LocalProgram : Program
        {
            #region Public Methods

            public static async Task<string> GetJson() => await GetJsonAsync();

            public static async Task<IOrderedEnumerable<IGrouping<string, Toppings>>> GroupAndOrder(IEnumerable<Toppings> toppings) => await GroupAndOrderAsync(toppings);

            public static void MyOutput(IEnumerable<IGrouping<string, Toppings>> toppings)
            {
                Output(toppings);
            }

            #endregion
        }

        private static string ReadJsonFile(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(path))
            {
                if (stream == null)
                {
                    return string.Empty;
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        [Test]
        public async Task GetJsonTest()
        {
            var expected = ReadJsonFile("OloPizzasTest.pizzas.json");
            var actual = await LocalProgram.GetJson();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GroupAndOrderTest()
        {
            var toppings = JsonConvert.DeserializeObject<List<Toppings>>(ReadJsonFile("OloPizzasTest.simple.json"));
            var groupedAndOrdered = (await LocalProgram.GroupAndOrder(toppings)).ToList();

            Assert.AreEqual(5, groupedAndOrdered.Count);
            Assert.AreEqual("pepperoni", groupedAndOrdered[0].Key);
            Assert.AreEqual("bacon", groupedAndOrdered[1].Key);
            Assert.AreEqual("feta cheese", groupedAndOrdered[2].Key);
            Assert.AreEqual("beef, sausage", groupedAndOrdered[3].Key);
            Assert.AreEqual("bacon, beef, mozzarella cheese, onions, pineapple", groupedAndOrdered[4].Key);

            Assert.AreEqual(5, groupedAndOrdered[0].Count());
            Assert.AreEqual(4, groupedAndOrdered[1].Count());
            Assert.AreEqual(3, groupedAndOrdered[2].Count());
            Assert.AreEqual(2, groupedAndOrdered[3].Count());
            Assert.AreEqual(1, groupedAndOrdered[4].Count());
        }

        [Test]
        public async Task OutputTest()
        {
            const string expected = "1 pepperoni 5\r\n2 bacon 4\r\n3 feta cheese 3\r\n4 beef, sausage 2\r\n5 bacon, beef, mozzarella cheese, onions, pineapple 1\r\n";
            var toppings = JsonConvert.DeserializeObject<List<Toppings>>(ReadJsonFile("OloPizzasTest.simple.json"));
            var groupedAndOrdered = (await LocalProgram.GroupAndOrder(toppings)).ToList();

            using (var consoleOutput = new ConsoleOutput())
            {
                LocalProgram.MyOutput(groupedAndOrdered);

                Assert.AreEqual(expected, consoleOutput.GetOuput());
            }
        }
    }
}