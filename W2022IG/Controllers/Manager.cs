// **************************************************
// WEB524 Project Template V3 == 6c0bf153-60c0-48a8-8601-9254afafd8c8
// Do not change this header.
// **************************************************

using AutoMapper;
using W2022IG.EntityModels;
using W2022IG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace W2022IG.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<ArtistAddViewModel, Artist>();
                cfg.CreateMap<Artist, ArtistWithMediaInfoViewModel>();
                cfg.CreateMap<ArtistBaseViewModel, ArtistEditFormViewModel>();

                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<AlbumAddViewModel, Album>();

                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackMediaViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<TrackBaseViewModel, TrackEditFormViewModel>();

                cfg.CreateMap<Genre, GenreBaseViewModel>();

                cfg.CreateMap<ArtistMediaItem, ArtistMediaItemBaseViewModel>();
                cfg.CreateMap<ArtistMediaItem, ArtistMediaItemContentViewModel>();
                cfg.CreateMap<ArtistMediaItemAddViewModel, ArtistMediaItem>();
                

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>


        public IEnumerable<GenreBaseViewModel> GetAllGenre()
        {
            
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(ds.Genres.OrderBy(a => a.Name));
        }

        public IEnumerable<ArtistBaseViewModel> GetAllArtist()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(ds.Artists.OrderBy(a => a.Name));
        }

        public ArtistBaseViewModel GetArtistByID(int id)
        {
            
            var artist = ds.Artists.Find(id);

            if (null == artist)
            {
                return null;
            }

            return mapper.Map<Artist, ArtistBaseViewModel>(artist);
        }

        public ArtistBaseViewModel AddArtist(ArtistAddViewModel newArtist)
        {
            var genre = ds.Genres.Find(newArtist.GenreId).Name;
            var artist = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newArtist));
            artist.Genre = genre;
            artist.Executive = HttpContext.Current.User.Identity.Name;

            ds.SaveChanges();

            if (null == artist)
            {
                return null;
            }

            return mapper.Map<Artist, ArtistBaseViewModel>(artist);
        }

        public ArtistBaseViewModel EditArtist(ArtistEditFormViewModel editArtist)
        {
            var artist = ds.Artists.Find(editArtist.Id);
            var genre = ds.Genres.Find(editArtist.GenreId);
            if ( null == artist)
            {
                return null;
            }

            artist.Name = editArtist.Name;
            artist.BirthName = editArtist.BirthName;
            artist.BirthOrStartDate = editArtist.BirthOrStartDate;
            artist.UrlArtist = editArtist.UrlArtist;
            artist.Biography = editArtist.Biography;
            artist.Genre = genre.Name;



 

            ds.SaveChanges();

            return mapper.Map<Artist, ArtistBaseViewModel>(artist);
        }

        public ArtistWithMediaInfoViewModel GetArtistWMediaInfoVM(int id)
        {
            var artist = ds.Artists.Include("ArtistMediaItems").SingleOrDefault(a => a.Id == id);

            if (null == artist)
            {
                return null;
            }



            return mapper.Map<Artist, ArtistWithMediaInfoViewModel>(artist);
        }

        public IEnumerable<AlbumBaseViewModel> GetAllAlbums()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(ds.Albums.OrderBy(a => a.Name));
        }

        public AlbumBaseViewModel GetAlbumByID(int id)
        {
            var album = ds.Albums.Find(id);

            if ( null == album)
            {
                return null;
            }

            return mapper.Map<Album, AlbumBaseViewModel>(album);
        }

        public AlbumBaseViewModel AddAlbum(AlbumAddViewModel newAlbum)
        {
            var genre = ds.Genres.Find(newAlbum.GenreId).Name;
            var album = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newAlbum));


            album.Genre = genre;
            album.Coordinator = HttpContext.Current.User.Identity.Name;

            

            ds.SaveChanges();

            if (null == album)
            {
                return null;
            }

            return mapper.Map<Album, AlbumBaseViewModel>(album);
        }

        public IEnumerable<TrackBaseViewModel> GetAllTrack()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.Include("Albums").OrderBy(a => a.Name));
        }

        public TrackBaseViewModel GetTrackByID(int id)
        {
            var track = ds.Tracks.Find(id);

            if (null == track)
            {
                return null;
            }


            return mapper.Map<Track, TrackBaseViewModel>(track);
        }

        public TrackMediaViewModel GetTrackMediaByID(int id)
        {
            var track = ds.Tracks.Find(id);

            if (null == track)
            {
                return null;
            }

            return mapper.Map<Track, TrackMediaViewModel>(track);
        }

        public TrackBaseViewModel AddTrack(TrackAddViewModel newTrack)
        {
    
            var genre = ds.Genres.Find(newTrack.GenreId).Name;
            var album = ds.Albums.Find(newTrack.AlbumId);

            if (null == album)
            {
                return null;
            }

            var track = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newTrack));

            track.Clerk = HttpContext.Current.User.Identity.Name;
            track.Genre = genre;

            track.Albums.Add(album);

            byte[] MediaBytes = new byte[newTrack.MediaUpload.ContentLength];
            newTrack.MediaUpload.InputStream.Read(MediaBytes, 0, newTrack.MediaUpload.ContentLength);



            track.Media = MediaBytes;

            track.MediaContentType = newTrack.MediaUpload.ContentType;


            ds.SaveChanges();

            if (null == track)
            {
                return null;
            }

            return mapper.Map<Track, TrackBaseViewModel>(track);
        }

        public TrackBaseViewModel EditTrack(TrackEditViewModel editTrack)
        {
            var track = ds.Tracks.Find(editTrack.Id);

            if(null == track)
            {
                return null;
            }

            byte[] MediaBytes = new byte[editTrack.MediaUpload.ContentLength];

            editTrack.MediaUpload.InputStream.Read(MediaBytes, 0, editTrack.MediaUpload.ContentLength);

            track.Media = MediaBytes;

            track.MediaContentType = editTrack.MediaUpload.ContentType;

            ds.SaveChanges();

            return mapper.Map<Track, TrackBaseViewModel>(track);
        }

        public bool DeleteTrack(int id)
        {
            var track = ds.Tracks.Find(id);

            if(track == null)
            {
                return false;
            }

            ds.Tracks.Remove(track);
            ds.SaveChanges();

            return true;
        }

        public ArtistBaseViewModel AddArtistMedia(ArtistMediaItemAddViewModel newArtistMediaItem)
        {
            var artist = ds.Artists.Find(newArtistMediaItem.ArtistId);

            if (artist == null)
            {
                return null;
            }

            var ArtistMediaItem = new ArtistMediaItem();
            ds.ArtistMediaItems.Add(ArtistMediaItem);

            ArtistMediaItem.Caption = newArtistMediaItem.Caption;
            ArtistMediaItem.Artist = artist;

            byte[] MediaBytes = new byte[newArtistMediaItem.MediaUpload.ContentLength];
            newArtistMediaItem.MediaUpload.InputStream.Read(MediaBytes, 0, newArtistMediaItem.MediaUpload.ContentLength);

            ArtistMediaItem.Content = MediaBytes;
            ArtistMediaItem.ContentType = newArtistMediaItem.MediaUpload.ContentType;

            ds.SaveChanges();

            if (ArtistMediaItem == null)
            {
                return null;
            }

            return mapper.Map<Artist, ArtistBaseViewModel>(artist);
        }

        public ArtistMediaItemContentViewModel GetArtistMediaByID(string stringId)
        {
            var ArtistMediaItem = ds.ArtistMediaItems.SingleOrDefault(a => a.StringId == stringId);

            if (ArtistMediaItem == null)
            {
                return null;
            }

            return mapper.Map<ArtistMediaItem, ArtistMediaItemContentViewModel>(ArtistMediaItem);
        }

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Genre

            if (ds.Genres.Count() == 0)
            {
                // Add genres

                ds.Genres.Add(new Genre { Name = "Alternative" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Easy Listening" });
                ds.Genres.Add(new Genre { Name = "Hip-Hop/Rap" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Soundtrack" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Artist

            if (ds.Artists.Count() == 0)
            {
                // Add artists

                ds.Artists.Add(new Artist
                {
                    Name = "The Beatles",
                    BirthOrStartDate = new DateTime(1962, 8, 15),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 5, 5),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Adele_2016.jpg/800px-Adele_2016.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Bryan Adams",
                    BirthOrStartDate = new DateTime(1959, 11, 5),
                    Executive = user,
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Album

            if (ds.Albums.Count() == 0)
            {
                // Add albums

                // For Bryan Adams
                var bryan = ds.Artists.SingleOrDefault(a => a.Name == "Bryan Adams");

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "Reckless",
                    ReleaseDate = new DateTime(1984, 11, 5),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/56/Bryan_Adams_-_Reckless.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "So Far So Good",
                    ReleaseDate = new DateTime(1993, 11, 2),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/pt/a/ab/So_Far_so_Good_capa.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Track

            if (ds.Tracks.Count() == 0)
            {
                // Add tracks

                // For Reckless
                var reck = ds.Albums.SingleOrDefault(a => a.Name == "Reckless");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Run To You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Heaven",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Somebody",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Summer of '69",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Kids Wanna Rock",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                // For Reckless
                var so = ds.Albums.SingleOrDefault(a => a.Name == "So Far So Good");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Straight from the Heart",
                    Composers = "Bryan Adams, Eric Kagna",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "It's Only Love",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "This Time",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "(Everything I Do) I Do It for You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Heat of the Night",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }
}