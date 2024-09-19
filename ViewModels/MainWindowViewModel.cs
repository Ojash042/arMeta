using System.Reactive;

using ReactiveUI;

namespace arMeta.ViewModels;

public class MainWindowViewModel : ViewModelBase{
    private string _title = "";
    private string _artist = "";
    private string _albumName = "";
    private string _genre = "";
    private uint _trackNo = 0;
    private uint _trackCount = 0;
    private uint _discNo = 0;
    private uint _discTotal = 0;
    // private String _lyrics = ""; 
    private string _albumArtist = "";
    private string _composer = "";
    private uint _year = 0 ;

    private string _fileLocation = "";
    private bool _isSongSelected = false;
    
    public string Title{
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public string Artist{
        get => _artist;
        set => this.RaiseAndSetIfChanged(ref _artist, value);
    }

    public string AlbumName{
        get => _albumName;
        set => this.RaiseAndSetIfChanged(ref _albumName, value);
    }

    public string Genre{
        get => _genre;
        set => this.RaiseAndSetIfChanged(ref _genre, value);
    }

    public uint TrackNo{
        get => _trackNo;
        set => this.RaiseAndSetIfChanged(ref _trackNo, value);
    }

    public uint TrackCount{
        get => _trackCount;
        set => this.RaiseAndSetIfChanged(ref _trackCount, value);
    }

    public uint DiscNo{
        get => _discNo;
        set => this.RaiseAndSetIfChanged(ref _discNo, value);
    }

    public uint DiscTotal{
        get => _discTotal;
        set => this.RaiseAndSetIfChanged(ref _discTotal, value);
    }

    /* public string Lyrics{
        get => _lyrics;
        set => this.RaiseAndSetIfChanged(ref _lyrics, value);
    } */

    public string AlbumArtist{
        get => _albumArtist;
        set => this.RaiseAndSetIfChanged(ref _albumArtist, value);
    }

    public string Composer{
        get => _composer;
        set => this.RaiseAndSetIfChanged(ref _composer, value);
    }

    public uint Year{
        get => _year;
        set => this.RaiseAndSetIfChanged(ref _year, value);
    }

    public string FileLocation{
        get => _fileLocation;
        set => this.RaiseAndSetIfChanged(ref _fileLocation, value);
    }

    public bool IsSongSelected{
        get => _isSongSelected;
        set => this.RaiseAndSetIfChanged(ref _isSongSelected, value);
    }
    
}