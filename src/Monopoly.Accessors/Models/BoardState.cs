using System.Collections.Generic;
using System.Linq;

namespace Monopoly.Accessors.Models
{
    public class BoardState
    {
        public int PlayerTurn { get; set; } = 1;

        public List<Player> Players { get; set; } = new();
    }

    public class Player
    {
        public int PlayerNumber { get; set; } = 1;

        public long CashOnHand { get; set; } = 1500;
        
        //location status
        public LocationEnum CurrentLocation { get; set; } = LocationEnum.Go;
        /*public List<Location> OwnedLocations { get; set; }
        public bool IsInJail { get; set; }
        
        //roll status
        public int ConsecutiveDoublesRolls { get; set; }
        public int ConsecutiveJailRolls { get; set; }
        
        //card status
        public List<ChanceCard> OwnedChanceCards { get; set; }
        public List<CommunityChestCard> OwnedCommunityChestCards { get; set; }*/
    }

    public class Location
    {
        public LocationEnum LocationID { get; set; }
        /*public Player? Owner { get; set; }
        public bool IsChance { get; set; }
        public bool IsCommunityChest { get; set; }
        public bool IsRailroad { get; set; }
        public bool IsUtility { get; set; }*/
    }

    public enum LocationEnum
    {
        Go = 0,
        MediterraneanAvenue = 1,
        CommunityChest1 = 2,
        BalticAvenue = 3,
        IncomeTax = 4,
        ReadingRailroad = 5,
        OrientalAvenue = 6,
        Chance1 = 7,
        VermontAvenue = 8,
        ConnecticutAvenue = 9,
        Jail = 10,
        StCharlesPlace = 11,
        ElectricCompany = 12,
        StateAvenue = 13,
        VirginiaAvenue = 14,
        PennsylvaniaRailroad = 15,
        StJamesPlace = 16,
        CommunityChest2 = 17,
        TennesseeAvenue = 18,
        NewYorkAvenue = 19,
        FreeParking = 20,
        KentuckyAvenue = 21,
        Chance2 = 22,
        IndianaAvenue = 23,
        IllinoisAvenue = 24,
        BnORailroad = 25,
        AtlanticAvenue = 26,
        VentnorAvenue = 27,
        WaterWorks = 28,
        MarvinGardens = 29,
        GoToJail = 30,
        PacificAvenue = 31,
        NorthCarolinaAvenue = 32,
        CommunityChest3 = 33,
        PennsylvaniaAvenue = 34,
        ShortLine = 35,
        Chance3 = 36,
        ParkPlace = 37,
        LuxuryTax = 38,
        Boardwalk = 39,
    }
    
    public class ChanceCard
    {
        public ChanceCardEnum CardTypeID { get; set; }
    }

    public enum ChanceCardEnum
    {
        AdvanceToGo,
        AdvanceToStCharlesPlace,
        AdvanceToBoardwalk,
        AdvanceToNearestUtility,
        AdvanceToNearestRailroad,
        AdvanceToIllinoisAvenue,
        GoBackThreeSpaces,
        GoToJail,
        GetOutOfJailFree
    }
    
    public class CommunityChestCard
    {
        public ChanceCardEnum CardTypeID { get; set; }
    }
    
    public enum CommunityChestCardEnum
    {
        AdvanceToGo,
        GoToJail,
        GetOutOfJailFree
    }
}