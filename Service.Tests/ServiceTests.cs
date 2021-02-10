using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Models;
using Models.DataTransfer;
using Repository;
using System;
using System.Linq;
using Xunit;

namespace Service.Tests
{
    public class ServiceTests
    {
        //---------------------------Start of Mapper Tests------------------------

        /// <summary>
        /// Tests the ConvertImage() method of Mapper
        /// </summary>
        /// 
        [Fact]
        public void TestForConvertImage()
        {
            Mapper mapper = new Mapper();
            string textSting = "text,text";
            var convert = mapper.ConvertImage(textSting);

            Assert.IsType<byte[]>(convert);
            Assert.NotNull(convert);
        }

        /// <summary>
        /// Tests the ConvertToPlayDto() method of Mapper
        /// </summary>
        /// 
        [Fact]
        public void TestForConvertToPlayDto()
        {
            Mapper mapper = new Mapper();
            var play = new Play()
            {
                PlayID = 6,
                Name = "Tackle",
                Description = "Tackle other players",
                PlaybookId = 3,
                DrawnPlay = new byte[1]
            };

            var convert = mapper.ConvertToPlayDto(play);

            Assert.True(convert.Name.Equals(play.Name));
        }

        //----------------------------End of Mapper tests-------------------------

        //--------------------------Start of LogicClass Tests---------------------

        /// <summary>
        /// Tests the GetPlaybooks() method of Logic
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
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                var playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.Playbooks.Add(playbook);
                var listOfPlaybooks = await logic.GetPlaybooks();
                Assert.NotNull(listOfPlaybooks);
            }
        }

        /// <summary>
        /// Tests the GetPlaybookByid() method of Logic
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
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper,  new NullLogger<Repo>());
                var playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.playbooks.Add(playbook);
                var listOfPlaybooks = await logic.GetPlaybookById(playbook.PlaybookID);
                Assert.True(listOfPlaybooks.Equals(playbook));
            }
        }

        /// <summary>
        /// Tests the CreatePlaybook() method of Logic
        /// Tests that a playbook is added to the database
        /// </summary>
        [Fact]
        public async void TestForCreatePlaybook()
        {
            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                Team team = new Team()
                {
                    TeamID = 1,
                    Name = "Broncoes",
                    Wins = 2,
                    Losses = 0
                };
                var createPlaybook = await logic.CreatePlaybook(team.TeamID);

                //Assert.Equal(1, context.Playbooks.CountAsync().Result);

                Assert.Contains<Playbook>(createPlaybook, context.Playbooks);

            }
        }

        /// <summary>
        /// Tests the CreatePlay() method of Logic
        /// Tests that a play is added to the database
        /// </summary>
        /// 
        [Fact]
        public async void TestForCreatePlay()
        {
            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                PlayDto play = new PlayDto()
                {
                    PlaybookID = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    ImageString = "Football,football,football"
                };
                var createPlay = await logic.CreatePlay(play);
                //Assert.Equal(1, context.Plays.CountAsync().Result);
                Assert.Contains<Play>(createPlay, context.Plays);
            }
        }

        /// <summary>
        /// Tests the EditPlay method of Logic
        /// </summary>
        [Fact]
        public async void TestForEditPlay()
        {
            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                var play = new Play()
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle the player",
                    DrawnPlay = new byte[1]
                };

                r.plays.Add(play);
                await r.CommitSave();

                var play2 = new PlayDto()
                {
                    PlaybookID = 1,
                    Name = "Tackle",
                    Description = "Tackle the quarterback",
                    DrawnPlay = new byte[1]
                };

                var editedPlay = await logic.EditPlay(play.PlayID, play2);
                Assert.Equal(editedPlay.Description, context.Plays.Find(play.PlayID).Description);
            }
        }

        /// <summary>
        /// Tests the GetPlayById() method of Logic
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
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                var play = new Play
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.Plays.Add(play);
                var listOfPlays = await logic.GetPlayById(play.PlayID);
                Assert.True(listOfPlays.Equals(play));
            }
        }

        /// <summary>
        /// Tests the GetPlayDto() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetPlayDto()
        {
            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                var play = new Play
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.Plays.Add(play);
                var playDto = await logic.GetPlayDto(play.PlayID);
                Assert.True(playDto.PlayID.Equals(play.PlayID));
            }
        }

        /// <summary>
        /// Tests the GetPlays() method of Logic
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
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                var play = new Play
                {
                    PlayID = 8,
                    PlaybookId = 5,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = null
                };

                r.Plays.Add(play);
                await r.CommitSave();
                var listOfPlays = await logic.GetPlays();
                Assert.NotEmpty(listOfPlays);
                Assert.True(listOfPlays.FirstOrDefault(x => x.PlayID == play.PlayID).Name == play.Name);
                //Assert.Contains<PlayDto>(mapper.ConvertToPlayDto(play), listOfPlays);
            }
        }

        /// <summary>
        /// Tests the DeletePlay() method of the Logic
        /// Tests that a play is removed from the database
        /// </summary>
        [Fact]
        public async void TestForDeletePlay()
        {
            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                var play = new Play()
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Run",
                    Description = "Run to endzone",
                    DrawnPlay = new byte[1]
                };
                r.Plays.Add(play);
                await r.CommitSave();
                var deleteEmpty = await logic.DeletePlay(3);
                Assert.Contains<Play>(play, context.Plays);
                var deletePlay = await logic.DeletePlay(play.PlayID);
                var countPlays = from p in context.Plays
                                 where p.Name == play.Name
                                 select p;
                int count = 0;
                foreach (Play plays in countPlays)
                {
                    count++;
                }
                Assert.Equal(0, count);
            }
        }


        /// <summary>
        /// Tests the DeletePlaybook() method of the Logic
        /// Tests that a playbook is removed from the database
        /// </summary>
        [Fact]
        public async void TestForDeletePlaybook()
        {
            var options = new DbContextOptionsBuilder<PlaybookContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new PlaybookContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                var playbook = new Playbook()
                {
                    PlaybookID = 25,
                    TeamID = 13
                };
                r.Playbooks.Add(playbook);
                await r.CommitSave();
                var deleteEmpty = await logic.DeletePlaybook(3);
                Assert.Contains<Playbook>(playbook, context.Playbooks);
                var deletePlaybook = await logic.DeletePlaybook(playbook.PlaybookID);
                var countPlaybooks = from p in context.Playbooks
                                     where p.PlaybookID == playbook.PlaybookID
                                     select p;
                int count = 0;
                foreach (Playbook playbooks in countPlaybooks)
                {
                    count++;
                }
                Assert.Equal(0, count);
            }
        }

        //--------------------------End of LogicClass Tests-----------------------
    }
}
