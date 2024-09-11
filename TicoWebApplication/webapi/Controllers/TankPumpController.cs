﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTOs.StruckInfo;
using webapi.Helpers.QueryStruckInfomation;
using webapi.Helpers;
using webapi.Interface;
using webapi.Wrappers;
using webapi.Helpers.QueryTankPump;
using webapi.Mapper;
using webapi.DTOs.TankPump;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TankPumpController : ControllerBase
    {
        private readonly ITankStruckRepository _repository;
        public TankPumpController(ITankStruckRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPage(int pageIndex, int pageSize, [FromQuery] QueryObjectTankPump queryObject)
        {
            try
            {
                var list = await _repository.GetAllStruckScaleAsync(queryObject);
                var data = list.Select(x => x.ToTankPumpDataDTO()).ToList();
                var totalItem = data.Count();
                var totalPage = (int)Math.Ceiling((double)totalItem / pageSize);
                //Get Page
                var pagedData = PaginationHelper.GetPagedData(data, pageIndex, pageSize);
                var response = new PagedResponse<TankPumpDTO>(data, pageIndex, pageSize, totalPage);
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
                var list = await _repository.GetAllStruckScaleByIdAsync(id);
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
        public async Task<IActionResult> CreateTankPump([FromBody] CreateStruckInfomationDTO createData)
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
        public async Task<IActionResult> UpdateTankPump([FromRoute] int id, UpdatekInfomationDTO updatekInfomation)
        {
            try
            {
                var query = await _repository.UpdateStruckInfo(id, updatekInfomation);
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
        public async Task<IActionResult> DeleteTankPump([FromRoute] int id)
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
