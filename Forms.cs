using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace AplikasiMenu;

public class FormBilangan : Window
{
    public FormBilangan(bool ganjil)
    {
        Title = ganjil ? "Bilangan Ganjil" : "Bilangan Genap";
        Width = 340;
        Height = 430;
        CanResize = false;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        var txt = new TextBox { Width = 80, Watermark = "mis. 20" };
        var btn = new Button { Content = "Tampilkan" };
        var lst = new ListBox { Height = 280 };
        var lblJumlah = new TextBlock { Text = "Jumlah: 0" };

        var atas = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 8,
            Children =
            {
                new TextBlock { Text = "Sampai bilangan:", VerticalAlignment = VerticalAlignment.Center },
                txt,
                btn
            }
        };

        Content = new StackPanel
        {
            Margin = new Thickness(15),
            Spacing = 10,
            Children = { atas, lst, lblJumlah }
        };

        btn.Click += async (_, _) =>
        {
            if (!int.TryParse(txt.Text, out int n) || n < 1 || n > 10000)
            {
                await UI.Alert(this, "Masukkan angka bulat 1 sampai 10000!");
                return;
            }

            // simpan hasil ke dalam ARRAY
            int[] arr = new int[n];
            int k = 0;
            for (int i = 1; i <= n; i++)
            {
                if ((i % 2 == 1) == ganjil)
                {
                    arr[k] = i;
                    k++;
                }
            }

            // tampilkan isi array ke ListBox
            lst.Items.Clear();
            for (int i = 0; i < k; i++)
                lst.Items.Add(arr[i]);

            lblJumlah.Text = "Jumlah: " + k;
        };
    }
}

public class FormHitung : Window
{
    public FormHitung(string op)
    {
        Title = op switch
        {
            "+" => "Penjumlahan",
            "-" => "Pengurangan",
            "*" => "Perkalian",
            "/" => "Pembagian",
            "Mod" => "Sisa Bagi (Modulus)",
            _ => "Perpangkatan"
        };
        Width = 360;
        Height = 430;
        CanResize = false;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        var txtA = new TextBox { Width = 85 };
        var txtB = new TextBox { Width = 85 };
        var btn = new Button { Content = "Hitung" };
        var lblHasil = new TextBlock { Text = "Hasil: -", FontSize = 16, FontWeight = FontWeight.Bold };
        var lst = new ListBox { Height = 250 };

        var baris = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 8,
            Children =
            {
                txtA,
                new TextBlock { Text = op, FontSize = 16, VerticalAlignment = VerticalAlignment.Center },
                txtB,
                btn
            }
        };

        Content = new StackPanel
        {
            Margin = new Thickness(15),
            Spacing = 10,
            Children = { baris, lblHasil, new TextBlock { Text = "Riwayat:" }, lst }
        };

        btn.Click += async (_, _) =>
        {
            if (!UI.TryNum(txtA.Text, out double a) || !UI.TryNum(txtB.Text, out double b))
            {
                await UI.Alert(this, "Isi kedua angka dengan benar!");
                return;
            }

            double hasil = 0;
            switch (op)
            {
                case "+": hasil = a + b; break;
                case "-": hasil = a - b; break;
                case "*": hasil = a * b; break;
                case "/":
                    if (b == 0) { await UI.Alert(this, "Tidak bisa dibagi nol!", "Error"); return; }
                    hasil = a / b;
                    break;
                case "Mod":
                    if (b == 0) { await UI.Alert(this, "Pembagi tidak boleh nol!", "Error"); return; }
                    hasil = a % b;
                    break;
                case "^": hasil = Math.Pow(a, b); break;
            }

            lblHasil.Text = "Hasil: " + UI.Fmt(hasil);
            lst.Items.Add($"{UI.Fmt(a)} {op} {UI.Fmt(b)} = {UI.Fmt(hasil)}");
        };
    }
}

public class FormFaktorial : Window
{
    public FormFaktorial()
    {
        Title = "Faktorial";
        Width = 340;
        Height = 430;
        CanResize = false;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        var txt = new TextBox { Width = 80, Watermark = "0 - 20" };
        var btn = new Button { Content = "Hitung" };
        var lblHasil = new TextBlock { Text = "Hasil: -", FontSize = 16, FontWeight = FontWeight.Bold };
        var lst = new ListBox { Height = 280 };

        var atas = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 8,
            Children =
            {
                new TextBlock { Text = "Bilangan:", VerticalAlignment = VerticalAlignment.Center },
                txt,
                btn
            }
        };

        Content = new StackPanel
        {
            Margin = new Thickness(15),
            Spacing = 10,
            Children = { atas, lblHasil, lst }
        };

        btn.Click += async (_, _) =>
        {
            if (!int.TryParse(txt.Text, out int n) || n < 0 || n > 20)
            {
                await UI.Alert(this, "Masukkan angka 0 sampai 20!");
                return;
            }

            long hasil = 1;
            lst.Items.Clear();
            lst.Items.Add("0! = 1");
            for (int i = 1; i <= n; i++)
            {
                hasil *= i;
                lst.Items.Add($"{i}! = {hasil}");
            }
            lblHasil.Text = $"Hasil: {n}! = {hasil}";
        };
    }
}

public class FormAbout : Window
{
    public FormAbout()
    {
        Title = "About";
        Width = 380;
        Height = 320;
        CanResize = false;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        string[,] data =
        {
            { "Nama",        "FAADHILLAH DESTIA FITRI" },
            { "NIM",         "292102779" },
            { "Kelas",       "TI" },
            { "Prodi",       "Teknik Informatika" },
            { "Kampus",      "STIMIK PONTIANAK" },
            { "Email",       "FAADHILLAH07@FMAIL.com" },
            { "Mata Kuliah", "Pemrograman Visual" }
        };

        var panel = new StackPanel { Margin = new Thickness(20), Spacing = 8 };
        panel.Children.Add(new TextBlock
        {
            Text = "Tentang Pembuat Aplikasi",
            FontSize = 18,
            FontWeight = FontWeight.Bold,
            Margin = new Thickness(0, 0, 0, 10)
        });

        for (int i = 0; i < data.GetLength(0); i++)
        {
            panel.Children.Add(new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    new TextBlock { Text = data[i, 0], Width = 110, FontWeight = FontWeight.SemiBold },
                    new TextBlock { Text = ": " + data[i, 1] }
                }
            });
        }

        var tutup = new Button
        {
            Content = "Tutup",
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 15, 0, 0)
        };
        tutup.Click += (_, _) => Close();
        panel.Children.Add(tutup);

        Content = panel;
    }
}
