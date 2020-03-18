class Flight {
    constructor(id, airlineid, origincountry, destcountry, landingtime,departuretime, status) {
        this.Id = id
        this.AirLine_Id  = airlineid
        this.Origin_Country_Code = origincountry
        this.Destination_Country_Code = destcountry
        this.Departure_Time = departuretime
        this.Landing_Time = landingtime
        if (status == undefined) {
            this.status = Date(departuretime) < Date.now()?"Landing":"Scehduled"
        }
    }
}
class jFlight {
    constructor(id, airlineName, origin, destination, landingtime, departuretime, status) {
        this.Id = id
        this.AirLineName = airlineName
        this.Origin_Country_Name = origin
        this.Destination_Country_Name = destination
        this.DepartureTime = departuretime
        this.LandingTime = landingtime
        if (status == undefined) {
            this.status = Date(departuretime) < Date.now() ? "Landing" : "Scehduled"
        }
    }
}
class Country {
    constructor(id, name) {
        this.Id = id
        this.Name=name
    }
}
class Airline {
    constructor(id, name, countryId) {
        this.Id = id
        this.AirLine_Name = name
        this.CountryCode =countryId
    }
}