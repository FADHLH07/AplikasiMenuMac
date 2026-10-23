using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace AplikasiMenu;

public static class UI
{

    public static async Task Alert(Window owner, string pesan, string judul = "Peringatan")
    {
        var w = new Window
        {
            Title = judul,
            Width = 330,
            Height = 150,
            CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        var ok = new Button { Content = "OK", HorizontalAlignment = HorizontalAlignment.Center };
        ok.Click += (_, _) => w.Close();

        w.Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 15,
            Children =
            {
                new TextBlock { Text = pesan, TextWrapping = Avalonia.Media.TextWrapping.Wrap },
                ok
            }
        };
        await w.ShowDialog(owner);
    }

    public static bool TryNum(string? teks, out double hasil)
    {
        teks = (teks ?? "").Trim().Replace(',', '.');
        return double.TryParse(teks, NumberStyles.Float, CultureInfo.InvariantCulture, out hasil);
    }

    public static string Fmt(double d) => d.ToString("G15", CultureInfo.InvariantCulture);
}
