using Microsoft.AspNetCore.Mvc;
using webapi.DTOs.StruckInfo;
using webapi.Helpers;
using webapi.Helpers.QueryStruckInfomation;
using webapi.Interface;
using webapi.Mapper;
using webapi.Wrappers;
namespace webapi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StruckInfomationController : Controller
    {
        private readonly IStruckInfomationRepository _repository;
        public StruckInfomationController(IStruckInfomationRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPage(int pageIndex, int pageSize, [FromQuery] QueryObjectStruckInfomation queryObject)
        {
            try
            {
                var list = await _repository.GetAllStruckInfosAsync(queryObject);
                var data = list.Select(x => x.ToStruckInfomationDTO()).ToList();
                var totalItem = data.Count();
                var totalPage = (int)Math.Ceiling((double)totalItem / pageSize);
                //Get Page
                var pagedData = PaginationHelper.GetPagedData(data, pageIndex, pageSize);
                var response = new PagedResponse<StruckInfomationDTO>(data, pageIndex, pageSize, totalPage);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                var error = new Response<string>
                {
                    Succeeded = false,
                    Message = "An error data.",
                    Errors = new[] { ex.Message }
                };
                return StatusCode(500, error);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPageByID([FromRoute] int id)
        {
            try
            {
                var list = await _repository.GetAllStruckInfosByIdAsync(id);
                if (list == null) return NotFound();
                return Ok(list);
            }
            catch (Exception ex)
            {
                var error = new Response<string>
                {
                    Succeeded = false,
                    Message = "An error data.",
                    Errors = new[] { ex.Message }
                };
                return StatusCode(500, error);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateStruckInfomation([FromBody] CreateStruckInfomationDTO createData)
        {
            try
            {
                var query = createData.ToCreateStruckInfoDTO();
                await _repository.CreateStruckInfo(query);
                return CreatedAtAction(nameof(GetPageByID), new { id = query.id }, query.ToStruckInfomationDTO());
            }
            catch (Exception ex)
            {
                var error = new Response<string>
                {
                    Succeeded = false,
                    Message = "An error data.",
                    Errors = new[] { ex.Message }
                };
                return StatusCode(500, error);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStruckInfomation([FromRoute] int id, UpdateInfomationDTO updateInfomation)
        {
            try
            {
                var query = await _repository.UpdateStruckInfo(id, updateInfomation);
                if (query == null) return NotFound();
                return Ok(query.ToStruckInfomationDTO());
            }
            catch (Exception ex)
            {
                var error = new Response<string>
                {
                    Succeeded = false,
                    Message = "An error data.",
                    Errors = new[] { ex.Message }
                };
                return StatusCode(500, error);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStruckInfomation([FromRoute] int id)
        {
            try
            {
                var query = await _repository.DeleteStruckInfo(id);
                if (query == null) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                var error = new Response<string>
                {
                    Succeeded = false,
                    Message = "An error data.",
                    Errors = new[] { ex.Message }
                };
                return StatusCode(500, error);
            }
        }
    }
}
