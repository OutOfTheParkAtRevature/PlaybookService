using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using System;
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
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Playbook playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 2
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
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.Playbooks.Add(playbook);
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
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.Playbooks.Add(playbook);
                var listOfPlaybooks = await r.GetPlaybookById(playbook.PlaybookID);
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
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var play = new Play
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.Plays.Add(play);
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
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var play = new Play
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.Plays.Add(play);
                var listOfPlays = await r.GetPlayById(play.PlayID);
                Assert.True(listOfPlays.Equals(play));
            }
        }
    }
}
