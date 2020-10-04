using CommandAPI.Models;
using System;
using Xunit;

namespace CommandAPI.Tests
{
	public class CommandTests : IDisposable
	{
		private Command _testCommand;

		public CommandTests()
		{
			_testCommand = new Command
			{
				HowTo = "Do something",
				Platform = "Some platform",
				CommandLine = "Some commandline"
			};
		}
		public void Dispose()
		{
			_testCommand = null;
		}

		[Fact]
		public void CanChangeCommandLine()
		{
			//Arrange
			var newCommandLine = "dotnet test";

			//Act
			_testCommand.CommandLine = newCommandLine;

			//Assert
			Assert.Equal(newCommandLine, _testCommand.CommandLine);
		}

		[Fact]
		public void CanChangeHowTo()
		{
			//Arrange
			var newHowTo = "Execute Unit Tests";

			//Act
			_testCommand.HowTo = newHowTo;

			//Assert
			Assert.Equal(newHowTo, _testCommand.HowTo);
		}

		[Fact]
		public void CanChangePlatform()
		{
			//Arrange
			var newPlatform = "xUnit";

			//Act
			_testCommand.Platform = newPlatform;

			//Assert
			Assert.Equal(newPlatform, _testCommand.Platform);
		}
	}
}
