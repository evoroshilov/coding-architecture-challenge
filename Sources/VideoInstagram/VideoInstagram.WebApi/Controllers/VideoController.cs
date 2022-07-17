using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoInstagram.DataLayer.Repositories;
using VideoInstagram.Shared.Contracts;
using VideoInstagram.WebApi.Dtos;
using VideoInstagram.WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoInstagram.WebApi.Controllers
{
    [Route("api/v1/video")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoMetaDataRepository _videoMetaDataRepository;
        private readonly BusService _busService;
        private readonly IMapper _mapper;
        private readonly VideoUploader _videoUploader;


        public VideoController(IVideoMetaDataRepository videoMetaDataRepository, BusService busService, IMapper mapper, VideoUploader videoUploader)
        {
            _videoMetaDataRepository = videoMetaDataRepository;
            _busService = busService;
            _mapper = mapper;
            _videoUploader = videoUploader;
        }

        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateVideoAsync(VideoMetadataDto videoMetadataDto, byte[] content, CancellationToken cancellationToken)
        {
            var toCreate = _mapper.Map<VideoMetadata>(videoMetadataDto);
            await _videoMetaDataRepository.CreateVideoMetadataAsync(toCreate, cancellationToken);
            var uploadMessage = _mapper.Map<VideoUploadMessage>(videoMetadataDto);

            uploadMessage.VideoTemporaryId = await _videoUploader.UploadVideoToTemporaryStorageAsync(toCreate.Id, content);
            await _busService.TryUploadMessageAsync(uploadMessage);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<int>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetVideoIdsAsync(double longitude, double latitude, CancellationToken cancellationToken)
        {
            //use cache 20/80 ..redis with ttl 2 min
            var list = await _videoMetaDataRepository.GetAllVideoMetadatasAsync(longitude, latitude, cancellationToken);
            //Select all videos ids by geolocation
            return Ok(list.Select(i => i.Id));
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(IReadOnlyList<UserDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetVideoIdAsync([FromRoute][Range(1, int.MaxValue)] int id, CancellationToken cancellationToken)
        {
            //use cache 20/80 ..redis with ttl 2 min
            var video = await _videoMetaDataRepository.GetVideoMetadataAsync(id, cancellationToken);
            return Ok(_mapper.Map<VideoMetadataDto>(video));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUsersAsync([FromRoute][Range(1, int.MaxValue)] int id, CancellationToken cancellationToken)
        {
            await _videoMetaDataRepository.RemoveVideoMetadataAsync(id, cancellationToken);

            return Ok();
        }


        //Update etc..
    }
}
