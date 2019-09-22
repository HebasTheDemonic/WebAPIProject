using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodWebAPI.Controllers
{
    public class FoodController : ApiController
    {
        FoodDAO FoodDAO = new FoodDAO();

        // GET api/food
        public HttpResponseMessage Get()
        {
            if (FoodDAO.GetAll().Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.OK, FoodDAO.GetAll());
        }

        // GET api/food/2
        public HttpResponseMessage Get([FromUri]int id)
        {
            Food food = FoodDAO.Get(id);
            if (food == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.OK, food);
        }

        // GET api/food/ByName/{name}
        [Route("api/food/ByName/{name}")]
        [HttpGet]
        public HttpResponseMessage GetBySenderName([FromUri]string name)
        {
            List<Food> foods = FoodDAO.GetByName(name);
            if (foods.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.OK, foods);
        }

        //GET api/food/ByMinCal/{calories}
        [Route("api/food/ByMinCal/{calories}")]
        [HttpGet]
        public HttpResponseMessage GetBySenderCalories([FromUri]int calories)
        {
            List<Food> foods = FoodDAO.GetByAboveCalory(calories);
            if (foods.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.OK, foods);
        }

        //GET api/food/search
        // add ? sender = d & text = o
        [Route("api/food/search")]
        [HttpGet]
        public HttpResponseMessage SearchFoodsByCriteria(string name = "", int min_cal = 0, int max_cal = int.MaxValue, int min_grade = 0)
        {
            List<Food> foods = FoodDAO.SearchFoodsByCriteria(name, max_cal, min_cal, min_grade);
            if (foods == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.OK, foods);
        }

        public HttpResponseMessage Post([FromBody]Food food)
        {
            try
            {
                FoodDAO.AddFood(food);
                return Request.CreateResponse(HttpStatusCode.Created, food);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete (int id)
        {
            Food food = FoodDAO.Get(id);

            if(food == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Food Item With This ID Was Not Found.");
            }
            FoodDAO.RemoveFood(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put([FromBody]Food food)
        {
            try
            {
                Food entity = FoodDAO.Get(food.ID);
                if(entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Food With This ID Was Not Found");
                }
                else
                {
                    FoodDAO.UpdateFood(food);
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
