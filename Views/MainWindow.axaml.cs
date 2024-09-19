using System;
using System.Diagnostics;
using System.IO;
using TagLib;
using arMeta.ViewModels;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using ReactiveUI;

namespace arMeta.Views;

public partial class MainWindow : Window{
    
    
    private MainWindowViewModel _mainWindowModel;

    private static FilePickerFileType MusicFile{ get; } = new("Music File"){
        Patterns = new []{"*.mp3", "*.ogg", "*.flac", "*.wav"},
        AppleUniformTypeIdentifiers = new [] {"public.data"},
        MimeTypes = new []{"audio/*"},
    };
    public MainWindow(){
        InitializeComponent();
        _mainWindowModel = new ();
        DataContext = _mainWindowModel;
    }

    public async void SelectSongDialog(object sender, RoutedEventArgs args){
        var topLevel = TopLevel.GetTopLevel(this);
        
        Debug.Assert(topLevel != null, nameof(topLevel) + " != null");
        var file = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions{
            Title = "Choose a mp3 file",
            AllowMultiple = false,
            FileTypeFilter = new []{MusicFile}
        });


        if (file.Count == 1 && DataContext is MainWindowViewModel){
            var tfile = TagLib.File.Create(Uri.UnescapeDataString(file[0].Path.AbsolutePath)).Tag;
            
            // TODO: Animate a Toaster;
            if (tfile is null){
                return;
            }
            
            _mainWindowModel = new MainWindowViewModel(){
                Title = tfile.Title,
                Artist = tfile.JoinedAlbumArtists,
                AlbumName = tfile.Album,
                Genre = tfile.JoinedGenres,
                TrackNo = tfile.Track,
                TrackCount = tfile.TrackCount,
                DiscNo = tfile.Disc,
                DiscTotal = tfile.DiscCount,
                AlbumArtist = tfile.JoinedAlbumArtists,
                Composer = tfile.JoinedComposers,
                Year = tfile.Year,
                
                IsSongSelected = true,
                FileLocation = file[0].Name,
            };
            DataContext = _mainWindowModel;
        }
    }
}