# Fleet Management App

Fleet Management App to aplikacja do zarządzania flotą samochodów, umożliwiająca rejestrację użytkowników, logowanie, dodawanie samochodów, tras oraz zarządzanie markami i modelami samochodów. Projekt składa się z części frontendowej w React, backendowej w .NET oraz testów jednostkowych dla backendu.

## Spis treści

- [Opis](#opis)
- [Funkcjonalności](#funkcjonalności)
- [Struktura projektu](#struktura-projektu)
- [Jak uruchomić projekt](#jak-uruchomić-projekt)
- [Testowanie](#testowanie)
- [CI/CD](#cicd)
- [Dokumentacja](#dokumentacja)
- [Badge](#badge)
- [Kontakt](#kontakt)

## Opis

Fleet Management App umożliwia zarządzanie flotą pojazdów, w tym dodawanie nowych samochodów, przypisywanie ich do tras, oraz zarządzanie markami i modelami. Aplikacja wspiera rejestrację i logowanie użytkowników, a także integrację z zewnętrznym API do pobierania danych o markach i modelach samochodów.

## Funkcjonalności

- Rejestracja i logowanie użytkowników
- Dodawanie, edytowanie i usuwanie samochodów
- Dodawanie, edytowanie i usuwanie tras
- Przypisywanie samochodów do tras
- Pobieranie listy marek i modeli samochodów z API
- Walidacja formularzy i błędów

## Struktura projektu

- `frontend/` - Projekt frontendowy w React
- `backend/` - Projekt backendowy w .NET (ASP.NET Core)
- `FleetManagmentApp.Tests/` - Testy jednostkowe dla backendu

## Jak uruchomić projekt

### Backend

1. Przejdź do folderu `backend`:
    ```bash
    cd backend
    ```

2. Zainstaluj zależności:
    ```bash
    dotnet restore
    ```

3. Uruchom projekt:
    ```bash
    dotnet run
    ```

### Frontend

1. Przejdź do folderu `frontend`:
    ```bash
    cd frontend
    ```

2. Zainstaluj zależności:
    ```bash
    npm install
    ```

3. Uruchom projekt:
    ```bash
    npm start
    ```

## Testowanie

### Testy jednostkowe backendu

1. Przejdź do folderu `FleetManagmentApp.Tests`:
    ```bash
    cd FleetManagmentApp.Tests
    ```

2. Uruchom testy:
    ```bash
    dotnet test
    ```

## CI/CD

W projekcie skonfigurowano pipeline CI/CD za pomocą GitHub Actions. Pipeline wykonuje:

- **Testy jednostkowe**: Uruchamiane automatycznie przy każdym pushu do repozytorium.
- **Linting i formatting**: Zapewnia czytelność i zgodność ze standardami kodowania.
- **Conventional Commit**: Sprawdza zgodność commitów z konwencją Conventional Commit.
