using Android.App;
using Android.Content;
using AndroidX.Core.App;

namespace WeatherApp.Droid
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var title = intent.GetStringExtra("title");
            var message = intent.GetStringExtra("message");

            var builder = new NotificationCompat.Builder(context, "weather_channel")
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.ic_notification)
                .SetPriority(NotificationCompat.PriorityHigh)
                .SetAutoCancel(true);

            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(0, builder.Build());
        }
    }
}