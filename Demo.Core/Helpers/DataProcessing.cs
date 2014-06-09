using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Demo.Core.Helpers
{
    public static class DataProcessing
    {
        public static IQueryable<T> Processing<T>(this IQueryable<T> model, NameValueCollection queryString)
        {
            var propertyInfo = model.ElementType.GetProperties();

            foreach (string item in queryString)
            {
                if (!item.StartsWith("Search_") || string.IsNullOrEmpty(queryString[item]) || queryString[item] == ",")
                    continue;

                var proname = item.Replace("Search_", "");
                switch (propertyInfo.Single(a => a.Name.Equals(proname)).GetMethod.ReturnType.Name)
                {
                    case "Boolean":
                        model = model.Where(proname + " = @0", Boolean.Parse(queryString[item]));
                        break;
                    case "Int32":
                        var int32 = queryString[item];
                        if (!string.IsNullOrEmpty(int32.Split(',')[0]))
                        {
                            model = model.Where(proname + " == @0 ", int.Parse(int32.Split(',')[0]));
                        }
                        //if (!string.IsNullOrEmpty(int32.Split(',')[1]))
                        //{
                        //    model = model.Where(proname + " <= @0 ", int.Parse(int32.Split(',')[1]));
                        //}
                        break;
                    case "Decimal":
                        var val = queryString[item];
                        var temp = val.Split(',');
                        if (!string.IsNullOrEmpty(val.Split(',')[0]))
                        {
                            model = model.Where(proname + " >= @0 ", decimal.Parse(val.Split(',')[0]));
                        }
                        if (temp.Length > 1 && !string.IsNullOrEmpty(val.Split(',')[1]))
                        {
                            model = model.Where(proname + " <= @0 ", decimal.Parse(val.Split(',')[1]));
                        }
                        break;
                    case "DateTime":
                        var dateTime = queryString[item];
                        if (!string.IsNullOrEmpty(dateTime.Split(',')[0]))
                        {
                            model = model.Where(proname + " == @0 ", DateTime.Parse(dateTime.Split(',')[0]));
                        }
                        //if (!string.IsNullOrEmpty(dateTime.Split(',')[1]))
                        //{
                        //    model = model.Where(proname + " <= @0 ", DateTime.Parse(dateTime.Split(',')[1]));
                        //}
                        break;
                    default:
                        model = model.Where(proname + ".Contains(@0)", queryString[item]);
                        break;
                }
            }

            //order
            if (!string.IsNullOrEmpty(queryString["ordering"]))
            {
                model = model.OrderBy(queryString["ordering"], null);
            }

            return model;
        }
    }
}