using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MEC;


namespace Kaideu.Utils
{
    public static class Helpers
    {
        /// <summary>
        /// Uses Lambda to use bool by reference (x => myBool = x).
        /// Can take multiple bools if they cooldown at the same time.
        /// This is done for each bool given.
        /// </summary>
        /// <param name="time"> Length of time to keep bool(s) in timerValue</param>
        /// <param name="toggleValue">Value to keep bools at during length of time</param>
        /// <param name="boolToTimeArr">Bools to keep at timerValue for specified length of time</param>
        public static IEnumerator<float> IE_ToggleBoolForSecondsAt(float time, bool toggleValue, params Action<bool>[] boolToTimeArr)
        {
            for (int i = 0; i < boolToTimeArr.Length; i++)
            {
                boolToTimeArr[i](toggleValue);
            }

            yield return Timing.WaitForSeconds(time);

            for (int i = 0; i < boolToTimeArr.Length; i++)
            {
                boolToTimeArr[i](!toggleValue);
            }
        }

        public static IEnumerator IE_WaitForFrames(int frameCount)
        {
            if (frameCount <= 0)
            {
                throw new ArgumentOutOfRangeException("frameCount", "Cannot wait for less that 1 frame");
            }

            while (frameCount > 0)
            {
                frameCount--;
                yield return null;
            }
        }

        public static Vector2 AngleToDirection2D(float angle)
        {
            return new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));
        }

        //usage ->  var copy = myComp.GetCopyOf(someOtherComponent);
        public static T GetCopyOf<T>(this Component comp, T other) where T : Component
        {
            Type type = comp.GetType();
            if (type != other.GetType()) return null; // type mis-match
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
            PropertyInfo[] pinfos = type.GetProperties(flags);
            foreach (var pinfo in pinfos)
            {
                if (pinfo.CanWrite)
                {
                    try
                    {
                        pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                    }
                    catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
                }
            }
            FieldInfo[] finfos = type.GetFields(flags);
            foreach (var finfo in finfos)
            {
                finfo.SetValue(comp, finfo.GetValue(other));
            }
            return comp as T;
        }

        //usage ->  Health myHealth = gameObject.AddComponent<Health>(enemy.health);
        public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
        {
            return go.AddComponent<T>().GetCopyOf(toAdd) as T;
        }

        public static bool IsInLayerMask(LayerMask mask, int layer) => mask == (mask | 1 << layer);
        public static string FormatTime(float time)
        {
            int minutes = (int)time / 60;
            int seconds = (int)time - 60 * minutes;
            int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));
            return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }

        public static List<T> ReturnListOfDuplicates<T>(List<T> l1, List<T> l2 )
        {
            var temp = new List<T>(); 
            foreach(var obj in l1)
            {
                if (l2.Contains(obj))
                    temp.Add(obj);
            }

            return temp;
        }

    }

#if UNITY_EDITOR
    public class InspectorUtilities
    {
        //For Inspector Debugging
        public class ReadOnlyAttribute : PropertyAttribute
        {

        }

        [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
        public class ReadOnlyDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property,
                                                    GUIContent label)
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }

            public override void OnGUI(Rect position,
                                       SerializedProperty property,
                                       GUIContent label)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label, true);
                GUI.enabled = true;
            }
        }
    }

#endif
}