using Kelompok23.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kelompok23.View
{
    public class HalamanUbahData : ContentPage
    {
        private ListView _listView;
        private Entry _id;
        private Entry _nama;
        private Entry _jurusan;
        private Button _ubah;
        DataMahasiswa _mahasiswa = new DataMahasiswa();
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db4");

        public HalamanUbahData()
        {
            this.Title = "Ubah Mahasiswa";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<DataMahasiswa>().OrderBy(x => x.Nama).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _id = new Entry();
            _id.Placeholder = "ID";
            _id.IsVisible = false;
            stackLayout.Children.Add(_id);

            _nama = new Entry();
            _nama.Keyboard = Keyboard.Text;
            _nama.Placeholder = "Nama Mahasiswa";
            stackLayout.Children.Add(_nama);

            _jurusan = new Entry();
            _jurusan.Keyboard = Keyboard.Text;
            _jurusan.Placeholder = "Jurusan";
            stackLayout.Children.Add(_jurusan);

            _ubah = new Button();
            _ubah.Text = "Ubah";
            _ubah.Clicked += _ubah_Clicked;
            stackLayout.Children.Add(_ubah);

            Content = stackLayout;
        }

        private async void _ubah_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            DataMahasiswa dbm = new DataMahasiswa()
            {
                Id = Convert.ToInt32(_id.Text),
                Nama = _nama.Text,
                Jurusan = _jurusan.Text
            };
            db.Update(dbm);
            await DisplayAlert(null, "Data " + dbm.Nama + " Berhasil Diubah", "Ok");
            await Navigation.PopAsync();
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _mahasiswa = (DataMahasiswa)e.SelectedItem;
            _id.Text = _mahasiswa.Id.ToString();
            _nama.Text = _mahasiswa.Nama;
            _jurusan.Text = _mahasiswa.Jurusan;
        }
    }
}