using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KavoshFrameWorkCommon.Extensions
{
    public static class GridMVCExtentions
    {
        public static List<T> Filter<T>(this List<T> list, string search)
        {
            try
            {
                if (search == null)
                    return list;
                return list.Select(x => new
                {
                    X = x,
                    Props = x.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                })
                .Where(x => x.Props.Any(p =>
                {
                    var val = p.GetValue(x.X, null);
                    return val != null
                    && val.GetType().GetMethod("ToString", Type.EmptyTypes).DeclaringType == val.GetType()
                    && val.ToString().Contains(search);
                }))
                .Select(x => x.X).ToList();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

    }
}
