using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Repository.Tests
{
    public class RepoTests
    {
        /// <summary>
        /// Tests the CommitSave() method of Repo
        /// </summary>
        [Fact]
        public async void TestForCommitSave()
        {
            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Playbook playbook = new Playbook
                {
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook",
                    InDev = true
                };

                r.Playbooks.Add(playbook);
                await r.CommitSave();
                Assert.NotEmpty(context.Playbooks);
            }
        }

        /// <summary>
        /// Tests the GetPlaybooks() method of Repo
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
                var playbook = new Playbook
                {
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook",
                    InDev = true
                };

                r.Playbooks.Add(playbook);
                await r.CommitSave();

                var listOfPlaybooks = await r.GetPlaybooks();
                Assert.NotNull(listOfPlaybooks);
            }
        }

        /// <summary>
        /// Tests the GetPlaybookById() method of Repo
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
                var playbook = new Playbook
                {
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook",
                    InDev = true
                };

                r.Playbooks.Add(playbook);
                await r.CommitSave();

                var listOfPlaybooks = await r.GetPlaybookById(playbook.Playbookid);
                Assert.True(listOfPlaybooks.Equals(playbook));
            }
        }

        /// <summary>
        /// Tests the GetPlays() method of Repo
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

                var listOfPlays = await r.GetPlays();
                Assert.NotNull(listOfPlays);
            }
        }

        /// <summary>
        /// Tests the GetPlayById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetPlayById()
        {
            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
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

                var listOfPlays = await r.GetPlayById(play.PlayID);
                Assert.True(listOfPlays.Equals(play));
            }
        }

        /// <summary>
        /// Tests the GetPlaysByPlaybookId() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetPlaysByPlaybookId()
        {
            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
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

                var listOfPlays = await r.GetPlaysByPlaybookId(play.PlaybookId);
                var castedList = (List<Play>)listOfPlays;
                Assert.True(castedList[0].Equals(play));
            }
        }

        /// <summary>
        /// Tests the GetPlaybooksByTeamId() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetPlaybooksByTeamId()
        {
            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var playbook = new Playbook
                {
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook",
                    InDev = true
                };

                r.Playbooks.Add(playbook);
                await r.CommitSave();

                var listOfPlaybooks = await r.GetPlaybooksByTeamId(playbook.TeamID);
                var castedList = (List<Playbook>)listOfPlaybooks;
                Assert.True(castedList[0].Equals(playbook));
            }
        }
    }
}
