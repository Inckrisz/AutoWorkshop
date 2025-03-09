# AutoWorkshop

Ez egy egyetemi C# projekt, amely egy autószerelő műhely munkáinak nyilvántartását kezeli. A projekt egy .NET 8 alapú Web API-t és egy Blazor WebAssembly klienst tartalmaz.

## Technológiai stack
- **Backend**: ASP.NET Core Web API (.NET 8)
- **Frontend**: Blazor WebAssembly
- **Adatbázis**: SQLite (Entity Framework Core)
- **Version Control**: Git (GitHub)
- **Unit Tesztelés**: xUnit (Web API service-ek tesztelésére)

## Fő funkciók
- Ügyfelek CRUD műveletei
- Munkák CRUD műveletei
- Ügyfelekhez kapcsolódó munkák listázása
- Munkaóra esztimáció automatikusan számolva
- Adatvalidáció frontenden és backend oldalon
- Munka státuszának követése (Felvett -> Elvégzés alatt -> Befejezett)

## Munkaóra számítás
A munkaórák kiszámítása az alábbi képlet alapján történik:
```
kategória * kor súlyozás * hiba súlyosság súlyozás
```

**Kategóriák és alap munkaórák:**
- Karosszéria: 3 óra
- Motor: 8 óra
- Futómű: 6 óra
- Fékberendezés: 4 óra

**Kor szerinti súlyozás:**
- 0-5 év: 0.5
- 5-10 év: 1
- 10-20 év: 1.5
- 20+ év: 2

**Hiba súlyosság szerinti súlyozás (1-10 skála):**
- 1-2: 0.2
- 3-4: 0.4
- 5-7: 0.6
- 8-9: 0.8
- 10: 1

## Adatmodell

### Ügyfél
- Ügyfélszám (automatikus)
- Név
- Lakcím
- Email (validáció: email formátum szükséges)

### Munka
- Munka azonosító (automatikus)
- Ügyfél azonosító
- Gépjármű rendszáma (formátum: XXX-YYY)
- Gépjármű gyártási éve (minimum 1900)
- Munka kategóriája (Karosszéria, Motor, Futómű, Fékberendezés)
- Hiba leírása
- Hiba súlyossága (1-10 skála)
- Munka állapota (Felvett -> Elvégzés alatt -> Befejezett)

## Telepítés és futtatás

1. **Projekt klónozása:**
   ```sh
   git clone https://github.com/Inckrisz/AutoWorkshop.git
   cd AutoWorkshop
   ```

2. **Backend futtatása:**
   Navigálj a backend könyvtárba, és telepítsd a szükséges csomagokat:

   ```sh
   cd AutoWorkshop.API
   dotnet restore
   ```
   Ez letölti az összes szükséges NuGet csomagot.
   
   Futtatás előtt migráld az adatbázist (SQLite):
   
   ```sh
   dotnet ef database update
   ```
   Majd indítsd el a szervert:
   
   ```sh
   dotnet run
   ```
   ```sh
   cd AutoWorkshop.API
   dotnet run
   ```

3. **Frontend futtatása:**
   Nyiss egy új terminált, majd navigálj a frontend mappába:

   ```sh
   cd ../AutoWorkshop.Blazor
   dotnet restore
   ```
   Indítsd el a Blazor klienst:
    ```sh
   cd AutoWorkshop.Blazor
   dotnet run
   ```

## Licenc
MIT

---

**Fejlesztő**: [Krisz](https://github.com/Inckrisz)
