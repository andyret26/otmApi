namespace OtmApi.Utils;

public class Functions
{
    public static DateTime GetRandomeDate(DateTime startDate, DateTime endDate)
    {
        while (true)
        {
            Random rand = new Random();
            int range = (int)(endDate - startDate).TotalHours;
            int randomeHoures = rand.Next(range + 1);
            // between 08 utc and 22 utc
            if (startDate.AddHours(randomeHoures).Hour >= 8 && startDate.AddHours(randomeHoures).Hour <= 22)
            {
                return startDate.AddHours(randomeHoures);
            }
        }

    }

    public static DateTime RoundToNearestHour(DateTime dateTime)
    {
        int minutes = dateTime.Minute;
        int seconds = dateTime.Second;
        int milliseconds = dateTime.Millisecond;

        // Calculate total minutes and seconds
        double totalMinutes = minutes + (seconds / 60.0) + (milliseconds / 60000.0);

        if (totalMinutes >= 30)
        {
            // If total minutes >= 30, round up to the next hour
            return dateTime.AddHours(1).AddMinutes(-minutes).AddSeconds(-seconds).AddMilliseconds(-milliseconds);
        }
        else
        {
            // Otherwise, round down to the current hour
            return dateTime.AddMinutes(-minutes).AddSeconds(-seconds).AddMilliseconds(-milliseconds);
        }
    }
}