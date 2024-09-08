using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using System;

namespace WeatherApp.Droid
{
    public class NotificationService
    {
        public void ScheduleNotification(string title, string message, DateTime notifyTime)
        {
            var intent = new Intent(Application.Context, typeof(AlarmReceiver));
            intent.PutExtra("title", title);
            intent.PutExtra("message", message);

            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, intent, PendingIntentFlags.UpdateCurrent);

            var alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
            var triggerTime = (long)(notifyTime - DateTime.Now).TotalMilliseconds;

            alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + triggerTime, pendingIntent);
        }
    }
}

public partial class MainPage : ContentPage
{
    private readonly LocationService _locationService;
    private readonly FavoritesService _favoritesService;
    private readonly NotificationService _notificationService;

    public MainPage()
    {
        InitializeComponent();
        _locationService = new LocationService(new HttpClient());
        _favoritesService = new FavoritesService();
        _notificationService = new NotificationService();
    }

    private async void OnSearchClicked(object sender, EventArgs e)
    {
        var query = SearchEntry.Text;
        var results = await _locationService.SearchLocationsAsync(query);
        ResultsListView.ItemsSource = results;
    }

    private async void OnViewFavoritesClicked(object sender, EventArgs e)
    {
        var favorites = await _favoritesService.GetFavoritesAsync();
        ResultsListView.ItemsSource = favorites;
    }

    private void ScheduleWeatherNotification(string location, DateTime notifyTime)
    {
        var title = "Weather Alert";
        var message = $"Check the weather forecast for {location}.";
        _notificationService.ScheduleNotification(title, message, notifyTime);
    }
}