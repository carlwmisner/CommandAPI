using CommandAPI.Models;
using System;
using System.Collections.Generic;

namespace CommandAPI.Data
{
	public class MockCommandAPIRepo : ICommandAPIRepo
	{
		private static readonly Dictionary<int, Command> _commands;

		static MockCommandAPIRepo()
		{
			_commands = new Dictionary<int, Command>
			{
				{
					0,
					new Command
					{
						Id=0,
						HowTo="How to generate a migration",
						CommandLine="dotnet ef migrations add <Name of Migration>",
						Platform=".Net Core EF",
					}
				},
				{
					1,
					new Command
					{
						Id=1,
						HowTo="Run Migrations",
						CommandLine="dotnet ef database update",
						Platform=".Net Core EF",
					}
				},
				{
					2,
					new Command
					{
						Id=2,
						HowTo="List active migrations",
						CommandLine="dotnet ef migrations list",
						Platform=".Net Core EF",
					}
				},
			};
		}

		public void CreateCommand(Command cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException(nameof(cmd));
			}

			if(_commands.ContainsKey(cmd.Id))
			{
				throw new Exception($"Command {cmd.Id} already exists in store");
			}

			_commands[cmd.Id] = cmd;
		}

		public void DeleteCommand(Command cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException(nameof(cmd));
			}

			if (!_commands.ContainsKey(cmd.Id))
			{
				throw new Exception($"Command {cmd.Id} does not exist in store");
			}

			_commands.Remove(cmd.Id);
		}

		public IEnumerable<Command> GetAllCommands()
		{
			return _commands.Values;
		}

		public Command GetCommandById(int id)
		{
			return _commands[id];
		}

		public bool SaveChanges()
		{
			return true;
		}

		public void UpdateCommand(Command cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException(nameof(cmd));
			}

			if (!_commands.ContainsKey(cmd.Id))
			{
				throw new Exception($"Command {cmd.Id} does not exist in store");
			}

			_commands[cmd.Id] = cmd;
		}
	}
}
