using KatlaSport.Services.EmployeeManagement;
using KatlaSport.Services.Interfaces;
using KatlaSport.WebApi.CustomFilters;
using Microsoft.Web.Http;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace KatlaSport.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/employees")]
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

        private EmployeeListItem EmployeeToListItem(Employee employee)
        {
            return new EmployeeListItem()
            {
                Id = employee.Id,
                Name = employee.Name,
                DateBirth = employee.DateBirth,
                EmployeeCVId = employee.EmployeeCV.Id,
                ChiefEmployeeName = (employee.ChiefEmployee != null) ? employee.ChiefEmployee.Name : "Employee doesn't have chief",
                PositionName = employee.Position.Name
            };
        }

        private Employee EmployeeToUpdateOrAdd(UpdateEmployeeRequest entity)
        {
            return new Employee()
            {
                Id = entity.Id,
                Position = new Position() { Id = entity.PositionId },
                EmployeeCV = new EmployeeCV() { Id = entity.EmployeeCVId },
                Name = entity.Name,
                DateBirth = entity.DateBirth,
                ChiefEmployee = entity.ChiefEmployeeId == 0 ? null : new Employee() { Id = entity.ChiefEmployeeId }
            };
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns all employees.", Type = typeof(Employee[]))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetEmployees()
        {
            var employees = await _employeeRepositoryService.GetAllEntitiesAsync();
            List<EmployeeListItem> employeeListItem = new List<EmployeeListItem>();
            foreach (var employee in employees)
            {
                employeeListItem.Add(EmployeeToListItem(employee));
            }
            return Ok(employeeListItem);
        }

        [HttpGet]
        [Route("{employeeId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns an employee entity.", Type = typeof(Employee))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetEmployee(int employeeId)
        {
            var employee = await _employeeRepositoryService.GetEntityAsync(employeeId);
            return Ok(EmployeeToListItem(employee));
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

            await _employeeRepositoryService.AddEntityAsync(EmployeeToUpdateOrAdd(createRequest));

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

            await _employeeRepositoryService.UpdateEntityAsync(EmployeeToUpdateOrAdd(updateRequest));

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

        private async Task SaveFile(int id, HttpPostedFile postedFile)
        {
            string cvName = null;
            if (postedFile != null)
            {
                cvName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                cvName = cvName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);

                await UploadFileToStorage(postedFile.InputStream, cvName);
                await _employeeCVRepositoryService.AddEntityAsync(new EmployeeCV()
                {
                    File = await FileToBytes(postedFile),
                    LastUpdate = DateTime.UtcNow,
                    Name = cvName
                });

                var employee = await _employeeRepositoryService.GetEntityAsync(id);

                var cvs = await _employeeCVRepositoryService.GetAllEntitiesAsync();

                employee.EmployeeCV.Id = cvs.First(p => p.Name == cvName).Id;

                await _employeeRepositoryService.UpdateEntityAsync(employee);
            }
        }

        private async Task<byte[]> FileToBytes(HttpPostedFile postedFile)
        {
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(postedFile.InputStream))
            {
                fileData = binaryReader.ReadBytes(postedFile.ContentLength);
            }

            return fileData;
        }

        private async Task<bool> UploadFileToStorage(Stream fileStream, string fileName)
        {
            StorageCredentials storageCredentials = new StorageCredentials(ConfigurationManager.AppSettings["StorageName"], ConfigurationManager.AppSettings["StorageKey"]);
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(ConfigurationManager.AppSettings["ContainerName"]);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            await blockBlob.UploadFromStreamAsync(fileStream);
            return await Task.FromResult(true);
        }

        [HttpPatch]
        [Route("{id:int:min(1)}/cv")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Upload cv to employee.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<IHttpActionResult> UploadCVFile([FromUri] int id)
        {
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["fileKey"];
            await SaveFile(id, postedFile);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}