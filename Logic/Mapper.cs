using Models;
using Models.DataTransfer;
using System;

namespace Service
{
    public class Mapper
    {
        public byte[] ConvertImage(string image)
        {
            //take everything after the ,
            string base64Image1 = image.Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64Image1);
            return bytes;
        }

        public PlayDto ConvertToPlayDto(Play play)
        {
            PlayDto playDto = new PlayDto
            {
                PlayID = play.PlayID,
                PlaybookID = play.PlaybookId,
                Name = play.Name,
                Description = play.Description,
                DrawnPlay = play.DrawnPlay,
                ImageString = ConvertByteArrayToJpgString(play.DrawnPlay)
            };
            return playDto;
        }

        private string ConvertByteArrayToJpgString(byte[] byteArray)
        {
            if (byteArray != null)
            {
                string imageBase64Data = Convert.ToBase64String(byteArray, 0, byteArray.Length);
                string imageDataURL = string.Format($"data:image/jpg;base64,{imageBase64Data}");
                return imageDataURL;
            }
            else return null;
        }
    }
}
