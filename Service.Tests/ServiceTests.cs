using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using Models.DataTransfer;
using Repository;
using System;
using System.Collections.Generic;
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
                PlayID = Guid.NewGuid(),
                Name = "Tackle",
                Description = "Tackle other players",
                PlaybookId = Guid.NewGuid(),
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
            //for coverage
            var dbContext = new PlaybookContext();
            var logicClass = new Logic();

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
                var playbook = new Playbook
                {
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook",
                    InDev = true
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
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
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
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook",
                    InDev = true
                };

                r.Playbooks.Add(playbook);
                var listOfPlaybooks = await logic.GetPlaybookById(playbook.Playbookid);
                Assert.True(listOfPlaybooks.Equals(playbook));
            }
        }

        /// <summary>
        /// Tests the GetPlaybooksByTeamid() method of Logic
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
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
                var playbook = new Playbook
                {
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook",
                    InDev = true
                };

                r.Playbooks.Add(playbook);
                await r.CommitSave();
                var listOfPlaybooks = await logic.GetPlaybooksByTeamId(playbook.TeamID);
                var castedList = (List<Playbook>)listOfPlaybooks;
                Assert.True(castedList[0].Equals(playbook));
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
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
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
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook"
                };

                var createPlaybook = await logic.CreatePlaybook(playbook.TeamID, playbook.Name);

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
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
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
                    PlaybookID = Guid.NewGuid(),
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
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
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
                    PlayID = Guid.NewGuid(),
                    PlaybookId = Guid.NewGuid(),
                    Name = "Tackle",
                    Description = "Tackle the player",
                    DrawnPlay = new byte[1]
                };

                r.Plays.Add(play);
                await r.CommitSave();

                var play2 = new PlayDto()
                {
                    PlaybookID = Guid.NewGuid(),
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
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
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
                    PlayID = Guid.NewGuid(),
                    PlaybookId = Guid.NewGuid(),
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
        /// Tests the GetPlaysByPlaybookId() method of Logic
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
                Mapper mapper = new Mapper();
                Logic logic = new Logic(r, mapper, new NullLogger<Repo>());
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
                var listOfPlays = await logic.GetPlaysByPlaybookId(play.PlaybookId);
                var castedList = (List<Play>)listOfPlays;
                Assert.True(castedList[0].Equals(play));
            }
        }

        /// <summary>
        /// Tests the GetPlayDto() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetPlayDto()
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
                var play = new Play
                {
                    PlayID = Guid.NewGuid(),
                    PlaybookId = Guid.NewGuid(),
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
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
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
                    PlayID = Guid.NewGuid(),
                    PlaybookId = Guid.NewGuid(),
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
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
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
                    PlayID = Guid.NewGuid(),
                    PlaybookId = Guid.NewGuid(),
                    Name = "Run",
                    Description = "Run to endzone",
                    DrawnPlay = new byte[1]
                };
                r.Plays.Add(play);
                await r.CommitSave();
                var deleteEmpty = await logic.DeletePlay(Guid.NewGuid());
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
            .UseInMemoryDatabase(databaseName: "p3PlaybookService")
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
                    Playbookid = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Name = "myplaybook",
                    InDev = true
                };
                r.Playbooks.Add(playbook);
                await r.CommitSave();
                var deleteEmpty = await logic.DeletePlaybook(Guid.NewGuid());
                Assert.Contains<Playbook>(playbook, context.Playbooks);
                var deletePlaybook = await logic.DeletePlaybook(playbook.Playbookid);
                var countPlaybooks = from p in context.Playbooks
                                     where p.Playbookid == playbook.Playbookid
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
