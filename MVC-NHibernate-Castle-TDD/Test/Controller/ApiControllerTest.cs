using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Service;
using Moq;
using System.Collections.Generic;
using Core.Entity;

namespace Test.Controller
{
    [TestClass]
    public class ApiControllerTest
    {
        private Mock<IClienteService> _mockClienteService;
        private WebAPI.Controllers.ApiController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockClienteService = new Mock<IClienteService>();
            _controller = new WebAPI.Controllers.ApiController(_mockClienteService.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockClienteService.VerifyAll();
        }

        [TestMethod]
        public void GetTodosClientes_ExpectIEnumerableOfClienteReturned()
        {
            List<Cliente> clientes = new List<Cliente>();
            _mockClienteService.Setup(x => x.GetAll()).Returns(clientes).Verifiable();

            var result = _controller.GetTodosClientes();

            Assert.AreEqual(clientes, result);
        }

        [TestMethod]
        public void GetClientes_ExpectClienteReturned()
        {
            Cliente cliente = new Cliente();
            _mockClienteService.Setup(x => x.Get(1)).Returns(cliente).Verifiable();

            var result = _controller.GetCliente(1);

            Assert.AreEqual(cliente, result);
        }

        [TestMethod]
        public void AddEdit_ExpectHttpResponseMessageReturned()
        {
            Cliente cliente = new Cliente { Id = 0,
                                            Nome = "Teste",
                                            Telefone = "7987",
                                            Endereco = "Rua A" };

            _mockClienteService.Setup(x => x.Create(cliente)).Returns(true);
            _mockClienteService.Setup(x => x.Update(cliente)).Returns(true);

            var resultCreate = _mockClienteService.Object.Create(cliente);
            var resultUpdate = _mockClienteService.Object.Update(cliente);

            Assert.AreEqual(resultCreate, resultUpdate);
        }

        [TestMethod]
        public void Delete_ExpectHttpResponseMessageReturned()
        {
            _mockClienteService.Setup(x => x.Delete(1)).Returns(true);
            var result = _mockClienteService.Object.Delete(1);

            Assert.IsTrue(result);
        }

    }
}
