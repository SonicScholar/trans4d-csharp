using System;
using System.Collections.Generic;
using System.Text;

namespace TRANS4D
{
    //todo: decorate these with Rich's comments in SETTP
    public enum Datum
    {
        Nad83_2011_or_CORS96 = 1,
        ITRF88 = 2,
        ITRF89 = 3,
        ITRF90 = 4,
        ITRF91 = 5,
        ITRF92 = 6,
        ITRF93 = 7,
        ITRF94_or_96 = 8,
        ITRF97 = 9,
        ITRF2014_PMM_NorthAmerica = 10,
        ITRF2000 = 11,
        PACP00_or_PA11 = 12,
        MARP00_or_MA11 = 13,
        ITRF2005 = 14,
        ITRF2008_or_IGS08_or_IGb08 = 15,
        ITRF2014 = 16,
        PMM_Caribbean = 17,
        PMM_Pacific = 18,
        PMM_Mariana = 19,
        PMM_Bering = 20,
        ITRF20 = 21,

        [Obsolete("Parameters are in beta. Future versions will remove the beta version and replace with the final one. See https://beta.ngs.noaa.gov/NATRF2022")]
        NATRF2022_BETA = 22,
        [Obsolete("Parameters are in beta. Future versions will remove the beta version and replace with the final one. See https://beta.ngs.noaa.gov/NATRF2022")]
        CATRF2022_BETA = 23,
        [Obsolete("Parameters are in beta. Future versions will remove the beta version and replace with the final one. See https://beta.ngs.noaa.gov/NATRF2022")]
        PATRF2022_BETA = 24,
        [Obsolete("Parameters are in beta. Future versions will remove the beta version and replace with the final one. See https://beta.ngs.noaa.gov/NATRF2022")]
        MATRF2022_BETA = 25,
    }
}
