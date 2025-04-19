using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NepaliDateConverter.Net;

namespace WebApplication1.Models;

public class DateBsConverter
{
    public static DateConverter ToBs(DateConverter date) => DateConverter.ConvertToNepali(date.Year, date.Month, date.Day);
    public static DateConverter ToAd(DateTime date) => DateConverter.ConvertToEnglish(date.Year, date.Month, date.Day);
    
}