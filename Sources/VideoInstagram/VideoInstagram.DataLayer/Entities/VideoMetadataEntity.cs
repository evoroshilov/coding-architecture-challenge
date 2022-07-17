namespace VideoInstagram.DataLayer.Entities
{
    public class VideoMetadataEntity: EntityBase
    {
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
