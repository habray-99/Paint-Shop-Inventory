using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NepaliDateConverter.Net;

namespace WebApplication1.Models;

public class DateBsConverter : ValueConverter<string, DateTime>
{
    public DateBsConverter() : base(
        v => ConvertToGregorian(v),
        v => ConvertToBikramSambat(v))
    {
    }

    // Converts a BS date string ("yyyy-MM-dd") to Gregorian DateTime
    private static DateTime ConvertToGregorian(string bsDate)
    {
        var parts = bsDate.Split('-').Select(int.Parse).ToArray();
        var ad = DateConverter.ConvertToEnglish(parts[0], parts[1], parts[2]);
        return new DateTime(ad.Year, ad.Month, ad.Day);
    }

    // Converts a Gregorian DateTime to a BS date string ("yyyy-MM-dd")
    private static string ConvertToBikramSambat(DateTime adDate)
    {
        var bs = DateConverter.ConvertToNepali(adDate.Year, adDate.Month, adDate.Day);
        return $"{bs.Year}-{bs.Month:D2}-{bs.Day:D2}";
    }
}