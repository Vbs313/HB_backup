using System;
using System.IO;
using System.Xml;
using HREngine.Bots;

class GenCache
{
    static void Main()
    {
        string basePath = Path.Combine(
            Environment.CurrentDirectory,
            "Routines", "DefaultRoutine", "Silverfish");
        string dataPath = Path.Combine(basePath, "data");
        string xmlPath = Path.Combine(dataPath, "CardDefs.xml");
        string binPath = Path.Combine(dataPath, "CardDefs.bin");

        Console.WriteLine("Generating CardDefs.bin...");
        Console.WriteLine("XML: " + xmlPath);
        Console.WriteLine("BIN: " + binPath);

        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("ERROR: CardDefs.xml not found at " + xmlPath);
            return;
        }

        var sw = System.Diagnostics.Stopwatch.StartNew();

        // Load XML
        var doc = new XmlDocument();
        doc.Load(xmlPath);
        var entities = doc.SelectNodes("CardDefs/Entity");
        Console.WriteLine("Loaded XML: " + entities.Count + " entities in " + sw.Elapsed.TotalSeconds.ToString("0.0s"));

        // Parse entities and create Card objects (simplified - just IDs)
        // The real CardDB parsing uses 100+ tag types, we replicate the essentials
        var cardlist = new System.Collections.Generic.List<CardDB.Card>();
        var cardidDict = new System.Collections.Generic.Dictionary<CardDB.cardIDEnum, CardDB.Card>();
        var dbfidDict = new System.Collections.Generic.Dictionary<string, CardDB.Card>();
        var nameCNDict = new System.Collections.Generic.Dictionary<CardDB.cardNameCN, CardDB.Card>();
        var nameENDict = new System.Collections.Generic.Dictionary<CardDB.cardNameEN, CardDB.Card>();
        var unknownCard = new CardDB.Card { nameEN = CardDB.cardNameEN.unknown };
        cardlist.Add(unknownCard);

        // We can't use CardDB's internal parsing (it's private).
        // Instead, trigger CardDB.Instance which will parse and save the cache.
        // Need to initialize Settings first.
        Settings.Instance.setFilePath(dataPath + Path.DirectorySeparatorChar);
        
        Console.WriteLine("Triggering CardDB initialization (will parse XML + save cache)...");
        var instance = CardDB.Instance;
        
        sw.Stop();
        Console.WriteLine("Done! Elapsed: " + sw.Elapsed.TotalSeconds.ToString("0.0s"));

        if (File.Exists(binPath))
            Console.WriteLine("CardDefs.bin generated: " + new FileInfo(binPath).Length + " bytes");
        else
            Console.WriteLine("WARNING: CardDefs.bin not found at " + binPath);
    }
}
