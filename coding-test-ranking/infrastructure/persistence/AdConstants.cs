using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_test_ranking.infrastructure.persistence
{
    public static class AdConstants
    {
        public const int HasNoPictureScore = -10;
        public const int HasSDPictureScore = 10;
        public const int HasHDPictureScore = 20;
        public const int HasDescriptionScore = 5;
        public const int HasFlatShortDescriptionScore = 10;
        public const int HasFlatLongDescriptionScore = 30;
        public const int HasChaletLongDescriptionScore = 20;
        public const int HasPositiveWordInDescriptionScore = 5;
        public const int CompletedAdScore = 40;
        public const int MinScore = 0;
        public const int MaxScore = 100;

        public const int ShortDescriptionWordsNumber = 20;
        public const int LongDescriptionWordsNumber = 50;

    }
}
