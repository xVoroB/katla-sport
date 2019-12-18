using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using KatlaSport.Services.EmployeeManagement;
using KatlaSport.Services.Interfaces;
using KatlaSport.WebApi.CustomFilters;
using Microsoft.Web.Http;
using Swashbuckle.Swagger.Annotations;


namespace KatlaSport.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/positions")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    [SwaggerResponseRemoveDefaults]
    public class PositionsController : ApiController
    {
        private readonly IRepository<Position> _positionRepositoryService;

        public PositionsController(IRepository<Position> positionRepositoryService)
        {
            _positionRepositoryService = positionRepositoryService ?? throw new ArgumentNullException(nameof(positionRepositoryService));
        }

        [HttpGet]
        [Route("getAll")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list of positions.", Type = typeof(Position[]))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetAllPositionsAsync()
        {
            var positions = await _positionRepositoryService.GetAllEntitiesAsync();
            return Ok(positions);
        }
        [HttpGet]
        [Route("getSingle/{positionId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns position.", Type = typeof(Position[]))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetPositionAsync(int positionId)
        {
            var positions = await _positionRepositoryService.GetEntityAsync(positionId);
            return Ok(positions);
        }

        [HttpPost]
        [Route("create")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new position.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> AddPosition([FromBody] Position createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _positionRepositoryService.AddEntityAsync(createRequest);
            var positions = await _positionRepositoryService.GetAllEntitiesAsync();
            var position = positions.Last();
            var location = string.Format("/api/positions/getSingle/{0}", position.Id);
            return Ok();
        }

        [HttpPost]
        [Route("update/{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Updates an existed position.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdatePosition([FromUri] int id, [FromBody] Position updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            updateRequest.Id = id;
            await _positionRepositoryService.UpdateEntityAsync(updateRequest);
            return Ok(ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent)).ToString());
        }

        [HttpPost]
        [Route("delete/{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes an existed hive section.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeletePosition([FromUri] int id)
        {
            await _positionRepositoryService.RemoveEntityAsync(new Position() { Id = id });
            return Ok(ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent)).ToString());
        }

    }
}
