using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CoreRCON;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;

/*
 * Run tests against a running RCON server
 * Configure settings before running
 */

namespace CoreRCON.Tests
{
    [TestClass]
    public class UnitTest
    {

        RCON rconClient;
        //Connection settings for server
        private readonly IPAddress _ip = IPAddress.Parse("127.0.0.1");
        private readonly ushort _port = 27807;
        private readonly string _password = "rcon";

        [TestCleanup]
        public void testClean()
        {
            rconClient.Dispose();
        }

        [TestInitialize]
        public async Task testInitAsync()
        {
            rconClient = new RCON(_ip, _port, _password);
            await rconClient.ConnectAsync();

        }


        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public async Task testBadAuthAsync()
        {
            rconClient.Dispose();
            rconClient = new RCON(_ip, _port, "wrong PW");
            await rconClient.ConnectAsync();
        }

        [TestMethod]
        public async Task testEmptyResponseAsync()
        {
            string response = await rconClient.SendCommandAsync("//comment");
            Assert.AreEqual("", response);
        }

        [TestMethod]
        public async Task testEchoAsync()
        {
            string response = await rconClient.SendCommandAsync("say hi");
            Assert.AreEqual("Console: hi", response);
        }


        [TestMethod]
        public async Task testLongResponseAsync()
        {
            string response = await rconClient.SendCommandAsync("cvarList");
            Assert.IsTrue(response.EndsWith("total convars/concommands"));
        }


        [TestMethod]
        public async Task testMultipleCommands()
        {
            for (int i = 0; i < 10; i++)
            {
                string response = await rconClient.SendCommandAsync($"say {i}");
                Assert.AreEqual($"Console: {i}", response);
            }
        }

        [TestMethod]
        public async Task testCommandsConcurent()
        {
            List<Task> tasks = new List<Task>();

            tasks = Enumerable.Range(1, 10)
                .Select(async (i) =>
                {
                    string response = await rconClient.SendCommandAsync($"say {i}");
                    Console.WriteLine($"recived response {i} : {response}");
                    Assert.AreEqual($"Console: {i}", response);
                }).ToList();
            //Parallel.ForEach(tasks, task => task.Start());
            await Task.WhenAll(tasks);
            Console.Out.Flush();
        }

    }
}