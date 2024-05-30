using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class CheckWhichHand
{
    public int CheckGrabGun(bool LeftHand, bool RightHand)
    {
        if (LeftHand && RightHand || RightHand) // righthand 
        {
            return 1;
        }

        if (LeftHand) // lefthand
        {
            return 0;
        }

        return 3;
    }
}

