using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_test_ranking.infrastructure.persistence
{
    public class AdEnum
    {
        public enum PositiveWords
        {
            Luminoso,
            Nuevo,
            Céntrico,
            Reformado,
            Ático
        }

        public enum Typology
        {
            CHALET,
            FLAT,
            GARAGE
        }

        public enum PictureQuality
        {
            HD,
            SD
        }
    }
}
