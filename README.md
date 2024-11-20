# FlightFinder API

FlightFinder API is a lightweight and efficient API built using ASP.NET to help you find flight information and search for flights between airports. It provides two key endpoints for retrieving airport details and searching for flights. The project also uses in-memory caching to optimize response times for repeated queries.

---

## Features

- Retrieve airport information using IATA codes.
- Search for flights based on departure, arrival, date, and passenger count.
- Support for optional return dates in flight searches.
- Optimized with in-memory caching for faster repeated queries.

---

## Endpoints

### 1. **Get Airport Information**
**Endpoint**: `GET /api/Flights/iata`

Returns a list of airports with their IATA code, name, location, and country information.

**Response Example**:
```json
{
  "data": [
    {
      "code": "string",
      "name": "string",
      "latitude": 0,
      "longitude": 0,
      "countryCode": "string"
    }
  ]
}
```

---

### 2. **Search for Flights**
**Endpoint**: 
```
GET /api/Flights/search?departureairport={departureCode}&arrivalairport={arrivalCode}&departureDate={YYYY-MM-DD}&passengercount={number}&currency={currency}
```

**Optional Parameters**:
- `returnDate={YYYY-MM-DD}`: Specifies the return date for a round-trip flight.

**Example Request**:
```
GET https://localhost:7094/api/Flights/search?departureairport=LON&arrivalairport=ZAG&departureDate=2024-11-21&passengercount=1&currency=USD
```
---

## Performance Optimization

This API uses **in-memory caching** to store data for frequently repeated queries. This ensures:
- Faster response times for identical requests.
- Reduced load on backend processing for repeated queries.

---

## Getting Started

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/FlightFinderAPI.git
   ```
2. **Navigate to the Project Directory**:
   ```bash
   cd FlightFinderAPI
   ```
3. **Build and Run the Project**:
   Open the project in Visual Studio and run it. Note that the port may change.

4. **Access the API**:
   - Airport Info: `https://localhost:7094/api/Flights/iata`
   - Flight Search: `https://localhost:7094/api/Flights/search`
