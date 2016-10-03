using Core.Entity;
using Core.Factory;
using Core.Service;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            List<Cliente> clientes = new List<Cliente>();
            var url = string.Format("{0}{1}/api/api/gettodosclientes", "http://", Request.Url.Authority);
            var response = ExecutaRequisicao(url);

            if (response.IsSuccessStatusCode)
                clientes = response.Content.ReadAsAsync<List<Cliente>>().Result;

            return View(clientes);
        }

        public ActionResult Add()
        {
            return View(new Cliente());
        }

        public ActionResult Edit(int id)
        {
            Cliente cliente = new Cliente();
            var url = string.Format("{0}{1}/api/api/getcliente?Id={2}", "http://", Request.Url.Authority, id);
            var response = ExecutaRequisicao(url);

            if (response.IsSuccessStatusCode)
                cliente = response.Content.ReadAsAsync<Cliente>().Result;

            return View(cliente);
        }

        public HttpResponseMessage ExecutaRequisicao(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "p57ba741-7803-54cb-c8u4-ef9587129685");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client.GetAsync(url).Result;
        }

    }
}