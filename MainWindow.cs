using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace AplikasiMenu;

public class MainWindow : Window
{
    public MainWindow()
    {
        Title = "Aplikasi Menu - Pemrograman Visual";
        Width = 600;
        Height = 400;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        var menu = new Menu();

        var mFile = new MenuItem { Header = "_File" };
        mFile.Items.Add(Item("Keluar", () => Close()));

        var mBil = new MenuItem { Header = "_Bilangan" };
        mBil.Items.Add(Item("Bilangan Ganjil", () => Buka(new FormBilangan(true))));
        mBil.Items.Add(Item("Bilangan Genap", () => Buka(new FormBilangan(false))));

        var mHitung = new MenuItem { Header = "_Hitung" };
        mHitung.Items.Add(Item("Tambah", () => Buka(new FormHitung("+"))));
        mHitung.Items.Add(Item("Kurang", () => Buka(new FormHitung("-"))));
        mHitung.Items.Add(Item("Kali", () => Buka(new FormHitung("*"))));
        mHitung.Items.Add(Item("Bagi", () => Buka(new FormHitung("/"))));
        mHitung.Items.Add(Item("Sisa Bagi (Mod)", () => Buka(new FormHitung("Mod"))));
        mHitung.Items.Add(Item("Pangkat", () => Buka(new FormHitung("^"))));
        
        var mLain = new MenuItem { Header = "_Lainnya" };
        mLain.Items.Add(Item("Faktorial", () => Buka(new FormFaktorial())));

        var mBantuan = new MenuItem { Header = "B_antuan" };
        mBantuan.Items.Add(Item("About", () => Buka(new FormAbout())));

        menu.Items.Add(mFile);
        menu.Items.Add(mBil);
        menu.Items.Add(mHitung);
        menu.Items.Add(mLain);
        menu.Items.Add(mBantuan);

        var sambutan = new TextBlock
        {
            Text = "Selamat Datang!\nSilakan pilih menu di atas.",
            FontSize = 22,
            FontWeight = FontWeight.Bold,
            TextAlignment = TextAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        var dock = new DockPanel();
        DockPanel.SetDock(menu, Dock.Top);
        dock.Children.Add(menu);
        dock.Children.Add(sambutan);
        Content = dock;
    }

    private static MenuItem Item(string teks, Action aksi)
    {
        var m = new MenuItem { Header = teks };
        m.Click += (_, _) => aksi();
        return m;
    }

    private void Buka(Window form) => form.ShowDialog(this);
}
