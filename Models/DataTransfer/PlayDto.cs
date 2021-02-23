using System;

namespace Models.DataTransfer
{
    public class PlayDto
    {
        public Guid PlayID { get; set; }
        public Guid PlaybookID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] DrawnPlay { get; set; }
        public string ImageString { get; set; }
    }
}
