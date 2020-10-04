using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CommandAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommandsController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ICommandAPIRepo _repository;

		public CommandsController(ICommandAPIRepo repository, IMapper mapper)
		{
			_mapper = mapper;
			_repository = repository;
		}

		[HttpGet]
		public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
		{
			Debug.WriteLine("CommandsController::GetAllCommands()");
			var commandItems = _repository.GetAllCommands();
			return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
		}

		[HttpGet("{id}", Name = nameof(GetCommandById))]
		public ActionResult<CommandReadDto> GetCommandById(int id)
		{
			Debug.WriteLine($"CommandsController::GetCommandById({id})");
			var commandItem = _repository.GetCommandById(id);

			if (commandItem == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<CommandReadDto>(commandItem));
		}

		[HttpPost]
		public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
		{
			Debug.WriteLine($"CommandsController::CreateCommand()");
			var commandModel = _mapper.Map<Command>(commandCreateDto);
			_repository.CreateCommand(commandModel);
			_repository.SaveChanges();
			var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
			return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteCommand(int id)
		{
			Debug.WriteLine($"CommandsController::DeleteCommand({id})");
			var commandModelFromRepo = _repository.GetCommandById(id);

			if (commandModelFromRepo == null)
			{
				return NotFound();
			}

			_repository.DeleteCommand(commandModelFromRepo);
			_repository.SaveChanges();
			return NoContent();
		}

		[HttpPatch("{id}")]
		public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
		{
			Debug.WriteLine($"CommandsController::PartialCommandUpdate()");
			var commandModelFromRepo = _repository.GetCommandById(id);

			if (commandModelFromRepo == null)
			{
				return NotFound();
			}
			
			var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
			patchDoc.ApplyTo(commandToPatch, ModelState);
			
			if (!TryValidateModel(commandToPatch))
			{
				return ValidationProblem(ModelState);
			}
			
			_mapper.Map(commandToPatch, commandModelFromRepo);
			_repository.UpdateCommand(commandModelFromRepo);
			_repository.SaveChanges();
			return NoContent();
		}

		[HttpPut("{id}")]
		public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
		{
			Debug.WriteLine($"CommandsController::UpdateCommand()");
			var commandModelFromRepo = _repository.GetCommandById(id);

			if (commandModelFromRepo == null)
			{
				return NotFound();
			}

			_mapper.Map(commandUpdateDto, commandModelFromRepo);
			_repository.UpdateCommand(commandModelFromRepo);
			_repository.SaveChanges();
			return NoContent();
		}
	}
}
