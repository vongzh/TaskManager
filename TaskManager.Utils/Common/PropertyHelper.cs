using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TaskManager.Utils.Common
{

    public class PropertyHelper
    {
        public static void Copy(object from, object to)
        {
            //利用反射获得类成员
            var fieldFroms = from.GetType().GetProperties();
            var fieldTos = to.GetType().GetProperties();
            int lenTo = fieldTos.Length;

            for (int i = 0, l = fieldFroms.Length; i < l; i++)
            {
                for (int j = 0; j < lenTo; j++)
                {
                    if (fieldTos[j].Name != fieldFroms[i].Name) continue;
                    fieldTos[j].SetValue(to, fieldFroms[i].GetValue(from,null),null);
                    break;
                }
            }
        }

    }
}
