using RESTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controller
{
    public class ListItemsController : ApiController
    {
        private static List<People> listItems { get; set; } = new List<People>();
        public IEnumerable<People> Get()
        {
            return listItems;
        }

        public HttpResponseMessage Get(int id)
        {
            var item = listItems.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Post([FromBody] People model)
        {
            if (string.IsNullOrEmpty(model?.name))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var maxId = 0;

            if (listItems.Count > 0)
            {
                maxId = listItems.Max(x => x.id);
            }
            model.id = maxId + 1;
            listItems.Add(model);
            return Request.CreateResponse(HttpStatusCode.Created, model);
        }

        public HttpResponseMessage Put(int id, [FromBody] People model)
        {
            if (string.IsNullOrEmpty(model?.name))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var item = listItems.FirstOrDefault(x => x.id == id);

            if (item != null)
            {
                item.name = model.name;
                item.city = model.city;
                item.year = model.year;

                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Delete(int id)
        {
            var item = listItems.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                listItems.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}