using Microsoft.Extensions.Logging;
using Model.DataTransfer;
using Models;
using Models.DataTransfer;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class Logic
    {
        public Logic() { }
        public Logic(Repo repo, Mapper mapper, ILogger<Repo> logger)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        private readonly Repo _repo;
        private readonly ILogger<Repo> _logger;
        private readonly Mapper _mapper;
      
        /// <summary>
        /// Get Playbook
        /// </summary>
        /// <param name="id">PlaybookID</param>
        /// <returns>PlaybookID</returns>
        public async Task<Playbook> GetPlaybookById(Guid id)
        {
            return await _repo.GetPlaybookById(id);
        }
        /// <summary>
        /// Get list of Playbooks
        /// </summary>
        /// <returns>list of Playbooks</returns>
        public async Task<IEnumerable<Playbook>> GetPlaybooks()
        {
            return await _repo.GetPlaybooks();
        }
        /// <summary>
        /// Create new Playbook and assign it to a team
        /// </summary>
        /// <param name="teamId">TeamID</param>
        /// <returns>Playbook</returns>
        public async Task<Playbook> CreatePlaybook(Guid teamId, string name)
        {
            Playbook newPlayBook = new Playbook()
            {
                TeamID = teamId,
                Name = name
            };

            await _repo.Playbooks.AddAsync(newPlayBook);
            await _repo.CommitSave();
            return newPlayBook;
        }
        /// <summary>
        /// Create new Play and assign it to the current Playbook
        /// </summary>
        /// <param name="playDto">new Play</param>
        /// <returns>Play</returns>
        public async Task<Play> CreatePlay(PlayDto playDto)
        {
            Play newPlay = new Play()
            {
                PlaybookId = playDto.PlaybookID,
                Name = playDto.Name,
                Description = playDto.Description,
                DrawnPlay = _mapper.ConvertImage(playDto.ImageString)
            };

            await _repo.Plays.AddAsync(newPlay);

            await _repo.CommitSave();
            return newPlay;
        }
        /// <summary>
        /// Edit a Play
        /// </summary>
        /// <param name="playId">Play to edit</param>
        /// <param name="playDto">New Play info</param>
        /// <returns>edited Play</returns>
        public async Task<Play> EditPlay(Guid playId, PlayDto playDto)
        {
            Play editedPlay = await GetPlayById(playId);
            if (editedPlay != null)
            {
                if (editedPlay.Name != playDto.Name) { editedPlay.Name = playDto.Name; }
                if (editedPlay.Description != playDto.Description) { editedPlay.Description = playDto.Description; }
                if (editedPlay.DrawnPlay != playDto.DrawnPlay) { editedPlay.DrawnPlay = playDto.DrawnPlay; }
                await _repo.CommitSave();
            }
            return editedPlay;
        }
        /// <summary>
        /// Get Play by PlayID
        /// </summary>
        /// <param name="id">PlayID</param>
        /// <returns>Play</returns>
        public async Task<Play> GetPlayById(Guid id)
        {
            return await _repo.GetPlayById(id);
        }
        /// <summary>
        /// Get PlayDto by PlayID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PlayDto> GetPlayDto(Guid id)
        {
            Play play = await _repo.GetPlayById(id);
            return _mapper.ConvertToPlayDto(play);
        }
        /// <summary>
        /// Get list of Plays
        /// </summary>
        /// <returns>list of Plays</returns>
        public async Task<IEnumerable<PlayDto>> GetPlays()
        {
            IEnumerable<Play> playList = await _repo.GetPlays();
            List<PlayDto> playDtos = new List<PlayDto>();
            foreach (var play in playList)
            {
                PlayDto playDto = _mapper.ConvertToPlayDto(play);
                playDtos.Add(playDto);
            }
            return playDtos;
        }

        public async Task<PlayBookWithPlaysDto> GetPlaybookWithPlays(Guid TeamId, string bookName)
        {
            PlayBookWithPlaysDto playBookWithPlaysDto = new PlayBookWithPlaysDto();
            var playbookList = await GetPlaybooksByTeamId(TeamId);
            foreach(var item in playbookList)
            {
                if (item.Name == bookName)
                {
                    playBookWithPlaysDto.Name = item.Name;
                    playBookWithPlaysDto.Playbookid = item.Playbookid;
                    playBookWithPlaysDto.TeamID = item.TeamID;

                }
            }
            var plays = await GetPlaysByPlaybookId(playBookWithPlaysDto.Playbookid);
            
            foreach(var item in plays)
            {
                playBookWithPlaysDto.playDtos.Add(_mapper.ConvertToPlayDto(item));
            }
            return playBookWithPlaysDto;
        }

        /// <summary>
        /// Delete a Playbook by ID
        /// </summary>
        /// <param name="id">PlaybookID</param>
        /// <returns>deleted Playbook</returns>
        public async Task<Playbook> DeletePlaybook(Guid id)
        {
            Playbook playbook = await GetPlaybookById(id);
            if (playbook != null)
            {
                _repo.Playbooks.Remove(playbook);
                await _repo.CommitSave();
            }
            return playbook;
        }
        /// <summary>
        /// Delete a Play from a Playbook
        /// </summary>
        /// <param name="id">PlayID</param>
        /// <returns>deleted Play</returns>
        public async Task<Play> DeletePlay(Guid id)
        {
            Play play = await GetPlayById(id);
            if (play != null)
            {
                _repo.Plays.Remove(play);
                await _repo.CommitSave();
            }
            return play;
        }
        /// <summary>
        /// returns a list of plays for a playbook by the playbookID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Play>> GetPlaysByPlaybookId(Guid id)
        {
            return await _repo.GetPlaysByPlaybookId(id);
        }
        /// <summary>
        /// returns a list of playbooks by the TeamId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Playbook>> GetPlaybooksByTeamId(Guid id)
        {
            return await _repo.GetPlaybooksByTeamId(id);
        }
    }
}
