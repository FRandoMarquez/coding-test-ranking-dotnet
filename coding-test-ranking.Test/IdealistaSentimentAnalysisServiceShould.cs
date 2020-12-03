using coding_test_ranking.infrastructure.persistence;
using coding_test_ranking.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using static coding_test_ranking.infrastructure.persistence.AdEnum;

namespace coding_test_ranking.Test
{
    public class IdealistaSentimentAnalysisServiceShould
    {
        private readonly IdealistaSentimentAnalysisService _service;
        public IdealistaSentimentAnalysisServiceShould()
        {
            _service = new IdealistaSentimentAnalysisService();
        }

        [Theory]
        [InlineData("Luminoso")]
        [InlineData("Nuevo")]
        [InlineData("Céntrico")]
        [InlineData("Reformado")]
        [InlineData("Ático")]
        public void AddsPositiveWordPointsIfDescriptionAdHasOnePositiveWord(string positiveWord)
        {
            var points = _service.PositiveWordsEvaluation(positiveWord);

            Assert.Equal(AdConstants.HasPositiveWordInDescriptionScore, points);
        }

        [Fact]
        public void AddsMultiplePositiveWordsPointsIfTheirAreDifferent()
        {
            var positiveDescription = $"{PositiveWords.Céntrico} {PositiveWords.Ático}";
            var points = _service.PositiveWordsEvaluation(positiveDescription);
            Assert.Equal((2 * AdConstants.HasPositiveWordInDescriptionScore), points);
        }

        [Fact]
        public void AddsPositiveWordsPointOnlyOneTimePerPositiveWord()
        {
            var positiveDescription = $"{PositiveWords.Céntrico} {PositiveWords.Céntrico}";
            var points = _service.PositiveWordsEvaluation(positiveDescription);
            Assert.Equal(AdConstants.HasPositiveWordInDescriptionScore, points);
        }
    }
}
