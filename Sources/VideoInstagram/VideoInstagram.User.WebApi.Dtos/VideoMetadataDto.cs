﻿namespace VideoInstagram.WebApi.Dtos
{
    public class VideoMetadataDto
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public byte Thumbnail { get; set; }

        public string CustomerId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public long Likes { get; set; }

        public long Views { get; set; }
    }
}
