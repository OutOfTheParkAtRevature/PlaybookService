using Models;
using Models.DataTransfer;
using System;


namespace Service
{
    public class Mapper
    {
        /// <summary>
        /// takes a string image as a parameter. coverts to a byte array and returns the byte array.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public byte[] ConvertImage(string image)
        {
            //take everything after the ,
            string base64Image1 = image.Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64Image1);
            return bytes;
        }
        /// <summary>
        /// take in a Play as a parameter and creates a PlayDto. returns the new dto
        /// </summary>
        /// <param name="play"></param>
        /// <returns></returns>
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
        /// <summary>
        /// takes in the byte[] as a parameter and converts it to a JpgString image.
        /// return the string image. returns null if the byte[] parameter passed in is null
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
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
