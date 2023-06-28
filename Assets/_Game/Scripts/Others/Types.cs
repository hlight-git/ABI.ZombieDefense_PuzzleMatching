public enum PoolType
{
    None = 0,

    Hero = 10,
    HeroBody = 100,


    //
    MU_Bonus = 10001,

    MU_Melee1_0 = 11000,
    MU_Melee1_1 = 11001,
    MU_Melee1_2 = 11002,
    MU_Melee1_3 = 11003,
    MU_Melee1_4 = 11004,

    MU_Melee2_0 = 11010,
    MU_Melee2_1 = 11011,
    MU_Melee2_2 = 11012,
    MU_Melee2_3 = 11013,
    MU_Melee2_4 = 11014,

    MU_Range1_0 = 12000,
    MU_Range1_1 = 12001,
    MU_Range1_2 = 12002,
    MU_Range1_3 = 12003,
    MU_Range1_4 = 12004,

    MU_Range2_0 = 12010,
    MU_Range2_1 = 12011,
    MU_Range2_2 = 12012,
    MU_Range2_3 = 12013,
    MU_Range2_4 = 12014,
}

public enum MatchUnitType
{
    Melee1 = PoolType.MU_Melee1_0,
    Melee2 = PoolType.MU_Melee2_0,
    Range1 = PoolType.MU_Range1_0,
    Range2 = PoolType.MU_Range2_0,
    Bonus = PoolType.MU_Bonus,
}

public enum MatchType
{
    CollectUnit,
    Hero
}

public enum EnemyType
{
    Small,
    Medium,
    Large
}