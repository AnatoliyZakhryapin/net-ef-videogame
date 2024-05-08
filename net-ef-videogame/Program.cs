using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace net_ef_videogame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine();
                Console.WriteLine("1. Inserire un nuovo videogioco.");
                Console.WriteLine("2. Ricercare un videogioco per ID.");
                Console.WriteLine("3. Ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input.");
                Console.WriteLine("4. Cancellare un videogioco.");
                Console.WriteLine("5. Inserire una nuova softwareHouse.");
                Console.WriteLine("6. Chiudere il programma.");

                Console.WriteLine();
                string inputChoice = Console.ReadLine();

                try
                {
                    if (!int.TryParse(inputChoice, out int userChoice))
                        throw new Exception("Devi inserire solo il numero compreso tra 1 e 5! Riprova.");

                    int choice = Convert.ToInt32(inputChoice);

                    switch (choice)
                    {
                        case 1:
                            CreateNewGame();
                            break;
                        case 2:
                            SearchGameById();
                            break;
                        case 3:
                            SearchGameByName();
                            break;
                        case 4:
                            DeleteGame();
                            break;
                        case 5:
                            CreateNewSoftwareHouse();
                            break; 
                        case 6:
                            return;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
        }

        static void CreateNewSoftwareHouse()
        {
            string name, taxId, city, country;

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Inserisci il nome del software house");
                    string inputName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(inputName))
                        throw new Exception("Input non puo essere vuoto. Riprova");

                    name = inputName;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {ex.Message}");
                }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Inserisci tax_id del software house");
                    string inputTaxId = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(inputTaxId))
                        throw new Exception("Input non puo essere vuoto. Riprova");

                    taxId = inputTaxId;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {ex.Message}");
                }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Inserisci city del software house");
                    string inputCity = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(inputCity))
                        throw new Exception("Input non puo essere vuoto. Riprova");

                    city = inputCity;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {ex.Message}");
                }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Inserisci country del software house");
                    string inputCountry = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(inputCountry))
                        throw new Exception("Input non puo essere vuoto. Riprova");

                    country = inputCountry;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {ex.Message}");
                }
            }

           SoftwareHouse newSoftwareHoude = new SoftwareHouse(name,taxId,city,country, DateTime.Now, null);

            VideoGameManager.AddNewSoftwareHouse(newSoftwareHoude);
        }

        static void CreateNewGame()
        {
            string name, overview;
            DateTime releaseDate;
            long softwareHouseID;

            while (true)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Inserisci il nome del gioco: ");
                        string inputName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(inputName))
                            throw new Exception("Il nome non puo essere vuoto. Riprova");

                        name = inputName;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Errore: {ex.Message}");
                    }
                }

                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Inserisci il overview del gioco: ");
                        string inputOverview = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(inputOverview))
                            throw new Exception("Overview non puo essere vuoto. Riprova");

                        overview = inputOverview;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Errore: {ex.Message}");
                    }
                }

                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Inserisci la data di rilascio del gioco (formato: yyyy-MM-dd): ");
                        string inputReleaseDate = Console.ReadLine();

                        if (!DateTime.TryParse(inputReleaseDate, out releaseDate))
                            throw new Exception("Formato data non valido. Riprova.");

                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Errore: {ex.Message}");
                    }
                }

                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Inserisci id di softwareHouseID: ");
                        string inputSoftwareHouseID = Console.ReadLine();

                        List<SoftwareHouse> softwareHouses = VideoGameManager.GetSoftwareHouseList();

                        if (!long.TryParse(inputSoftwareHouseID, out long id))
                            throw new Exception("Formato Id non valido. Riprova.");

                        if (!softwareHouses.Any(sh => sh.SoftwareHouseId == id))
                            throw new Exception("L'ID inserito non corrisponde a nessuna casa di software. Riprova.");

                        softwareHouseID = id;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Errore: {ex.Message}");
                    }
                }

                VideoGame newVideoGame = new VideoGame(name, overview, releaseDate, DateTime.Now, DateTime.Now, softwareHouseID);

                if (VideoGameManager.AddNewGame(newVideoGame))
                {
                    Console.WriteLine();
                    Console.WriteLine("VideoGame è stato inserito con successo!");
                    Console.WriteLine();
                    Console.WriteLine("VideoGame inserito è: ");
                    Console.WriteLine();
                    Console.WriteLine($"{newVideoGame.ToString()}");
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Salvatoggio non e andato a buon fine, riprova.");
                }
            }
            
        }

        static void SearchGameById ()
        {
            long idToSerach;

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Inserisci id di VideoGame da cercare: ");
                    string inputIdToSerach = Console.ReadLine();

                    if (!long.TryParse(inputIdToSerach, out long id))
                        throw new Exception("Formato Id non valido. Riprova.");

                    idToSerach = id;

                    VideoGame videoGameFinded = VideoGameManager.GetVideoGameById(idToSerach);

                    if(videoGameFinded == null)
                        throw new ValueNotFoundException($"VideoGame con id - {idToSerach} non e stata trovata.");

                    Console.WriteLine();
                    Console.WriteLine(videoGameFinded.ToString());
                    Console.WriteLine();
                    break;
                }
                catch( ValueNotFoundException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {ex.Message}");
                    Console.WriteLine();
                    string input = "";
                    while (input != "no")
                    {
                        Console.WriteLine("Vuoi uscire al menu principale? (si/no)");
                        Console.WriteLine();
                        input = Console.ReadLine();
                        Console.WriteLine();

                        if (input == "si")
                        {
                           return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {ex.Message}");
                }
            }
        }

        static void SearchGameByName()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Inserisci il nome di VideoGame da cercare: ");
                    string inputNameToSerach = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(inputNameToSerach))
                        throw new Exception("Il nome non puo essere vuoto. Riprova");

                    List<VideoGame> listVideoGames = VideoGameManager.GetVideoGamesByName(inputNameToSerach);

                    Console.WriteLine();
                    Console.WriteLine($"Sono stati trovati {listVideoGames.Count()} VideoGames: ");
                    Console.WriteLine();

                    if(listVideoGames.Count > 0)
                    {
                        foreach (VideoGame videoGame in listVideoGames)
                        {
                            Console.WriteLine(videoGame.ToString());
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Non ci sono i resultati per nome: {inputNameToSerach}");
                        Console.WriteLine();
                    }
                    
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {ex.Message}");
                }
            }
        }

        static void DeleteGame()
        {

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Inserisci id di VideoGame da cancelare: ");
                    string inputIdToSerach = Console.ReadLine();

                    if (!long.TryParse(inputIdToSerach, out long id))
                        throw new Exception("Formato Id non valido. Riprova.");

                    VideoGame videoGameFinded = VideoGameManager.GetVideoGameById(id);

                    if (videoGameFinded == null)
                        throw new ValueNotFoundException($"VideoGame con id - {id} non e stata trovata.");

                    VideoGameManager.DeleteVideoGame(videoGameFinded);
                    Console.WriteLine();
                    Console.WriteLine($"VideoGame con id {id} è stata eliminata con successo!");
                    Console.WriteLine();
                    break;
                }
                catch (ValueNotFoundException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {ex.Message}");
                    Console.WriteLine();
                    string input = "";
                    while (input != "no")
                    {
                        Console.WriteLine("Vuoi uscire al menu principale? (si/no)");
                        Console.WriteLine();
                        input = Console.ReadLine();
                        Console.WriteLine();

                        if (input == "si")
                        {
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {ex.Message}");
                }
            }
        }

    }


}
