using AutoMapper;
using CommandAPI.Dtos;
using CommandAPI.Models;

namespace CommandAPI.Profiles
{
	public class CommandsProfile : Profile
	{
		public CommandsProfile()
		{
			CreateMap<CommandCreateDto, Command>();
			CreateMap<Command, CommandReadDto>();
			CreateMap<CommandUpdateDto, Command>();
			CreateMap<Command, CommandUpdateDto>();
		}
	}
}
