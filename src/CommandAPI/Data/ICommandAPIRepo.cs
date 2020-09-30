using CommandAPI.Models;
using System.Collections.Generic;

namespace CommandAPI.Data
{
    public interface ICommandAPIRepo
    {
        void CreateCommand(Command cmd);
        void DeleteCommand(Command cmd);
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        void UpdateCommand(Command cmd);
        bool SaveChanges();
    }
}
