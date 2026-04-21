# TCP + UDP Monitoring Server

## Opis projektu

Jest to serwer napisany w C# (.NET), który umożliwia:

- obsługę wielu klientów TCP
- automatyczne wykrywanie serwera w sieci (UDP discovery)
- odbiór screenshotów od klientów
- zapis screenshotów na dysku
- monitorowanie aktywności klientów (PING / LastSeen)

---

## Architektura

```
CLIENT
  │
  ├── UDP DISCOVERY (9999)
  │
  └── TCP CONNECTION (6767)
          │
          ▼
      TCP SERVER
          │
          ▼
      DATA SAVER
          │
          ▼
   /clients/<client_id>/
```

---

## Uruchomienie

### Wymagania:
- .NET 6 / 7 / 8

### Start:
```bash
dotnet run
```

---

## UDP Server (Discovery)

### Port:
```
9999
```

### Zapytanie klienta:
```
DISCOVER_SERVER
```

### Odpowiedź:
```
<IP_SERWERA>:6767
```

---

## TCP Server

### Port:
```
6767
```

---

## Protokół TCP

Każda wiadomość:

```
[4 bytes LENGTH][UTF-8 MESSAGE]
```

---

## Komendy

### PING
```
PING
```
 aktualizacja LastSeen

---

### SCREENSHOT
```
SCREENSHOT|<BASE64_IMAGE>
```

✔ zapis obrazu PNG  
✔ aktualizacja historii klienta  

---

## Struktura danych

```
/clients/
   ├── client1/
   │     ├── screen_1.png
   │     ├── screen_2.png
   │
   ├── client2/
```

---

## Model klienta

```csharp
public class ClientInfo
{
    public string Id { get; set; }
    public string Folder { get; set; }
    public List<string> Screenshots { get; set; }
    public string LastScreenshotPath { get; set; }
    public DateTime LastSeen { get; set; }
}
```

---

## Działanie systemu

1. UDP discovery
2. Połączenie TCP
3. Rejestracja klienta
4. Ping (heartbeat)
5. Wysyłanie screenshotów
6. Zapis danych lokalnie

---

## Komponenty

### TCP Server
- obsługa wielu klientów
- async I/O
- odbiór danych binarnych

### UDP Server
- discovery serwera w LAN
- szybka odpowiedź IP

### DataSaver
- zarządzanie klientami
- zapis screenshotów
- historia aktywności

---

## Ograniczenia

- brak szyfrowania
- brak autoryzacji
- brak bazy danych
- dane tylko lokalnie
- brak limitów payloadów

---

## Możliwe ulepszenia

- TLS / szyfrowanie
- system logowania klientów
- SQLite / PostgreSQL
- panel webowy
- monitoring realtime

---

## Porty

| Usługa     | Port |
|------------|------|
| TCP        | 6767 |
| UDP        | 9999 |
```