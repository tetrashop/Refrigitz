using System;

namespace RefrigtzDLL
{
    [Serializable]
    public class ThingsConverter
    {
        //#pragma warning disable CS0414 // The field 'ThingsConverter.Spaces' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'ThingsConverter.Spaces' is assigned but its value is never used

#pragma warning restore CS0414 // The field 'ThingsConverter.Spaces' is assigned but its value is never used
        //#pragma warning restore CS0414 // The field 'ThingsConverter.Spaces' is assigned but its value is never used

        //Initiate Global Variables.
        public static bool LoadConvertTable = false;

        public static int[,] TableConverted = null;
        private readonly bool ArrangmentsChanged = true;
        public static bool ClickOcurred = false;
        public static bool ActOfClickEqualTow = false;
        public bool Convert = false;
        public bool ConvertedToMinister = false;
        public bool ConvertedToCastle = false;
        public bool ConvertedToElefant = false;
        public bool ConvertedToHourse = false;
        public int Max;
        public int RowS, ColumnS;
        private int Order;

        //AllDraw. THIS;
        public ThingsConverter()
        { //long Time = TimeElapced.TimeNow();Spaces++;
        }

        //Constructor
        public ThingsConverter(bool Arrangments, int i, int j, int Ord//,ref AllDraw. THI
            )
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            //Initite Global Variables with Local Parameter.
            //THIS = THI;
            ArrangmentsChanged = Arrangments;
            RowS = i;
            ColumnS = j;
            Order = Ord;

            ////{ //AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) //AllDraw.OutPut.Append(Space);  //AllDraw.OutPut.Append("ThingsConverter:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
        }

        public ThingsConverter(bool arrangmentsChanged, int order)
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            ArrangmentsChanged = arrangmentsChanged;

            Order = order;
            ///{ //AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) //AllDraw.OutPut.Append(Space);  //AllDraw.OutPut.Append("ThingsConverter:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
        }

        //Convert Operation of Randomly All State Method.
        public bool ConvertOperation(int i, int j, int[,] Tab, int Ord)
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            object OOO = new object();
            lock (OOO)
            {
                //Initiate Global variables with Local One.
                RowS = i;
                ColumnS = j;
                Order = Ord;
                //If Convert is Act and click tow time occured
                if (!Convert && (ActOfClickEqualTow || AllDraw.StateCC || (!AllDraw.Person)))

                {
                    object O = new object();
                    lock (O)
                    {
                        AllDraw.ConvertWait = true;
                    }

                    object O1 = new object();
                    lock (O1)
                    {
                        ClickOcurred = true;
                    }
                    //Set tow time click unclicked.
                    object O2 = new object();
                    lock (O2)
                    {
                        ActOfClickEqualTow = false;
                    }
                    if (!ArrangmentsChanged)
                    {
                        //Convert State Boolean Variable Consideration.
                        if (Order == 1 && ColumnS == 7)
                        {
                            Convert = true;
                        }

                        if (Order == -1 && ColumnS == 0)
                        {
                            Convert = true;
                        }
                    }
                    else
                    {
                        //Convert State Boolean Variable Consideration.
                        if (Order == 1 && ColumnS == 0)
                        {
                            Convert = true;
                        }

                        if (Order == -1 && ColumnS == 7)
                        {
                            Convert = true;
                        }
                    }
                    //If Converted is Occured the Operation od Set and table reference content occured.
                    if (Convert)
                    {
                        bool ASS = false; object OOOAAA = new object(); lock (OOOAAA) { ASS = AllDraw.Blitz; }
                        if (!ASS)
                        {
                            AllDraw.ConvertedKind = -1;
                            AllDraw.SodierConversionOcuured = true;
                            //Randomly Number of 4 kind Object.
                            int Rand = -1;
                            if (//AllDraw.Person &&
                                AllDraw.StateCP && AllDraw.THISSecradioButtonGrayOrderChecked)
                            {
                                if (AllDraw.OrderPlateDraw == 1)
                                {
                                    /* while (AllDraw.ConvertedKind == -1) {
                                     }

                                     Rand = AllDraw.ConvertedKind;

                                     AllDraw.ConvertedKind = -2;
 */
                                    Rand = 0;
                                    ConvertedToMinister = true;
                                    AllDraw.ConvertedKind = -2;
                                }
                            }
                            else
                                if (//AllDraw.Person &&
                                AllDraw.StateCP && AllDraw.THISSecradioButtonBrownOrderChecked)
                            {
                                if (AllDraw.OrderPlateDraw == -1)
                                {
                                    //(new FormُSelectItems()).ShowDialog();
                                    /*  while (AllDraw.ConvertedKind == -1) {
                                      }

                                      Rand = AllDraw.ConvertedKind;

                                      AllDraw.ConvertedKind = -2;
                                 */
                                    Rand = 0;
                                    ConvertedToMinister = true;
                                    AllDraw.ConvertedKind = -2;
                                }
                            }
                            else
                            {
                                Rand = (new Random()).Next(0, 4);
                            }
                            //If Rand is Equaled the Operation will cuased automaticcally base on Color..
                            if (Rand == 0)
                            {
                                if (Order == 1)
                                {
                                    // MinisterMidle+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                    //MinisterHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                    Tab[RowS, ColumnS] = 5;
                                }
                                else if (Order == -1)
                                {
                                    //MinisterHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                    Tab[RowS, ColumnS] = -5;
                                }
                                ConvertedToMinister = true;
                            }
                            else if (Rand == 1)
                            {
                                if (Order == 1)
                                {
                                    //CastleMidle+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                    //CastleHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                    Tab[RowS, ColumnS] = 4;
                                }
                                else if (Order == -1)
                                {
                                    //CastleHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                    Tab[RowS, ColumnS] = -4;
                                }
                                ConvertedToCastle = true;
                            }
                            else if (Rand == 2)
                            {
                                if (Order == 1)
                                {
                                    //HourseMidle+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                    //HourseHight+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                    Tab[RowS, ColumnS] = 3;
                                }
                                else if (Order == -1)
                                {
                                    //HourseHight+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                    Tab[RowS, ColumnS] = -3;
                                }
                                ConvertedToHourse = true;
                            }
                            else
                            {
                                if (Order == 1)
                                {
                                    //ElefantMidle+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                    //ElefantHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                    Tab[RowS, ColumnS] = 2;
                                }
                                else if (Order == -1)
                                {
                                    //ElefantHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                    Tab[RowS, ColumnS] = -2;
                                }
                                ConvertedToElefant = true;
                            }
                        }
                        else
                        {
                            if (AllDraw.OrderPlateDraw == 1)
                            {
                                AllDraw.ConvertedKind = -1;
                                AllDraw.SodierConversionOcuured = true;
                                //Randomly Number of 4 kind Object.
                                int Rand = -1;
                                if (//AllDraw.Person &&
                                    AllDraw.StateCP && AllDraw.THISSecradioButtonGrayOrderChecked)
                                {
                                    if (AllDraw.OrderPlateDraw == 1)
                                    {
                                        /* while (AllDraw.ConvertedKind == -1) {
                                         }

                                         Rand = AllDraw.ConvertedKind;

                                         AllDraw.ConvertedKind = -2;
                                    */
                                        Rand = 0;
                                        ConvertedToMinister = true;
                                        AllDraw.ConvertedKind = -2;
                                    }
                                }
                                else
                                    if (//AllDraw.Person &&
                                    AllDraw.StateCP && AllDraw.THISSecradioButtonBrownOrderChecked)
                                {
                                    if (AllDraw.OrderPlateDraw == -1)
                                    {
                                        /*  while (AllDraw.ConvertedKind == -1) {
                                          }

                                          Rand = AllDraw.ConvertedKind;

                                          AllDraw.ConvertedKind = -2;
                                      */
                                        Rand = 0;
                                        ConvertedToMinister = true;
                                        AllDraw.ConvertedKind = -2;
                                    }
                                }
                                else
                                {
                                    Rand = (new Random()).Next(0, 4);
                                }
                                //If Rand is Equaled the Operation will cuased automaticcally base on Color..
                                if (Rand == 0)
                                {
                                    if (Order == 1)
                                    {
                                        //  MinisterMidle+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        //MinisterHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = 5;
                                    }
                                    else if (Order == -1)
                                    {
                                        //MinisterHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = -5;
                                    }
                                    ConvertedToMinister = true;
                                }
                                else if (Rand == 1)
                                {
                                    if (Order == 1)
                                    {
                                        //CastleMidle+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        //CastleHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = 4;
                                    }
                                    else if (Order == -1)
                                    {
                                        //CastleHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = -4;
                                    }
                                    ConvertedToCastle = true;
                                }
                                else if (Rand == 2)
                                {
                                    if (Order == 1)
                                    {
                                        //HourseMidle+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        //HourseHight+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = 3;
                                    }
                                    else if (Order == -1)
                                    {
                                        //HourseHight+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = -3;
                                    }
                                    ConvertedToHourse = true;
                                }
                                else
                                {
                                    if (Order == 1)
                                    {
                                        //ElefantMidle+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        //ElefantHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = 2;
                                    }
                                    else if (Order == -1)
                                    {
                                        //ElefantHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = -2;
                                    }
                                    ConvertedToElefant = true;
                                }
                            }
                            else
                            {
                                AllDraw.ConvertedKind = -1;
                                AllDraw.SodierConversionOcuured = true;
                                //Randomly Number of 4 kind Object.
                                int Rand = -1;
                                if (//AllDraw.Person &&
                                    AllDraw.StateCP && AllDraw.THISSecradioButtonGrayOrderChecked)
                                {
                                    if (AllDraw.OrderPlateDraw == 1)
                                    {
                                        /*  while (AllDraw.ConvertedKind == -1) {
                                          }

                                          Rand = AllDraw.ConvertedKind;

                                          AllDraw.ConvertedKind = -2;
                                    */
                                        Rand = 0;
                                        ConvertedToMinister = true;
                                        AllDraw.ConvertedKind = -2;
                                    }
                                }
                                else
                                    if (//AllDraw.Person &&
                                    AllDraw.StateCP && AllDraw.THISSecradioButtonBrownOrderChecked)
                                {
                                    if (AllDraw.OrderPlateDraw == -1)
                                    {
                                        /*   while (AllDraw.ConvertedKind == -1) {
                                           }

                                           Rand = AllDraw.ConvertedKind;

                                           AllDraw.ConvertedKind = -2;
                                      */
                                        Rand = 0;
                                        ConvertedToMinister = true;
                                        AllDraw.ConvertedKind = -2;
                                    }
                                }
                                else
                                {
                                    Rand = (new Random()).Next(0, 4);
                                }
                                //If Rand is Equaled the Operation will cuased automaticcally base on Color..
                                if (Rand == 0)
                                {
                                    if (Order == 1)
                                    {
                                        //MinisterMidle+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        //MinisterHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = 5;
                                    }
                                    else if (Order == -1)
                                    {
                                        //MinisterHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = -5;
                                    }
                                    ConvertedToMinister = true;
                                }
                                else if (Rand == 1)
                                {
                                    if (Order == 1)
                                    {
                                        //CastleMidle+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        //CastleHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = 4;
                                    }
                                    else if (Order == -1)
                                    {
                                        //CastleHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = -4;
                                    }
                                    ConvertedToCastle = true;
                                }
                                else if (Rand == 2)
                                {
                                    if (Order == 1)
                                    {
                                        //HourseMidle+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        //HourseHight+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = 3;
                                    }
                                    else if (Order == -1)
                                    {
                                        //HourseHight+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = -3;
                                    }
                                    ConvertedToHourse = true;
                                }
                                else
                                {
                                    if (Order == 1)
                                    {
                                        //ElefantMidle+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        //ElefantHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = 2;
                                    }
                                    else if (Order == -1)
                                    {
                                        //ElefantHigh+=0.00000000000000000000000000000000000000000000000000000000000000000000000001;
                                        Tab[RowS, ColumnS] = -2;
                                    }
                                    ConvertedToElefant = true;
                                }
                            }
                        }
                        AllDraw.ConvertWait = false;
                    }
                }
                AllDraw.ConvertWait = false;
                if (Convert)
                {
                    object O = new object();
                    lock (O)
                    {
                        TableConverted = new int[8, 8];
                        for (int iii = 0; iii < 8; iii++)
                        {
                            for (int jjj = 0; jjj < 8; jjj++)
                            {
                                TableConverted[iii, jjj] = Tab[iii, jjj];
                            }
                        }
                    }
                    object O1 = new object();
                    lock (O1)
                    {
                        LoadConvertTable = true;
                    }
                }

                ////{ //AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) //AllDraw.OutPut.Append(Space);  //AllDraw.OutPut.Append("ConvertOperation:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
                //return Convert State.
                return Convert;
            }
        }
    }
}

//End of Documeatation.