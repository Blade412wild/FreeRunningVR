using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class CheckWhichHand
{
    public event Action<int> OnGrabGun;

    public void CheckGrabGun(bool input, bool LeftHand, bool RightHand)
    {

        if (!input) return;

        if (LeftHand && RightHand || RightHand)
        {
            OnGrabGun?.Invoke(1);
        }

        if (LeftHand)
        {
            OnGrabGun?.Invoke(0);
        }

    }
}

