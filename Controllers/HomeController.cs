using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LIMPIO_3.Models;
using Newtonsoft.Json;
using RestSharp;

namespace LIMPIO_3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        var rand = new Random();

        ///////conectar API Spotify GetPlaylist
        SpotifyGetplaylist datosSpotify = null; //declarar variable aqui para poder acceder fuera del if

        const string linkSpotify = "https://api.spotify.com/v1/playlists/37i9dQZF1DXb2RUUTjIk3t";

        ////ACESS TOKEN 
        const string accessToken = "BQB278CVTc4Y9-ATT76ifM76_kkMzCmhMrmZtQFLpFMrAloaXUT50Mlo8IhWdc22kVqEn-GH1M1V7PlYSWiepGZdu9Jp0OM-LIasRL21CNAVZwRlAl9e4YNluFifY0TTAJorn0MHIIGFOMdPawWYt_vMetRoVdoB2hTawtmeMrVL5ueziruEMM-WMUHTcSLxXZDUqVQBzGFH9P0fvTY";
        var clientSpoti = new HttpClient();
        clientSpoti.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken); //autorizacion para conectar API
        var responseSpoty =  clientSpoti.GetAsync(linkSpotify).Result;

        if (responseSpoty.IsSuccessStatusCode) //comprobar que api funciona
            {
                string contentSpoti =  responseSpoty.Content.ReadAsStringAsync().Result;
                Console.WriteLine("SOLICITUD EXITOSA!!!!!!!!");
                //accedemos a los datos de una playlist de Spotify
                datosSpotify = JsonConvert.DeserializeObject<SpotifyGetplaylist>(contentSpoti); 
            }
        else
            {
                Console.WriteLine("La solicitud NO FUE EXITOSA. Código de estado: " + responseSpoty.StatusCode);
            }

        //Escogemos una cancion de la plylist aleatoriamente y nos guardamos 
        // 1 titulo album+nombre artista para buscar el album el Amazon
        // 2 la informacion del Json de la cancion para pasarla por la View
        List<Item> cancionesPlaylist = datosSpotify.tracks.items; //lista de las canciones de la playlist
        
        int indiceAleatorio= rand.Next(cancionesPlaylist.Count());//hacemos un random con la 'length' de la playlist

        var datosCancion= datosSpotify.tracks.items[indiceAleatorio];//guardamos datos de cancion aleatoria, esto lo pasaremos a la view
        string tituloAlbum =datosCancion.track.album.name; 
        string artistaAlbum=datosCancion.track.artists[0].name;

        string tituloArtista= tituloAlbum+"+"+artistaAlbum; //jun tamos titulo y artista para busqueda mas precisa
        string albumArtistaLink= tituloArtista.Replace(" ","+");//modificamos el titulo para poder añdirlo al link de Amazon


        // /////////Conectar API AMAZON Search 
        
        // string linkSearchAmazon = "https://api.rainforestapi.com/request?api_key=BAB510A2176344DE97D5BBB6AE3D7B2C&type=search&amazon_domain=amazon.com&search_term="+albumArtistaLink;
        // var clientSearch = new HttpClient();
        // var responseSearch = clientSearch.GetAsync(linkSearchAmazon).Result;
        // var contentSearch = responseSearch.Content.ReadAsStringAsync().Result;
        // SearchAmazon busquedaAmazon = JsonConvert.DeserializeObject<SearchAmazon>(contentSearch);
        // //datos amazon
        // ViewBag.LinkAlbumAmazon = busquedaAmazon.search_results[0].link;/// el link lo paso con ViewBag pq solo necesito 1 dato de esta api y es mas facil que pasar los 2 Json


        // //Conectar api colores
        string apiKey = "acc_6cc2350e33b8254";
        string apiSecret = "fd03cc394de5c46d52758a000c683a7d";
        string imageUrl = datosCancion.track.album.images[0].url;

        string basicAuthValue ="YWNjXzZjYzIzNTBlMzNiODI1NDpmZDAzY2MzOTRkZTVjNDZkNTI3NThhMDAwYzY4M2E3ZA==";
            
            var client = new RestClient("https://api.imagga.com/v2/colors");
            var request = new RestRequest();
            request.AddParameter("image_url", imageUrl);
            request.AddHeader("Authorization", String.Format("Basic {0}", basicAuthValue));
            var response = client.Execute(request);

            Root datosColor = JsonConvert.DeserializeObject<Root>(response.Content);
            
            ViewBag.colorMayoritario=datosColor.result.colors.image_colors[0].html_code;
            var listaColores = datosColor.result.colors.image_colors;
            int numeroColores= listaColores.Count();
            if (numeroColores>=5){
                numeroColores=5;
                }
            ViewBag.colordetalles = datosColor.result.colors.image_colors[numeroColores-1].html_code;
            if (numeroColores%2!= 0){
                numeroColores= numeroColores-1;
            }
            ViewBag.colordetalles2 = datosColor.result.colors.image_colors[numeroColores/2].closest_palette_color_parent;

        return View(datosCancion);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
