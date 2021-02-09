using Models;
using Models.DataTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Model.Tests
{
    public class ModelTests
    {

        /// <summary>
        /// Checks the data annotations of Models to make sure they aren't being violated
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private IList<ValidationResult> ValidateModel(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result, true);
            // if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);

            return result;
        }

        /// <summary>
        /// Makes sure Playbook Model works with valid data
        /// </summary>
        [Fact]
        public void ValidatePlaybook()
        {
            var playbook = new Playbook()
            {
                PlaybookID = 1,
                TeamID = 2
            };

            var results = ValidateModel(playbook);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure Play Model works with valid data
        /// </summary>
        [Fact]
        public void ValidatePlay()
        {
            var play = new Play()
            {
                PlayID = 1,
                PlaybookId = 1,
                Name = "Tackle",
                Description = "Tackles other players",
                DrawnPlay = new byte[1]
            };

            var results = ValidateModel(play);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Validates the PlayDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidatePlayDto()
        {
            var play = new PlayDto()
            {
                PlayID = 4,
                PlaybookID = 1,
                Name = "tackles",
                Description = "tackle other players",
                DrawnPlay = new byte[1],
                ImageString = "football, football"
            };

            var errorcount = ValidateModel(play).Count;
            Assert.Equal(0, errorcount);
        }
    }
}
