﻿using ABC_Vending_API.DTOs;
using ABC_Vending_API.Models;
using ABC_Vending_API.Repos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ABC_Vending_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WarehouseController : ControllerBase
	{
		IWarehouesRepository _warehouesRepository;
		public WarehouseController(IWarehouesRepository warehouesRepository)
		{
			_warehouesRepository = warehouesRepository;
		}

		// GET: api/<WarehouseController>
		[HttpGet]
		public async Task<IEnumerable<Warehouse>> Get()
		{
			return await _warehouesRepository.GetWarehousesAsync().ConfigureAwait(false);
		}

		// GET api/<WarehouseController>/5
		[HttpGet("{id}")]
		public async Task<Warehouse> Get(Guid Id)
		{
			return await _warehouesRepository.GetWarehouseAsync(Id).ConfigureAwait(false);
		}

		// POST api/<WarehouseController>
		[HttpPost]
		public async void Post([FromBody] CreateWarehouseDto dto)
		{
			await _warehouesRepository.AddWarehouseAsync(dto).ConfigureAwait(false);
		}

		// PUT api/<WarehouseController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<WarehouseController>/5
		[HttpDelete("{id}")]
		public async void Delete(Guid Id)
		{
			await _warehouesRepository.DeleteWarehouseAsync(Id).ConfigureAwait(false);
		}
	}
}
