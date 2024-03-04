using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimatorHashes
{
    public static class AnimatorHashes { }

    public static class OneShotHashes
    {
        public static readonly int Activate = Animator.StringToHash("Activate");
    }

    public static class BinaryHashes
    {
        public static readonly int Toggle = Animator.StringToHash("Toggle");
    }

    public static class UIHashes
    {
        public static readonly int Open = Animator.StringToHash("Open");
        public static readonly int Close = Animator.StringToHash("Close");
        public static readonly int Shy = Animator.StringToHash("Shy");
        public static readonly int Bold = Animator.StringToHash("Bold");
        public static readonly int Stop = Animator.StringToHash("Stop");
    }
}
