using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using Models.DataTransfer;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace PlaybookService.Tests
{
    public class ControllerTests
    {
        /// <summary>
        /// Tests the GetPlaybooks() method of PlaybookController
        /// </summary>
        [Fact]
        public async void TestForGetPlaybooks()
        {

            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                PlaybookController playbookController = new PlaybookController(logic, new NullLogger<PlaybookController>());
                var playbook = new Playbook
                {
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook",
                    InDev = true
                };
                r.Playbooks.Add(playbook);
                var playbook2 = new Playbook
                {
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook2",
                    InDev = true
                };
                r.Playbooks.Add(playbook2);
                await r.CommitSave();

                var listOfPlaybooks = await playbookController.GetPlaybooks();
                var convertedList = (List<Playbook>)listOfPlaybooks;
                Assert.NotNull(listOfPlaybooks);
                Assert.Equal("myplaybook", convertedList[0].Name);
                Assert.Equal("myplaybook2", convertedList[1].Name);
            }
        }

        /// <summary>
        /// Tests the GetPlaybookById() method of PlaybookController
        /// </summary>
        [Fact]
        public async void TestForGetPlaybookById()
        {

            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                PlaybookController playbookController = new PlaybookController(logic, new NullLogger<PlaybookController>());
                var playbook = new Playbook
                {
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook",
                    InDev = true
                };
                r.Playbooks.Add(playbook);
                await r.CommitSave();

                var getPlaybook = await playbookController.GetPlaybook(playbook.Playbookid.ToString(), playbook.Name);
                Assert.Equal("myplaybook", getPlaybook.Value.Name);
            }
        }

        /// <summary>
        /// Tests the CreatePlaybook() method of PlaybookController
        /// </summary>
        [Fact]
        public async void TestForCreatePlaybook()
        {

            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                PlaybookController playbookController = new PlaybookController(logic, new NullLogger<PlaybookController>());
                var playbook = new Playbook
                {
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook",
                    InDev = true
                };

                var createPlaybook = await playbookController.CreatePlaybook(playbook.Playbookid.ToString(), playbook.Name);
                Assert.NotEmpty(context.Playbooks);
                Assert.Equal("myplaybook", createPlaybook.Value.Name);
            }
        }

        /// <summary>
        /// Tests the DeletePlaybook() method of PlaybookController
        /// </summary>
        [Fact]
        public async void TestForDeletePlaybook()
        {

            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookController")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                PlaybookController playbookController = new PlaybookController(logic, new NullLogger<PlaybookController>());
                var playbook = new Playbook
                {
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook",
                    InDev = true
                };
                r.Playbooks.Add(playbook);
                await r.CommitSave();

                Assert.NotEmpty(context.Playbooks);
                var deletePlaybook = await playbookController.DeletePlaybook(playbook.Playbookid.ToString());
                var getPlaybook = await logic.GetPlaybookById(playbook.Playbookid);
                Assert.Null(getPlaybook);
            }
        }

        /// <summary>
        /// Tests the GetPlays() method of PlaybookController
        /// </summary>
        [Fact]
        public async void TestForGetPlays()
        {

            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                PlaybookController playbookController = new PlaybookController(logic, new NullLogger<PlaybookController>());
                var play = new Play
                {
                    PlayID = Guid.NewGuid(),
                    PlaybookId = Guid.NewGuid(),
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.Plays.Add(play);
                var play2 = new Play
                {
                    PlayID = Guid.NewGuid(),
                    PlaybookId = Guid.NewGuid(),
                    Name = "Run",
                    Description = "Run with ball",
                    DrawnPlay = new byte[1]
                };

                r.Plays.Add(play2);
                await r.CommitSave();

                var listOfPlays = await playbookController.GetPlays();
                var convertedList = (List<PlayDto>)listOfPlays;
                Assert.NotNull(listOfPlays);
                Assert.Equal("Tackle", convertedList[0].Name);
                Assert.Equal("Tackle other players", convertedList[0].Description);
                Assert.Equal("Run", convertedList[1].Name);
                Assert.Equal("Run with ball", convertedList[1].Description);
            }
        }

        /// <summary>
        /// Tests the GetPlayDto() method of PlaybookController
        /// </summary>
        [Fact]
        public async void TestForGetPlayDtoById()
        {

            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                PlaybookController playbookController = new PlaybookController(logic, new NullLogger<PlaybookController>());
                var play = new Play
                {
                    PlayID = Guid.NewGuid(),
                    PlaybookId = Guid.NewGuid(),
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.Plays.Add(play);
                await r.CommitSave();

                var getPlay = await playbookController.GetPlayDto(play.PlayID.ToString(), play.Name);
                Assert.Equal("Tackle", getPlay.Value.Name);
                Assert.Equal("Tackle other players", getPlay.Value.Description);
            }
        }

        /// <summary>
        /// Tests the CreatePlay() method of PlaybookController
        /// </summary>
        [Fact]
        public async void TestForCreatePlay()
        {

            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                PlaybookController playbookController = new PlaybookController(logic, new NullLogger<PlaybookController>());
                var playDto = new PlayDto
                {
                    PlayID = Guid.NewGuid(),
                    PlaybookID = Guid.NewGuid(),
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1],
                    ImageString = "football,football"
                };

                var createPlay = await playbookController.CreatePlay(playDto);
                Assert.NotEmpty(context.Plays);
                Assert.Equal("Tackle", createPlay.Value.Name);
                Assert.Equal("Tackle other players", createPlay.Value.Description);
            }
        }

        /// <summary>
        /// Tests the EditPlay() method of PlaybookController
        /// </summary>
        [Fact]
        public async void TestForEditPlay()
        {

            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                PlaybookController playbookController = new PlaybookController(logic, new NullLogger<PlaybookController>());
                var play = new Play
                {
                    PlayID = Guid.NewGuid(),
                    PlaybookId = Guid.NewGuid(),
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.Plays.Add(play);
                await r.CommitSave();

                var playDto = new PlayDto
                {
                    PlayID = Guid.NewGuid(),
                    PlaybookID = Guid.NewGuid(),
                    Name = "Run",
                    Description = "Run with ball",
                    DrawnPlay = new byte[1],
                };

                var getPlay = await logic.GetPlayById(play.PlayID);
                Assert.Equal("Tackle", getPlay.Name);
                Assert.Equal("Tackle other players", getPlay.Description);
                var editPlay = await playbookController.EditPlay(play.PlayID.ToString(), playDto);
                Assert.Equal("Run", editPlay.Value.Name);
                Assert.Equal("Run with ball", editPlay.Value.Description);
            }
        }

        /// <summary>
        /// Tests the DeletePlay() method of PlaybookController
        /// </summary>
        [Fact]
        public async void TestForDeletePlay()
        {

            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                PlaybookController playbookController = new PlaybookController(logic, new NullLogger<PlaybookController>());
                var play = new Play
                {
                    PlayID = Guid.NewGuid(),
                    PlaybookId = Guid.NewGuid(),
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.Plays.Add(play);
                await r.CommitSave();
                Assert.NotEmpty(context.Plays);
                var deletePlay = await playbookController.DeletePlay(play.PlayID.ToString());
                var getPlay = await logic.GetPlayById(play.PlayID);
                Assert.Null(getPlay);
            }
        }
    }
}
