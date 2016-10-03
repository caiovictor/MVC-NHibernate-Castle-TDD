using Core.Entity;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApiController : System.Web.Http.ApiController
    {

        private readonly IClienteService _clienteService;

        public ApiController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public IEnumerable<Cliente> GetTodosClientes()
        {
            return _clienteService.GetAll();
        }

        [HttpPost]
        public Cliente GetCliente(int Id)
        {
            return _clienteService.Get(Id);
        }

        [HttpPost]
        public HttpResponseMessage AddEdit(int Id, string Nome, string Telefone, string Endereco)
        {
            bool statusCliente = false;
            Cliente cliente = new Cliente{ Id = Id, Nome = Nome, Telefone = Telefone, Endereco = Endereco };

            if (cliente.Id == 0)
                statusCliente = _clienteService.Create(cliente);
            else
                statusCliente = _clienteService.Update(cliente);

            if (statusCliente)
                return Request.CreateResponse(HttpStatusCode.OK, cliente);
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro.");

        }

        [HttpPost]
        public HttpResponseMessage Delete(int Id)
        {
            if (_clienteService.Delete(Id))
                return Request.CreateResponse(HttpStatusCode.OK, "Excluído com sucesso.");
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro");
        }

    }
}
