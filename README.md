# spotify-player-api


This repository contains an API built in .NET Core that interacts with the Spotify API to search for tracks and albums based on a search term provided by the user. 
The API allows you to search for music, returning relevant information such as track name, album name, and preview link for the song.

## Features

- **Music Search**: Allows you to search for tracks on Spotify based on an artist or track name.
- **Search Parameters**:
  - `query`: The name of the artist or track to search for.
  - `limit`: The maximum number of results to return (optional, default is 10).

## Technologies Used

- **.NET 6**: The framework used to build the API.
- **Spotify API**: Used to search for music and related information.
- **Swagger**: For generating interactive API documentation.

## How to Use

### 1. Configuration

Before running the application, you need to configure your Spotify API credentials in the `appsettings.json` file.

```json
{
  "SPOTIFY_CLIENT_ID": "your-client-id",
  "SPOTIFY_CLIENT_SECRET": "your-client-secret"
}
```

- **SPOTIFY_CLIENT_ID**: Your Client ID from Spotify Developer.
- **SPOTIFY_CLIENT_SECRET**: Your Client Secret from Spotify Developer.

### 2. Installation

1. Clone the repository:

```bash
git clone https://github.com/LeticiaMartins/spotify-player-api.git
```

2. Navigate to the project directory:

```bash
cd spotify-player-api
```

3. Restore the project dependencies:

```bash
dotnet restore
```

4. Run the application:

```bash
dotnet run
```

The API will be available at `http://localhost:5000`.

### 3. Using the API

#### Endpoints

- **GET /api/spotify/search**
  
  Searches for tracks on Spotify.

  **Parameters**:
  - `query` (required): The search term (artist or track name).
  - `limit` (optional): The number of results to return (default is 10).

  **Example**:

  ```http
  GET /api/spotify/search?query=blink-182&limit=5
  ```

  **Response**:
  
  - Returns a text containing the track name, album name, and preview link (if available) of the found tracks.

#### Example Response

```json
[
  "Track: All The Small Things, Album: Enema Of The State, Preview URL: Not available",
  "Track: I Miss You, Album: Blink-182, Preview URL: Not available",
  ...
]
```

### 4. Documentation

API documentation is available via **Swagger**. Once the application is running, access:

```
http://localhost:5000/swagger
```

Swagger provides an interactive interface where you can test the endpoints directly.

## Contributing

Feel free to open issues and submit pull requests for improvements and new features.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.