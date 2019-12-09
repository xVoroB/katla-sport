using KatlaSport.Services.EmployeeManagement;
using KatlaSport.Services.Interfaces;
using KatlaSport.WebApi.CustomFilters;
using Microsoft.Web.Http;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace KatlaSport.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/employee")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    [SwaggerResponseRemoveDefaults]
    public class EmloyeesController : ApiController
    {
        private readonly IRepository<Employee> _employeeRepositoryService;
        private readonly IRepository<EmployeeCV> _employeeCVRepositoryService;

        public EmloyeesController(IRepository<Employee> employeeRepositoryService, IRepository<EmployeeCV> employeeCVRepositoryService)
        {
            _employeeRepositoryService = employeeRepositoryService ?? throw new ArgumentNullException(nameof(employeeRepositoryService));
            _employeeCVRepositoryService = employeeCVRepositoryService ?? throw new ArgumentNullException(nameof(employeeCVRepositoryService));
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns all employees.", Type = typeof(Employee[]))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetEmployees()
        {
            var employees = await _employeeRepositoryService.GetAllEntitiesAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("{employeeId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns an employee entity.", Type = typeof(Employee))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetEmployee(int employeeId)
        {
            var employee = await _employeeRepositoryService.GetEntityAsync(employeeId);
            return Ok(employee);
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new employee entity.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> AddEmployee([FromBody] UpdateEmployeeRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           // var employee = await _employeeRepositoryService.AddEntityAsync(createRequest);

            return Ok(); 
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Updates an existing employee.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdateEmployee([FromUri] int id, [FromBody] UpdateEmployeeRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes an existing employee entity.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeleteEmployee([FromUri] int id)
        {
            await _employeeRepositoryService.RemoveEntityAsync(new Employee() { Id = id });
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}