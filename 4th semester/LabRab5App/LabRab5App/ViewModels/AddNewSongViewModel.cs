﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LabRab5App.Application.SongUseCase.Commands;

namespace LabRab5App.ViewModels
{
    [QueryProperty("SelectedArtist", "SelectedArtist")]
    [QueryProperty("Title", "Title")]
    [QueryProperty("Genre", "Genre")]
    [QueryProperty("Length", "Length")]
    [QueryProperty("ChartPos", "ChartPos")]
    [QueryProperty("Image", "Image")]
    [QueryProperty("Action", "Action")]
    public partial class AddNewSongViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        public AddNewSongViewModel(IMediator mediator) => _mediator = mediator;

        public Func <Song, Task<Song>> Action { get; set; }

        [ObservableProperty]
        FileResult image;

        [ObservableProperty]
        Artist selectedArtist;

        [ObservableProperty]
        string title;

        [ObservableProperty]
        string genre;

        [ObservableProperty]
        double length;

        [ObservableProperty]
        int chartPos;


        [RelayCommand]
        public async Task AddSong()
        {
            if (selectedArtist is null
                || title == string.Empty
                || genre == string.Empty
                || length == 0.0
                || chartPos == 0) return;
            await _mediator.Send(new AddSongCommand(title, genre, chartPos, length, selectedArtist.Id));
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task ChangeSong()
        {
            if (selectedArtist is null
                || title == string.Empty
                || genre == string.Empty
                || length == 0.0
                || chartPos == 0) return;

            var newSong = new Song(title, chartPos, genre, length);
            newSong.AddToArtist(selectedArtist.Id);

            if(image != null)
            {
                using var stream = await image.OpenReadAsync();
                var dirPath = Path.Combine(FileSystem.Current.AppDataDirectory, "Images");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                var filename = Path.Combine(dirPath, $"{newSong.Id}.png");
                using var fstream = File.Create(filename);
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fstream);
                stream.Seek(0, SeekOrigin.Begin);
            }

            await _mediator.Send(new ChangeSongsInfoCommand(newSong));
            await Shell.Current.GoToAsync("..");
            return;
        }

        [RelayCommand]
        public async void PickPhoto()
        {
            var filePickerTypes = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] {".png" } },
                    { DevicePlatform.WinUI,new[] {".png" } }
                });

            var options = new PickOptions()
            {
                PickerTitle = "Выберите, пожалуйста, обложку песни",
                FileTypes = filePickerTypes
            };

            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result == null) return;
                if (result.FileName.EndsWith("png",StringComparison.OrdinalIgnoreCase))
                {
                    Image = result;
                }
            }
            catch(Exception ex)
            {
                return;
            }
            return;
        }
    }
}
