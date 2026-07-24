# Generyczny parser danych przesyłanych przez API

Prosty endpoint HTTP w ASP.NET Core, który przyjmuje dane w formacie CSV lub JSON 
(zakodowane w Base64), parsuje je i zwraca w ujednoliconej strukturze.

## Wymagania

- .NET 8 SDK lub nowszy

## Uruchomienie lokalne

```bash
git clone <adres-repozytorium>
cd ParserApi
dotnet run --project ParserApi
```

Aplikacja domyślnie wystartuje pod adresem `https://localhost:xxxx` 
(port widoczny w konsoli po uruchomieniu).

Swagger UI dostępny jest pod `/swagger` w środowisku Development.

## Endpoint

**POST** `/api/v1/parse-content`

### Request

```json
{
  "type": "CSV",
  "content": "bmFtZSxhZ2UKSmFuLDI1"
}
```

- `type` — `CSV` lub `INTERNAL_JSON`
- `content` — dane zakodowane w Base64

### Response (sukces)

```json
{
  "success": true,
  "processedCount": 1,
  "data": [ { "name": "Jan", "age": "25" } ],
  "errorMessage": null
}
```

### Response (błąd)

```json
{
  "success": false,
  "processedCount": 0,
  "data": null,
  "errorMessage": "Invalid Base64 string format."
}
```

## Architektura

Parsowanie zaimplementowano wg wzorca **Strategy** — każdy format (`CsvContentParser`, 
`JsonContentParser`) implementuje wspólny interfejs `IContentParser` i jest rejestrowany 
w DI jako kolekcja. Kontroler wybiera odpowiedni parser na podstawie pola `type`. 
Dodanie nowego formatu wymaga jedynie stworzenia nowej klasy implementującej `IContentParser` 
— bez modyfikacji istniejącego kodu.

## Testy

```bash
dotnet test
```

## Założenia

- Pierwszy wiersz CSV jest traktowany jako nagłówek (kolumny).