using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringsManager
{
    public static string t1p1;
    public static string t1p2;
    public static string t1p3;
    public static string t1p4;
    public static string t2p1;
    public static string t2p2;
    public static string t2p3;
    public static string t2p4;

    public static void ChangeString(int team, int player, string string_)
    {
        if (team == 1)
        {
            switch (player)
            {
                case 1:
                    t1p1 = string_;
                    break;
                case 2:
                    t1p2 = string_;
                    break;
                case 3:
                    t1p3 = string_;
                    break;
                case 4:
                    t1p4 = string_;
                    break;
            }
        }
        else
        {
            switch (player)
            {
                case 1:
                    t2p1 = string_;
                    break;
                case 2:
                    t2p2 = string_;
                    break;
                case 3:
                    t2p3 = string_;
                    break;
                case 4:
                    t2p4 = string_;
                    break;
            }
        }
    }

    public static void Reset()
    {
        t1p1 = "";
        t1p2 = "";
        t1p3 = "";
        t1p4 = "";
        t2p1 = "";
        t2p2 = "";
        t2p3 = "";
        t2p4 = "";
    }
}