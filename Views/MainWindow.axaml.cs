using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    
    // HACK: REMOVE THIS
    private String fileLocation; 

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
            fileLocation = file[0].Path.AbsolutePath;
            
            var tFile = TagLib.File.Create(Uri.UnescapeDataString(file[0].Path.AbsolutePath));
            var tFileTag = tFile.Tag;
            
            // TODO: Animate a Toaster;
            if ( tFileTag == null){
                return;
            }
            
            _mainWindowModel = new MainWindowViewModel(){
                Title = tFileTag.Title,
                Artist = tFile.Tag.JoinedPerformers,
                AlbumName = tFileTag.Album,
                Genre = tFileTag.JoinedGenres,
                TrackNo = tFileTag.Track,
                TrackCount = tFileTag.TrackCount,
                DiscNo = tFileTag.Disc,
                DiscTotal = tFileTag.DiscCount,
                AlbumArtist = tFileTag.JoinedAlbumArtists,
                Composer = tFileTag.JoinedComposers,
                Year = tFileTag.Year,
                
                IsSongSelected = true,
                FileLocation = file[0].Name,
            };
            
            DataContext = _mainWindowModel;
        }
    }

    public void SaveID3TagToFile(object? sender, RoutedEventArgs routedEventArgs){

        var tFile = TagLib.File.Create(Uri.UnescapeDataString(fileLocation));
        var tFileTag = tFile.Tag;
        if (tFileTag == null){
           // TODO: Animate Toaster 
           return;
        }

        if (!string.IsNullOrEmpty(_mainWindowModel.Title)) tFileTag.Title = _mainWindowModel.Title;
        if (!string.IsNullOrEmpty(_mainWindowModel.Artist)) tFileTag.Performers = _mainWindowModel.Artist.Split(",");
        if (!string.IsNullOrEmpty(_mainWindowModel.AlbumName)) tFileTag.Album = _mainWindowModel.AlbumName;
        if (!string.IsNullOrEmpty(_mainWindowModel.Genre)) tFileTag.Genres = _mainWindowModel.Genre.Split(",");
        if (_mainWindowModel.TrackNo != null) tFileTag.Track = _mainWindowModel.TrackNo.Value;
        if (_mainWindowModel.TrackCount != null) tFileTag.TrackCount = _mainWindowModel.TrackCount.Value;
        if (_mainWindowModel.DiscNo != null)   tFileTag.Disc = _mainWindowModel.DiscNo.Value;
        if (_mainWindowModel.DiscTotal != null) tFileTag.DiscCount = _mainWindowModel.DiscTotal.Value; 
        if (!string.IsNullOrEmpty(_mainWindowModel.AlbumArtist)) tFileTag.AlbumArtists = _mainWindowModel.AlbumArtist.Split(",");
        if (!string.IsNullOrEmpty(_mainWindowModel.Composer)) tFileTag.Composers = _mainWindowModel.Composer.Split(",");
        if(_mainWindowModel.Year != null) tFileTag.Year = _mainWindowModel.Year.Value;
        
        tFile.Save();
    }
}